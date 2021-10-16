using ConstructionCalculator.Api.Models.DTO;
using System;

namespace ConstructionCalculator.Api.Helpers
{
    /// <summary>
    /// Хелпер вычислений глубины заделки подпорной стены ниже дна котлована
    /// </summary>
    public class DeterminationDepthOfTheRetainingWallBelowBottomOfPitHelper
    {
        /// <summary>
        /// Определение глубины заделки подпорной стены ниже дна котлована
        /// </summary>
        /// <param name="input">Параметры для расчетов глубины заделки подпорной стены ниже дна котлована</param>
        /// <returns>Глубина заделки подпорной стены ниже дна котлована</returns>
        public static double GetDepthOfSealingOfRetainingWallBelowTheBottomOfPit(InputNumbersDto input)
        {
            var gamma1 = input.gamma2 / 1;
            var fi1 = input.fi2 / 1.15;
            var c1 = input.c2 / 1.5;
            var teta = 45 - fi1 / 2;

            //Координата точки начала эпюры активного давления грунта
            var hc = (2 * c1) / (gamma1 * (Math.Tan(teta * (Math.PI / 180))));

            //проекция ширины фундамента здания №1 bf (полосы с пригрузом q ) на подпорную стенку
            var Hq = input.bf / (Math.Tan(teta * (Math.PI / 180)));

            //Определяем равнодействующую бокового давления, возникающую от фундамента № 1
            var Eq = DeterminationPressureHelper.CalculateSidePressure(input, Hq);

            //Расстояние от поверхности земли до начала эпюры бокового давления от фундамента здания №1(пригрузка q) на подпорную стенку
            var hq = input.d1 + (input.L1 - (input.bf / 2)) / (Math.Tan(teta * (Math.PI / 180)));

            //Расстояние от центра тяжести эпюры до точки 0
            var rq = hq + (Hq / 2) - hc;

            //Выражение для определения равнодействующей активного давления грунта на подпорную стенку будет иметь вид
            var EaResult = GetEaParams(gamma1, teta, input.H, c1, hc);
            var A_Ea = EaResult[0];
            var B_Ea = EaResult[1];
            var C_Ea = EaResult[2];

            //Расстояние от равнодействующей активного давления грунта Е0 до точки 0 определится по выражению
            var RaResult = GetRaParams(input.H, hc);
            var A_Ra = RaResult[0];
            var B_Ra = RaResult[1];

            var A_Eara = A_Ea * A_Ra;
            var B_Eara = A_Ea * B_Ra + B_Ea * A_Ra;
            var C_Eara = B_Ea * B_Ra + C_Ea * A_Ra;
            var D_Eara = C_Ea * B_Ra;

            //Определяем трапециевидную эпюру пассивного давления грунта разбиваем на треугольную и прямоугольную составляющие и момент относительно точки 0
            var EprpResult = GetEprpParams(gamma1, fi1, input.H, c1, hc);
            var A_Eprp = EprpResult[0];
            var B_Eprp = EprpResult[1];
            var C_Eprp = EprpResult[2];

            //Вычисляем кубическое уравнение
            int tip = 0;
            double p1 = 0, p2 = 0, p3 = 0;
            Kardano(A_Eara - A_Eprp, B_Eara - B_Eprp, C_Eara - C_Eprp, D_Eara + Eq + rq,ref tip,ref p1,ref p2,ref p3);

            return Math.Round(p1,2);
        }

        /// <summary>
        /// Определение равнодействующей активного давления грунта на подпорную стенку
        /// </summary>
        /// <param name="gamma">Удельный вес грунта</param>
        /// <param name="teta">Угол к вертикали воздействия нагрузки на стенку</param>
        /// <param name="H">Глубина подземного сооружения</param>
        /// <param name="C">Удельное сцепление грунта</param>
        /// <param name="hc">Координата точки начала эпюры активного давления грунта</param>
        /// <returns>Параметры квадратного уравнения равнодействующей активного давления грунта на подпорную стенку</returns>
        private static double[] GetEaParams(double gamma, double teta, double H, double C, double hc)
        {
            var a = (gamma * (Math.Tan(teta * (Math.PI / 180)) * (Math.Tan(teta * (Math.PI / 180))))) / 2;
            var b = (2 * (gamma * H * (Math.Tan(teta * (Math.PI / 180)) * (Math.Tan(teta * (Math.PI / 180))))) - (2 * C * (Math.Tan(teta * (Math.PI / 180)))) - (hc * gamma * (Math.Tan(teta * (Math.PI / 180)) * (Math.Tan(teta * (Math.PI / 180)))))) / 2;
            var c = ((gamma * H * H * (Math.Tan(teta * (Math.PI / 180)) * (Math.Tan(teta * (Math.PI / 180))))) - (2 * C * H * (Math.Tan(teta * (Math.PI / 180)))) - (hc * gamma * H * (Math.Tan(teta * (Math.PI / 180)) * (Math.Tan(teta * (Math.PI / 180))))) + (hc * 2 * C * Math.Tan(teta * (Math.PI / 180)))) / 2;
            return new double[3] { a, b, c };
        }

