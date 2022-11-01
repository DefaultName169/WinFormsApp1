using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BSS;
using ExpressiveAnnotations.Attributes;
using Newtonsoft.Json;
using WebAPI.Common;

namespace WebAPI.Models
{
    public class Gallery : Audit
    {
        [JsonIgnore]
        public long GalleryID { get; set; }

        public Guid ObjectGuid { get; set; }

        public string OrganizationCollectCode { get; set; }

        public string InforSign { get; set; }

        public int StorageTimeType { get; set; }

        public int? Maintenance { get; set; }

        public string GalleryContent { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int NationalAssembly { get; set; }

        public int CongressMeeting { get; set; }

        public int Meeting { get; set; }

        public int NegativeNo { get; set; }

        public int PositiveNo { get; set; }

        public string Description { get; set; }

        public int GalleryStatus { get; set; }

        [JsonIgnore]
        public Guid FileObjectGuid { get; set; }

        [JsonIgnore]
        public long FileID { get; set; }

        public int PhotoType { get; set; }

        public static string GetListSearch(GallerySearch ms, out List<GallerySearchResult> lt, out int total)
        {
            lt = null;
            total = 0;

            dynamic o;
            string msg = GetListPaging(ms, out lt, out total);
            if (msg.Length > 0) return msg;

            return "";

        }
        public static string GetListPaging(GallerySearch ms, out List<GallerySearchResult> lt, out int total)
        {
            lt = null; total = 0;

            string msg = "";

            QueryStringBuilder q = new QueryStringBuilder();
            msg = q.InitWithStore("usp_Gallery_Select_Search");
            if (msg.Length > 0) return msg;


            msg = GetListPaging_Parameter(false, ms, out dynamic para);
            if (msg.Length > 0) return msg;

            msg = Paging.Exec(q.Sql, "g.GalleryId", para, out lt, out total);
            if (msg.Length > 0) return msg;

            return msg;
        }
        private static string GetListPaging_Parameter(bool IsReport, GallerySearch ms, out dynamic o)
        {
            o = null;

            o = new
            {
                ms.TextSearch,
                ms.FileID,
                ms.NationalAssembly,
                ms.CongressMeeting,
                ms.Meeting,
                //ms.StartDate,
                //ms.EndDate,
                ms.GalleryStatus,
                ms.PageSize,
                ms.CurrentPage,
                DefaultDate = "1900-01-01"
            };

            return "";
        }

        /// <summary>
        /// Insert or update gallery
        /// </summary>
        /// <param name="dbm"></param>
        /// <param name="o"></param>
        /// <param name="UserID"></param>
        /// <returns>string</returns>
        public string InsertOrUpdate(DBM dbm, out Gallery o, int UserID)
        {
            o = null;
            string msg = dbm.SetStoreNameAndParams("usp_Gallery_InsertOrUpdate", new
            {
                GalleryID,
                ObjectGuid,
                OrganizationCollectCode,
                InforSign,
                GalleryContent,
                StartDate,
                EndDate,
                Description,
                CreateUser,
                UpdateUser,
                StorageTimeType,
                Maintenance,
                CongressMeeting,
                Meeting,
                NationalAssembly,
                NegativeNo,
                PositiveNo,
                GalleryStatus,
                FileID,
                PhotoType
            });
            if (msg.Length > 0) return msg;

            return dbm.GetOne(out o);
        }
        public static string GetOneByGuid(Guid ObjectGuid, out long GalleryID)
        {
            GalleryID = 0;
            string msg = GetOneByObjectGuid(ObjectGuid, out Gallery g);
            if (msg.Length > 0) return msg;

            if (g == null) return ("Không tồn tại Film có ObjectGuid = " + ObjectGuid).ToMessageForUser();
            GalleryID = g.GalleryID;

            return msg;
        }

        public static string Delete(long GalleryID)
        {
            return DBM.ExecStore("usp_Gallery_UpdateIsDelete", new { GalleryID });
        }

        public static string GetOneByObjectGuid(Guid ObjectGuid, out Gallery gallery)
        {
            return DBM.GetOne("usp_Gallery_GetOneByObjectGuid", new { ObjectGuid }, out gallery);
        }

        public static string GetOneByOrganizationCollectCode(string OrganizationCollectCode, out Gallery outGallery)
        {
            return DBM.GetOne("usp_Gallery_GetOneByOrganizationCollectCode", new { OrganizationCollectCode }, out outGallery);
        }
        public static string CheckExistPhoto(long GalleryID, out int Count)
        {
            return DBM.ExecStore("usp_Document_CheckExistPhoto", new { GalleryID }, out Count);
        }

        public static string GetListByListFileGuidID(List<Guid> guids, int status, out List<GallerySearchResult> listGallery)
        {
            return DBM.GetList("usp_Gallery_GetListByListFileGuidID", new
            {
                Guids = guids.Select(s =>
            new {
                ObjGuid = s,
                DefaultColumn = 0
            }).ToList().ToDataTable(),
                Status = status
            }, out listGallery);
        }

        public static string GetListByListGuidID(List<Guid> listObjectGuidID, int status, out List<GallerySearchResult> lt)
        {
            return DBM.GetList("usp_Gallery_GetListByListGuidID", new
            {
                Guids = listObjectGuidID.Select(s =>
            new {
                ObjGuid = s,
                DefaultColumn = 0
            }).ToList().ToDataTable(),
                Status = status
            }, out lt);
        }
    }


