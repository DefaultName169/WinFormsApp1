using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class CauTraLoiTable
    {
        public int CauTraLoiId { get; set; }
        public string? NguoiTraLoi { get; set; }
        public DateTime? NgayTraLoi { get; set; }
        public byte[]? NoiDungTraLoi { get; set; }
        public int? CauHoiId { get; set; }
    }
}
