using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LinkedinLearningWarehouse.Services;
using System.Reflection;
using LinkedinLearningWarehouse.Identity;
using Serilog;
using LinkedinLearningWarehouse.Interfaces;
using Microsoft.EntityFrameworkCore;
using LinkedinLearningWarehouse.DataAccess;
using LinkedinLearningWarehouse.Mappers;
using LinkedinLearningWarehouse.Services.LearningAsset;
using LinkedinLearningWarehouse.Services.LearningActivity;
using LinkedinLearningWarehouse.Services.Client;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Interfaces.LearningActivity;
using LinkedinLearningWarehouse.Interfaces.Client;

namespace LinkedinLearningWarehouse
{
    public static class Startup
    {
        public static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            
            // Setup configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets(Assembly.GetAssembly(typeof(OAuthTokenResponse)) ?? throw new Exception("No Assembly found!"))
                .Build();

            Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}Logs\\");

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            services.Configure<AppSettings>(configuration);

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.AddDbContext<LinkedinLearningDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("LinkedinLearning")));
            services.AddTransient(typeof(ILinkedInApiClientService<>), typeof(LinkedInApiClientService<>));
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddTransient<ILinkedInAuthService, LinkedInAuthService>();
            services.AddTransient<ILearningActivityService, LearningActivityService>();
            services.AddTransient<ILearningAssetsService, LearningAssetsService>();
            services.AddTransient<IEnterpriseGroupsService, EnterpriseGroupsService>();
            services.AddTransient<IAssetTypeService, AssetTypeService>();
            services.AddTransient<IEngagementMetricQualifierService, EngagementMetricQualifierService>();
            services.AddTransient<IEngagementMetricTypeService, EngagementMetricTypeService>();
            services.AddTransient<IEngagementActivityService, EngagementActivityService>();
            services.AddTransient<IContentDetailsService, ContentDetailsService>();            
            services.AddTransient<ILearnerDetailsService, LearnerDetailsService>();
            services.AddTransient<IQueryParameterBuilderService, QueryParameterBuilderService>();
            services.AddTransient<IIntegratorHistoryService, IntegratorHistoryService>();
            services.AddTransient<IAssetUrlService, AssetUrlService>();
            services.AddTransient<IAssetClassificationService, AssetClassificationService>();
            services.AddTransient<IAssetAvailableLocaleService, AssetAvailableLocaleService>();
            services.AddTransient<IContributorDetailService, ContributorDetailService>();
            services.AddTransient<IContributorTypeService, ContributorTypeService>();
            services.AddTransient<IAssetContributorService, AssetContributorService>();
            services.AddTransient<IOwnerDetailsService, OwnerDetailsService>();
            services.AddTransient<IClassificationTypeService, ClassificationTypeService>();
            services.AddTransient<IClassificationDetailsService, ClassificationDetailsService>();
            services.AddTransient<IAssetContentService, AssetContentService>();
            services.AddTransient<IAssetService, AssetService>();

            services.AddSingleton<IConfiguration>(configuration);

            // Build service provider
            return services.BuildServiceProvider();
        }
    }
}