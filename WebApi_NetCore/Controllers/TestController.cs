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
            var ngaybatdau = _demoDbContext.GiaoDichChuyenTiens.OrderByDescending(o => o.GiaoDich_ThoiGian).Select(o=>o.GiaoDich_ThoiGian).FirstOrDefault();
            var _callApi = new CallApi();
            var res = _callApi.B1(input.user, input.pass,ngaybatdau);
            res = res.Where(o => o.TenDangNhap.Contains("nap")).ToList();
            foreach(var item in res)
            {
                _demoDbContext.GiaoDichChuyenTiens.Add(new DB.Table.GiaoDichChuyenTien
                {
                    GiaoDich_SoTien = item.SoTien.Replace(".",","),
                    GiaoDich_PhuongThuc = "CKNH",
                    GiaoDich_TaiKhoan = item.TaiKhoan,
                    GiaoDich_ThoiGian = Convert.ToDateTime(item.Ngay.Replace(item.Ngay.Split("-")[0]+"-"+ item.Ngay.Split("-")[1], item.Ngay.Split("-")[1] + "-" + item.Ngay.Split("-")[0])),
                    TenDangNhap = item.TenDangNhap.Replace("nap","").Trim(),
                    CreationTime= DateTime.Now
                });
                _demoDbContext.SaveChanges();
            }
           


            return res;
        }
        //[HttpGet]
        //public string TestDocHinh()
        //{
        //    var _callApi = new CallApi();
        //    return _callApi.TestDocHinh();
        //}
    }
}
