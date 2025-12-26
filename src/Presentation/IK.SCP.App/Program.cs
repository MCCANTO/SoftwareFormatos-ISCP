using IK.SCP.App.Common.Installers;
using IK.SCP.App.Models;
using IK.SCP.Application;
using IK.SCP.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using NLog;
using NLog.Web;
using System.Text;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllersWithViews(options =>
    {
        options.EnableEndpointRouting = false;

        var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();

        options.Filters.Add(new IK.SCP.App.Common.Auth.AuthorizeAttribute());

    });

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    IConfigurationSection settingsSection = builder.Configuration.GetSection("AppSettings");
    AppSettings settings = settingsSection.Get<AppSettings>();
    byte[] signingKey = Encoding.UTF8.GetBytes(settings.EncryptionKey);
    builder.Services.AddAuthentication(signingKey);

    builder.Services.AddSwagger();
    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddApplication(builder.Configuration);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "appcors",
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                          });
    });


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
    }

    app.UseStaticFiles();
    app.UseRouting();


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("appcors");

    app.UseAuthentication();
    app.UseAuthorization();


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");

    if (!app.Environment.IsDevelopment())
    {
        app.MapFallbackToFile("index.html");
    }


    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}