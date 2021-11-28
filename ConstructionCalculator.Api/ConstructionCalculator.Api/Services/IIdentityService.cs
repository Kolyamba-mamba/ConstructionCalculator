using ConstructionCalculator.Api.Models;
using ConstructionCalculator.Api.Repositories;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Services
{
    public interface IIdentityService
    {
        Task<AuthResult> AuthenticationUserAsunc(string login, string password, IRepository<User> repository);
    }
}
