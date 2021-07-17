using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi_NetCore.Shared;

namespace WebApi_NetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            return new WebHostBuilder()
                 .UseKestrel(opt => opt.AddServerHeader = false)
                 .UseContentRoot(Directory.GetCurrentDirectory())
                 .UseIIS()
                 .UseIISIntegration()
                 .UseStartup<Startup>()
                 .UseUrls("http://0.0.0.0:5555");
        }
    }
}
