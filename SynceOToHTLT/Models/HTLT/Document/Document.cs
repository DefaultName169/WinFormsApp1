using BSS;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SynceOToHTLT.Models.HTLT.Document
{
    public class Document : Audit
    {
        public long DocumentID { get; set; }
        public Guid ObjectGuid { get; set; }
        public string DocCode { get; set; }
        public string FileNotation { get; set; }
        public string CodeNumber { get; set; }
        public int TypeName { get; set; }
        [JsonIgnore]
        public string TypeNameValue { get; set; }
        public int OrganName { get; set; }
        [JsonIgnore]
        public string OrganNameValue { get; set; }
        public string AgencyCreate { get; set; }
        public string Field { get; set; }
        public DateTime IssuedDate { get; set; }
        public string InforSign { get; set; }
        public int PiecesOfPaper { get; set; }
        public int PageAmount { get; set; }
        public List<string> Languages { get; set; }
        [JsonIgnore]
        public string Language { get; set; }
        [JsonIgnore]
        public string LanguagesValue { get; set; }
        public int SecurityLevel { get; set; }
        [JsonIgnore]
        public string SecurityLevelValue { get; set; }
        public string Autograph { get; set; }
        public string Subject { get; set; }
        public string KeywordIssue { get; set; }
        public string KeywordPlace { get; set; }
        public string KeywordEvent { get; set; }
        public string Description { get; set; }
        public int DocStatus { get; set; }
        public int Format { get; set; }
        [JsonIgnore]
        public string FormatValue { get; set; }
        public int Mode { get; set; }
        [JsonIgnore]
        public string ModeValue { get; set; }
        public int ConfidenceLevel { get; set; }
        [JsonIgnore]
        public string ConfidenceLevelValue { get; set; }
        public int StorageTimeType { get; set; }
        [JsonIgnore]
        public string StorageTimeTypeValue { get; set; }
        public int? Maintenance { get; set; }
        public int CongressMeeting { get; set; }
        public int Meeting { get; set; }
        public int NationalAssembly { get; set; }
        [JsonIgnore]
        public string NationalAssemblyValue { get; set; }
        [JsonIgnore]
        public long FileID { get; set; }
        public long OrdinalNumber { get; set; }
        public List<string> DocumentPaths { get; set; }
        [JsonIgnore]
        public string DocumentPath { get; set; }
        [JsonIgnore]
        public int FileSecurityLevel { get; set; }
        public List<DocumentReferen> ListReferences { get; set; }
        public long? RefID { get; set; }
        public long EgovID { get; set; }
        public string InsertOrUpdate(DBM dbm, out Document o, int UserID)
        {
            o = null;
            string msg = dbm.SetStoreNameAndParams("usp_Document_InsertOrUpdate", new
            {
                DocumentID,
                DocCode,
                FileNotation,
                CodeNumber,
                TypeName,
                OrganName,
                AgencyCreate,
                Field,
                IssuedDate,
                InforSign,
                PiecesOfPaper,
                PageAmount,
                Language,
                SecurityLevel,
                Autograph,
                Subject,
                KeywordIssue,
                KeywordPlace,
                KeywordEvent,
                Description,
                Format,
                Mode,
                ConfidenceLevel,
                StorageTimeType,
                Maintenance,
                CongressMeeting,
                Meeting,
                NationalAssembly,
                CreateUser = UserID,
                UpdateUser = UserID,
                FileID,
                DocumentPath,
                FileSecurityLevel,
                OrdinalNumber,
                ListReferences = ListReferences?.Select(s=> new
                {
                    ReferenID = s.RefID,
                    ReferenType = s.RefType
                })?.ToList().ToDataTable()
            });
            if (msg.Length > 0) return msg;

            return dbm.GetOne(out o);
        }

        public static string GetOne(Guid ObjectGuid, out Document o)
        {
            string msg = DBM.GetOne("usp_Document_GetOneByObjectGuid", new { ObjectGuid }, out o);
            if (o == null) return ("Tài liệu không tồn tại").ToMessageForUser();
            return msg;

        }

        /// <summary>
        /// Lây danh sách tài liệu liên quan theo documentID
        /// </summary>
        /// <param name="ObjectGuid"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetListReferenceByDocumentID(long DocId, out List<DocumentReferen> o)
        {
            return DBM.GetList("usp_Document_GetListReference", new { DocId }, out o);

        }

        /// <summary>
        /// Lây danh sách tài liệu liên quan theo refID
        /// </summary>
        /// <param name="ObjectGuid"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetListReferenceByRefID(long refId, out List<DocumentReferen> o)
        {
            return DBM.GetList("usp_Document_GetListReference_By_RefID", new { RefId = refId }, out o);

        }

        public static string GetMaxID(long FileID, out long MaxID)
        {
            return DBM.ExecStore("usp_Document_GetMaxID", new { FileID }, out MaxID);
        }

        public static string CheckExistOrdinalNumber(long FileID, long OrdinalNumber, out int Count)
        {
            return DBM.ExecStore("usp_Document_CheckExistOrdinalNumber", new { FileID, OrdinalNumber }, out Count);
        }

        /// <summary>
        /// Check the Document exists or not by FileNotation
        /// </summary>
        /// <param name="FileNotation"></param>
        /// <param name="DocumentExist"></param>
        /// <returns></returns>
        public static string CheckDocumentExistByFileNotation(string FileNotation, int OrganName, DateTime IssuedDate,long FileID, out Document DocumentExist)
        {
            return DBM.GetOne("usp_Document_GetIDByFileNotation", new { FileNotation, OrganName, IssuedDate, FileID }, out DocumentExist);
        }

        /// <summary>
        /// Get doccument list by FileNotation
        /// </summary>
        /// <param name="FileNotation"></param>
        /// <param name="DocumentExist"></param>
        /// <returns></returns>
        public static string GetDocumentListByFileNotation(string FileNotation, out List<Document> DocumentList)
        {
            return DBM.GetList("usp_Document_GetDocListByFileNotation", new { FileNotation }, out DocumentList);
        }

        public static string Delete(long DocumentID)
        {
            return DBM.ExecStore("usp_Document_UpdateIsDelete", new { DocumentID });
        }

        /// <summary>
        /// Cập nhật trạng thái đồng bộ
        /// </summary>
        /// <param name="ObjectGuid"></param>
        /// <param name="Status">1: chờ tiếp nhận, 2: khả dụng, 3:Từ chối, 4: đã xóa</param>
        /// <returns></returns>
        public static string UpdateStatus(long DocumentID, int Status)
        {
            return DBM.ExecStore("usp_Document_UpdateStatus", new { DocumentID, Status });
        }
        public static string DeleteById(long DocumentID)
        {
            return DBM.ExecStore("usp_Document_DeleteByID", new { DocumentID });
        }

        public static string GetListSearch(DocumentSearch ms, out List<DocumentSearchResult> lt, out int total)
        {
            lt = null;
            total = 0;
            string msg = GetListPaging(ms, out lt, out total);
            if (msg.Length > 0) return msg;

            return "";

        }
        public static string GetListPaging(DocumentSearch ms, out List<DocumentSearchResult> lt, out int total)
        {
            lt = null; total = 0;

            string msg = "";

            QueryStringBuilder q = new QueryStringBuilder();
            if(ms.FileStatus == 1)
            {
                msg = q.InitWithStore("usp_DocumentSync_Select_Search");
            }
            else
            {
                msg = q.InitWithStore("usp_Document_Select_Search");
            }
            
            if (msg.Length > 0) return msg;

            msg = GetListPaging_Parameter(false, ms, out dynamic para);
            if (msg.Length > 0) return msg;

            msg = Paging.Exec(q.Sql, "d.DocumentID", para, out lt, out total);
            if (msg.Length > 0) return msg;

            return msg;
        }
        private static string GetListPaging_Parameter(bool IsReport, DocumentSearch ms, out dynamic o)
        {
            o = null;

            o = new
            {
                ms.TextSearch,
                ms.FileID,
                ms.NationalAssembly,
                ms.CongressMeeting,
                ms.Meeting,
                ms.TypeName,
                ms.SecurityLevel,
                ms.Mode,
                ms.IssuedDateFrom,
                ms.IssuedDateTo,
                ms.DocStatus,
                ms.PageSize,
                ms.CurrentPage,
                DefaultDate = "1900-01-01"
            };

            return "";
        }

        public static string GetOneByGuid(Guid ObjectGuid, out long DocumentID)
        {
            DocumentID = 0;

            Document f;
            string msg = DBM.GetOne("usp_Document_GetOneByObjectGuid", new { ObjectGuid }, out f);
            if (msg.Length > 0) return msg;

            if (f == null) return ("Tài liệu không tồn tại").ToMessageForUser();
            DocumentID = f.DocumentID;

            return msg;
        }

        public static string Receive(int UserID, long FileID, string ObjectGuidList)
        {
            return DBM.ExecStore("usp_Document_Receive", new { UserID = UserID, FileID = FileID, ObjectGuidList = ObjectGuidList });
        }

        public static string GetListByListFileGuidID(List<Guid> guids, int docStatus, out List<DocumentSearchResult> ls)
        {
            return DBM.GetList("usp_Document_GetListByListFileGuidID", new { 
                Guids = guids.Select(s =>
            new {
                ObjGuid = s,
                DefaultColumn = 0
            }).ToList().ToDataTable(),
                DocStatus = docStatus
            }, out ls);
        }

        public static string GetListByListGuidID(List<Guid> listObjectGuidID, int docStatus, out List<DocumentSearchResult> lt)
        {
            return DBM.GetList("usp_Document_GetListByListGuidID", new
            {
                Guids = listObjectGuidID.Select(s =>
            new {
                ObjGuid = s,
                DefaultColumn = 0
            }).ToList().ToDataTable(),
                DocStatus = docStatus
            }, out lt);
        }
    }

    public class DocumentInput : Audit
    {
        [JsonIgnore]
        public long DocumentID { get; set; }
        public Guid ObjectGuid { get; set; }

        public string DocCode { get; set; }
        //[Required(ErrorMessage = "Hãy nhập Số văn bản")]
        [StringLength(11, MinimumLength = 0, ErrorMessage = "Số văn bản, nội dung không được quá 11 kí tự")]
        public string CodeNumber { get; set; }
        public int TypeName { get; set; }
        public int OrganName { get; set; }
        //[Required(ErrorMessage = "Hãy nhập Đơn vị soạn thảo")]
        //[AssertThat("CheckAgencyCreate == true", ErrorMessage = "Đơn vị soạn thảo không tồn tại")]
        [StringLength(200, MinimumLength = 0, ErrorMessage = "Đơn vị soạn thảo, nội dung không được quá 200 kí tự")]
        public string AgencyCreate { get; set; }
        //[AssertThat("CheckFileNotation == true", ErrorMessage = "Số và ký hiệu không đúng định dạng")]
        //[Required(ErrorMessage = "Hãy nhập Số và ký hiệu")]
        public string FileNotation { get; set; }
        [StringLength(500, MinimumLength = 0, ErrorMessage = "Lĩnh vực, nội dung không được quá 500 kí tự")]
        public string Field { get; set; }
        public DateTime IssuedDate { get; set; }
        [StringLength(30, MinimumLength = 0, ErrorMessage = "Ký hiệu thông tin, nội dung không được quá 30 kí tự")]
        public string InforSign { get; set; }
        [Range(0, 9999999999, ErrorMessage = "Số tờ, hãy nhập không quá 10 kí tự số")]
        public int PiecesOfPaper { get; set; }
        //[Range(0, 9999999999, ErrorMessage = "Số trang, hãy nhập không quá 10 kí tự số")]
        public int PageAmount { get; set; }
        public List<string> Languages { get; set; }
        [Range(1, 5, ErrorMessage = "Độ mật của tài liệu giấy, chỉ được nhập 1-Thường, 2-Tài liệu thu hồi, 3-Mật, 4-Tối mật hoặc 5-Tuyệt mật")]
        public int SecurityLevel { get; set; }
        [StringLength(2000, MinimumLength = 0, ErrorMessage = "Từ khóa_Vấn đề chính, nội dung không được quá 100 kí tự")]
        public string Autograph { get; set; }
        public string Subject { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Từ khóa_Vấn đề chính, nội dung không được quá 100 kí tự")]
        public string KeywordIssue { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Từ khóa_Địa danh, nội dung không được quá 100 kí tự")]
        public string KeywordPlace { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Từ khóa_Sự kiện, nội dung không được quá 100 kí tự")]
        public string KeywordEvent { get; set; }
        [StringLength(500, MinimumLength = 0, ErrorMessage = "Ghi chú, nội dung không được quá 500 kí tự")]
        public string Description { get; set; }
        [Range(1, 4, ErrorMessage = "Trạng thái của tài liệu giấy, chỉ được nhập 1-Chờ tiếp nhận, 2-Khả dụng, 3-Đang khai thác hoặc 4-Đã xóa")]
        public int DocStatus { get; set; }
        [Range(1, 2, ErrorMessage = "Tình trạng vật lý, chỉ được nhập 1-Bình thường hoặc 2-Hư hỏng")]
        public int Format { get; set; }
        [Range(1, 2, ErrorMessage = "Chế độ sử dụng, chỉ được nhập 1-Hạn chế hoặc 2-Không hạn chế")]
        public int Mode { get; set; }
        public int ConfidenceLevel { get; set; }
        [Range(1, 2, ErrorMessage = "Loại thời hạn bảo quản, chỉ được nhập 1-Vĩnh viễn hoặc 2-Có thời hạn")]
        public int StorageTimeType { get; set; }
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
        public Boolean IsExist { get; set; }
        [Range(0, 11, ErrorMessage = "Kỳ họp, chỉ được nhập không quá 11")]
        public int CongressMeeting { get; set; }
        public int FileCongressMeeting { get; set; }
        [Range(0, 54, ErrorMessage = "Phiên họp, chỉ được nhập không quá 54")]
        
        public int Meeting { get; set; }
        public int FileMeeting { get; set; }
        
        public int NationalAssembly { get; set; }
        public int FileNationalAssembly { get; set; }
        public Guid FileObjectGuid { get; set; }
        [JsonIgnore]
        public long FileID { get; set; }
        public List<string> DocumentPaths { get; set; }
        [JsonIgnore]
        public string DocumentPath { get; set; }
        public int FileSecurityLevel { get; set; }
        public long OrdinalNumber { get; set; }
        /// <summary>
        /// Danh sách tài liệu liên quan
        /// </summary>
        public List<DocumentReferen> ListReferences { get; set; }
    }

    public class DocumentSearch
    {
        public string TextSearch { get; set; }
        [JsonIgnore]
        public long FileID { get; set; }
        public Guid ObjectGuidFile { get; set; }
        public int NationalAssembly { get; set; }
        public int CongressMeeting { get; set; }
        public int Meeting { get; set; }
        public string TypeName { get; set; }
        public int SecurityLevel { get; set; }
        public int Mode { get; set; }
        public DateTime IssuedDateFrom { get; set; }
        public DateTime IssuedDateTo { get; set; }
        public int DocStatus { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public List<Guid> ListObjectGuidID { get; set; }
        public int FileStatus { get; set; }
        public DocumentSearch()
        {
            FileID = NationalAssembly = CongressMeeting = Meeting = Mode = DocStatus = 0;
            PageSize = 10;
            CurrentPage = 1;
            DateTime dtDefault = DateTime.Parse("1900-01-01");
            IssuedDateFrom = IssuedDateTo = dtDefault;

        }
    }
    public class DocumentSearchResult
    {
        [JsonIgnore]
        public long DocumentId { get; set; }
        public Guid ObjectGuid { get; set; }
        public string FileNotation { get; set; }
        public DateTime IssuedDate { get; set; }
        public string Subject { get; set; }
        public int PiecesOfPaper { get; set; }
        public int PageAmount { get; set; }
        public int Mode { get; set; }
        public string ModeName { get; set; }
        public string Description { get; set; }
        public int DocStatus { get; set; }
        public string DocStatusName { get; set; }
        public string AgencyCreate { get; set; }
        public string CodeNumber { get; set; }
        public string Maintenance { get; set; }
        public string MyProperty { get; set; }
        /// <summary>
        /// Mã hồ sơ
        /// </summary>
        public string FileCode { get; set; }
    }

    public class DocumentUploadOutput
    {
        public List<string> DocumentPaths { get; set; }
    }

    public class FileDocumentSyncInput
    {
        public long FileID { get; set; }
        [Required(ErrorMessage = "Hãy nhập thông tin mã hồ sơ đồng bộ")]
        public string FileNo { get; set; }
        public string FileCode { get; set; }
        public string FileNotation { get; set; }
        public List<DocumentSync> ListDocument { get; set; }


        public string InsertOrUpdate()
        {
            return DBM.ExecStore("usp_DocumentSync_InsertOrUpdate", new { FileNo, ListDocumentSync = ListDocument.ToDataTable() });
        }

        public string InsertOrUpdateFromEgov()
        {
           return DBM.ExecStore("usp_DocumentSync_InsertOrUpdate_Egov", new { FileNo, FileCode, FileNotation, ListDocumentSync = ListDocument.ToDataTable() });
            //return DBM.ExecStore("usp_DocumentSync_InsertOrUpdate_Egov_V2", new { FileID, FileSecurityLevel = 1, ListDocumentSync = ListDocument.ToDataTable() });
        }
    }

    public class DocumentReceive
    {
        public Guid FileObjectGuid { get; set; }
        public List<Guid> ListObjectGuidDocSync { get; set; }
    }

    public class DocumentSync
    {
        [Key]
        public string DocCode { get; set; }
        public string FileNotation { get; set; }
        public string Field { get; set; }
        public DateTime IssuedDate { get; set; }
        public int PiecesOfPaper { get; set; }
        public int PageAmount { get; set; }
        public string Language { get; set; }
        public int SecurityLevel { get; set; }
        public string Autograph { get; set; }
        public string Subject { get; set; }
        public string KeywordIssue { get; set; }
        public string KeywordPlace { get; set; }
        public string KeywordEvent { get; set; }
        public string Description { get; set; }
        public int ConfidenceLevel { get; set; }
        public int StorageTimeType { get; set; }
        public int Maintenance { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public DateTime LastUpdate { get; set; }
        public int UpdateUser { get; set; }
        public int NationalAssembly { get; set; }
        public int Mode { get; set; }
        public int Format { get; set; }
        public int CongressMeeting { get; set; }
        public int Meeting { get; set; }
        public int TypeName { get; set; }
        public string InforSign { get; set; }
        public string DocumentPath { get; set; }
        public int OrganName { get; set; }
        public string AgencyCreate { get; set; }
        public string CodeNumber { get; set; }
        public int SyncType { get; set; }
    }

    public class DocumentSyncInput:Audit
    {
        /// <summary>
        /// ID tài liệu giấy bên Egov
        /// </summary>
        [Required]
        [Range(1, long.MaxValue)]
        public long EgovID { get; set; }
        [JsonIgnore]
        public long DocumentID { get; set; }
        public Guid ObjectGuid { get; set; }
        public string DocCode { get; set; }
        public string FileNotation { get; set; }
        public string Field { get; set; }
        public DateTime IssuedDate { get; set; }
        public int PiecesOfPaper { get; set; }
        public int PageAmount { get; set; }
        public List<string> Languages { get; set; }
        [JsonIgnore]
        public string Language { get; set; }
        public int SecurityLevel { get; set; }
        public string Autograph { get; set; }
        public string Subject { get; set; }
        public string KeywordIssue { get; set; }
        public string KeywordPlace { get; set; }
        public string KeywordEvent { get; set; }
        public string Description { get; set; }
        public int ConfidenceLevel { get; set; }
        public int StorageTimeType { get; set; }
        public int Maintenance { get; set; }
        public int NationalAssembly { get; set; }
        public int Mode { get; set; }
        public int DocStatus { get; set; }
        public int Format { get; set; }
        public int CongressMeeting { get; set; }
        public int Meeting { get; set; }
        public int TypeName { get; set; }
        public string InforSign { get; set; }
        public string DocumentPath { get; set; }
        public int OrganName { get; set; }
        public string AgencyCreate { get; set; }
        public string CodeNumber { get; set; }
        [JsonIgnore]
        public int SyncType { get; set; }

        public static string GetOne(Guid ObjectGuid, out DocumentSyncInput o)
        {
            string msg = DBM.GetOne("usp_DocSync_GetOneByObjectGuid", new { ObjectGuid }, out o);
            if (o == null) return ("Tài liệu đồng bộ hiện không tồn tại").ToMessageForUser();
            return msg;
        }
        public static string UpdateStatusIds(string ids)
        {
            string msg = DBM.ExecStore("usp_DocumentSysn_RejectList", new { DocumentSysnIds=ids });
            return msg;
        }
    }
    public class DocumentRejectInput
    {
        public Guid FileObjectGuid { get; set; }
        [StringLength(1000,MinimumLength =1,ErrorMessage ="Bạn chưa nhập lí do từ chối")]
        public string Content { get; set; }
        public List<Document> ListObjectGuidDocSync { get; set; }
    }

    public class DocumentReferen
    {
        public long OrganID { get; set; }
        public long RefID { get; set; }
        public int RefType { get; set; }
        public string DocSubject { get; set; }
        public bool isChecked { get { return true; } }
        public string Notation { get; set; }
        public bool IsHasChildren { get; set; }
        public bool IsChildren { get; set; }
        public Guid DocGuidID { get; set; }
        public Guid FileGuidID { get; set; }
    }
}