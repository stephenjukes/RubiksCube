using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public class RotationArrangement
    {
        public RotationArrangement(double dividend, double divisor)
        {
            Dividend = dividend;
            Divisor = divisor != 0 ? divisor : 0.0001;
        }

        public double Dividend { get; set; }

        public double Divisor { get; set; }

        public double OrderRank => Math.Atan2(Dividend, Divisor);
    }
}
