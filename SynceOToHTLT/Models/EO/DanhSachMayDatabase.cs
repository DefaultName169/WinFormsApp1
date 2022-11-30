using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class DanhSachMayDatabase
    {
        public int Id { get; set; }
        public string? Server { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool? Used { get; set; }
    }
}
