using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class UserDepTable
    {
        public string Email { get; set; } = null!;
        public string DepId { get; set; } = null!;
        public int? KeyUser { get; set; }
        public int? ChucVuId { get; set; }
        public string? TenChucVu { get; set; }
        public int? ChucNang { get; set; }
    }
}
