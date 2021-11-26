using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionCalculator.Api.Models.DTO
{
    public class CalculateResultDto
    {
        public double h { get; set; }
        public double Np { get; set; }
        public double Mmax { get; set; }
        public double As { get; set; }

        public CalculateResultDto(double h, double Np, double Mmax, double As)
        {
            this.h = h;
            this.Np = Np;
            this.Mmax = Mmax;
            this.As = As;
        }
    }
}
