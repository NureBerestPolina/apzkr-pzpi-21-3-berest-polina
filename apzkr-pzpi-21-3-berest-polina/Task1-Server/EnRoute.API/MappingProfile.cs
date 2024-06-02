using AutoMapper;
using EnRoute.API.Contracts.Auth.Requests;
using EnRoute.API.Contracts.Auth.Responses;
using EnRoute.Infrastructure.Commands;
using EnRoute.Infrastructure.DTOs;

namespace EnRoute.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, RegisterCommand>();
            CreateMap<RegisterCompanyRequest, RegisterCommand>();
            CreateMap<GetStatisticsDto, GetStatisticsResponse>();
            CreateMap<GetOrganizationStatisticsDto, GetOrganizationStatisticsResponse>();
            CreateMap<GetDeliveryDestinationDto, GetDeliveryDestinationResponse>();
            CreateMap<GetCounterDeliveryDto, GetCounterDeliveryResponse>();
        }
    }
}
