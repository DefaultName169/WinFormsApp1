using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class SmsTable
    {
        public int IdSms { get; set; }
        public string? SSender { get; set; }
        public string? SReceiver { get; set; }
        public string? SMobileNum { get; set; }
        public int? Time { get; set; }
        public string? SMessage { get; set; }
        public int? IStatus { get; set; }
        public string? SOwner { get; set; }
        public int? BDelay { get; set; }
        public int? BReport { get; set; }
    }
}
