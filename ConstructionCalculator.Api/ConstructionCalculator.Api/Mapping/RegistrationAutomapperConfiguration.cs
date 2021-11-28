using AutoMapper;
using ConstructionCalculator.Api.Models;
using ConstructionCalculator.Api.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Mapping
{
    public class RegistrationAutomapperConfiguration
    {
        public RegistrationAutomapperConfiguration(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserRegistrationDto, User>();
        }
    }
}
