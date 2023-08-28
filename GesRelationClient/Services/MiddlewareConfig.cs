using Microsoft.Extensions.Options;

namespace GesRelationClient.Services
{
    public static class MiddlewareConfig
    {
        public static void ConfigureMiddleware(this WebApplication app)
        {
            app.UseStaticFiles();


            var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOptions.Value);



            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

        }
    }
}
