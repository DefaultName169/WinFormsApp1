using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class CauHoiTable
    {
        public int CauHoiId { get; set; }
        public int? CongVanId { get; set; }
        public byte[]? NoiDungCauHoi { get; set; }
        public string? NguoiHoi { get; set; }
        public int? TrangThai { get; set; }
        public DateTime? NgayHoi { get; set; }
        public string? NguoiDuocHoi { get; set; }
    }
}
