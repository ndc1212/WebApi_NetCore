using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_NetCore.Model
{
    public class ACBInput
    {
        public string dse_sessionId { get; set; }
        public string dse_applicationId { get; set; }
        public string dse_pageId { get; set; }
        public string dse_operationName { get; set; }
        public string dse_errorPage { get; set; }
        public string dse_processorState { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string glbLogedIn { get; set; }
        public string SecurityCode { get; set; }
    }
    public class ChiTietGiaoDich
    {
        public string So_P1 { get; set; }
        public string NgayLap_P1 { get; set; }
        public string TrangThai_P1 { get; set; }
        public string TenDonViDaTraTien_P1 { get; set; }
        public string TaiKhoanSo_P1 { get; set; }
        public string TaiNganHang_P1 { get; set; }
        public string TenDonViNhanTien_P2 { get; set; }
        public string SoCMND_P2 { get; set; }
        public string NgayCap_P2 { get; set; }
        public string NoiCap_P2 { get; set; }
        public string TaiKhoanSo_P2  { get; set; }
        public string TaiNganHang_P2  { get; set; }
        public string SoTien_P3 { get; set; }
        public string SoTienBangChu_P3 { get; set; }
        public string NoiDungChuyenKhoan_P3 { get; set; }
    }
}
