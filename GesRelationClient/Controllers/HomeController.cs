using GesRelationClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.WebUtilities;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Localization;
using FastReport;
using Microsoft.AspNetCore.Hosting.Server;
using GesRelationClient.Services;
using Microsoft.Extensions.Localization;

namespace GesRelationClient.Controllers
{
    
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _localizer;



        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var v = CultureInfo.CurrentCulture.Name;

            //string lang = HttpContext.Request.Query["lang"];

            //if (!string.IsNullOrEmpty(lang))
            //{
            //    SetLanguage(lang);
            //}

            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        //private void SetLanguage(string lang)
        //{

        //    CultureInfo culture;
        //    try
        //    {
        //        culture = new CultureInfo(lang);
        //    }
        //    catch (CultureNotFoundException)
        //    {
        //        culture = new CultureInfo("fr");
        //    }

        //    CultureInfo.CurrentCulture = culture;
        //    CultureInfo.CurrentUICulture = culture;
        //}

        //[HttpGet]
        //public IActionResult ChangeLanguage(string lang)
        //{
        //    SetLanguage(lang);

        //    string returnUrl = HttpContext.Request.Headers["Referer"].ToString();
        //    if (string.IsNullOrEmpty(returnUrl))
        //    {
        //        returnUrl = "/";
        //    }
        //    return Redirect(returnUrl);
        //}



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}