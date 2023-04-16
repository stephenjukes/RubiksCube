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
            // this seems to be the opposite way round, but actually means that a face takes on the color of the previous face
            // try to make this a bit more intuitive
            { Direction.Clockwise, index => (index - 1).Mod(4) },    
            { Direction.AntiClockwise, index => (index + 1).Mod(4) }
        };

        public Cubelet(Coordinate coordinate, IEnumerable<CubeletFace> faces)
        {
            Coordinate = coordinate;
            Faces = faces.ToArray();
        }

        public Coordinate Coordinate { get; set; }

        // Cannot be IEnumerable
        // https://stackoverflow.com/questions/43856404/object-property-not-updating
        public CubeletFace[] Faces { get; set; }

        public CubeletFace GetFace(Orientation orientation)
        {
            return Faces.Single(face => face.Orientation == orientation);
        }

        public void Rotate(Orientation orientation, Direction direction)
        {
            //Console.WriteLine(this);

            var clockwiseRotations = _clockwiseRotations[orientation];
            var staticFaces = Faces
                .Select(f => new CubeletFace1 { Orientation = f.Orientation, Color = f.Color })
                .DeepClone();

            foreach (var face in Faces)
            {
                var currentOrientationIndex = Array.IndexOf(clockwiseRotations, face.Orientation);
                if (currentOrientationIndex == -1) continue;

                var sourceFaceIndex = _rotationFuncs[direction](currentOrientationIndex);
                var sourceFaceOrientation = clockwiseRotations[sourceFaceIndex];

                var sourceFaceColor = staticFaces
                    .Single(face => face.Orientation == sourceFaceOrientation)
                    .Color;

                face.Color = sourceFaceColor;
            }

            //Console.WriteLine(this);
        }

        public override string ToString()
        {
            return Coordinate + ": " + string.Join(" - ", Faces.Select(f => $"{f.Orientation}:{f.Color?.Hue}"));
        }
    }
}
