using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public class Face
    {
        public Face(Orientation orientation)
        {
            Orientation = orientation;
        }

        public Orientation Orientation { get; }
    }

    public class CubeFace : Face
    {
        public CubeFace(
            Orientation orientation,
            Func<Coordinate, bool> hasCoordinate,
            Func<Coordinate, double> getRotationRank,
            Func<IEnumerable<Cubelet>, IEnumerable<IGrouping<int, Cubelet>>> arrangeForDisplay
            ) : base(orientation)
        {
            HasCoordinate = hasCoordinate;
            GetRotationRank = getRotationRank;
            ArrangeForDisplay = arrangeForDisplay;
        }

        public Func<IEnumerable<Cubelet>, IEnumerable<IGrouping<int, Cubelet>>> ArrangeForDisplay { get; }

        public Func<Coordinate, bool> HasCoordinate { get; }

        public Func<Coordinate, double> GetRotationRank { get; }
    }


    public class CubeletFace : Face
    {
        public CubeletFace(
            Orientation orientation, 
            Color color
            ) : base(orientation)
        {
            Color = color;
        } 

        public Color Color { get; set; }

        public override string ToString() => $"{Orientation}: {Color}";
    }
}
