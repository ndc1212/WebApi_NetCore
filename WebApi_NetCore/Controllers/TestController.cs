using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApi_NetCore.Model;
using WebApi_NetCore.Shared;

namespace WebApi_NetCore.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TestController : ControllerBase
    {
        //private readonly ICallApi _callApi;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //private readonly ILogger _logger;

        public TestController()
        {
            //_logger = logger;
            //_callApi = callApi;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //var _callApi = new CallApi();
            //var a = _callApi.TestDocHinh();
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost]
        public List<ChiTietGiaoDich> TestApi(string user, string pass)
        {
            var lst = new List<ChiTietGiaoDich>();
            if (string.IsNullOrWhiteSpace(user))
            {
                lst.Add(new ChiTietGiaoDich { NoiDungChuyenKhoan_P3 = "Tên đăng nhập không được để trống" });
                return lst;
            }
            if (string.IsNullOrWhiteSpace(pass))
            {
                lst.Add(new ChiTietGiaoDich { NoiDungChuyenKhoan_P3 = "Mật khẩu không được để trống" });
                return lst;
            }
            //var token = _callApi.GetToken("",)
            var _callApi = new CallApi();
            var res = _callApi.B1(user,pass);
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
