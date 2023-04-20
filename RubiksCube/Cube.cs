namespace RubiksCube
{
    public class Cube
    {
        private readonly Dictionary<Orientation, Hue> _initialFaceColors;
        private readonly int _size;
        private readonly int _boundaryCoordinate;
        private readonly List<Cubelet> _cubelets = new List<Cubelet>();
        private readonly Dictionary<Orientation, CubeFace> _faces;

        public Cube(
            Dictionary<Orientation, Hue> initialFaceColors,
            int size = 3
            )
        {
            _initialFaceColors = initialFaceColors;
            _size = size; 
            _boundaryCoordinate = size / 2;
            _faces = ConfigureFaces();
            PopulateCube();
        }

        public void Rotate(Orientation orientation, Direction direction)
        {
            var face = _faces[orientation];

            var rotationRing = _cubelets
                .Where(c => face.HasCoordinate(c.Coordinate) && !c.IsCenter)
                .OrderBy(c => (direction == Direction.Clockwise ? 1 : -1) * face.GetRotationRank(c.Coordinate))
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

        public void Display()
        {
            // Better to group faces instead?
            foreach (var orientation in _initialFaceColors.Keys)
            {
                // this could be done better
                var face = _faces[orientation];

                var cubeletsInFace = _cubelets
                    .Where(c => face.HasCoordinate(c.Coordinate));

                var orderedFaces = face.ArrangeForDisplay(cubeletsInFace)
                    .Select(group => string.Join(" ", group
                        .Select(cube => cube.GetFace(orientation).Color.Symbol)));

                var orderedString = string.Join("\n", orderedFaces);

                // Abstract UI out
                Console.WriteLine(orientation);
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
                        var faces = _initialFaceColors.ToArray()
                            .Select(kvp => AssignFaceColor(
                                new CubeletFace(kvp.Key, Config.Colors[kvp.Value]),
                                coordinate)
                            );

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

        private CubeletFace AssignFaceColor(CubeletFace cubeletFace, Coordinate coordinate)
        {
            var face = _faces[cubeletFace.Orientation];

            if (!face.HasCoordinate(coordinate))
            { 
                var color = new Color(Hue.Null, ' ', cubeletFace.Color.ConsoleColor);

                return new CubeletFace(cubeletFace.Orientation, color);
            }

            return cubeletFace;
        }

        private Dictionary<Orientation, CubeFace> ConfigureFaces()
        {
            return new CubeFace[]
            {
                new CubeFace(
                    Orientation.Top,
                    hasCoordinate: c => c.Y == _boundaryCoordinate,
                    getRotationRank: c => new RotationRankParameters(c.Z, c.X).OrderRank,
                    arrangeForDisplay: c => c
                        .OrderByDescending(c => c.Coordinate.Z)
                        .ThenBy(c => c.Coordinate.X)
                        .GroupBy(c => c.Coordinate.Z)
                    ),

                new CubeFace(
                    Orientation.Bottom,
                    hasCoordinate: c => c.Y == -_boundaryCoordinate,
                    getRotationRank: c => new RotationRankParameters(-c.Z, c.X).OrderRank,
                    arrangeForDisplay: c => c
                        .OrderBy(c => c.Coordinate.Z)
                        .ThenBy(c => c.Coordinate.X)
                        .GroupBy(c => c.Coordinate.Z)
                    ),

                new CubeFace(
                    Orientation.Front,
                    hasCoordinate: c => c.Z == -_boundaryCoordinate,
                    getRotationRank: c => new RotationRankParameters(c.Y, c.X).OrderRank,
                    arrangeForDisplay: c => c
                        .OrderByDescending(c => c.Coordinate.Y)
                        .ThenBy(c => c.Coordinate.X)
                        .GroupBy(c => c.Coordinate.Y)
                    ),

                new CubeFace(
                    Orientation.Back,
                    hasCoordinate: c => c.Z == _boundaryCoordinate,
                    getRotationRank: c => new RotationRankParameters(c.Y, -c.X).OrderRank,
                    arrangeForDisplay: c => c
                        .OrderByDescending(c => c.Coordinate.Y)
                        .ThenByDescending(c => c.Coordinate.X)
                        .GroupBy(c => c.Coordinate.Y)
                    ),

                new CubeFace(
                    Orientation.Left,
                    hasCoordinate:  c => c.X == -_boundaryCoordinate,
                    getRotationRank: c => new RotationRankParameters(c.Y, -c.Z).OrderRank,
                    arrangeForDisplay: c => c
                        .OrderByDescending(c => c.Coordinate.Y)
                        .ThenByDescending(c => c.Coordinate.Z)
                        .GroupBy(c => c.Coordinate.Y)
                    ),

                new CubeFace(
                    Orientation.Right,
                    hasCoordinate: c => c.X == _boundaryCoordinate,
                    getRotationRank: c => new RotationRankParameters(c.Y, c.Z).OrderRank,
                    arrangeForDisplay: c => c
                        .OrderByDescending(c => c.Coordinate.Y)
                        .ThenBy(c => c.Coordinate.Z)
                        .GroupBy(c => c.Coordinate.Y)
                    )
            }.ToDictionary(face => face.Orientation);
        }
    }
}
