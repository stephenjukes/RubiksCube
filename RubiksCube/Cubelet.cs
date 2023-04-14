using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public class Cubelet
    {
        private static readonly Dictionary<Orientation, Orientation[]> _clockwiseRotations = new Dictionary<Orientation, Orientation[]>
        {
            { Orientation.Top, new Orientation[] { Orientation.Front, Orientation.Left, Orientation.Back, Orientation.Right } },
            { Orientation.Bottom, new Orientation[] { Orientation.Front, Orientation.Right, Orientation.Back, Orientation.Left } },
            { Orientation.Front, new Orientation[] { Orientation.Top, Orientation.Right, Orientation.Bottom, Orientation.Left } },
            { Orientation.Back, new Orientation[] { Orientation.Top, Orientation.Left, Orientation.Bottom, Orientation.Right } },
            { Orientation.Left, new Orientation[] { Orientation.Top, Orientation.Front, Orientation.Bottom, Orientation.Back } },
            { Orientation.Right, new Orientation[] { Orientation.Top, Orientation.Back, Orientation.Bottom, Orientation.Front } },
        };

        private static readonly Dictionary<Direction, Func<int, int>> _rotationFuncs = new Dictionary<Direction, Func<int, int>>
        {
            // try to address magic number 4
            { Direction.Clockwise, orientationIndex => (orientationIndex + 1) % 4 },    
            { Direction.AntiClockwise, orientationIndex => (orientationIndex - 1) % 4 }, // this will likely need the more complex mod for -ve numbers
        };

        public Cubelet(Coordinate coordinate, Dictionary<Orientation, CubeletFace> faces)
        {
            Coordinate = coordinate;
            Faces = faces;
        }

        public Coordinate Coordinate { get; set; }

        public Dictionary<Orientation, CubeletFace> Faces { get; set; }

        public void Rotate(Orientation orientation, Direction direction)
        {
            foreach (var face in Faces)
            {
                var clockwiseRotations = _clockwiseRotations[orientation];
                var currentOrientationIndex = Array.IndexOf(clockwiseRotations, face.Value.Orientation);

                if (currentOrientationIndex == -1) continue;

                var newOrientationIndex = _rotationFuncs[direction](currentOrientationIndex);
                var newOrientation = clockwiseRotations[newOrientationIndex];
                face.Value.Orientation = newOrientation;
            }
        }
    }
}
