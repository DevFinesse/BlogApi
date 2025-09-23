using BlogApi.Presentation.ActionFilters;
using BlogApi.Utility;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
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
            services.AddAutoMapper(cfg => cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg0NzY0ODAwIiwiaWF0IjoiMTc1MzMwNTQ2MiIsImFjY291bnRfaWQiOiIwMTk4MzkyNTdmOWM3NTU1YmNjNzM2M2EwNTI0MTQ0YSIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazB3amNkNzV3Z2hndDl4a3diMHBkd3p6Iiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.SU6g1Uo08eX4s34DWbW8qkONRGW2vC8SeNFW2v8EkAE52fuAI7sjBCN-K-i6w6GuL10E9nYXIlxnkYDzrWPWpeKviqqeLKj_QopHJFVmblPVjJWW-G8z5tp0M9gFDeFjCF2-6XIlFVJg4tD-r-yTApM7D3aJT9UqcYvZkyJtOPJk58wSqportSFpk0B2OuCOYbw6w8C2JEQTjmy5u7MaTEW5_P4RIv7s8RO2KP_8PF0vv7K7Kgpz5fQrL_kXivhzHSy292VTThzQbOxssYvKPNb7wJlUtdPXXDed1mq5N5aRo84u6oPpzCtgr02PmyxV3pRtG32RL80gGr0pNldpoQ", typeof(Program));
            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<ISlugService, SlugService>();
            
        }
    }
}
