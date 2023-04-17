namespace RubiksCube
{
    public class Cube
    {
        private readonly List<Cubelet> _cubelets = new List<Cubelet>();

        private readonly int _size;
        private readonly int _boundaryCoordinate;
        
        public Cube(int size = 3)
        {
            _size = size; 
            _boundaryCoordinate = size / 2;

            PopulateCube();
        }

        public void Rotate(Orientation orientation, Direction direction)
        {
            var rotationRing = _cubelets
                .Where(c => IsInCubeFace(c.Coordinate, orientation) && !c.IsCenter)
                .OrderBy(c => direction == Direction.Clockwise ? Clockwise(c) : AntiClockwise(c))
                .ToArray();

            var staticCoordinates = rotationRing
                .Select(c => c.Coordinate)
                .ToArray()
                .DeepClone();

            for (var i = 0; i < rotationRing.Length; i++)
            {
                var cubelet = rotationRing[i];

                cubelet.Coordinate = staticCoordinates[(i + _size - 1) % staticCoordinates.Length];
                cubelet.Rotate(orientation, direction);
            }
        }

        private bool IsInCubeFace(Coordinate coordinate, Orientation orientation)
        {
            var cubeletsByFace = new Dictionary<Orientation, Func<Coordinate, bool>>
            {
                { Orientation.Top,      c => c.Y == _boundaryCoordinate },
                { Orientation.Bottom,   c => c.Y == -_boundaryCoordinate },
                { Orientation.Left,     c => c.X == -_boundaryCoordinate },
                { Orientation.Right,    c => c.X == _boundaryCoordinate },
                { Orientation.Front,    c => c.Z == -_boundaryCoordinate },
                { Orientation.Back,     c => c.Z == _boundaryCoordinate },
            };

            return cubeletsByFace[orientation](coordinate);
        }

        private double AntiClockwise(Cubelet cubelet)
        {
            var dividend = cubelet.Coordinate.Y;
            double divisor = cubelet.Coordinate.X;
            if (divisor == 0) divisor = 0.0001; // approximation of division by 0

            return Math.Atan2(dividend, divisor);
        }

        private double Clockwise(Cubelet cubelet) => -AntiClockwise(cubelet);

        public void Display()
        {
            // Better to group faces instead?
            foreach (var initialFace in Config.InitialFaces)
            {
                var cubeletsInFace = _cubelets
                    .Where(c => IsInCubeFace(c.Coordinate, initialFace.Orientation));

                var orderedFaces = Config
                    .ArrangeCubelets[initialFace.Orientation](cubeletsInFace)
                    .Select(group => string.Join(" ", group
                        .Select(cube => cube.GetFace(initialFace.Orientation).Color.Symbol)));

                var orderedString = string.Join("\n", orderedFaces);

                // Abstract UI out
                Console.WriteLine(initialFace.Orientation);
                Console.WriteLine(orderedString);
                Console.WriteLine();
            }
        }

        private void PopulateCube()
        {
            for (var x = -_boundaryCoordinate; x <= _boundaryCoordinate; x = IncrementAccordingToSize(x))
            {
                for(var y = -_boundaryCoordinate; y <= _boundaryCoordinate; y = IncrementAccordingToSize(y))
                {
                    for(var z = -_boundaryCoordinate; z <= _boundaryCoordinate; z = IncrementAccordingToSize(z))
                    {
                        var coordinate = new Coordinate(x, y, z);
                        var faces = Config.InitialFaces.Select(face => AssignFaceColor(face, coordinate));

                        var cubelet = new Cubelet(coordinate, faces);

                        _cubelets.Add(new Cubelet(coordinate, faces));
                    }
                }
            }
        }

        /// <summary>
        /// Ensures symmetry either side of zero for both odd and even sizes. Odd sizes will include a zero while even sizes will not
        /// </summary>
        /// <param name="i">iterator</param>
        /// <returns></returns>
        private int IncrementAccordingToSize(int i)
        {
            if (_size % 2 == 0 && i == -1)
            {
                return i + 2;
            }

            return i + 1;
        }

        private CubeletFace AssignFaceColor(CubeletFace face, Coordinate coordinate)
        {
            if (!IsInCubeFace(coordinate, face.Orientation))
            { 
                var color = new Color(Hue.Null, ' ', face.Color.ConsoleColor);
                
                return new CubeletFace 
                { 
                    Orientation = face.Orientation, 
                    Color = color
                };
            }

            return face;
        }
    }
}
