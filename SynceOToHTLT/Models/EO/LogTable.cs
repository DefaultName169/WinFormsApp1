using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class LogTable
    {
        public long HistoryId { get; set; }
        public DateTime NgayThang { get; set; }
        public string? HanhDong { get; set; }
        public string? NguoiThucHien { get; set; }
        public int? HanhDongId { get; set; }
        public string? ThamSo { get; set; }
        public string? Ip { get; set; }
        public int? PhienLamViec { get; set; }
        public string? MacAddress { get; set; }
        public string? ComputerName { get; set; }
    }
}
