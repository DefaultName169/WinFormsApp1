using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class EdxmlService
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public int? Port { get; set; }
        public bool? Sync { get; set; }
        public string? Descriptions { get; set; }
        public string? Domain { get; set; }
        public string? Type { get; set; }
    }
}
