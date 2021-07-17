using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_NetCore.DB.Table;

namespace WebApi_NetCore.DB
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
           : base(options)
        {

        }
        public DbSet<GiaoDichChuyenTien> GiaoDichChuyenTiens { get; set; }
        public DbSet<NguoiDungThongTin> NguoiDungThongTins { get; set; }


    } 
    
}
