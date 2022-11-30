using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using BSS;
using Newtonsoft.Json;
using SynceOToHTLT.Common;
using SynceOToHTLT.Models.HTLT.Document;
using SynceOToHTLT.Models.HTLT.Film;
using SynceOToHTLT.Models.HTLT.Photo;

namespace SynceOToHTLT.Models.HTLT.File
{
    public class Files : Audit
    {
        [JsonIgnore]
        [Key]
        public long FileID { get; set; }
        public Guid ObjectGuid { get; set; }
        public string Title { get; set; }
        public string FileCode { get; set; }
        public string FileNotation { get; set; }
        public int GroupFile { get; set; }
        public string FileNo { get; set; }
        public long FileCatalog { get; set; }
        public string Identifier { get; set; }
        public int PiecesOfPaper { get; set; }
        public int PageNumber { get; set; }
        public int TotalDoc { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Rights { get; set; }
        public string NationalAssembly { get; set; }
        public List<string> NationalAssemblys { get; set; }
        public int StorageTimeType { get; set; }
        public int? Maintenance { get; set; }
        public string PersonallyFiled { get; set; }
        public DateTime? DeliveryDate { get; set; }
        [JsonIgnore]
        public string OrganID { get; set; }
        public string FontName { get; set; }
        public string Gear { get; set; }
        public int Racking { get; set; }
        public string RackingValue { get; set; }
        public int Compartment { get; set; }
        public string CompartmentValue { get; set; }
        public int FileRowNumber { get; set; }
        public string FileRowNumberValue { get; set; }
        public string InforSign { get; set; }
        [JsonIgnore]
        public string Language { get; set; }
        public string KeywordIssue { get; set; }
        public string KeywordPlace { get; set; }
        public string KeywordEvent { get; set; }
        public string Description { get; set; }
        public int FileStatus { get; set; }
        public int Format { get; set; }
        public List<string> Languages { get; set; }
        public int CongressMeeting { get; set; }
        public int Meeting { get; set; }
        public int SecurityLevel { get; set; }
        public int SystemType { get; set; }
        public int FileType { get; set; }
        public int? StorageLocationID { get; set; }
        public int? FileTypeInGroup { get; set; }
        public long EgovID { get; set; }
        /// <summary>
        /// Danh sách tài liệu giấy (sử dụng cho nhận dữ liệu từ Egov)
        /// </summary>
        [JsonIgnore]
        public List<DocumentSyncInput> Documents { get; set; }
        /// <summary>
        /// Danh sách tài liệu ảnh (sử dụng cho nhận dữ liệu từ Egov)
        /// </summary>
        [JsonIgnore]
        public List<PhotoSyncInput>? Photos { get; set; }
        /// <summary>
        /// Danh sách tài liệu video (sử dụng cho nhận dữ liệu từ Egov)
        /// </summary>
        [JsonIgnore]
        public List<FilmSyncInput>? Films { get; set; }
        /// <summary>
        /// Insert or update file
        /// </summary>
        /// <param name="dbm"></param>
        /// <param name="o"></param>
        /// <param name="UserID"></param>
        /// <returns>string</returns>
        public string InsertOrUpdate(DBM dbm, out Files o, int UserID)
        {
            o = null;
            string msg = dbm.SetStoreNameAndParams("usp_File_InsertOrUpdate", new
            {
                FileID,
                Title,
                FileCode,
                FileNotation,
                GroupFile,
                FileNo, //= FileNo.PadLeft(3, '0'),
                FileCatalog,
                Identifier = Identifier.Trim(),
                PiecesOfPaper,
                PageNumber,
                TotalDoc,
                StartDate,
                EndDate,
                Rights,
                NationalAssembly,
                StorageTimeType,
                Maintenance,
                PersonallyFiled,
                DeliveryDate,
                Gear, //= Gear.ToLower(),
                Racking,
                Compartment,
                FileRowNumber,
                InforSign,
                Language,
                KeywordIssue,
                KeywordPlace,
                KeywordEvent,
                Description,
                Format,
                CreateUser = UserID,
                UpdateUser = UserID,
                CongressMeeting,
                Meeting,
                SecurityLevel,
                SystemType,
                FileType,
                StorageLocationID,
                FontName,
                FileTypeInGroup
            });
            if (msg.Length > 0) return msg;

            return dbm.GetOne(out o);
        }
        /// <summary>
        /// Insert or update file from Egov
        /// </summary>
        /// <param name="dbm"></param>
        /// <param name="o"></param>
        /// <param name="UserID"></param>
        /// <returns>string</returns>
        public string InsertOrUpdateFromEgov(DBM dbm, out Files o, int UserID)
        {
            o = null;
            string msg = dbm.SetStoreNameAndParams("usp_File_InsertOrUpdate_ToEgov", new
            {
                Title,
                FileCode,
                FileNotation,
                GroupFile,
                FileNo, //= FileNo.PadLeft(3, '0'),
                FileCatalog,
                Identifier = Identifier.Trim(),
                PiecesOfPaper,
                PageNumber,
                TotalDoc,
                StartDate = StartDate == DateTime.MinValue ? DateTime.Now : StartDate,
                EndDate = EndDate == DateTime.MinValue ? DateTime.Now : EndDate,
                Rights,
                NationalAssembly,
                StorageTimeType,
                Maintenance,
                PersonallyFiled,
                DeliveryDate = DeliveryDate == DateTime.MinValue ? DateTime.Now : DeliveryDate,
                Gear, //= Gear.ToLower(),
                Racking,
                Compartment,
                FileRowNumber,
                InforSign,
                Language,
                KeywordIssue,
                KeywordPlace,
                KeywordEvent,
                Description,
                Format,
                CreateUser = UserID,
                UpdateUser = UserID,
                CongressMeeting,
                Meeting,
                SecurityLevel,
                SystemType,
                FileType,
                StorageLocationID,
                FontName,
                FileTypeInGroup,
                EgovID,
                Documents = Documents?.Select(s => new
                {
                    s.EgovID,
                    FileID = 0,
                    s.DocumentID,
                    s.DocCode,
                    s.FileNotation,
                    s.Field,
                    s.IssuedDate,
                    s.PiecesOfPaper,
                    s.PageAmount,
                    Language = s.Languages?.Count() > 0 ? string.Join(",", s.Languages) : "",
                    s.SecurityLevel,
                    s.Autograph,
                    s.Subject,
                    s.KeywordIssue,
                    s.KeywordPlace,
                    s.KeywordEvent,
                    s.Description,
                    s.ConfidenceLevel,
                    s.StorageTimeType,
                    s.Maintenance,
                    s.CreateDate,
                    s.CreateUser,
                    s.LastUpdate,
                    s.UpdateUser,
                    s.NationalAssembly,
                    s.Mode,
                    s.DocStatus,
                    s.Format,
                    s.CongressMeeting,
                    s.Meeting,
                    s.TypeName,
                    s.InforSign,
                    s.DocumentPath,
                    SystemType = 1,
                    s.OrganName,
                    s.AgencyCreate,
                    s.CodeNumber
                }).ToList().ToDataTableV2(),
                Photos = Photos?.Select(s => new
                {
                    s.EgovID,
                    s.EventName,
                    s.ImageTitle,
                    s.ArchivesNumber,
                    s.PhotoGearNo,
                    s.PhotoPocketNo,
                    s.PhotoNo,
                    s.FilmGearNo,
                    s.FilmPocketNo,
                    s.FilmNo,
                    s.Photographer,
                    s.PhotoTime,
                    s.PhotoPlace,
                    s.FilmSize,
                    s.DeliveryUnit,
                    s.DeliveryDate,
                    s.Description,
                    s.CreateDate,
                    s.CreateUser,
                    s.LastUpdate,
                    s.UpdateUser,
                    s.Format,
                    s.SecurityLevel,
                    s.PhotoStatus,
                    s.Mode,
                    s.NationalAssembly,
                    s.CongressMeeting,
                    s.Meeting,
                    s.Colour,
                    s.StorageTimeType,
                    s.Maintenance,
                    s.Form,
                    s.InforSign,
                    s.ImagePath
                }).ToList().ToDataTableV2(),
                Films = Films?.Select(s => new
                {
                    s.EgovID,
                    s.EventName,
                    s.MovieTitle,
                    s.ArchivesNumber,
                    s.Recorder,
                    s.RecordDate,
                    s.PlayTime,
                    s.Quality,
                    s.RecordPlace,
                    s.Description,
                    s.CreateDate,
                    s.CreateUser,
                    s.LastUpdate,
                    s.UpdateUser,
                    s.CongressMeeting,
                    s.Meeting,
                    s.Format,
                    s.SecurityLevel,
                    s.Mode,
                    s.NationalAssembly,
                    s.StorageTimeType,
                    s.Maintenance,
                    s.FilmStatus,
                    s.Language,
                    s.InforSign,
                    s.FilmPath
                }).ToList().ToDataTableV2()
            });
            if (msg.Length > 0) return msg;

            return dbm.GetOne(out o);
        }

        public static string GetOneByGuid(Guid ObjectGuid, out long FileID)
        {
            FileID = 0;

            Files f;
            string msg = DBM.GetOne("usp_File_GetOneByObjectGuid", new { ObjectGuid }, out f);
            if (msg.Length > 0) return msg;

            if (f == null) return ("Hồ sơ không tồn tại").ToMessageForUser();
            FileID = f.FileID;

            return msg;
        }
        public static string Delete(long FileID)
        {
            return DBM.ExecStore("usp_File_UpdateIsDelete", new { FileID });
        }

        /// <summary>
        /// Cập nhật trạng thái đồng bộ
        /// </summary>
        /// <param name="ObjectGuid"></param>
        /// <param name="Status">1: chờ tiếp nhận, 2: khả dụng, 3:Từ chối, 4: đã xóa</param>
        /// <returns></returns>
        public static string UpdateStatus(long FileID, int Status)
        {
            return DBM.ExecStore("usp_File_UpdateStatus", new { FileID, Status });
        }
        /// <summary>
        /// Cập nhật trạng thái hồ sơ tài liệu
        /// </summary>
        /// <returns></returns>
        public static string UpdateStatusByDoc()
        {
            return DBM.ExecStore("usp_File_UpdateStatusByDoc");
        }
        public static string GetListSearch(FileSearch ms, out List<FileSearchResult> lt, out int total)
        {
            lt = null;
            total = 0;

            dynamic o;
            string msg = GetListPaging(ms, out lt, out total);
            if (msg.Length > 0) return msg;

            return "";

        }
        public static string GetListPaging(FileSearch ms, out List<FileSearchResult> lt, out int total)
        {
            lt = null; total = 0;

            string msg = "";

            QueryStringBuilder q = new QueryStringBuilder();
            msg = q.InitWithStore("usp_File_Select_Search");
            if (msg.Length > 0) return msg;


            msg = GetListPaging_Parameter(false, ms, out dynamic para);
            if (msg.Length > 0) return msg;

            msg = Paging.Exec(q.Sql, "f.FileID", para, out lt, out total);
            if (msg.Length > 0) return msg;

            return msg;
        }
        private static string GetListPaging_Parameter(bool IsReport, FileSearch ms, out dynamic o)
        {
            o = null;

            o = new
            {
                ms.TextSearch,
                ms.Racking,
                ms.Compartment,
                ms.FileRowNumber,
                ms.Gear,
                ms.NationalAssembly,
                ms.CongressMeeting,
                ms.Meeting,
                ms.SecurityLevel,
                ms.StartDate,
                ms.EndDate,
                ms.Rights,
                ms.FileStatus,
                ms.PageSize,
                ms.CurrentPage,
                ms.FileType,
                DefaultDate = "1900-01-01"
            };

            return "";
        }

        public static string GetOneByObjectGuid(Guid ObjectGuid, out Files file)
        {
            string msg = DBM.GetOne("usp_File_GetOneByObjectGuid", new { ObjectGuid }, out file);
            if (file == null) return ("Hồ sơ không tồn tại").ToMessageForUser();
            return msg;
        }

        /// <summary>
        /// Check the File exists or not by FileNo
        /// </summary>
        /// <param name="FileNo"></param>
        /// <param name="FileExist"></param>
        /// <returns></returns>
        public static string CheckFileExistByFileNo(string FileNo, out Files FileExist)
        {
            return DBM.GetOne("usp_File_GetIDByFileNo", new { FileNo }, out FileExist);
        }

        /// <summary>
        /// Check the File exists or not by FileCode
        /// </summary>
        /// <param name="FileNo"></param>
        /// <param name="FileExist"></param>
        /// <returns></returns>
        public static string CheckFileExistByFileCode(string FileCode, out Files FileExist)
        {
            return DBM.GetOne("usp_File_GetIDByFileCode", new { FileCode }, out FileExist);
        }

        /// <summary>
        /// Check FileCode or FileNo
        /// 1. Trùng trong trường hợp có tồn tại FileCode hoặc FileNo trong File thuộc HTLT (SystemType=1)
        /// 2. Trùng trong trường hợp chỉ tồn tại FileCode hoặc FileNo trong file thuộc Egov (SystemType=2)
        /// </summary>
        /// <param name="FileCode"></param>
        /// <param name="FileNo"></param>
        /// <param name="FileExist"></param>
        /// <returns></returns>
        public static string CheckFileCodeOrFileNo(string FileCode, string FileNo, out Files FileExist)
        {
            return DBM.GetOne("usp_File_GetIDByFileCodeOrFileNo", new { FileCode, FileNo }, out FileExist);
        }

        public static string GetFileByStatusForHomePage(out long MaxID)
        {
            return DBM.ExecStore("usp_File_GetFileByStatus", out MaxID);
        }
        public static string GetTopFile(out List<FileSearch> lstFile, int Top)
        {
            return DBM.GetList("usp_File_GetTopFile", new { Top }, out lstFile);
        }

        public static string Receive(string FileIDs)
        {
            return DBM.ExecStore("usp_File_Receive", new { ListFileID = FileIDs });
        }

        public static string GetListByListGuid(List<Guid> Guids, out List<FileSearchResult> files)
        {
            return DBM.GetList("usp_File_GetListByListGuid", new
            {
                Guids = Guids.Select(s => new
                {
                    ObjGuid = s,
                    DefaultColumn = 0
                }).ToList().ToDataTable()
            }, out files);
        }
    }

