using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class LinkTable
    {
        public int Id { get; set; }
        public string? Displayname { get; set; }
        public string? Command { get; set; }
        public string? Parameter { get; set; }
        public int? Repeattype { get; set; }
        public int? Ngay { get; set; }
        public int? Thang { get; set; }
        public DateTime? Gio { get; set; }
        public int? Thu { get; set; }
        public int? GioHieuLuc { get; set; }
        public string? PhongBanSuDung { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? Login { get; set; }
    }
}
