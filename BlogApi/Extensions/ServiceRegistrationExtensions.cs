using BlogApi.Presentation.ActionFilters;
using BlogApi.Utility;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Contracts;
using Service.DataShaping;
using Shared.DataTransferObjects;

namespace BlogApi.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        
        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureCors();
            services.ConfigureSwagger();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.ConfigureRepositoryManager();
            services.ConfigureServiceManager();
            services.ConfigureSqlContext(configuration);
            services.AddCustomMediaTypes();
            services.AddMemoryCache();
            services.ConfigureRateLimitOptions();
            services.AddHttpContextAccessor();
            services.AddAuthentication();
            services.ConfigureIdentity();
            services.AddJwtConfiguration(configuration);
            services.ConfigureJWT(configuration);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddScoped<ValidateMediaTypeAttribute>();
            services.AddScoped<IDataShaper<PostDto>, DataShaper<PostDto>>();
            services.AddScoped<IPostLinks, PostLinks>();
            var licenseKey = configuration.GetSection("AutomapperSettings:LicenseKey").Value;
            services.AddAutoMapper(cfg => cfg.LicenseKey = licenseKey , typeof(Program));
            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<ISlugService, SlugService>();
            
        }
    }
}
