using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class CongVanNguoiThamGiaTable
    {
        public long CongVanNguoiThamGiaId { get; set; }
        public string NguoiThamGia { get; set; } = null!;
        public int? CongVanId { get; set; }
        public DateTime? NgayThamGia { get; set; }
    }
}
