using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class UyQuyenTable
    {
        public int UyQuyenId { get; set; }
        public string NguoiUyQuyen { get; set; } = null!;
        public string NguoiDuocUyQuyen { get; set; } = null!;
        public DateTime NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? GhiChu { get; set; }
        public int HieuLuc { get; set; }
        public int LoaiCongVanId { get; set; }
    }
}
