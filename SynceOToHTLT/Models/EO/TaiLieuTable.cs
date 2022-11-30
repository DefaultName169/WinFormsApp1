using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class TaiLieuTable
    {
        public int TaiLieuId { get; set; }
        public string? TenTaiLieu { get; set; }
        public string? NguoiThem { get; set; }
        public DateTime? NgayThem { get; set; }
        public string? GhiChu { get; set; }
        public int? HoSoId { get; set; }
        public byte[]? NoiDungTaiLieu { get; set; }
        public int? TaiLieuVersion { get; set; }
    }
}
