using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class AllowedToRunTable
    {
        public int Id { get; set; }
        public string MacAddress { get; set; } = null!;
        public string ComputerName { get; set; } = null!;
        public int? AllowToRun { get; set; }
        public string Email { get; set; } = null!;
        public string ThoiGianDangNhap { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
    }
}
