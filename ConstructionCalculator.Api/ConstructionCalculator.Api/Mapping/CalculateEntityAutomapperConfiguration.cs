using AutoMapper;
using ConstructionCalculator.Api.Models;
using ConstructionCalculator.Api.Models.DTO;

namespace ConstructionCalculator.Api.Mapping
{
    /// <summary>
    /// Класс конфигурации маппинга модели расчетов на дто расчетов
    /// </summary>
    public class CalculateEntityAutomapperConfiguration
    {
        public CalculateEntityAutomapperConfiguration(IMapperConfigurationExpression cfg) 
        {
            cfg.CreateMap<CalculateEntity, CalculateEntityDto>();
        }
    }
}
