using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class YKienTable
    {
        public int Id { get; set; }
        public int? CongVanId { get; set; }
        public string? NhanTuClient { get; set; }
        public string? YKienXuLy { get; set; }
        public DateTime? ThoiGian { get; set; }
        public string? NguoiGui { get; set; }
        public int? LoaiYKien { get; set; }
        public string? ListInform { get; set; }
        public string? DanhSachCoQuan { get; set; }
        public string? NoiNhan { get; set; }
        public string? YKien { get; set; }
        public int? Compress { get; set; }
    }
}
