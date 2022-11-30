using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class EdxmlAccount
    {
        public int Id { get; set; }
        public string? OrganId { get; set; }
        public string? OrganName { get; set; }
        public string? Email { get; set; }
        public string? EdxmlPassword { get; set; }
        public string? AvailableServiceId { get; set; }
        public bool? Status { get; set; }
        public bool? Income { get; set; }
        public bool? Outcome { get; set; }
        public string? AccessKey { get; set; }
        public string? Cert { get; set; }
        public bool? UpdateProgress { get; set; }
        public string? AppName { get; set; }
        public string? UserName { get; set; }
        public string? UserPass { get; set; }
    }
}
