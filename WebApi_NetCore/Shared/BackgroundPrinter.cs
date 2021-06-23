using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rebus.Workers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi_NetCore.DB;

namespace WebApi_NetCore.Shared
{
    public class BackgroundPrinter : IHostedService
    {
        private readonly ILogger<BackgroundPrinter> logger;
        private readonly IWorker worker;
        private readonly DemoDbContext _demoDbContext;
        public BackgroundPrinter(ILogger<BackgroundPrinter> logger, DemoDbContext _demoDbContext,
            IWorker worker)
        {
            this.logger = logger;
            this.worker = worker;
            this._demoDbContext = _demoDbContext;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Debug.Print("AAA");
            //var ngaybatdau = _demoDbContext.GiaoDichChuyenTiens.OrderByDescending(o => o.GiaoDich_ThoiGian).Select(o => o.GiaoDich_ThoiGian).FirstOrDefault();
            //var _callApi = new CallApi();
            //var res = _callApi.B1("01652992622", "Cocaca@1", ngaybatdau);
            //res = res.Where(o => o.TenDangNhap.Contains("nap")).ToList();
            //foreach (var item in res)
            //{
            //    _demoDbContext.GiaoDichChuyenTiens.Add(new DB.Table.GiaoDichChuyenTien
            //    {
            //        GiaoDich_SoTien = item.SoTien,
            //        GiaoDich_PhuongThuc = "CKNH",
            //        GiaoDich_TaiKhoan = item.TaiKhoan,
            //        GiaoDich_ThoiGian = Convert.ToDateTime(item.Ngay.Replace(item.Ngay.Split("-")[0] + "-" + item.Ngay.Split("-")[1], item.Ngay.Split("-")[1] + "-" + item.Ngay.Split("-")[0])),
            //        TenDangNhap = item.TenDangNhap.Replace("nap", "").Trim(),
            //        CreationTime = DateTime.Now
            //    });
            //    _demoDbContext.SaveChanges();
            //}
            await worker.DoWork(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
    public interface IWorker
    {
        Task DoWork(CancellationToken cancellationToken);
    }
    public class Worker : IWorker
    {
        private readonly ILogger<Worker> logger;
        private int number = 0;
        private readonly DemoDbContext demoDbContext;
        public Worker(ILogger<Worker> logger, IServiceScopeFactory factory)
        {
            this.logger = logger;
            this.demoDbContext = factory.CreateScope().ServiceProvider.GetRequiredService<DemoDbContext>();
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Interlocked.Increment(ref number);

                var ngaybatdau = demoDbContext.GiaoDichChuyenTiens.OrderByDescending(o => o.GiaoDich_ThoiGian).Select(o => o.GiaoDich_ThoiGian).FirstOrDefault();
                var _callApi = new CallApi();
                var res = _callApi.B1("01652992622", "Cocaca@1", ngaybatdau);
                Debug.Print(res.Count().ToString());
                //res = res.Where(o => o.TenDangNhap.Contains("nap")).ToList();
                foreach (var item in res)
                {
                    
                    demoDbContext.GiaoDichChuyenTiens.Add(new DB.Table.GiaoDichChuyenTien
                    {
                        GiaoDich_SoTien = item.SoTien.Replace(".",","),
                        GiaoDich_PhuongThuc = "CKNH",
                        GiaoDich_TaiKhoan = item.TaiKhoan,
                        GiaoDich_ThoiGian = Convert.ToDateTime(item.Ngay.Replace(item.Ngay.Split("-")[0] + "-" + item.Ngay.Split("-")[1], item.Ngay.Split("-")[1] + "-" + item.Ngay.Split("-")[0])),
                        TenDangNhap = item.TenDangNhap.Replace("nap", "").Trim(),
                        CreationTime = DateTime.Now
                    });
                    demoDbContext.SaveChanges();
                }

                logger.LogInformation($"Worker printing number {number}");
                await Task.Delay(30000);
            }
        }
    }
}
