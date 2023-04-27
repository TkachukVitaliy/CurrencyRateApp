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
        private readonly List<CurrencyConverter> currency = new List<CurrencyConverter>();
        CurrencyViewModel cvm = new CurrencyViewModel();

        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

            currency = JsonConvert.DeserializeObject<List<CurrencyConverter>>((string)_memoryCache.Get("key_currency_json"));

        }

        [HttpGet]
        public IActionResult Index()
        {
            cvm.CurrencyList = currency;
            return View(cvm);
        }
        [HttpPost]
        public IActionResult Index(int amount, decimal currTo)
        {
            cvm.CurrencyList = currency;

            ViewBag.Result = (amount / currTo).ToString("0.00");
            return View("Index", cvm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
