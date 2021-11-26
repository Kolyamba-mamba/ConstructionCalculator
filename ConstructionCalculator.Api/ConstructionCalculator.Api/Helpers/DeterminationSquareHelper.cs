using ConstructionCalculator.Api.Models.DTO;
using System;
using System.Linq;

namespace ConstructionCalculator.Api.Helpers
{
    /// <summary>
    /// Хелпер вычислений толщины стены в грунте и площади поперечного сечения продольной рабочей арматуры
    /// </summary>
    public class DeterminationSquareHelper
    {
        /// <summary>
        /// Определение максимального значение изгибающего момента в стенке и площади поперечного сечения
        /// </summary>
        /// <param name="inputNumbers">Параметры для расчетов</param>
        /// <param name="h">Глубина заделки подпорной стены</param>
        /// <param name="Np">Усилие в распорке</param>
        /// <returns></returns>
        public static WidthAndSquareArmatureDto GetMomentAndSquare(InputNumbersDto inputNumbers, double h, double Np) 
        {
            var gamma1 = inputNumbers.gamma2 / 1;
            var fi1 = inputNumbers.fi2 / 1.15;
            var c1 = inputNumbers.c2 / 1.5;
            var teta = 45 - fi1 / 2;
            //Координата точки начала эпюры активного давления грунта
            var hc = (2 * c1) / (gamma1 * (Math.Tan(teta * (Math.PI / 180))));
            //Расстояние от поверхности земли до начала эпюры бокового давления от фундамента здания №1(пригрузка q) на подпорную стенку
            var hq = inputNumbers.d1 + (inputNumbers.L1 - (inputNumbers.bf / 2)) / (Math.Tan(teta * (Math.PI / 180)));
            //проекция ширины фундамента здания №1 bf (полосы с пригрузом q ) на подпорную стенку
            var Hq = inputNumbers.bf / (Math.Tan(teta * (Math.PI / 180)));

            //Точка 0
            var z0 = 0;
            var M0 = 0;

            //Точка 1
            var z1 = hq - hc;
            var M1 = Np * z1 - (gamma1 * hq * (Math.Tan(teta * (Math.PI / 180))) * (Math.Tan(teta * (Math.PI / 180))) - 2 * c1 * (Math.Tan(teta * (Math.PI / 180)))) * (z1 / 2) * (z1 / 3);

            //Точка 2
            var z2 = hq - hc + Hq / 2;
            var M2 = Np * z2 - (gamma1 * (hq + Hq / 2) * (Math.Tan(teta * (Math.PI / 180))) * (Math.Tan(teta * (Math.PI / 180))) - 2 * c1 * (Math.Tan(teta * (Math.PI / 180)))) * (z1 / 2) * (z1 / 3) - inputNumbers.q * (Math.Tan(teta * (Math.PI / 180))) * (Math.Tan(teta * (Math.PI / 180))) * (Hq / 2) * (Hq / 4);

            //Точка 3
            var z3 = hq + Hq - hc;
            var M3 = Np * z3 - (gamma1 * (hq + Hq) * (Math.Tan(teta * (Math.PI / 180))) * (Math.Tan(teta * (Math.PI / 180))) - 2 * c1 * (Math.Tan(teta * (Math.PI / 180)))) * (z3 / 2) * (z3 / 3) - inputNumbers.q * (Math.Tan(teta * (Math.PI / 180))) * (Math.Tan(teta * (Math.PI / 180))) * ((Hq * Hq / 2));

            //Точка 4
            var z4 = z3 + (inputNumbers.H - hq - Hq) / 2;
            var M4 = Np * z4 - (gamma1 * (z4 + hc) * (Math.Tan(teta * (Math.PI / 180))) * (Math.Tan(teta * (Math.PI / 180))) - 2 * c1 * (Math.Tan(teta * (Math.PI / 180)))) * (z4 / 2) * (z4 / 3) - (inputNumbers.q * (Math.Tan(teta * (Math.PI / 180))) * (Math.Tan(teta * (Math.PI / 180))) * Hq * ((inputNumbers.H - hq) / 2));

            //Точка 5
            var z5 = inputNumbers.H - hc;
            var M5 = Np * z5 - (gamma1 * inputNumbers.H * (Math.Tan(teta * (Math.PI / 180))) * (Math.Tan(teta * (Math.PI / 180))) - 2 * c1 * (Math.Tan(teta * (Math.PI / 180)))) * (z5 / 2) * (z5 / 3) - inputNumbers.q * (Math.Tan(teta * (Math.PI / 180))) * (Math.Tan(teta * (Math.PI / 180))) * Hq * (inputNumbers.H - hq - Hq / 2);

            //Точка 6
            var z6 = inputNumbers.H - hc + h / 2;
            var M6 = Np * z6 - ((gamma1 * (inputNumbers.H + h / 2) * (Math.Tan(teta * (Math.PI / 180))) * (Math.Tan(teta * (Math.PI / 180))) - (2 * c1 * (Math.Tan(teta * (Math.PI / 180))))) * (z6 / 2) * (z6 / 3)) - (inputNumbers.q * (Math.Tan(teta * (Math.PI / 180))) * (Math.Tan(teta * (Math.PI / 180))) * Hq * (inputNumbers.H - hq - Hq / 2 + (h / 2))) + (gamma1 * (h / 2) * (Math.Tan(teta * (Math.PI / 180))) * (Math.Tan(teta * (Math.PI / 180))) * ((h * h) / 24)) + 2 * c1 * (Math.Tan(teta * (Math.PI / 180))) * ((h * h) / 8);

            //Точка 7
            var M7 = 0;

            var ArrM = new double[] { M0, M1, M2, M3, M4, M5, M6, M7 };
            var Mmax = Math.Round(ArrM.Max(), 2);

            //Назначаем толщину подпорной стенки из условия
            var t = (inputNumbers.H + h) / 20;

            //Константа
            var As = 0.06;
            var t0 = t - As;

            //Удельное сопротивлеие арматуры А400
            var Rs = 350000;
            var As1 = Mmax / (0.9 * t0 * Rs);

            var result = new WidthAndSquareArmatureDto(Mmax, Math.Round(As1, 5));

            return result;
        }
    }
}
