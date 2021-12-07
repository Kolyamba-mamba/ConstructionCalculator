using ConstructionCalculator.Api.Helpers.DatabaseHelpers;
using ConstructionCalculator.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Helpers
{
    public class SampleData
    {
        /// <summary>
        /// Добавление дефолтных пользователей
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <returns></returns>
        public static void Initialize(ApplicationContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Email = "admin@admin.com",
                        Password = "1234",
                        UserName = "admin"
                    },
                    new User
                    {
                        Email = "admin2@admin.com",
                        Password = "1234",
                        UserName = "admin2"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
