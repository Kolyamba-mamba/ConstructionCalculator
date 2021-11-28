using AutoMapper;

namespace ConstructionCalculator.Api.Mapping
{
    public class AutomapperConfigurationStorage
    {
        public MapperConfiguration Configuration;

        public AutomapperConfigurationStorage()
        {
            Configuration = new MapperConfiguration(cfg =>
            {
                new RegistrationAutomapperConfiguration(cfg);
            });
        }
    }
}