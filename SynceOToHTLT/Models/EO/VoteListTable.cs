using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class VoteListTable
    {
        public int VoteId { get; set; }
        public int? VoteDailyId { get; set; }
        public DateTime? GioBatDau { get; set; }
        public DateTime? GioKetThuc { get; set; }
        public int? SoLuaChon { get; set; }
        public string? CacLuaChon { get; set; }
        public int? Daily { get; set; }
        public string? TieuDe { get; set; }
        public int? LoaiLuaChon { get; set; }
        public int? LonHon { get; set; }
        public int? NhoHon { get; set; }
        public int? CongKhai { get; set; }
        public int? XemNgayKetQua { get; set; }
        public string? NguoiKhoiTao { get; set; }
        public string? PhongVote { get; set; }
        public string? PhongXem { get; set; }
        public string? NguoiVote { get; set; }
        public string? NguoiXem { get; set; }
        public DateTime? NgayTao { get; set; }
        public int? ChonTuyY { get; set; }
        public int? ChoPhepYKienKhac { get; set; }
    }
}