    public class FileInput : Audit
    {
        [JsonIgnore]
        public long FileID { get; set; }
        [Required(ErrorMessage = "Hãy nhập Guid")]
        public Guid ObjectGuid { get; set; }
        [Required(ErrorMessage = "Hãy nhập Tiêu đề hồ sơ")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Tiêu đề, hãy nhập nội dung và không được quá 1000 kí tự")]
        public string Title { get; set; }
        public int GroupFile { get; set; }
        [JsonIgnore]
        public string GroupFileValue { get; set; }
        [Required(ErrorMessage = "Hãy nhập hồ sơ số")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Hồ sơ số, hãy nhập nội dung và không được quá 20 kí tự")]
        //[AssertThat("IsRegexMatch(FileNo, '^[a-zA-Z0-9]+$')", ErrorMessage = "Hồ sơ số, không được nhập ký tự có dấu và các ký tự đặc biệt")]
        public string FileNo { get; set; }
        //[AssertThat("ValidateFileNotation == true", ErrorMessage = "Số và kí hiệu không đúng định dạng")]
        [StringLength(20, MinimumLength = 0, ErrorMessage = "Số và kí hiệu, nội dung không được quá 20 kí tự")]
        public string FileNotation { get; set; }
        [Range(0, 99999, ErrorMessage = "Mục lục số chỉ được nhập 5 kí tự số")]
        public long FileCatalog { get; set; }
        //[Range(1000, 9999, ErrorMessage = "Năm của hồ sơ chỉ được nhập 4 kí tự số")]
        //public int FileYear { get; set; }
        //[AssertThat("Identifier == '000.00.00.C01' || Identifier == ''", ErrorMessage = "Mã CQ lưu trữ không đúng")]
        public string Identifier { get; set; }
        //[AssertThat("ValidateFileCode == true", ErrorMessage = "Mã hồ sơ không đúng định dạng")]
        [Required(ErrorMessage = "Hãy nhập mã hồ sơ")]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Mã hồ sơ, nội dung không được quá 100 kí tự")]
        public string FileCode { get; set; }
        public int PiecesOfPaper { get; set; }
        public int PageNumber { get; set; }
        public int TotalDoc { get; set; }
        //[JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
        [Required(ErrorMessage = "Hãy chọn ngày bắt đầu")]
        public DateTime StartDate { get; set; }
        //[JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
        [Required(ErrorMessage = "Hãy chọn ngày kết thúc")]
        public DateTime EndDate { get; set; }
        [Required]
        [Range(1, 2, ErrorMessage = "Chế độ sử dụng, chỉ được nhập 1-Hạn chế hoặc 2-Không hạn chế")]
        public int Rights { get; set; }
        [JsonIgnore]
        public string RightsValue { get; set; }
        public string NationalAssembly { get; set; }
        public List<string> NationalAssemblys { get; set; }
        [JsonIgnore]
        public string NationalAssemblyValue { get; set; }
        [Range(1, 2, ErrorMessage = "Loại thời hạn bảo quản, chỉ được nhập 1-Vĩnh viễn hoặc 2-Có thời hạn")]
        public int StorageTimeType { get; set; }
        [JsonIgnore]
        public string StorageTimeTypeValue { get; set; }
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
        [StringLength(200, MinimumLength = 0, ErrorMessage = "Đ/v, cá nhân, nội dung không được quá 200 kí tự")]
        public string PersonallyFiled { get; set; }
        //[JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
        [Required(ErrorMessage = "Hãy chọn ngày giao")]
        public DateTime DeliveryDate { get; set; }
        [JsonIgnore]
        public string OrganID { get; set; }
        public string FontName { get; set; }
        [StringLength(20, MinimumLength = 0, ErrorMessage = "Hộp số, nội dung không được quá 20 kí tự")]
        //[AssertThat("IsRegexMatch(Gear, '^[a-zA-Z0-9]*$')", ErrorMessage = "Hộp số, không được nhập ký tự có dấu và các ký tự đặc biệt")]
        public string Gear { get; set; }
        public int Racking { get; set; }
        [JsonIgnore]
        public string RackingValue { get; set; }
        public int Compartment { get; set; }
        [JsonIgnore]
        public string CompartmentValue { get; set; }
        public int FileRowNumber { get; set; }
        [JsonIgnore]
        public string FileRowNumberValue { get; set; }
        [StringLength(30, MinimumLength = 0, ErrorMessage = "Ký hiệu thông tin, nội dung không được quá 30 kí tự")]
        public string InforSign { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Từ khóa_Vấn đề chính, nội dung không được quá 100 kí tự")]
        public string KeywordIssue { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Từ khóa_Địa danh, nội dung không được quá 100 kí tự")]
        public string KeywordPlace { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Từ khóa_Sự kiện, nội dung không được quá 100 kí tự")]
        public string KeywordEvent { get; set; }
        [StringLength(2000, MinimumLength = 0, ErrorMessage = "Chú giải, nội dung không được quá 2000 kí tự")]
        public string Description { get; set; }
        [Range(1, 4, ErrorMessage = "Trạng thái của hồ sơ, chỉ được nhập 1-Chờ tiếp nhận, 2-Khả dụng, 3-Từ chối hoặc 4-Đã xóa")]
        public int FileStatus { get; set; }
        [Range(1, 2, ErrorMessage = "Tình trạng vật lý, chỉ được nhập 1-Bình thường hoặc 2-Hư hỏng")]
        public int Format { get; set; }
        [JsonIgnore]
        public string FormatValue { get; set; }
        public List<string> Languages { get; set; }
        [JsonIgnore]
        public string LanguagesValue { get; set; }
        [Range(0, 11, ErrorMessage = "Kỳ họp, chỉ được nhập không quá 11")]
        public int CongressMeeting { get; set; }
        [Range(0, 54, ErrorMessage = "Phiên họp, chỉ được nhập không quá 54")]
        public int Meeting { get; set; }
        [Range(1, 2, ErrorMessage = "Loại hệ thống, chỉ được nhập 1-Hệ Thống Lưu Trữ hoặc 2-EGov")]
        public int SystemType { get; set; } = 1;
        [Range(1, 5, ErrorMessage = "Độ mật của hồ sơ, chỉ được nhập 1-Thường, 2-Tài liệu thu hồi, 3-Mật, 4-Tối mật hoặc 5-Tuyệt mật")]
        public int SecurityLevel { get; set; }
        [JsonIgnore]
        public string SecurityLevelValue { get; set; }
        [Range(1, 3, ErrorMessage = "Loại hồ sơ giấy, chỉ được nhập 1-HS giấy, 2-HS nhìn, 3-HS nghe nhìn")]
        public int FileType { get; set; }

        public int StorageLocationID { get; set; }
        public int? FileTypeInGroup { get; set; }

        ///// <summary>
        ///// Validate FileNotation
        ///// </summary>
        //[JsonIgnore]
        //public bool ValidateFileNotation
        //{
        //    get
        //    {
        //        if (string.IsNullOrWhiteSpace(FileNotation)) return true;
        //        string msg = CacheSystemKey.GetSystemKeyByParentAndChildValue("FILE_GROUP", GroupFile.ToString().Trim(), out SystemKey sys);
        //        if (msg.Length > 0 || sys.CodeKey == null) return false;
        //        string FileCodeCompare = string.Join(".", new string[] { FileNo, sys.CodeKey });
        //        if (!string.Equals(FileNotation, FileCodeCompare)) return false;
        //        return true;
        //    }
        //}

        ///// <summary>
        ///// Validate GroupFile
        ///// </summary>
        //[JsonIgnore]
        //public bool ValidateGroupFile
        //{
        //    get
        //    {
        //        if (GroupFile == 0) return true;
        //        if (string.Equals(GroupFile.GetTextFromSysCode("FILE_GROUP", ""), "")) return false;
        //        return true;
        //    }
        //}
    }

    public class FileInputSync
    {
        /// <summary>
        /// ID hồ sơ phía Egov
        /// </summary>
        [Required]
        [Range(1, long.MaxValue)]
        public long EgovID { get; set; }
        [Description("Tiêu đề hồ sơ")]
        [Required(ErrorMessage = "Hãy nhập Tiêu đề hồ sơ")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Tiêu đề, hãy nhập nội dung và không được quá 1000 kí tự")]
        public string Title { get; set; }
        //[AssertThat("ValidateGroupFile == true", ErrorMessage = "Nhóm hồ sơ không tồn tại")]
        public int GroupFile { get; set; }
        [Required(ErrorMessage = "Hãy nhập hồ sơ số")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Hồ sơ số, hãy nhập nội dung và không được quá 20 kí tự")]
        //[AssertThat("IsRegexMatch(FileNo, '^[a-zA-Z0-9]+$')", ErrorMessage = "Hồ sơ số, không được nhập ký tự có dấu và các ký tự đặc biệt")]
        public string FileNo { get; set; }
        //[AssertThat("ValidateFileNotation == true", ErrorMessage = "Số và kí hiệu không đúng định dạng")]
        [StringLength(20, MinimumLength = 0, ErrorMessage = "Số và kí hiệu, nội dung không được quá 20 kí tự")]
        public string FileNotation { get; set; }
        [Range(0, 99999, ErrorMessage = "Mục lục số chỉ được nhập 5 kí tự số")]
        public long FileCatalog { get; set; }
        //[Range(1000, 9999, ErrorMessage = "Năm của hồ sơ chỉ được nhập 4 kí tự số")]
        //public int FileYear { get; set; }
        //[AssertThat("Identifier == '000.00.00.C01' || Identifier == ''", ErrorMessage = "Mã CQ lưu trữ không đúng")]
        public string Identifier { get; set; }
        //[AssertThat("ValidateFileCode == true", ErrorMessage = "Mã hồ sơ không đúng định dạng")]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Mã hồ sơ, nội dung không được quá 100 kí tự")]
        public string FileCode { get; set; }
        public int PiecesOfPaper { get; set; }
        public int PageNumber { get; set; }
        public int TotalDoc { get; set; }
        //[JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
        [Required(ErrorMessage = "Hãy chọn ngày bắt đầu")]
        public DateTime StartDate { get; set; }
        //[JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
        [Required(ErrorMessage = "Hãy chọn ngày kết thúc")]
        public DateTime EndDate { get; set; }
        [Required]
        [Range(1, 2, ErrorMessage = "Chế độ sử dụng, chỉ được nhập 1-Hạn chế hoặc 2-Không hạn chế")]
        public int Rights { get; set; }
        public List<string> NationalAssemblys { get; set; }

        [Range(1, 2, ErrorMessage = "Loại thời hạn bảo quản, chỉ được nhập 1-Vĩnh viễn hoặc 2-Có thời hạn")]
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
        [StringLength(200, MinimumLength = 0, ErrorMessage = "Đ/v, cá nhân, nội dung không được quá 200 kí tự")]
        public string PersonallyFiled { get; set; }
        //[JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
        [Required(ErrorMessage = "Hãy chọn ngày giao")]
        public DateTime DeliveryDate { get; set; }
        public string FontName { get; set; }
        [StringLength(20, MinimumLength = 0, ErrorMessage = "Hộp số, nội dung không được quá 20 kí tự")]
        //[AssertThat("IsRegexMatch(Gear, '^[a-zA-Z0-9]*$')", ErrorMessage = "Hộp số, không được nhập ký tự có dấu và các ký tự đặc biệt")]
        public string Gear { get; set; }
        public int Racking { get; set; }

        public int Compartment { get; set; }

        public int FileRowNumber { get; set; }

        [StringLength(30, MinimumLength = 0, ErrorMessage = "Ký hiệu thông tin, nội dung không được quá 30 kí tự")]
        public string InforSign { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Từ khóa_Vấn đề chính, nội dung không được quá 100 kí tự")]
        public string KeywordIssue { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Từ khóa_Địa danh, nội dung không được quá 100 kí tự")]
        public string KeywordPlace { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Từ khóa_Sự kiện, nội dung không được quá 100 kí tự")]
        public string KeywordEvent { get; set; }
        [StringLength(2000, MinimumLength = 0, ErrorMessage = "Chú giải, nội dung không được quá 2000 kí tự")]
        public string Description { get; set; }
        [Range(1, 4, ErrorMessage = "Trạng thái của hồ sơ, chỉ được nhập 1-Chờ tiếp nhận, 2-Khả dụng, 3-Từ chối hoặc 4-Đã xóa")]
        public int FileStatus { get; set; }
        [Range(1, 2, ErrorMessage = "Tình trạng vật lý, chỉ được nhập 1-Bình thường hoặc 2-Hư hỏng")]
        public int Format { get; set; }

        public List<string> Languages { get; set; }

        [Range(0, 11, ErrorMessage = "Kỳ họp, chỉ được nhập không quá 11")]
        public int CongressMeeting { get; set; }
        [Range(0, 54, ErrorMessage = "Phiên họp, chỉ được nhập không quá 54")]
        public int Meeting { get; set; }
        [Range(1, 5, ErrorMessage = "Độ mật của hồ sơ, chỉ được nhập 1-Thường, 2-Tài liệu thu hồi, 3-Mật, 4-Tối mật hoặc 5-Tuyệt mật")]
        public int SecurityLevel { get; set; }

        public int StorageLocationID { get; set; }
        public int? FileTypeInGroup { get; set; }

        ///Chỗ này có thể phải sửa lại
        public List<DocumentSyncInput> Documents { get; set; }
        public List<PhotoSyncInput> Photos { get; set; }
        public List<FilmSyncInput> Films { get; set; }
    }

    public class FileSearch
    {
        public string TextSearch { get; set; }
        /// <summary>
        /// Giá
        /// </summary>
        public int Racking { get; set; }
        /// <summary>
        /// Khoang
        /// </summary>
        public int Compartment { get; set; }
        /// <summary>
        /// Hàng
        /// </summary>
        public int FileRowNumber { get; set; }
        /// <summary>
        /// Hộp số
        /// </summary>
        public string Gear { get; set; }
        public int NationalAssembly { get; set; }
        public int CongressMeeting { get; set; }
        public int Meeting { get; set; }
        /// <summary>
        /// Độ mật
        /// </summary>
        public int SecurityLevel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Chế độ sử dụng
        /// </summary>
        public int Rights { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public int FileStatus { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int FileType { get; set; }

        public List<Guid> ListObjectGuidID { get; set; }
        public FileSearch()
        {
            NationalAssembly = 0;
            CongressMeeting = 0;
            SecurityLevel = 0;
            Meeting = 0;
            Rights = 0;
            FileStatus = 0;
            PageSize = 20;
            CurrentPage = 1;
            FileType = 0;
            DateTime dtDefault = DateTime.Parse("1900-01-01");
            StartDate = EndDate = dtDefault;
        }
    }
    public class FileSearchResult
    {
        [JsonIgnore]
        public long FileID { get; set; }
        public Guid ObjectGuid { get; set; }
        public int Racking { get; set; }
        public string RackingValue { get; set; }
        public int Compartment { get; set; }
        public string CompartmentValue { get; set; }
        public int FileRowNumber { get; set; }
        public string FileRowNumberValue { get; set; }
        public int SecurityLevel { get; set; }
        public string Gear { get; set; }
        public string FileNo { get; set; }
        public string FileCode { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalDoc { get; set; }
        public int Rights { get; set; }
        public string RightsName { get; set; }
        public string Description { get; set; }
        public int FileStatus { get; set; }
        public string FileStatusName { get; set; }
        public int PiecesOfPaper { get; set; }
        public int StorageTimeType { get; set; }
        public long EgovID { get; set; }
    }

    public class CountDocFilmGallery
    {
        public long DocTotal { get; set; }
        public long GalleryTotal { get; set; }
        public long FilmTotal { get; set; }

        public static string GetDocFilmGalleryTotal(long FileID, out CountDocFilmGallery o)
        {
            return DBM.GetOne("usp_FileRef_CountDocFilmGallery", new { FileID }, out o);
        }
    }

    public class FileRejectInput
    {
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Chưa nhập lí do từ chối")]
        public string Content { get; set; }
        public List<Files> ListObjectGuidFileSync { get; set; }

        public static string RejectIds(string ids)
        {
            string msg = DBM.ExecStore("usp_FileSysn_RejectList", new { FileSysnIds = ids });
            return msg;
        }
    }
}