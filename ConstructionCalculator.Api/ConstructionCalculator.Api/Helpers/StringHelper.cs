using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Helpers
{
    public class StringHelper
    {
        public static bool IsNullOrEmptyString(string str) => 
            str == null || String.IsNullOrEmpty(str) || String.IsNullOrWhiteSpace(str);
    }
}
