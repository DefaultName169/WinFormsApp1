using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class LoaiCongVanTable
    {
        public int LoaiCongVanId { get; set; }
        public string TenLoaiCongVan { get; set; } = null!;
        public int? DuongDiId { get; set; }
        public short CongVanDenDi { get; set; }
        public string? DonVi { get; set; }
        public int? FolderId { get; set; }
        public string? TrichYeuDefault { get; set; }
        public string? LoaiDefault { get; set; }
        public string? Extern { get; set; }
        public int? Order { get; set; }
    }
}
