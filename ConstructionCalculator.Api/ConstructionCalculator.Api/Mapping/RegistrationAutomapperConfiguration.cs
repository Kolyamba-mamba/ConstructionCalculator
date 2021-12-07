using AutoMapper;
using ConstructionCalculator.Api.Models;
using ConstructionCalculator.Api.Models.DTO;

namespace ConstructionCalculator.Api.Mapping
{
    /// <summary>
    /// Класс конфигурации маппинга модели регистрации на сущность пользователя
    /// </summary>
    public class RegistrationAutomapperConfiguration
    {
        public RegistrationAutomapperConfiguration(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserRegistrationDto, User>();
        }
    }
}
