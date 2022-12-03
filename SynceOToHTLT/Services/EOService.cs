using BSS;
using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualBasic.ApplicationServices;
using SynceOToHTLT.Common;
using SynceOToHTLT.DataAccess.Common;
using SynceOToHTLT.Models.HTLT.Document;
using SynceOToHTLT.Models.HTLT.File;
using SynceOToHTLT.Models.HTLT.Film;
using SynceOToHTLT.Models.HTLT.Photo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SynceOToHTLT.Services
{
    public class EOService
    {
        public readonly DbContext _dbContext;
        public EOService()
        {
            _dbContext = new DbContext(Program.AppSettings.ConnectionSetting.EOConnectionString);
        }

        public IEnumerator<dynamic> GetListCongVan(int count)
        {
            List<DocumentSyncInput> listdocument = new List<DocumentSyncInput>();
            DateTime? date = new DateTime();
            var results = _dbContext.GetSQLServer<dynamic>("select top "+ count.ToString() +"* from cong_van_table");
            int i = 0;
            foreach (var result in results)
            {
                DocumentSyncInput document = new DocumentSyncInput() {
                    EgovID = 7,
                    DocCode = "Doc " + i.ToString(),
                    FileNotation = "string",
                    Field = "string",
                    IssuedDate = result.ngay_cong_van,
                    PiecesOfPaper = 0,
                    PageAmount = result.so_trang,
                    Language = "string",
                    SecurityLevel = 0,
                    Autograph = "string",
                    Subject = "Doc 7",
                    KeywordIssue = "string",
                    KeywordPlace = "string",
                    KeywordEvent = "string",
                    Description = "string",
                    ConfidenceLevel = 0,
                    StorageTimeType = 0,
                    Maintenance = 0,
                    CreateDate = result.ngay_cong_van,
                    CreateUser = 0,
                    LastUpdate = (result.last_update != null) ? result.last_update : DateTime.Now,
                    UpdateUser = 0,
                    NationalAssembly = 0,
                    Mode = 0,
                    Format = 0,
                    CongressMeeting = 0,
                    Meeting = 0,
                    TypeName = 0,
                    InforSign = "string",
                    DocumentPath = "string",
                    OrganName = 0,
                    AgencyCreate = "string",
                    CodeNumber = "string",
                    SyncType = 0,
                };
                i++;
                listdocument.Add(document);
                yield return i;
            }

            Files newfile = new Files()
            {
                EgovID = 7,
                Title = "Title 7",
                GroupFile = 0,
                FileNo = "007",
                FileNotation = "01",
                FileCatalog = 0,
                Identifier = "000.00.00.C01",
                FileCode = "0007",
                PiecesOfPaper = 0,
                PageNumber = 0,
                TotalDoc = 0,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2022, 06, 30, 04, 50, 42),
                Rights = 1,
                NationalAssemblys = new List<string>() { "1", "2" },
                StorageTimeType = 1,
                Maintenance = 0,
                PersonallyFiled = "string",
                DeliveryDate = new DateTime(2022, 06, 30, 04, 50, 41),
                FontName = "01",
                Gear = "01",
                Racking = 0,
                Compartment = 0,
                FileRowNumber = 0,
                InforSign = "string",
                KeywordIssue = "string",
                KeywordPlace = "string",
                KeywordEvent = "string",
                Description = "string",
                FileStatus = 1,
                Format = 1,
                Languages = new List<string>() { "TV", "TTQQT" },
                CongressMeeting = 0,
                Meeting = 0,
                SecurityLevel = 1,
                StorageLocationID = 0,
                FileTypeInGroup = 0,
                Documents = listdocument,
                Photos = new List<PhotoSyncInput>(){ new PhotoSyncInput {
                     EgovID= 7,
                     EventName= "Photo 7",
                     ImageTitle= "b",
                     ArchivesNumber= "c",
                     PhotoGearNo= 0,
                     PhotoPocketNo= 0,
                     PhotoNo= 0,
                     FilmGearNo= 0,
                     FilmPocketNo= 0,
                     FilmNo= 0,
                     Photographer= "a",
                     PhotoTime= new DateTime(2022, 06, 30, 04, 50, 42),
                     PhotoPlace= "b",
                     FilmSize= "1234",
                     DeliveryUnit= "c",
                     DeliveryDate= null,
                     Description= "s",
                     CreateDate= new DateTime(2022, 06, 30, 04, 50, 42),
                     CreateUser= 0,
                     LastUpdate= null,
                     UpdateUser= 0,
                     Format= 0,
                     SecurityLevel= 0,
                     PhotoStatus= 0,
                     Mode= 0,
                     NationalAssembly= 0,
                     CongressMeeting= 0,
                     Meeting= 0,
                     Colour= 0,
                     StorageTimeType= 0,
                     Maintenance= 0,
                     Form= 0,
                     InforSign= "s",
                     ImagePath= "s"
                    }
                },
                Films = new List<FilmSyncInput>() { new FilmSyncInput(){
                    EgovID= 7,
                    EventName= "Fiml 7",
                    MovieTitle= "string",
                    ArchivesNumber= "string",
                    Recorder= "string",
                    RecordDate= new DateTime(2022, 06, 30, 04, 50, 42),
                    PlayTime= "string",
                    Quality= "string",
                    RecordPlace= "string",
                    Description= "string",
                    CreateDate= new DateTime(2022, 06, 30, 04, 50, 42),
                    CreateUser= 0,
                    LastUpdate= new DateTime(2022, 06, 30, 04, 50, 42),
                    UpdateUser= 0,
                    CongressMeeting= 0,
                    Meeting= 0,
                    Format= 0,
                    SecurityLevel= 0,
                    Mode= 0,
                    NationalAssembly= 0,
                    StorageTimeType= 0,
                    Maintenance= 0,
                    FilmStatus= 0,
                    Language= "string",
                    InforSign= "string",
                    FilmPath= "string"
                    }
                }
            };
            var bdm = new BSS.DBM(new System.Data.SqlClient.SqlConnection(Program.AppSettings.ConnectionSetting.HtltConnectionString));
            var msg2 = newfile.InsertOrUpdateFromEgov(bdm, out Files files, 1);
        }

        public IEnumerable<Files> UpdateFileServer(dynamic file)
        {
            return _dbContext.ExcuteStore<Files>(Program.AppSettings.ConnectionSetting.HtltConnectionString,
                "usp_File_InsertOrUpdate_ToEgov", file);


           // _dbContext.Update(file, "Data Source= DUONGHVB; Initial Catalog= htlt_tinh; Integrated Security=True");
        }
    }


}
