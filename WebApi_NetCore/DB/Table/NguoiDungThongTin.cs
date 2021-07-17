using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_NetCore.DB.Table
{
    [Table("mmo_nguoidung_thongtin")]
    public class NguoiDungThongTin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int? NguoiDung_Loai { get; set; }
        public string NguoiDung_MaTaiKhoan { get; set; }
    }
}
