using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class ContactTable
    {
        public int ContactId { get; set; }
        public string? TenContact { get; set; }
        public string? Email { get; set; }
        public string? Fax { get; set; }
        public string? DiaChi { get; set; }
        public string? GhiChu { get; set; }
        public string? DonVi { get; set; }
        public string? VietTat { get; set; }
        public int? ContactGroupId { get; set; }
    }
}
