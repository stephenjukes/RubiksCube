using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public static class Config
    {
        // Remove once this logic has been transferred to UI logic
        public static Dictionary<Hue, Color> Colors = new Dictionary<Hue, Color>
        {
            { Hue.Green,    new Color(Hue.Green, 'G', ConsoleColor.Green) },
            { Hue.White,    new Color(Hue.White, 'W', ConsoleColor.White) },
            { Hue.Red,      new Color(Hue.Red, 'R', ConsoleColor.Red) },
            { Hue.Yellow,   new Color(Hue.Yellow, 'Y', ConsoleColor.Yellow) },
            { Hue.Blue,     new Color(Hue.Blue, 'B', ConsoleColor.Blue) },
            { Hue.Orange,   new Color(Hue.Orange, 'O', ConsoleColor.Magenta) },
        };
    }
}

