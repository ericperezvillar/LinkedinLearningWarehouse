using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LinkedinLearningWarehouse.DataAccess
{
    public class LinkedinLearningDbContextFactory : IDesignTimeDbContextFactory<LinkedinLearningDbContext>
    {
        public LinkedinLearningDbContext CreateDbContext(string[] args)
        {
            // Setup configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets(Assembly.GetAssembly(typeof(LinkedinLearningDbContext)) ?? throw new Exception("No Assembly found!"))
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<LinkedinLearningDbContext>();

            optionsBuilder.UseLazyLoadingProxies()
                            .UseLoggerFactory(LoggerFactory.Create(c => c.AddSerilog()))
                            .UseSqlServer(configuration.GetConnectionString("LinkedinLearning"));
            
            LinkedinLearningDbContext context = new(optionsBuilder.Options);
            context.Database.SetCommandTimeout(60);
            return context;
        }
    }
}
