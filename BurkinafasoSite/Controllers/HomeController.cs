using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BurkinafasoSite.Models;
using Microsoft.Extensions.Localization;
using BurkinafasoSite.Resources;
using System.Globalization;
using BurkinafasoSite.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace BurkinafasoSite.Controllers
{
    public class HomeController : Controller
    {
        public string CurrentLanguage { get; set; }
        private readonly IStringLocalizer<SharedResources> _localizer;
        private ApplicationDbContext _context;

        public HomeController(IStringLocalizer<SharedResources> localizer,
                              ApplicationDbContext context)
        {
            _localizer = localizer;
            _context = context;
        }

        //public string GetLocalizedString()
        //{
        //    return _localizer["My localized string"];
        //}

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            CurrentLanguage = culture;

            return LocalRedirect(returnUrl);
        }

        public IActionResult Index()
        {
            _localizer.WithCulture(new CultureInfo("en"));
            var btnSave = _localizer["Btn-save"];
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            var test = _context.Course.FirstOrDefault();

            return View(test);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
