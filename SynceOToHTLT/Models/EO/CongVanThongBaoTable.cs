using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class CongVanThongBaoTable
    {
        public int CongVanThongBaoId { get; set; }
        public int? CongVanId { get; set; }
        public string? NguoiNhan { get; set; }
        public int? XemDayDu { get; set; }
        public int? DaXem { get; set; }
        public int? HoSoId { get; set; }
        public string? NguoiGui { get; set; }
    }
}
