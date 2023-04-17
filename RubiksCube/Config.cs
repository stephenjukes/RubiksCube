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
        public static ReadOnlyCollection<CubeletFace> InitialFaces => new List<CubeletFace>
        {
            new CubeletFace { Orientation = Orientation.Top,    Color = Colors[Hue.Green] },
            new CubeletFace { Orientation = Orientation.Bottom, Color = Colors[Hue.White] },
            new CubeletFace { Orientation = Orientation.Front,  Color = Colors[Hue.Red] },
            new CubeletFace { Orientation = Orientation.Back,   Color = Colors[Hue.Yellow] },
            new CubeletFace { Orientation = Orientation.Left,   Color = Colors[Hue.Blue] },
            new CubeletFace { Orientation = Orientation.Right,  Color = Colors[Hue.Orange] },
        }.AsReadOnly(); // This doesn't seem to be working as ReadOnly

        public static readonly Dictionary<Orientation, Func<IEnumerable<Cubelet>, IEnumerable<IGrouping<int, Cubelet>>>> ArrangeCubelets
            = new Dictionary<Orientation, Func<IEnumerable<Cubelet>, IEnumerable<IGrouping<int, Cubelet>>>>
        {
            { Orientation.Front, cubes => cubes.OrderByDescending(c => c.Coordinate.Y).ThenBy(c => c.Coordinate.X).GroupBy(c => c.Coordinate.Y) },
            { Orientation.Back, cubes => cubes.OrderByDescending(c => c.Coordinate.Y).ThenByDescending(c => c.Coordinate.X).GroupBy(c => c.Coordinate.Y) },
            { Orientation.Left, cubes => cubes.OrderByDescending(c => c.Coordinate.Y).ThenByDescending(c => c.Coordinate.Z).GroupBy(c => c.Coordinate.Y) },
            { Orientation.Right, cubes => cubes.OrderByDescending(c => c.Coordinate.Y).ThenBy(c => c.Coordinate.Z).GroupBy(c => c.Coordinate.Y) },
            { Orientation.Top, cubes => cubes.OrderByDescending(c => c.Coordinate.Z).ThenBy(c => c.Coordinate.X).GroupBy(c => c.Coordinate.Z) },
            { Orientation.Bottom, cubes => cubes.OrderBy(c => c.Coordinate.Z).ThenBy(c => c.Coordinate.X).GroupBy(c => c.Coordinate.Z) }
        };
    }
}
