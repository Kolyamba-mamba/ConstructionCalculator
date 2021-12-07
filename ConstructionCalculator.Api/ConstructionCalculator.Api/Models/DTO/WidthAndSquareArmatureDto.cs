using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Models.DTO
{
    public class WidthAndSquareArmatureDto
    {
        /// <summary>
        /// Максимальное значение изгибающего момента в стенке
        /// <summary>
        public double Mmax { get; set; }
        /// <summary>
        /// Требуемая площадь сечения поперечной арматуры
        /// <summary>
        public double Square { get; set; }

        public WidthAndSquareArmatureDto(double mmax, double square)
        {
            this.Mmax = mmax;
            this.Square = square;
        }
    }
}
