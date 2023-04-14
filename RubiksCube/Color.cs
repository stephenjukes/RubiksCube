using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public class Color
    {
        public Color(Hue hue, char symbol, ConsoleColor consoleColor)
        {
            Hue = hue;
            Symbol = symbol;
            ConsoleColor = consoleColor;
        }

        public Hue Hue { get; set; }

        public char Symbol { get; set; }

        public ConsoleColor ConsoleColor { get; set; }

        public override string ToString() => Hue.ToString();
    }
}
