using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class SendFileTable
    {
        public string NguoiGui { get; set; } = null!;
        public string? ListNguoiNhan { get; set; }
        public string TenFile { get; set; } = null!;
        public string? MoTa { get; set; }
        public int? SoNguoiChuaNhan { get; set; }
        public string Id { get; set; } = null!;
    }
}
