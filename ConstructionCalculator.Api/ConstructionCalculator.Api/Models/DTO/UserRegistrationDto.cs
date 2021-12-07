using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Models.DTO
{
    /// <summary>
    /// Дто регистрации
    /// </summary>
    public class UserRegistrationDto
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; set; }
    }
}
