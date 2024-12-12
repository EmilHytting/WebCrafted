using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebCrafted.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Hardcode stien til WebCrafted.Web for at finde appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(@"C:\Users\emilm\RiderProjects\WebCrafted\WebCrafted.Web") 
                .AddJsonFile("appsettings.json") 
                .Build();

            // Hent connection string fra appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Brug connection string
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}