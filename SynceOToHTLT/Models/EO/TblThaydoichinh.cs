using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class TblThaydoichinh
    {
        public decimal Id { get; set; }
        public string? Filename { get; set; }
        public string? Pathname { get; set; }
        public string? Typechange { get; set; }
        public DateTime? Ngay { get; set; }
        public string? Oldfilename { get; set; }
    }
}
