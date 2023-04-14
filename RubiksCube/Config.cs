using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public static class Config
    {
        public static Dictionary<Hue, Color> Colors = new Dictionary<Hue, Color>
        {
            { Hue.Green,    new Color(Hue.Green, 'G', ConsoleColor.Green) },
            { Hue.White,    new Color(Hue.White, 'W', ConsoleColor.White) },
            { Hue.Red,      new Color(Hue.Red, 'R', ConsoleColor.Red) },
            { Hue.Yellow,   new Color(Hue.Yellow, 'Y', ConsoleColor.Yellow) },
            { Hue.Blue,     new Color(Hue.Blue, 'B', ConsoleColor.Blue) },
            { Hue.Orange,   new Color(Hue.Orange, 'O', ConsoleColor.Magenta) },
        };

        // color isn't alwasy needed. Do we need inheritance?
        // is this really config?
        public static CubeletFace[] InitialFaces = new CubeletFace[]
        {
            new CubeletFace(Orientation.Top, Colors[Hue.Green], cubelet => cubelet.Coordinate.Y == 1),
            new CubeletFace(Orientation.Bottom, Colors[Hue.White], cubelet => cubelet.Coordinate.Y == -1),
            new CubeletFace(Orientation.Front, Colors[Hue.Red], cubelet => cubelet.Coordinate.Z == -1),
            new CubeletFace(Orientation.Back, Colors[Hue.Yellow], cubelet => cubelet.Coordinate.Z == 1),
            new CubeletFace(Orientation.Left, Colors[Hue.Blue], cubelet => cubelet.Coordinate.X == -1),
            new CubeletFace(Orientation.Right, Colors[Hue.Orange], cubelet => cubelet.Coordinate.X == 1),
        };

        // is this really config?
        public static Dictionary<Orientation, Orientation[]> ClockwiseTurns = new Dictionary<Orientation, Orientation[]>
        {
            { Orientation.Top, new Orientation[] { Orientation.Front, Orientation.Left, Orientation.Back, Orientation.Right } },
            { Orientation.Bottom, new Orientation[] { Orientation.Front, Orientation.Right, Orientation.Back, Orientation.Left } },
            { Orientation.Front, new Orientation[] { Orientation.Top, Orientation.Right, Orientation.Bottom, Orientation.Left } },
            { Orientation.Back, new Orientation[] { Orientation.Top, Orientation.Left, Orientation.Bottom, Orientation.Right } },
            { Orientation.Left, new Orientation[] { Orientation.Top, Orientation.Front, Orientation.Bottom, Orientation.Back } },
            { Orientation.Right, new Orientation[] { Orientation.Top, Orientation.Back, Orientation.Bottom, Orientation.Front } },
        };
    }
}
