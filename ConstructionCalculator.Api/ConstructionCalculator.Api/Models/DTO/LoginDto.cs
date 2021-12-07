using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Models.DTO
{
    /// <summary>
    /// Дто авторизации
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
