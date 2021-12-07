using ConstructionCalculator.Api.Models.DTO;
using System;
using System.Collections.Generic;

namespace ConstructionCalculator.Api.Helpers
{
    public class ValidateInputDataHelper
    {
        /// <summary>
        /// Проверка параметров дто для расчетов
        /// </summary>
        /// <param name="inputNumber">Входные параметры для расчетов</param>
        /// <returns>Текст ошибки</returns>
        public string ValidateInputData(InputNumbersDto inputNumber)
        {
            var errors = new List<string>();
            if (inputNumber == null)
                return "Входные параметры не могут быть пустыми, проверьте корректность ввода.";
            if (inputNumber.H <= 0)
                errors.Add("Глубина подземного сооружения");
            if (inputNumber.B <= 0)
                errors.Add("Ширина подземного сооружения");
            if (inputNumber.S <= 0)
                errors.Add("Шаг распорок");
            if (inputNumber.d1 <= 0)
                errors.Add("Глубина заложения фундамента здания №1");
            if (inputNumber.bf <= 0)
                errors.Add("Ширина подошвы фундамента здания №1");
            if (inputNumber.q <= 0)
                errors.Add("Давление под подошвой фундамента здания №1");
            if (inputNumber.L1 <= 0)
                errors.Add("Расстояние от оси фундамента здания №1 до подпорной стены");
            if (inputNumber.Power <= 0)
                errors.Add("Мощность слоя");
            if (inputNumber.gamma2 <= 0)
                errors.Add("Удельный вес грунта");
            if (inputNumber.c2 <= 0)
                errors.Add("Удельное сцепление");
            if (inputNumber.fi2 <= 0)
                errors.Add("Угол внутреннего трения");

            if (errors.Count > 0)
                return String.Concat("Входные параметры ", String.Join(", ", errors), " должны быть больше 0.");
            else
                return null;
        }
    }
}
