using AutoMapper;

namespace ConstructionCalculator.Api.Mapping
{
    /// <summary>
    /// Класс регистрации конфигураций мапперов
    /// </summary>
    public class AutomapperConfigurationStorage
    {
        public MapperConfiguration Configuration;

        public AutomapperConfigurationStorage()
        {
            Configuration = new MapperConfiguration(cfg =>
            {
                new RegistrationAutomapperConfiguration(cfg);
                new CalculateEntityAutomapperConfiguration(cfg);
            });
        }
    }
}