    public class GalleryInput : Audit
    {
        [JsonIgnore]
        public long GalleryID { get; set; }

        public Guid ObjectGuid { get; set; }

        [Required(ErrorMessage = "Hãy nhập Mã ĐVBQ/ST ảnh")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Mã ĐVBQ/ST ảnh, nội dung không được quá 5 kí tự")]
        [AssertThat("IsRegexMatch(OrganizationCollectCode, '^[a-zA-Z0-9]+$')", ErrorMessage = "Mã ĐVBQ/ST ảnh, không được nhập ký tự có dấu và các ký tự đặc biệt")]
        public string OrganizationCollectCode { get; set; }

        [StringLength(30, MinimumLength = 0, ErrorMessage = "Kí tự số, nội dung không được quá 30 kí tự")]
        public string InforSign { get; set; }

        [Required(ErrorMessage = "Hãy nhập Loại thời hạn bảo quản")]
        [Range(1, 2, ErrorMessage = "Loại thời hạn bảo quản, chỉ được nhập 1-Vĩnh viễn hoặc 2-Có thời hạn")]
        public int StorageTimeType { get; set; }

        [RequiredIf("StorageTimeType == 2", ErrorMessage = "Hãy nhập thời hạn bảo quản")]
        [AssertThat("checkMaintenance", ErrorMessage = "Thời hạn bảo quản, chỉ được nhập từ 1 đến 70")]
        public int? Maintenance { get; set; }
        [JsonIgnore]
        public bool checkMaintenance
        {
            get
            {
                if (StorageTimeType == 2 && (Maintenance <= 0 || Maintenance > 70)) return false;
                return true;
            }
        }

        [Required(ErrorMessage = "Hãy nhập Nội dung STA")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Nội dung STA, nội dung không được quá 500 kí tự")]
        public string GalleryContent { get; set; }

        [Required(ErrorMessage = "Hãy nhập Thời gian bắt đầu")]
        [JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Hãy nhập Thời gian kết thúc")]
        [JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
        public DateTime EndDate { get; set; }

        public int FileNationalAssembly { get; set; }

        public int FileCongressMeeting { get; set; }

        public int FileMeeting { get; set; }

        [AssertThat("NationalAssembly == 0 || NationalAssembly == FileNationalAssembly", ErrorMessage = "Quốc Hội Khóa bằng 0 hoặc bằng QHK của hồ sơ")]
        public int NationalAssembly { get; set; }

        [Range(0, 11, ErrorMessage = "Kỳ họp, chỉ được nhập không quá 11")]
        [AssertThat("CongressMeeting == 0 || CongressMeeting == FileCongressMeeting", ErrorMessage = "Kỳ họp bằng 0 hoặc bằng kì họp của hồ sơ")]
        public int CongressMeeting { get; set; }

        [Range(0, 54, ErrorMessage = "Phiên họp, chỉ được nhập không quá 54")]
        [AssertThat("Meeting == 0 || Meeting == FileMeeting", ErrorMessage = "Phiên họp bằng 0 hoặc bằng phiên họp của hồ sơ")]
        public int Meeting { get; set; }

        public int NegativeNo { get; set; }

        public int PositiveNo { get; set; }

        [StringLength(500, MinimumLength = 0, ErrorMessage = "Ghi chú, nội dung không được quá 500 kí tự")]
        public string Description { get; set; }

        [Range(2, 4, ErrorMessage = "Trạng thái của sưu tập ảnh, chỉ được nhập 2-Khả dụng, 3-Đang khai thác hoặc 4-Đã xóa")]
        public int GalleryStatus { get; set; }

        public Guid FileObjectGuid { get; set; }

        [JsonIgnore]
        public long FileID { get; set; }

        [Required(ErrorMessage = "Hãy nhập Loại hình")]
        [Range(1, 2, ErrorMessage = "Loại hình, chỉ được nhập 1-Chân dung hoặc 2-Hoạt động")]
        public int PhotoType { get; set; }

        public static string GetListSearch(GallerySearch ms, out List<GallerySearchResult> lt, out int total)
        {
            lt = null;
            total = 0;

            dynamic o;
            string msg = GetListPaging(ms, out lt, out total);
            if (msg.Length > 0) return msg;

            return "";

        }
        public static string GetListPaging(GallerySearch ms, out List<GallerySearchResult> lt, out int total)
        {
            lt = null; total = 0;

            string msg = "";

            QueryStringBuilder q = new QueryStringBuilder();
            msg = q.InitWithStore("usp_Gallery_Select_Search");
            if (msg.Length > 0) return msg;


            msg = GetListPaging_Parameter(false, ms, out dynamic para);
            if (msg.Length > 0) return msg;

            msg = Paging.Exec(q.Sql, "g.GalleryId", para, out lt, out total);
            if (msg.Length > 0) return msg;

            return msg;
        }
        private static string GetListPaging_Parameter(bool IsReport, GallerySearch ms, out dynamic o)
        {
            o = null;

            o = new
            {
                ms.TextSearch,
                ms.FileID,
                ms.NationalAssembly,
                ms.CongressMeeting,
                ms.Meeting,
               // ms.StartDate,
                //ms.EndDate,
                ms.GalleryStatus,
                ms.PageSize,
                ms.CurrentPage,
                DefaultDate = "1900-01-01"
            };

            return "";
        }
    }
    public class GallerySearch
    {
        public string TextSearch { get; set; }
        [JsonIgnore]
        public long FileID { get; set; }
        public Guid ObjectGuidFile { get; set; }
        public int NationalAssembly { get; set; }
        public int CongressMeeting { get; set; }
        public int Meeting { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        public int GalleryStatus { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public List<Guid> ListObjectGuidID { get; set; }
        public GallerySearch()
        {
            FileID = NationalAssembly = CongressMeeting = Meeting = GalleryStatus = 0;
            PageSize = 10;
            CurrentPage = 1;
            DateTime dtDefault = DateTime.Parse("1900-01-01");
            //StartDate = EndDate = dtDefault;

        }
    }
    public class GallerySearchResult
    {
        [JsonIgnore]
        public long GalleryID { get; set; }
        public Guid ObjectGuid { get; set; }
        public string OrganizationCollectCode { get; set; }
        public string GalleryContent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NegativeNo { get; set; }
        public int PositiveNo { get; set; }
        public string Description { get; set; }
        public int GalleryStatus { get; set; }
        public string GalleryStatusName { get; set; }
        [JsonIgnore]
        public string InforSign { get; set; }
        [JsonIgnore]
        public int StorageTimeType { get; set; }
        /// <summary>
        /// Mã hồ sơ
        /// </summary>
        public string FileCode { get; set; }
        public int PhotoType { get; set; }
        //[JsonIgnore]
        //public int Maintenance { get; set; }

    }
}