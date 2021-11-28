using ConstructionCalculator.Api.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Helpers
{
    public class ValidateUserHelper
    {
        public string ValidateRegistrationParams(UserRegistrationDto user)
        {
            var errorList = new List<string>();
            if (user == null)
                return "Входные параметры не могут быть пустыми, проверьте корректность ввода.";
            if (StringHelper.IsNullOrEmptyString(user.Email))
                errorList.Add("email");
            if (StringHelper.IsNullOrEmptyString(user.Password))
                errorList.Add("пароль");
            if (StringHelper.IsNullOrEmptyString(user.Username))
                errorList.Add("имя пользователя");

            if (errorList.Count > 0)
                return String.Concat("Входные параметры ", String.Join(", ", errorList), " должны быть заполнены.");
            else
                return null;
        }

        public string ValidateLoginParams(LoginDto user) 
        {
            var errorList = new List<string>();
            if (user == null)
                return "Входные параметры не могут быть пустыми, проверьте корректность ввода.";
            if (StringHelper.IsNullOrEmptyString(user.Email))
                errorList.Add("email");
            if (StringHelper.IsNullOrEmptyString(user.Password))
                errorList.Add("пароль");

            if (errorList.Count > 0)
                return String.Concat("Входные параметры ", String.Join(", ", errorList), " должны быть заполнены.");
            else
                return null;
        }
    }
}
