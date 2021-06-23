using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_NetCore.DB.Table
{
    [Table("mmo_giaodich_chuyentien")]
    public class GiaoDichChuyenTien
    {
        public int Id { get; set; }
        public string GiaoDich_Ma { get; set; }
        public string GiaoDich_Ten { get; set; }
        public string GiaoDich_PhuongThuc { get; set; }
        public string GiaoDich_TaiKhoan { get; set; }
        public string GiaoDich_SoTien { get; set; }
        public DateTime? GiaoDich_ThoiGian { get; set; }
        public string TenDangNhap { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
