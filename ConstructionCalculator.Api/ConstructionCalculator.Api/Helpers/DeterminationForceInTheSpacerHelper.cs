using ConstructionCalculator.Api.Models.DTO;
using System;

namespace ConstructionCalculator.Api.Helpers
{
    /// <summary>
    /// Хелпер вычислений усилия в распорке
    /// </summary>
    public class DeterminationForceInTheSpacerHelper
    {
        /// <summary>
        /// Вычисление усилия в распорке
        /// </summary>
        /// <param name="input">Параметры для расчетов</param>
        /// <param name="h">Глубина заделки подпорной стены ниже дна котлована</param>
        /// <returns>Усилия в распорке</returns>
        public static double GetForceInTheSpacer(InputNumbersDto input, double h)
        {
            var Ea = DeterminationPressureHelper.CalculateActivePressure(input, h);
            var Ep = DeterminationPressureHelper.CalculatePassivePressure(input, h);
            var Eq = DeterminationPressureHelper.CalculateSidePressure(input);

            var Np = Ea + Eq - Ep;
            return Math.Round(Np,2);
        }
    }
}
