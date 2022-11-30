using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class UserDeadlineTable
    {
        public string Username { get; set; } = null!;
        public string? Deadline { get; set; }
        public int Inout { get; set; }
    }
}
