using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApi_NetCore.DB;
using WebApi_NetCore.Model;
using WebApi_NetCore.Shared;

namespace WebApi_NetCore.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly DemoDbContext _demoDbContext;
        //private readonly CallApi _callApi;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //private readonly ILogger _logger;

        public TestController(DemoDbContext demoDbContext)
        {
            _demoDbContext = demoDbContext;
            
            //_logger = logger;
            //_callApi = callApi;
        }

        [HttpGet]
        public string Get()
        {
            //var _callApi = new CallApi();
            //var a = _callApi.TestDocHinh();
           
            //var _callApi = new CallApi();
            var a = _demoDbContext.GiaoDichChuyenTiens.ToList();
            return "01652992622/Cocaca@1";
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
        [HttpPost]
        public List<ThongTinGiaoDich> TestApi(DangNhapInput input)
        {
            var lst = new List<ThongTinGiaoDich>();
            if (string.IsNullOrWhiteSpace(input.user))
            {
                lst.Add(new ThongTinGiaoDich { TaiKhoan = "Tên đăng nhập không được để trống" });
                return lst;
            }
            if (string.IsNullOrWhiteSpace(input.pass))
            {
                lst.Add(new ThongTinGiaoDich { TaiKhoan = "Mật khẩu không được để trống" });
                return lst;
            }
            //var token = _callApi.GetToken("",)
            var magd = _demoDbContext.GiaoDichChuyenTiens.Where(o => o.GiaoDich_PhuongThuc == "ACB").Select(o => o.GiaoDich_Ma).ToList();
            var ngaybatdau = _demoDbContext.GiaoDichChuyenTiens.OrderByDescending(o => o.GiaoDich_ThoiGian).Select(o=>o.GiaoDich_ThoiGian).FirstOrDefault();
            var _callApi = new CallApi();
            var res = _callApi.B1(input.user, input.pass,ngaybatdau);

            res = res.Where(o => o.TenDangNhap.ToLower().Contains("nap") && !magd.Contains(o.TaiKhoan)).ToList();
            foreach(var item in res)
            {
                _demoDbContext.GiaoDichChuyenTiens.Add(new DB.Table.GiaoDichChuyenTien
                {
                    GiaoDich_SoTien = item.SoTien.Replace(".",","),
                    GiaoDich_PhuongThuc = "ACB",
                    GiaoDich_Ma = item.TaiKhoan,
                    GiaoDich_ThoiGian = Convert.ToDateTime(item.Ngay.Split("-")[0] + "/" + item.Ngay.Split("-")[1] + "/20" + item.Ngay.Split("-")[2]),
                    TenDangNhap = item.TenDangNhap.Split(" ")[1].Trim(),
                    CreationTime= DateTime.Now
                });
                _demoDbContext.SaveChanges();
            }
           


            return res;
        }
        [Route("/TestjDO")]
        public string TestDocHinh()
        {
            string a = "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";
            var _callApi = new CallApi();
            var test = _callApi.TestDocHinh(a);
            return test;
        }
    }
}
