using PickupCounterIoT.Models;
using PickupCounterIoT.Settings;

namespace PickupCounterIoT.Services
{
    public class TemperatureService : IHostedService, IDisposable
    {
        private readonly IHttpClientFactory httpClient;
        private readonly CounterSettings settings;
        private readonly IConfiguration configuration;
        private readonly TimeSpan updateInterval = TimeSpan.FromMinutes(30);
        private Timer timer;
        private Random random = new Random();

        public TemperatureService(IHttpClientFactory httpClient, CounterSettings settings, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.settings = settings;
            this.configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(async (s) => await UpdateTemperatureAsync(s), null, TimeSpan.Zero, updateInterval);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        private async Task UpdateTemperatureAsync(object state)
         {
            double temperature = GetTemperature();

            if (temperature > settings.MaxCellTempC ||
                temperature < settings.MinCellTempC)
            {
                temperature = settings.locale == Locale.Ukrainian ? temperature : ((temperature - 32) / 1.8);
                await SendPostRequestAsync(temperature);
            }
        }

        public double GetTemperature()
        {
            return random.NextDouble() * (settings.MaxCellTempC - settings.MinCellTempC + 2) + (settings.MinCellTempC - 1);
        }

        private async Task SendPostRequestAsync(double temperature)
        {
            var client = httpClient.CreateClient();
            var baseUri = new Uri(configuration.GetValue<string>("ServerUrl"));

            var response = await client.PostAsJsonAsync(new Uri(baseUri, "odata/TechInspectionRequests"), new TechInspectionRequest
            {
                Temperature = temperature,
                OpensCount = 0
            });
        }
    }
}
