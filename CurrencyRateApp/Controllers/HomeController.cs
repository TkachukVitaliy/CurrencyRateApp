using CurrencyRateApp.Models;
using CurrencyRateApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyRateApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<HomeController> _logger;
        private readonly List<CurrencyConverter> currency = new List<CurrencyConverter>();
        CurrencyViewModel cvm = new CurrencyViewModel();

        public HomeController(IMemoryCache memoryCache, ILogger<HomeController> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            try
            {
                currency = JsonConvert.DeserializeObject<List<CurrencyConverter>>((string)_memoryCache.Get("key_currency_json"));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            

        }

        [HttpGet]
        public IActionResult Index()
        {
            cvm.CurrencyList = currency;
            return View(cvm);
        }
        [HttpPost]
        public JsonResult Index(int amount, decimal currTo)
        {

            var result = (amount / currTo).ToString("0.00");

            return Json(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
