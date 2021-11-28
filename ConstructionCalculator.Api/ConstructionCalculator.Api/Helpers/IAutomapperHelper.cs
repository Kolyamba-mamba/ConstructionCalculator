using AutoMapper;

namespace ConstructionCalculator.Api.Helpers
{
    public interface IAutomapperHelper
    {
        Mapper InitializeAutomapper();
        Mapper GetAutomapper();
    }
}
