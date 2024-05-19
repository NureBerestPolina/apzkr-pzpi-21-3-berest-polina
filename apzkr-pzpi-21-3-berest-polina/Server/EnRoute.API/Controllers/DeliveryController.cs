using EnRoute.Domain.Models;
using EnRoute.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.EntityFrameworkCore;
using EnRoute.Domain.Constants;
using EnRoute.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Azure.Core;
using EnRoute.Infrastructure.Commands;
using EnRoute.API.Contracts.Auth.Responses;

namespace EnRoute.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DeliveryController : ControllerBase
    { 
        private readonly IDeliveryService deliveryService;
        private readonly IMapper mapper;

        public DeliveryController(IDeliveryService deliveryService, IMapper mapper) 
        {
            this.deliveryService = deliveryService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("get-delivery-list/{organizationCode}")]
        public async Task<IActionResult> GetDeliveryList(Guid organizationCode)
        {
            var data = await deliveryService.GetDeliveryList(organizationCode);
            var response = mapper.Map<List<GetDeliveryDestinationResponse>>(data);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-counter-delivery-list/{counterCode}")]
        public async Task<IActionResult> GetCounterDeliveryList(Guid counterCode)
        {
            var data = await deliveryService.GetCounterDeliveryList(counterCode);
            var response = mapper.Map<List<GetCounterDeliveryResponse>>(data);

            return Ok(response);
        }
    }
}
