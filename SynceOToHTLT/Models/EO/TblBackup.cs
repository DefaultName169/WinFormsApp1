using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class TblBackup
    {
        public decimal Id { get; set; }
        public DateTime? Ngay { get; set; }
        public string? Typechange { get; set; }
        public string? Filename { get; set; }
        public string? Sourcepath { get; set; }
        public string? Backuppath { get; set; }
        public string? Oldfilename { get; set; }
        public string? Changepath { get; set; }
    }
}
