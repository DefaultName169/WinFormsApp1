using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class EdxmlMessage
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? CodeNumber { get; set; }
        public string? CodeNotation { get; set; }
        public string? FromOrganId { get; set; }
        public string? ToOrganIds { get; set; }
        public int? CongVanId { get; set; }
        public string? Type { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? LienThongId { get; set; }
        public string? EmailVanThu { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool? ReceivedStatus { get; set; }
        public bool? CompletedStatus { get; set; }
        public string? Via { get; set; }
    }
}
