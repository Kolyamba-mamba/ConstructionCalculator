using ConstructionCalculator.Api.Models.DTO;
using System;

namespace ConstructionCalculator.Api.Helpers
{
    /// <summary>
    /// Хелпер вычислений равнодействующих
    /// </summary>
    public class DeterminationPressureHelper
    {
        /// <summary>
        /// Вычисление равнодействующей активного давления грунта на подпорную стенку
        /// </summary>
        /// <param name="input">Параметры для расчетов</param>
        /// <param name="h">Глубина заделки подпорной стены ниже дна котлована</param>
        /// <returns>Равнодействующая активного давления грунта на подпорную стенку</returns>
        public static double CalculateActivePressure(InputNumbersDto input, double h)
        {
            var gamma1 = input.gamma2 / 1;
            var fi1 = input.fi2 / 1.15;
            var c1 = input.c2 / 1.5;
            var teta = 45 - fi1 / 2;

            //Координата точки начала эпюры активного давления грунта
            var hc = (2 * c1) / (gamma1 * (Math.Tan(teta * (Math.PI / 180))));

            var pressure = (gamma1 * (input.H + h) * (Math.Tan(teta * (Math.PI / 180))) * (Math.Tan(teta * (Math.PI / 180))) - 2 * c1 * (Math.Tan(teta * (Math.PI / 180)))) * ((input.H + h - hc) / 2);

            return pressure;
        }

        /// <summary>
        /// Вычисление равнодействующей бокового давления
        /// </summary>
        /// <param name="input">Параметры для расчетов глубины заделки подпорной стены ниже дна котлована</param>
        /// <param name="h">Глубина заделки подпорной стены ниже дна котлована</param>
        /// <returns>Равнодействующая бокового давления</returns>
        public static double CalculateSidePressure(InputNumbersDto input)
        {
            var fi1 = input.fi2 / 1.15;
            var teta = 45 - fi1 / 2;

            //проекция ширины фундамента здания №1 bf (полосы с пригрузом q ) на подпорную стенку
            var Hq = input.bf / (Math.Tan(teta * (Math.PI / 180)));

            var pressure = input.q * Hq * ((Math.Tan(teta * (Math.PI / 180))) * (Math.Tan(teta * (Math.PI / 180))));
            return pressure;
        }

        /// <summary>
        /// Вычисление равнодействующей бокового давления
        /// </summary>
        /// <param name="input">Параметры для расчетов глубины заделки подпорной стены ниже дна котлована</param>
        /// <param name="Hq">Проекция ширины фундамента здания</param>
        /// <returns>Равнодействующая бокового давления</returns>
        public static double CalculateSidePressure(InputNumbersDto input, double Hq)
        {
            var fi1 = input.fi2 / 1.15;
            var teta = 45 - fi1 / 2;

            var pressure = input.q * Hq * ((Math.Tan(teta * (Math.PI / 180))) * (Math.Tan(teta * (Math.PI / 180))));
            return pressure;
        }

        /// <summary>
        /// Вычисление равнодействующей пассивного давления грунта на подпорную стенку
        /// </summary>
        /// <param name="input">Параметры для расчетов</param>
        /// <param name="h">Глубина заделки подпорной стены ниже дна котлована</param>
        /// <returns>Равнодействующая пассивного давления грунта на подпорную стенку</returns>
        public static double CalculatePassivePressure(InputNumbersDto input, double h)
        {
            var gamma1 = input.gamma2 / 1;
            var fi1 = input.fi2 / 1.15;
            var c1 = input.c2 / 1.5;

            var corner = 45 + fi1 / 2;

            var pressure = (gamma1 * ((h * h) / 2) * (Math.Tan(corner * (Math.PI / 180))) * (Math.Tan(corner * (Math.PI / 180)))) + (2 * c1 * h * (Math.Tan(corner * (Math.PI / 180))));

            return pressure;
        }
    }
}
