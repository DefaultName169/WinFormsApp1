using System.ComponentModel.DataAnnotations;
using System.Data;
using BSS;
using Newtonsoft.Json;

namespace SynceOToHTLT.Models.HTLT.Photo
{
    public class Photo : Audit
    {
        [JsonIgnore]
        public long PhotoID { get; set; }

        public Guid ObjectGuid { get; set; }

        public string EventName { get; set; }

        public string ImageTitle { get; set; }

        public string ArchivesNumber { get; set; }

        public int PhotoGearNo { get; set; }

        public int PhotoPocketNo { get; set; }

        public int PhotoNo { get; set; }

        public int FilmGearNo { get; set; }

        public int FilmPocketNo { get; set; }

        public int FilmNo { get; set; }

        public string Photographer { get; set; }

        public DateTime PhotoTime { get; set; }

        public string PhotoPlace { get; set; }

        public string FilmSize { get; set; }

        public string DeliveryUnit { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string InforSign { get; set; }

        public string Description { get; set; }

        public int Format { get; set; }

        public int SecurityLevel { get; set; }
        
        public int PhotoStatus { get; set; }

        public int Mode { get; set; }

        public int Colour { get; set; }

        public int StorageTimeType { get; set; }

        public int? Maintenance { get; set; }

        public int Form { get; set; }

        public int NationalAssembly { get; set; }

        public int CongressMeeting { get; set; }

        public int Meeting { get; set; }

        [JsonIgnore]
        public Guid GalleryObjectGuid { get; set; }

        [JsonIgnore]
        public long GalleryID { get; set; }

        public string ImagePath { get; set; }

        [JsonIgnore]
        public int FileSecurityLevel { get; set; }
        public long EgovID { get; set; }

        /// <summary>
        /// Insert or update document
        /// </summary>
        /// <param name="dbm"></param>
        /// <param name="o"></param>
        /// <param name="UserID"></param>
        /// <returns>string</returns>
        public string InsertOrUpdate(DBM dbm, out Photo o, int UserID)
        {
            o = null;
            string msg = dbm.SetStoreNameAndParams("usp_Photo_InsertOrUpdate", new
            {
                PhotoID,
                ObjectGuid,
                EventName,
                ImageTitle,
                ArchivesNumber,
                PhotoGearNo,
                PhotoPocketNo,
                PhotoNo,
                FilmGearNo,
                FilmPocketNo,
                FilmNo,
                Photographer,
                PhotoTime,
                PhotoPlace,
                FilmSize,
                DeliveryUnit,
                DeliveryDate,
                InforSign,
                Description,
                CreateUser = UserID,
                UpdateUser = UserID,
                Format,
                SecurityLevel,
                PhotoStatus,
                Mode,
                NationalAssembly,
                CongressMeeting,
                Meeting,
                Colour,
                StorageTimeType,
                Maintenance,
                Form,
                GalleryID,
                ImagePath,
                FileSecurityLevel
            });
            if (msg.Length > 0) return msg;

            return dbm.GetOne(out o);
        }

        public static string GetListSearch(PhotoSearch ms, out List<PhotoSearchResult> lt, out int total)
        {
            lt = null;
            total = 0;

            dynamic o;
            string msg = GetListPaging(ms, out lt, out total);
            if (msg.Length > 0) return msg;

            return "";

        }
        public static string GetListPaging(PhotoSearch ms, out List<PhotoSearchResult> lt, out int total)
        {
            lt = null; total = 0;

            string msg = "";

            QueryStringBuilder q = new QueryStringBuilder();
            msg = q.InitWithStore("usp_Photo_Select_Search");
            if (msg.Length > 0) return msg;


            msg = GetListPaging_Parameter(false, ms, out dynamic para);
            if (msg.Length > 0) return msg;

            msg = Paging.Exec(q.Sql, "p.PhotoID", para, out lt, out total);
            if (msg.Length > 0) return msg;

            return msg;
        }
        private static string GetListPaging_Parameter(bool IsReport, PhotoSearch ms, out dynamic o)
        {
            o = null;

            o = new
            {
                ms.TextSearch,
                ms.GalleryID,
                ms.PhotoTimeFrom,
                ms.PhotoTimeTo,
                ms.Mode,
                ms.PhotoStatus,
                ms.PageSize,
                ms.CurrentPage,
                DefaultDate = "1900-01-01"
            };

            return "";
        }

        public static string Delete(long PhotoID)
        {
            return DBM.ExecStore("usp_Photo_UpdateIsDelete", new { PhotoID });
        }
        public static string GetOneByGuid(Guid ObjectGuid, out long PhotoID)
        {
            PhotoID = 0;
            string msg = GetOneByObjectGuid(ObjectGuid, out Photo g);
            if (msg.Length > 0) return msg;

            if (g == null) return ("Không tồn tại ảnh có ObjectGuid = " + ObjectGuid).ToMessageForUser();
            PhotoID = g.PhotoID;

            return msg;
        }

        public static string GetOneByObjectGuid(Guid ObjectGuid, out Photo o)
        {
            return DBM.GetOne("usp_Photo_GetOneByObjectGuid", new { ObjectGuid }, out o);
        }


        /// <summary>
        /// Check the Photo Document exists or not by ImageTitle, ArchivesNumber
        /// </summary>
        /// <param name="ImageTitle"></param>
        /// <param name="ArchivesNumber"></param>
        /// <param name="GalleryID"></param>
        /// <param name="outPhoto"></param>
        /// <returns></returns>
        public static string GetOneByImageTitleAndArchivesNumber(string ImageTitle, string ArchivesNumber,long GalleryID, out Photo outPhoto)
        {
            return DBM.GetOne("usp_Photo_GetOneByImageTitleAndArchivesNumber", new { ImageTitle, ArchivesNumber, GalleryID }, out outPhoto);
        }

        //public static string GetListByTicketID(long TicketID, out List<DocumentTicketSearch> o)
        //{
        //    return DBM.GetList("usp_Photo_GetListByTicketID", new { TicketID }, out o);
        //}

        internal static string GetPhotoListByArchivesNumber(string ArchivesNumber, out List<Photo> photoOutput)
        {
            return DBM.GetList("usp_Photo_GetPhotoListByArchivesNumber", new { ArchivesNumber }, out photoOutput);
        }

        public static string GetListByListFileGuidID(List<Guid> guids, int status, out List<PhotoSearchResult> listPhoto)
        {
            return DBM.GetList("usp_Photo_GetListByListFileGuidID", new
            {
                Guids = guids.Select(s =>
            new {
                ObjGuid = s,
                DefaultColumn = 0
            }).ToList().ToDataTable(),
                Status = status
            }, out listPhoto);
        }

        public static string GetListByListGuidID(List<Guid> listObjectGuidID, int status, out List<PhotoSearchResult> lt)
        {
            return DBM.GetList("usp_Photo_GetListByListGuidID", new
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



    public class PhotoInput : Audit
    {
        [JsonIgnore]
        public long PhotoID { get; set; }

        public Guid ObjectGuid { get; set; }

        [Required(ErrorMessage = "Hãy nhập Tên sự kiện")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Tên sự kiện, nội dung không được quá 500 kí tự")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Hãy nhập Tiêu đề")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Tiêu đề, nội dung không được quá 500 kí tự")]
        public string ImageTitle { get; set; }

        [Required(ErrorMessage = "Hãy nhập Số lưu trữ")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Số lưu trữ, nội dung không được quá 50 kí tự")]
        public string ArchivesNumber { get; set; }

        [Range(0, 99999, ErrorMessage = "Hộp ảnh số, Hãy nhập giá trị từ 0 đến 99999")]
        public int PhotoGearNo { get; set; }

        [Range(0, 99999, ErrorMessage = "Túi ảnh số, Hãy nhập giá trị từ 0 đến 99999")]
        public int PhotoPocketNo { get; set; }

        [Range(0, 99999, ErrorMessage = "Ảnh số, Hãy nhập giá trị từ 0 đến 99999")]
        public int PhotoNo { get; set; }

        [Range(0, 99999, ErrorMessage = "Hộp phim số, Hãy nhập giá trị từ 0 đến 99999")]
        public int FilmGearNo { get; set; }

        [Range(0, 99999, ErrorMessage = "Túi phim số, Hãy nhập giá trị từ 0 đến 99999")]
        public int FilmPocketNo { get; set; }

        [Range(0, 99999, ErrorMessage = "Phim số, Hãy nhập giá trị từ 0 đến 99999")]
        public int FilmNo { get; set; }

        [StringLength(300, MinimumLength = 0, ErrorMessage = "Tác giả, nội dung không được quá 300 kí tự")]
        public string Photographer { get; set; }

        [Required(ErrorMessage = "Hãy nhập thời gian chụp")]
        //[JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
        public DateTime PhotoTime { get; set; }

        [StringLength(300, MinimumLength = 0, ErrorMessage = "Địa điểm chụp, nội dung không được quá 300 kí tự")]
        public string PhotoPlace { get; set; }

        [StringLength(5, MinimumLength = 0, ErrorMessage = "Cỡ phim/ảnh, nội dung không được quá 5 kí tự")]
        public string FilmSize { get; set; }

        [StringLength(50, MinimumLength = 0, ErrorMessage = "ĐV, CN giao ảnh, nội dung không được quá 50 kí tự")]
        public string DeliveryUnit { get; set; }

        [Required(ErrorMessage = "Hãy nhập Ngày giao nộp")]
        //[JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
        public DateTime DeliveryDate { get; set; }

        [StringLength(30, MinimumLength = 0, ErrorMessage = "Kí tự số, nội dung không được quá 30 kí tự")]
        public string InforSign { get; set; }

        [StringLength(500, MinimumLength = 0, ErrorMessage = "Ghi chú, nội dung không được quá 500 kí tự")]
        public string Description { get; set; }

        [Range(1, 2, ErrorMessage = "Tình trạng vật lý, chỉ được nhập 1-Bình thường hoặc 2-Hư hỏng")]
        public int Format { get; set; }

        [Required(ErrorMessage = "Hãy chọn Độ mật của tài liệu phim/ âm thanh")]
        [Range(1, 5, ErrorMessage = "Độ mật của tài liệu phim/ âm thanh, chỉ được nhập 1-Thường, 2-Tài liệu thu hồi, 3-Mật, 4-Tối mật hoặc 5-Tuyệt mật")]
        public int SecurityLevel { get; set; }

        [Range(2, 4, ErrorMessage = "Trạng thái, chỉ được nhập 2-Khả dụng, 3-Đang khai thác hoặc 4-Đã xóa")]
        public int PhotoStatus { get; set; }

        [Required]
        [Range(1, 2, ErrorMessage = "Chế độ sử dụng, chỉ được nhập 1-Hạn chế hoặc 2-Không hạn chế")]
        public int Mode { get; set; }

        [Required]
        public int Colour { get; set; }

        [Required(ErrorMessage = "Hãy chọn loại thời hạn bảo quản của tài liệu ảnh")]
        [Range(1, 2, ErrorMessage = "Loại Hình thức, chỉ được nhập 1-Vĩnh viễn hoặc 2-Có thời hạn")]
        public int StorageTimeType { get; set; }

        //[RequiredIf("StorageTimeType == 2", ErrorMessage = "Hãy nhập thời hạn bảo quản")]
        //[AssertThat("checkMaintenance", ErrorMessage = "Thời hạn bảo quản, chỉ được nhập từ 1 đến 70")]
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

        [Range(1, 2, ErrorMessage = "Loại thời hạn bảo quản, chỉ được nhập 1-Âm bản hoặc 2-Dương bản")]
        public int Form { get; set; }

        public int GalleryNationalAssembly { get; set; }

        public int GalleryCongressMeeting { get; set; }

        public int GalleryMeeting { get; set; }

        public Boolean IsExist { get; set; }

        //[AssertThat("IsExist == true || (IsExist == false && (NationalAssembly == 0 || NationalAssembly == GalleryNationalAssembly))", ErrorMessage = "Quốc Hội Khóa bằng 0 hoặc bằng QHK của sưu tập ảnh")]
        public int NationalAssembly { get; set; }

        [Range(0, 11, ErrorMessage = "Kỳ họp, chỉ được nhập không quá 11")]
        //[AssertThat("IsExist == true || (IsExist == false && (CongressMeeting == 0 || CongressMeeting == GalleryCongressMeeting))", ErrorMessage = "Kỳ họp bằng 0 hoặc bằng kì họp của sưu tập ảnh")]
        public int CongressMeeting { get; set; }

        [Range(0, 54, ErrorMessage = "Phiên họp, chỉ được nhập không quá 54")]
        //[AssertThat("IsExist == true || (IsExist == false && (Meeting == 0 || Meeting == GalleryMeeting))", ErrorMessage = "Phiên họp bằng 0 hoặc bằng phiên họp của sưu tập ảnh")]
        public int Meeting { get; set; }

        public Guid GalleryObjectGuid { get; set; }

        [JsonIgnore]
        public long GalleryID { get; set; }

        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Hãy nhập Độ mật của Hồ Sơ")]
        [Range(1, 5, ErrorMessage = "Độ mật của Hồ Sơ, chỉ được nhập 1-Thường, 2-Tài liệu thu hồi, 3-Mật, 4-Tối mật hoặc 5-Tuyệt mật")]
        public int FileSecurityLevel { get; set; }

        public static string GetListSearch(PhotoSearch ms, out List<PhotoSearchResult> lt, out int total)
        {
            lt = null;
            total = 0;

            dynamic o;
            string msg = GetListPaging(ms, out lt, out total);
            if (msg.Length > 0) return msg;

            return "";

        }
        public static string GetListPaging(PhotoSearch ms, out List<PhotoSearchResult> lt, out int total)
        {
            lt = null; total = 0;

            string msg = "";

            QueryStringBuilder q = new QueryStringBuilder();
            msg = q.InitWithStore("usp_Photo_Select_Search");
            if (msg.Length > 0) return msg;


            msg = GetListPaging_Parameter(false, ms, out dynamic para);
            if (msg.Length > 0) return msg;

            msg = Paging.Exec(q.Sql, "p.PhotoID", para, out lt, out total);
            if (msg.Length > 0) return msg;

            return msg;
        }
        private static string GetListPaging_Parameter(bool IsReport, PhotoSearch ms, out dynamic o)
        {
            o = null;

            o = new
            {
                ms.TextSearch,
                ms.GalleryID,
                ms.PhotoTimeFrom,
                ms.PhotoTimeTo,
                ms.Mode,
                ms.PhotoStatus,
                ms.PageSize,
                ms.CurrentPage,
                DefaultDate = "1900-01-01"
            };

            return "";
        }
    }
    public class PhotoSearch
    {
        public string TextSearch { get; set; }
        [JsonIgnore]
        public long GalleryID { get; set; }
        public Guid ObjectGuidGallery { get; set; }
        public DateTime PhotoTimeFrom { get; set; }
        public DateTime PhotoTimeTo { get; set; }
        public int Mode { get; set; }
        public int PhotoStatus { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public List<Guid> ListObjectGuidID { get; set; }
        public PhotoSearch()
        {
            GalleryID = Mode = PhotoStatus = 0;
            PageSize = 10;
            CurrentPage = 1;
            DateTime dtDefault = DateTime.Parse("1900-01-01");
            PhotoTimeFrom = PhotoTimeTo = dtDefault;

        }
    }
    public class PhotoSearchResult
    {
        [JsonIgnore]
        public long PhotoID { get; set; }
        public Guid ObjectGuid { get; set; }
        public string EventName { get; set; }
        public string ImageTitle { get; set; }
        public string Photographer { get; set; }
        public string ArchivesNumber { get; set; }
        public string InforSign { get; set; }
        public DateTime PhotoTime { get; set; }
        public int Mode { get; set; }
        public string ModeName { get; set; }
        public string Description { get; set; }
        public int PhotoStatus { get; set; }
        public string PhotoStatusName { get; set; }
        public string GalleryCode { get; set; }
    }

    public class PhotoUploadOutput
    {
        public string ImagePath { get; set; }
    }

    public class PhotoSync
    {
        public long PhotoID { get; set; }

        public string EventName { get; set; }

        public string ImageTitle { get; set; }

        public string ArchivesNumber { get; set; }

        public int? PhotoGearNo { get; set; }

        public int? PhotoPocketNo { get; set; }

        public int? PhotoNo { get; set; }

        public int? FilmGearNo { get; set; }

        public int? FilmPocketNo { get; set; }

        public int? FilmNo { get; set; }

        public string Photographer { get; set; }

        public DateTime PhotoTime { get; set; }

        public string PhotoPlace { get; set; }

        public string FilmSize { get; set; }

        public string DeliveryUnit { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreateUser { get; set; }

        public DateTime? LastUpdate { get; set; }

        public int? UpdateUser { get; set; }

        public int? Format { get; set; }

        public int? SecurityLevel { get; set; }

        public int? PhotoStatus { get; set; }

        public int? Mode { get; set; }

        public int? NationalAssembly { get; set; }

        public int? CongressMeeting { get; set; }

        public int? Meeting { get; set; }

        public int? Colour { get; set; }

        public int? StorageTimeType { get; set; }

        public int? Maintenance { get; set; }

        public int? Form { get; set; }

        public string InforSign { get; set; }

        public string ImagePath { get; set; }

        public long? FileID { get; set; }
    }

    public class PhotoSyncInput
    {
        /// <summary>
        /// ID tài liệu ảnh phía Egov
        /// </summary>
        [Required]
        [Range(1, long.MaxValue)]
        public long EgovID { get; set; }
        public string EventName { get; set; }

        public string ImageTitle { get; set; }

        public string ArchivesNumber { get; set; }

        public int? PhotoGearNo { get; set; }

        public int? PhotoPocketNo { get; set; }

        public int? PhotoNo { get; set; }

        public int? FilmGearNo { get; set; }

        public int? FilmPocketNo { get; set; }

        public int? FilmNo { get; set; }

        public string Photographer { get; set; }

        public DateTime PhotoTime { get; set; }

        public string PhotoPlace { get; set; }

        public string FilmSize { get; set; }

        public string DeliveryUnit { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreateUser { get; set; }

        public DateTime? LastUpdate { get; set; }

        public int? UpdateUser { get; set; }

        public int? Format { get; set; }

        public int? SecurityLevel { get; set; }

        public int? PhotoStatus { get; set; }

        public int? Mode { get; set; }

        public int? NationalAssembly { get; set; }

        public int? CongressMeeting { get; set; }

        public int? Meeting { get; set; }

        public int? Colour { get; set; }

        public int? StorageTimeType { get; set; }

        public int? Maintenance { get; set; }

        public int? Form { get; set; }

        public string InforSign { get; set; }

        public string ImagePath { get; set; }
    }
}