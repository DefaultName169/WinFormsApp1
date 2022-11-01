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
    public class Film : Audit
    {
        [JsonIgnore]
        public long FilmID { get; set; }

        public Guid ObjectGuid { get; set; }

        public string EventName { get; set; }

        public string MovieTitle { get; set; }

        public string ArchivesNumber { get; set; }

        public string InforSign { get; set; }

        [JsonIgnore]
        public string Language { get; set; }

        public List<string> Languages { get; set; }

        public string Recorder { get; set; }

        public DateTime RecordDate { get; set; }

        public string PlayTime { get; set; }

        public string Quality { get; set; }

        public string RecordPlace { get; set; }

        public string Description { get; set; }

        public int FilmStatus { get; set; }

        public int Format { get; set; }

        public int SecurityLevel { get; set; }

        public int StorageTimeType { get; set; }

        public int? Maintenance { get; set; }

        public int Mode { get; set; }

        public int NationalAssembly { get; set; }

        public int CongressMeeting { get; set; }

        public int Meeting { get; set; }

        [JsonIgnore]
        public long FileID { get; set; }

        [JsonIgnore]
        public int FileSecurityLevel { get; set; }

        public string FilmPath { get; set; }
        public long EgovID { get; set; }

        public static string GetListSearch(FilmSearch ms, out List<FilmSearchResult> lt, out int total)
        {
            lt = null;
            total = 0;

            dynamic o;
            string msg = GetListPaging(ms, out lt, out total);
            if (msg.Length > 0) return msg;

            return "";

        }
        public static string GetListPaging(FilmSearch ms, out List<FilmSearchResult> lt, out int total)
        {
            lt = null; total = 0;

            string msg = "";

            QueryStringBuilder q = new QueryStringBuilder();
            msg = q.InitWithStore("usp_Film_Select_Search");
            if (msg.Length > 0) return msg;


            msg = GetListPaging_Parameter(false, ms, out dynamic para);
            if (msg.Length > 0) return msg;

            msg = Paging.Exec(q.Sql, "d.FilmID", para, out lt, out total);
            if (msg.Length > 0) return msg;

            return msg;
        }
        private static string GetListPaging_Parameter(bool IsReport, FilmSearch ms, out dynamic o)
        {
            o = null;

            o = new
            {
                ms.TextSearch,
                ms.FileID,
                ms.NationalAssembly,
                ms.CongressMeeting,
                ms.Meeting,
                ms.RecordDateFrom,
                ms.RecordDateTo,
                ms.Mode,
                ms.FilmStatus,
                ms.PageSize,
                ms.CurrentPage,
                DefaultDate = "1900-01-01"
            };

            return "";
        }


        /// <summary>
        /// Insert or update film
        /// </summary>
        /// <param name="dbm"></param>
        /// <param name="o"></param>
        /// <param name="UserID"></param>
        /// <returns>string</returns>
        public string InsertOrUpdate(DBM dbm, out Film o, int UserID)
        {
            o = null;
            string msg = dbm.SetStoreNameAndParams("usp_Film_InsertOrUpdate", new
            {
                FilmID,
                ObjectGuid,
                EventName,
                MovieTitle,
                InforSign,
                ArchivesNumber,
                Recorder,
                RecordDate,
                PlayTime,
                Quality,
                RecordPlace,
                Description,
                CreateUser = UserID,
                UpdateUser = UserID,
                CongressMeeting,
                Meeting,
                Format,
                SecurityLevel,
                Mode,
                NationalAssembly,
                StorageTimeType,
                Maintenance,
                FilmStatus,
                Language = string.Join(",", Languages),
                FileID,
                FileSecurityLevel,
                FilmPath
            });
            if (msg.Length > 0) return msg;

            return dbm.GetOne(out o);
        }
        public static string GetOneByGuid(Guid ObjectGuid, out long FilmID)
        {
            FilmID = 0;
            string msg = GetOne(ObjectGuid, out Film f);
            if (msg.Length > 0) return msg;

            if (f == null) return ("Không tồn tại Film có ObjectGuid = " + ObjectGuid).ToMessageForUser();
            FilmID = f.FilmID;

            return msg;
        }
        public static string GetOne(Guid ObjectGuid, out Film o)
        {
            return DBM.GetOne("usp_Film_GetOneByObjectGuid", new { ObjectGuid }, out o);
        }

        public static string Delete(long FilmID)
        {
            return DBM.ExecStore("usp_Film_UpdateIsDelete", new { FilmID });
        }

        /// <summary>
        /// Check the Film exists or not by EventName, ArchivesNumber
        /// </summary>
        /// <param name="EventName"></param>
        /// <param name="ArchivesNumber"></param>
        /// <param name="outFilms"></param>
        /// <returns></returns>
        public static string GetOneByMovieTitleAndArchivesNumber(string MovieTitle, string ArchivesNumber, out Film outFilms)
        {
            return DBM.GetOne("usp_Film_GetOneByMovieTitleAndArchivesNumber", new { MovieTitle, ArchivesNumber }, out outFilms);
        }

        public static string GetListByTicketID(long TicketID, out List<DocumentTicketSearch> o)
        {
            return DBM.GetList("usp_Film_GetListByTicketID", new { TicketID }, out o);
        }

        public static string GetListByListFileGuidID(List<Guid> guids, int status, out List<FilmSearchResult> listFilm)
        {
            return DBM.GetList("usp_Film_GetListByListFileGuidID", new
            {
                Guids = guids.Select(s =>
            new {
                ObjGuid = s,
                DefaultColumn = 0
            }).ToList().ToDataTable(),
                Status = status
            }, out listFilm);
        }

        public static string GetListByListGuidID(List<Guid> listObjectGuidID, int status, out List<FilmSearchResult> lt)
        {
            return DBM.GetList("usp_Film_GetListByListGuidID", new
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
    public class FilmSearch
    {
        public string TextSearch { get; set; }
        [JsonIgnore]
        public long FileID { get; set; }
        public Guid ObjectGuidFile { get; set; }
        public int NationalAssembly { get; set; }
        public int CongressMeeting { get; set; }
        public int Meeting { get; set; }
        public DateTime RecordDateFrom { get; set; }
        public DateTime RecordDateTo { get; set; }
        public int Mode { get; set; }
        public int FilmStatus { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public List<Guid> ListObjectGuidID { get; set; }

        public FilmSearch()
        {
            FileID = NationalAssembly = CongressMeeting = Meeting = FilmStatus= Mode = 0;
            PageSize = 10;
            CurrentPage = 1;
            DateTime dtDefault = DateTime.Parse("1900-01-01");
            RecordDateFrom = RecordDateTo = dtDefault;

        }
    }
    public class FilmSearchResult
    {
        [JsonIgnore]
        public long FilmID { get; set; }
        public Guid ObjectGuid { get; set; }
        public string EventName { get; set; }
        public string InforSign { get; set; }
        public string ArchivesNumber { get; set; }
        public string MovieTitle { get; set; }
        public string Recorder { get; set; }
        public string RecordDate { get; set; }
        public int Mode { get; set; }
        public string ModeName { get; set; }
        public string Description { get; set; }
        public int FilmStatus { get; set; }
        public string FilmStatusName { get; set; }
        public string FileCode { get; set; }

    }

    public class FilmInput : Audit
    {
        [JsonIgnore]
        public long FilmID { get; set; }

        public Guid ObjectGuid { get; set; }

        [Required(ErrorMessage = "Hãy nhập Tên sự kiện tài liệu phim/ âm thanh")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Tên sự kiện, nội dung không được quá 500 kí tự")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Hãy nhập Tiêu đề tài liệu phim/ âm thanh")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Tiêu đề, nội dung không được quá 500 kí tự")]
        public string MovieTitle { get; set; }

        [Required(ErrorMessage = "Hãy nhập số lưu trữ tài liệu phim/ âm thanh")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Số lưu trữ, nội dung không được quá 30 kí tự")]
        public string ArchivesNumber { get; set; }

        [StringLength(30, MinimumLength = 0, ErrorMessage = "Kí tự số, nội dung không được quá 30 kí tự")]
        public string InforSign { get; set; }

        [JsonIgnore]
        public string Language { get; set; }
        public List<string> Languages { get; set; }

        [StringLength(300, MinimumLength = 0, ErrorMessage = "Tác giả, nội dung không được quá 300 kí tự")]
        public string Recorder { get; set; }

        [Required(ErrorMessage = "Hãy nhập thời gian")]
        [JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
        public DateTime RecordDate { get; set; }

        [StringLength(8, MinimumLength = 0, ErrorMessage = "Thời lượng, nội dung không được quá 8 kí tự")]
        [AssertThat("IsRegexMatch(PlayTime, '^([0-9]{2}):([0-5][0-9]):([0-5][0-9])+$')", ErrorMessage = "Thời lượng, phải có định dạng: [số giờ:số phút:số giây]")]
        public string PlayTime { get; set; }

        [StringLength(50, MinimumLength = 0, ErrorMessage = "Chất lượng, nội dung không được quá 8 kí tự")]
        public string Quality { get; set; }

        [StringLength(300, MinimumLength = 0, ErrorMessage = "Địa điểm, nội dung không được quá 300 kí tự")]
        public string RecordPlace { get; set; }

        [StringLength(500, MinimumLength = 0, ErrorMessage = "Ghi chú, nội dung không được quá 500 kí tự")]
        public string Description { get; set; }

        [Range(2, 4, ErrorMessage = "Trạng thái của tài liệu phim/ âm thanh, chỉ được nhập 2-Khả dụng, 3-Đang khai thác hoặc 4-Đã xóa")]
        public int FilmStatus { get; set; }

        [Range(1, 2, ErrorMessage = "Tình trạng vật lý, chỉ được nhập 1-Bình thường hoặc 2-Hư hỏng")]
        public int Format { get; set; }

        [Required(ErrorMessage = "Hãy chọn Độ mật của tài liệu phim/ âm thanh")]
        [Range(1, 5, ErrorMessage = "Độ mật của tài liệu phim/ âm thanh, chỉ được nhập 1-Thường, 2-Tài liệu thu hồi, 3-Mật, 4-Tối mật hoặc 5-Tuyệt mật")]
        public int SecurityLevel { get; set; }

        [Required(ErrorMessage = "Hãy chọn loại thời hạn bảo quản của tài liệu phim/ âm thanh")]
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

        [Required]
        [Range(1, 2, ErrorMessage = "Chế độ sử dụng, chỉ được nhập 1-Hạn chế hoặc 2-Không hạn chế")]
        public int Mode { get; set; }

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

        public Guid FileObjectGuid { get; set; }

        [JsonIgnore]
        public long FileID { get; set; }

        [Required(ErrorMessage = "Hãy nhập Độ mật của Hồ Sơ")]
        [Range(1, 5, ErrorMessage = "Độ mật của Hồ Sơ, chỉ được nhập 1-Thường, 2-Tài liệu thu hồi, 3-Mật, 4-Tối mật hoặc 5-Tuyệt mật")]
        public int FileSecurityLevel { get; set; }

        public string FilmPath { get; set; }
    }

    public class FilmSync
    {
        public long FilmID { get; set; }

        public long FileID { get; set; }

        public string EventName { get; set; }

        public string MovieTitle { get; set; }

        public string ArchivesNumber { get; set; }

        public string Recorder { get; set; }

        public DateTime RecordDate { get; set; }

        public string PlayTime { get; set; }

        public string Quality { get; set; }

        public string RecordPlace { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreateUser { get; set; }

        public DateTime? LastUpdate { get; set; }

        public int? UpdateUser { get; set; }

        public int? CongressMeeting { get; set; }

        public int? Meeting { get; set; }

        public int? Format { get; set; }

        public int? SecurityLevel { get; set; }

        public int? Mode { get; set; }

        public int? NationalAssembly { get; set; }

        public int? StorageTimeType { get; set; }

        public int? Maintenance { get; set; }

        public int? FilmStatus { get; set; }

        public string Language { get; set; }

        public string InforSign { get; set; }

        public string FilmPath { get; set; }
    }
    public class FilmSyncInput
    {
        /// <summary>
        /// ID tài liệu phim phía Egov
        /// </summary>
        [Required]
        [Range(1, long.MaxValue)]
        public long EgovID { get; set; }
        public string EventName { get; set; }

        public string MovieTitle { get; set; }

        public string ArchivesNumber { get; set; }

        public string Recorder { get; set; }

        public DateTime RecordDate { get; set; }

        public string PlayTime { get; set; }

        public string Quality { get; set; }

        public string RecordPlace { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreateUser { get; set; }

        public DateTime? LastUpdate { get; set; }

        public int? UpdateUser { get; set; }

        public int? CongressMeeting { get; set; }

        public int? Meeting { get; set; }

        public int? Format { get; set; }

        public int? SecurityLevel { get; set; }

        public int? Mode { get; set; }

        public int? NationalAssembly { get; set; }

        public int? StorageTimeType { get; set; }

        public int? Maintenance { get; set; }

        public int? FilmStatus { get; set; }

        public string Language { get; set; }

        public string InforSign { get; set; }

        public string FilmPath { get; set; }
    }
}