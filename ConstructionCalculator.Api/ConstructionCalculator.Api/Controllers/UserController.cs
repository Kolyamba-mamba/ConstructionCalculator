using AutoMapper;
using ConstructionCalculator.Api.Helpers;
using ConstructionCalculator.Api.Models;
using ConstructionCalculator.Api.Models.DTO;
using ConstructionCalculator.Api.Repositories;
using ConstructionCalculator.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IRepository<User> _repository;
        private readonly Mapper _mapper;
        private readonly IIdentityService _identityService;

        public UserController(IRepository<User> repository, IAutomapperHelper automapperHelper, IIdentityService identityService)
        {
            _repository = repository;
            _mapper = automapperHelper.GetAutomapper();
            _identityService = identityService;
        }

        [Route("getusers")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_repository.Get());
        }

        [Route("registration")]
        [HttpPost]
        public async Task<IActionResult> UserRegistration([FromBody] UserRegistrationDto user)
        {
            var validationResult = new ValidateUserHelper().ValidateRegistrationParams(user);
            if (validationResult != null)
                return BadRequest(validationResult);
            User existingUser = await _repository.Get().FirstOrDefaultAsync(u => u.Email == user.Email || u.UserName == user.Username);
            if (existingUser != null)
                return BadRequest("Пользователь с указанным email или именем уже существет, введите другие значения.");

            var userClass = _mapper.Map<UserRegistrationDto, User>(user);

            await _repository.Create(userClass);
            return Ok("Регистрация прошла успешно.");
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            var validationResult = new ValidateUserHelper().ValidateLoginParams(user);
            if (validationResult != null)
                return BadRequest(validationResult);

            var authResult = await _identityService.AuthenticationUserAsunc(user.Email, user.Password, _repository);
            if (authResult.IsSuccess)
                return Ok(authResult);
            return Unauthorized();
        }
    }
}
