namespace ConstructionCalculator.Api.Models.DTO
{
    /// <summary>
    /// Дто входящих для расчетов параметров
    /// </summary>
    public class InputNumbersDto
    {
        /// <summary>
        ///Глубина подземного сооружения
        /// <summary>
        public double H { get; set; }
        /// <summary>
        ///Ширина подземного сооружения
        /// <summary>
        public double B { get; set; }
        /// <summary>
        ///Шаг распорок
        /// <summary>
        public double S { get; set; }
        /// <summary>
        ///Глубина заложения фундамента здания №1
        /// <summary>
        public double d1 { get; set; }
        /// <summary>
        ///Ширина подошвы фундамента здания №1
        /// <summary>
        public double bf { get; set; }
        /// <summary>
        ///Давление под подошвой фундамента здания №1
        /// <summary>
        public double q { get; set; }
        /// <summary>
        ///Расстояние от оси фундамента здания №1 до подпорной стены
        /// <summary>
        public double L1 { get; set; }
        //Вид грунта основания - супесь
        /// <summary>
        ///Мощность слоя
        /// <summary>
        public double Power { get; set; }
        //Характеристики грунта
        /// <summary>
        ///удельный вес грунта
        /// <summary>
        public double gamma2 { get; set; }
        /// <summary>
        ///удельное сцепление
        /// <summary>
        public double c2 { get; set; }
        /// <summary>
        /// угол внутреннего трения
        /// <summary>
        public double fi2 { get; set; }
    }
}
