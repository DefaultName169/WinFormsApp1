using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class VoteResultTable
    {
        public int VoteId { get; set; }
        public string? YKienRieng { get; set; }
        public string NguoiVote { get; set; } = null!;
        public int? CacLuaChon { get; set; }
        public int? DaVote { get; set; }
        public int? DaNhan { get; set; }
        public DateTime? GioKetThuc { get; set; }
        public DateTime? GioBatDau { get; set; }
        public int? DaGui { get; set; }
    }
}
