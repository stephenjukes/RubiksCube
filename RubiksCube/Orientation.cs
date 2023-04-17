using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public enum Orientation
    {
        Top,
        Bottom,
        Front,
        Back,
        Left,
        Right
    }

    public interface IOrientation
    {

    } 

    public class TopOrientation
    {
        public string Type { get; set; }



        public Func<IEnumerable<Cubelet>, IEnumerable<IGrouping<int, Cubelet>>> ArrangeCubelets { get; } = 
            cubes => cubes
                .OrderByDescending(c => c.Coordinate.Y)
                .ThenBy(c => c.Coordinate.X)
                .GroupBy(c => c.Coordinate.Y);
    }
}
