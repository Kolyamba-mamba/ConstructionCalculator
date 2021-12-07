using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Models
{
    /// <summary>
    /// Результат авторизации
    /// </summary>
    public class AuthResult
    {
        /// <summary>
        /// Флаг успешности
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Токен
        /// </summary>
        public string Token { get; set; }
    }
}
