using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PickupCounterIoT.Models;
using PickupCounterIoT.Settings;
using SmartFridgeIoT.Models;
using System;
using System.Net.Http;
using System.Text;

namespace PickupCounterIoT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly CounterSettings settings;
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;
        string filePath;

        public StatisticsController(CounterSettings settings, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.settings = settings;
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
            filePath = configuration.GetValue<string>("FileStorages:StatisticStorage");
        }

        [HttpGet]
        public IActionResult GetStatistics()
        {
            var statistics = ReadStatistics();
            return Ok(statistics);
        }

        [HttpPut("updateStatistics")]
        public async Task<IActionResult> UpdateStatistics(object obj)
        {
            var getIdFromHere = new TechInspectionRequest();

            var baseUri = new Uri(configuration.GetValue<string>("ServerUrl"));
            var orders = await httpClientFactory.CreateClient()
                .GetFromJsonAsync<List<OrderItem>>(new Uri(baseUri, $"/counter/{configuration.GetValue<Guid>("CounterId")}/order-positions"));

            var statistics = new Statistic
            {
                TopGoodId = GetTopGood(orders),
                MostPolpularCategory = GetMostPopularCategory(orders),
                TotalRevenue = CountTotalRevenue(orders)
            };

            WriteStatistics(statistics);

            return Ok();
        }

        private static Guid GetTopGood(List<OrderItem> orders)
        {
            var orderItemStatistics = orders
                .GroupBy(oi => oi.GoodId)
                .Select(g => new OrderItemStatistics
                {
                    GoodId = g.Key,
                    Revenue = g.Sum(oi => oi.Count * oi.GoodOrdered.Price)
                })
                .OrderByDescending(s => s.Revenue)
                .FirstOrDefault();

            return orderItemStatistics?.GoodId ?? Guid.Empty;
        }

        private static string GetMostPopularCategory(List<OrderItem> orders)
        {
            var orderItemStatistics = orders
                .GroupBy(oi => oi.GoodOrdered.Category.Name)
                .Select(g => new OrderItemStatistics
                {
                    CategoryName = g.Key,
                    Revenue = g.Sum(oi => oi.Count * oi.GoodOrdered.Price)
                })
                .OrderByDescending(s => s.Revenue)
                .FirstOrDefault();

            return orderItemStatistics?.CategoryName;
        }

        private static decimal CountTotalRevenue(List<OrderItem> orders)
        {
            return orders.Sum(oi => oi.Count * oi.GoodOrdered.Price);
        }

        private void WriteStatistics(Statistic statistic)
        {
            using (BinaryWriter writer = new BinaryWriter(System.IO.File.Open(filePath, FileMode.Create)))
            {
                writer.Write(statistic.TopGoodId.ToByteArray()); 
                writer.Write(statistic.TotalRevenue);
            }
        }

        private Statistic ReadStatistics()
        {
            Statistic stat = new Statistic();
            using (BinaryReader reader = new BinaryReader(System.IO.File.Open(filePath, FileMode.Open)))
            {
                byte[] idBytes = reader.ReadBytes(16); 
                stat.TopGoodId = new Guid(idBytes);
                stat.MostPolpularCategory = reader.ReadString();
                stat.TotalRevenue = reader.ReadDecimal();
            }

            return stat;
        }
    }
}