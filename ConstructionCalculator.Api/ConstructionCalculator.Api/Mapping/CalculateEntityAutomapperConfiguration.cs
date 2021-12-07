using AutoMapper;
using ConstructionCalculator.Api.Models;
using ConstructionCalculator.Api.Models.DTO;

namespace ConstructionCalculator.Api.Mapping
{
    public class CalculateEntityAutomapperConfiguration
    {
        public CalculateEntityAutomapperConfiguration(IMapperConfigurationExpression cfg) 
        {
            cfg.CreateMap<CalculateEntity, CalculateEntityDto>();
        }
    }
}
