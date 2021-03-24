using LoafAndStranger.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoafAndStranger.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Loaf> Loaves { get; set; }
        public DbSet<Top> Tops { get; set; }
        public DbSet<Stranger> Strangers { get; set; }
    }
}
