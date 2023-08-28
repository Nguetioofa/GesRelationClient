using GesRelationClient.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Security.Claims;

namespace GesRelationClient.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMyServices(this WebApplicationBuilder builder)
        {


            builder.Services.AddScoped<DbConnectionManager>();
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
            builder.Services.AddTransient<IRepositorie<Client>, ClientsServices>();
            builder.Services.AddTransient<IRepositorie<Appel>, AppelService>();
            builder.Services.AddTransient<EmployeService>();
            builder.Services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();
            builder.Services.AddSingleton(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
            builder.Services.AddScoped<IViewLocalizer, ViewLocalizer>();
            builder.Services.AddScoped<IHtmlLocalizerFactory, HtmlLocalizerFactory>();

            //builder.Services.AddSession();
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("fr") };
                options.DefaultRequestCulture = new RequestCulture("fr");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                });


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ManagerOnly", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "Manager"));
                options.AddPolicy("EmployeOnly", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "Employe"));
            });

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en", "fr" };
                options.SetDefaultCulture(supportedCultures[1])
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);
            });
            return builder.Services;
        }
    }
}
