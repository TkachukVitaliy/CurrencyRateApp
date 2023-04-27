using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyRateApp.Models
{
    public class CurrencyService : IHostedService, IDisposable
    {

        private readonly IMemoryCache _memoryCache;
        private WebClient client = new WebClient();
        public List<CurrencyConverter> currencies { get; private set; } = new List<CurrencyConverter>();
        private string pageURL;

        private Timer? _timer = null;
        private ILogger<CurrencyService> _logger;
        public CurrencyService(ILogger<CurrencyService> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Currency Service start working");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(24));

            return Task.CompletedTask;

        }

        private async void DoWork(object? state)
        {
            pageURL = client.DownloadString("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json");
            _memoryCache.Set("key_currency_json", pageURL);


        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Currency Service stop working");

            _timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}

