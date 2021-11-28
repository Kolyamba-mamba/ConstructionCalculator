using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Models
{
    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
    }
}
