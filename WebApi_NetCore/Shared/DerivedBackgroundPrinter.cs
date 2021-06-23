using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi_NetCore.DB;

namespace WebApi_NetCore.Shared
{
    public class DerivedBackgroundPrinter : BackgroundService
    {
        private readonly IWorker worker;

        public DerivedBackgroundPrinter(IWorker worker)
        {
            this.worker = worker;            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {          
            await worker.DoWork(stoppingToken);
        }
    }
}
