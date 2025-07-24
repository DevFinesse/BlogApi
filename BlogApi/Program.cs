using BlogApi.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;


var builder = WebApplication.CreateBuilder(args);
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "nlog/config"));

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(cfg => cfg.LicenseKey= "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg0NzY0ODAwIiwiaWF0IjoiMTc1MzMwNTQ2MiIsImFjY291bnRfaWQiOiIwMTk4MzkyNTdmOWM3NTU1YmNjNzM2M2EwNTI0MTQ0YSIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazB3amNkNzV3Z2hndDl4a3diMHBkd3p6Iiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.SU6g1Uo08eX4s34DWbW8qkONRGW2vC8SeNFW2v8EkAE52fuAI7sjBCN-K-i6w6GuL10E9nYXIlxnkYDzrWPWpeKviqqeLKj_QopHJFVmblPVjJWW-G8z5tp0M9gFDeFjCF2-6XIlFVJg4tD-r-yTApM7D3aJT9UqcYvZkyJtOPJk58wSqportSFpk0B2OuCOYbw6w8C2JEQTjmy5u7MaTEW5_P4RIv7s8RO2KP_8PF0vv7K7Kgpz5fQrL_kXivhzHSy292VTThzQbOxssYvKPNb7wJlUtdPXXDed1mq5N5aRo84u6oPpzCtgr02PmyxV3pRtG32RL80gGr0pNldpoQ", typeof(Program));

builder.Services.AddControllers()
    .AddApplicationPart(typeof(BlogApi.Presentation.AssemblyReference).Assembly);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
    app.UseHsts();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
