using AutoMapper;
using ConstructionCalculator.Api.Mapping;
using System;

namespace ConstructionCalculator.Api.Helpers
{
    public class AutomapperHelper : IAutomapperHelper
    {
        private Mapper _mapper;
        private MapperConfiguration _configuration;
        private IServiceProvider _provider;
        public AutomapperHelper(IServiceProvider provider, AutomapperConfigurationStorage configuration)
        {
            _provider = provider;
            _configuration = configuration.Configuration;
            _mapper = InitializeAutomapper();
        }
        /// <summary>
        /// Инициализация автомаппера
        /// </summary>
        public Mapper InitializeAutomapper() =>
            new Mapper(_configuration, _provider.GetService);

        /// <summary>
        /// Получение автомаппера
        /// </summary>
        public Mapper GetAutomapper() => _mapper;
    }
}