        /// <summary>
        /// Определение расстояния от равнодействующей активного давления грунта Е0 до точки 0
        /// </summary>
        /// <param name="H">Глубина подземного сооружения</param>
        /// <param name="hc">Координата точки начала эпюры активного давления грунта</param>
        /// <returns>Параметры уравнения расстояния от равнодействующей активного давления грунта Е0 до точки 0</returns>
        private static double[] GetRaParams(double H, double hc)
        {
            var a = 2.0 / 3.0;
            var b = (2 * H - 2 * hc) / 3;
            return new double[2] { a, b };
        }

        /// <summary>
        /// Определение равнодействующей пассивного давления грунта на подпорную стенку
        /// </summary>
        /// <param name="gamma">Удельный вес грунта</param>
        /// <param name="fi1">Угол внутреннего трения</param>
        /// <param name="H">Глубина подземного сооружения</param>
        /// <param name="C">Удельное сцепление грунта</param>
        /// <param name="hc">Координата точки начала эпюры активного давления грунта</param>
        /// <returns>Параметры кубического уравнения равнодействующей пассивного давления грунта на подпорную стенку</returns>
        private static double[] GetEprpParams(double gamma, double fi1, double H, double C, double hc)
        {
            var corner = 45 + fi1 / 2;
            var a = (gamma * (Math.Tan(corner * (Math.PI / 180)) * (Math.Tan(corner * (Math.PI / 180))))) / 3;
            var b = (H * gamma * (Math.Tan(corner * (Math.PI / 180))) * (Math.Tan(corner * (Math.PI / 180)))) / 2 - (gamma * (Math.Tan(corner * (Math.PI / 180))) * (Math.Tan(corner * (Math.PI / 180))) * hc) / 2 + (C * (Math.Tan(corner * (Math.PI / 180))));
            var c = (2 * C * H * (Math.Tan(corner * (Math.PI / 180)))) - (2 * C * (Math.Tan(corner * (Math.PI / 180))) * hc);
            return new double[3] { a, b, c };
        }

        /// <summary>
        /// Решение кубического уравнения (ax3 + bx2 + cx + d = 0) по формуле Кардано
        /// </summary>
        /// <param name="a">Коэффициент a</param>
        /// <param name="b">Коэффициент b</param>
        /// <param name="c">Коэффициент c</param>
        /// <param name="d">Коэффициент d</param>
        /// <param name="tip">Тип корней</param>
        /// <param name="p1">Корень 1</param>
        /// <param name="p2">Корень 2</param>
        /// <param name="p3">Корень 3</param>
        /// <returns>Параметры кубического уравнения равнодействующей пассивного давления грунта на подпорную стенку</returns>
        private static void Kardano(double a, double b, double c, double d, ref int tip, ref double p1, ref double p2, ref double p3)
        {
            double eps = 1E-14;
            double p = (3 * a * c - b * b) / (3 * a * a);
            double q = (2 * b * b * b - 9 * a * b * c + 27 * a * a * d) / (27 * a * a * a);
            double det = q * q / 4 + p * p * p / 27;
            if (Math.Abs(det) < eps)
                det = 0;
            if (det > 0)
            {
                tip = 1; // один вещественный, два комплексных корня
                double u = -q / 2 + Math.Sqrt(det);
                u = Math.Exp(Math.Log(u) / 3);
                double yy = u - p / (3 * u);
                p1 = yy - b / (3 * a); // первый корень
                p2 = -(u - p / (3 * u)) / 2 - b / (3 * a);
                p3 = Math.Sqrt(3) / 2 * (u + p / (3 * u));
            }
            else
            {
                if (det < 0)
                {
                    tip = 2; // три вещественных корня
                    double fi;
                    if (Math.Abs(q) < eps) // q=0
                        fi = Math.PI / 2;
                    else
                    {
                        if (q < 0) // q<0
                            fi = Math.Atan(Math.Sqrt(-det) / (-q / 2));
                        else // q<0
                            fi = Math.Atan(Math.Sqrt(-det) / (-q / 2)) + Math.PI;
                    }
                    double r = 2 * Math.Sqrt(-p / 3);
                    p1 = r * Math.Cos(fi / 3) - b / (3 * a);
                    p2 = r * Math.Cos((fi + 2 * Math.PI) / 3) - b / (3 * a);
                    p3 = r * Math.Cos((fi + 4 * Math.PI) / 3) - b / (3 * a);
                }
                else // det=0
                {
                    if (Math.Abs(q) < eps)
                    {
                        tip = 4; // 3-х кратный 
                        p1 = -b / (3 * a); // 3-х кратный 
                        p2 = -b / (3 * a);
                        p3 = -b / (3 * a);
                    }
                    else
                    {
                        tip = 3; // один и два кратных
                        double u = Math.Exp(Math.Log(Math.Abs(q) / 2) / 3);
                        if (q < 0)
                            u = -u;
                        p1 = -2 * u - b / (3 * a);
                        p2 = u - b / (3 * a);
                        p3 = u - b / (3 * a);
                    }
                }
            }
        }
    }
}
