using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class AttachmentTable
    {
        public int AttachmentId { get; set; }
        public string? TenAttachment { get; set; }
        public int? Type { get; set; }
        public int? CongVanId { get; set; }
        public int? Version { get; set; }
        public int? TrangThai { get; set; }
    }
}
