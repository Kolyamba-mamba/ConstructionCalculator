namespace ConstructionCalculator.Api.Models.DTO
{
    /// <summary>
    /// Результат расчетов
    /// </summary>
    public class CalculateResultDto
    {
        /// <summary>
        /// Глубина заделки подпорной стенки ниже дна подземного сооружения
        /// <summary>
        public double h { get; set; }
        /// <summary>
        /// Усилие в распорке
        /// <summary>
        public double Np { get; set; }
        /// <summary>
        /// Максимальное значение изгибающего момента в стенке
        /// <summary>
        public double Mmax { get; set; }
        /// <summary>
        /// Требуемая площадь сечения поперечной арматуры
        /// <summary>
        public double As { get; set; }

        /// <summary>
        /// Конструктор класса результатов расчетов
        /// <summary>
        public CalculateResultDto(double h, double Np, double Mmax, double As)
        {
            this.h = h;
            this.Np = Np;
            this.Mmax = Mmax;
            this.As = As;
        }
    }
}
