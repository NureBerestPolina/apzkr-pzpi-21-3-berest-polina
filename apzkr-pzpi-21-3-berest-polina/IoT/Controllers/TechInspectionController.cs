using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PickupCounterIoT.Models;
using PickupCounterIoT.Services;
using PickupCounterIoT.Settings;
using System;
using System.Net.Http;
using System.Text;

namespace PickupCounterIoT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TechInspectionController : ControllerBase
    {
        private readonly CounterSettings settings;
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;
        string filePath;

        public TechInspectionController(CounterSettings settings, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.settings = settings;
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
            filePath = configuration.GetValue<string>("FileStorages:OpenCountStorage");
        }

        [HttpGet]
        public IActionResult GetSettings()
        {
            return Ok(settings);
        }

        [HttpPut("updateDoorOpenCount")]
        public async Task<IActionResult> UpdateDoorOpenCountAsync(object requestData)
        {
            int currentCount = ReadDoorCount() + 1;

            if (currentCount >= settings.MaxDoorOpenCount)
            {
                SendTechInspectionRequest(currentCount);

                currentCount = 0;
            }
            WriteDoorCount(currentCount);
            return Ok();
        }

        [HttpPost]
        public IActionResult ResetFridge(object requestData)
        {
            WriteDoorCount(0);
            SetNormalTemperature();
            return Ok();
        }


        private void WriteDoorCount(int count)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.Write(count);
            }
        }

        private int ReadDoorCount()
        {
            int count = 0;

            if (System.IO.File.Exists(filePath))
            {
                string content = System.IO.File.ReadAllText(filePath);
                int.TryParse(content, out count);
                return count;
            }
            else
            {
                return 0;
            }
        }

        private async void SendTechInspectionRequest(int currentCount)
        {
            var client = httpClientFactory.CreateClient();
            var baseUri = new Uri(configuration.GetValue<string>("ServerUrl"));
            client.Timeout = TimeSpan.FromMinutes(5);

            var temperature = new TemperatureService(httpClientFactory, settings, configuration).GetTemperature();

            var response = await client.PostAsJsonAsync(new Uri(baseUri, "odata/TechInspectionRequests"), new TechInspectionRequest
            {
                Temperature = temperature,
                OpensCount = currentCount
            });
        }

        private void SetNormalTemperature()
        {

        }
    }
}