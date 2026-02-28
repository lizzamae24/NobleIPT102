using Microsoft.EntityFrameworkCore;
using NobleFinalDomain.Models;
using System.Collections.Generic;


namespace NobleFinalFramework.DbContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Sensor> Sensors { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    }
}