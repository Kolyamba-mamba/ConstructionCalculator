using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Models.DTO
{
    public class WidthAndSquareArmatureDto
    {
        public double Mmax { get; set; }
        public double Square { get; set; }

        public WidthAndSquareArmatureDto(double mmax, double square)
        {
            this.Mmax = mmax;
            this.Square = square;
        }
    }
}
