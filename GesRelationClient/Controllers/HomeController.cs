using GesRelationClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.WebUtilities;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Localization;

namespace GesRelationClient.Controllers
{
    
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var v = CultureInfo.CurrentCulture.Name;

            string lang = HttpContext.Request.Query["lang"];

            if (!string.IsNullOrEmpty(lang))
            {
                SetLanguage(lang);
            }

            return View();
        }

        private void SetLanguage(string lang)
        {
            var v = CultureInfo.CurrentCulture.Name;

            CultureInfo culture;
            try
            {
                culture = new CultureInfo(lang);
            }
            catch (CultureNotFoundException)
            {
                culture = new CultureInfo("fr");
            }
            HttpContext.Session.SetString("lang", lang);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }

        [HttpGet]
        public IActionResult ChangeLanguage(string lang)
        {
            SetLanguage(lang);

            string returnUrl = HttpContext.Request.Headers["Referer"].ToString();
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/";
            }
            var v = CultureInfo.CurrentCulture.Name;
            return Redirect(returnUrl);
        }


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