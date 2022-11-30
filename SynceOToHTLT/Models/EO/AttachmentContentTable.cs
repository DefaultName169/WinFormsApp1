using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class AttachmentContentTable
    {
        public int AttachmentContentId { get; set; }
        public int? AttachmentId { get; set; }
        public byte[]? NoiDung { get; set; }
        public int? Version { get; set; }
        public DateTime? NgayThem { get; set; }
        public string? NguoiThem { get; set; }
        public int? KichThuoc { get; set; }
        public int? Sign { get; set; }
        public string? Signature { get; set; }
        public int? Compress { get; set; }
    }
}
