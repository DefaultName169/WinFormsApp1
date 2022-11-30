using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class UserTable
    {
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
        public string? DuongDanAnh { get; set; }
        public int? GioiHanMay { get; set; }
        public int? SuDungVanBan { get; set; }
        public int? SoNgayLayVanBanVe { get; set; }
        public int? SoVanBanLayVe { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Ext { get; set; }
        public string? PrivatePhone { get; set; }
        public DateTime? NgayTao { get; set; }
        public string? NguoiTao { get; set; }
    }
}
