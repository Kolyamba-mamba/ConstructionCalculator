namespace ConstructionCalculator.Api.Models.DTO
{
    public class InputNumbersDto
    {
        //Глубина подземного сооружения
        public double H { get; set; }
        //Ширина подземного сооружения
        public double B { get; set; }
        //Шаг распорок
        public double S { get; set; }
        //Глубина заложения фундамента здания №1
        public double d1 { get; set; }
        //Ширина подошвы фундамента здания №1
        public double bf { get; set; }
        //Давление под подошвой фундамента здания №1
        public double q { get; set; }
        //Расстояние от оси фундамента здания №1 до подпорной стены
        public double L1 { get; set; }
        //Вид грунта основания - супесь
        //Мощность слоя
        public double Power { get; set; }
        //Характеристики грунта
        //удельный вес грунта
        public double gamma2 { get; set; }
        //удельное сцепление
        public double c2 { get; set; }
        // угол внутреннего трения
        public double fi2 { get; set; }
    }
}
