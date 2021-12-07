using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Helpers
{
    public class StringHelper
    {
        /// <summary>
        /// Проверка на пустоту строки
        /// </summary>
        /// <param name="str">Проверяемая строка</param>
        /// <returns>Флаг пустоты строки</returns>
        public static bool IsNullOrEmptyString(string str) => 
            str == null || String.IsNullOrEmpty(str) || String.IsNullOrWhiteSpace(str);
    }
}
