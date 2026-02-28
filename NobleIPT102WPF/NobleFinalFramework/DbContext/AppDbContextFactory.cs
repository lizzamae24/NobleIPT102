using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace NobleFinalFramework.DbContexts
{
    public class AppDbContextFactory
    {
        private readonly IConfiguration _configuration;


        public AppDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
            .Options;


            return new AppDbContext(options);
        }
    }
}