using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class CongVanTrangThaiTable
    {
        public long CongVanId { get; set; }
        public byte TrangThai { get; set; }
        public byte? DaGui { get; set; }
        public string? NoiDung { get; set; }
    }
}
