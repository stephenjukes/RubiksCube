namespace RubiksCube
{
    public class Cube
    {
        private readonly List<Cubelet> _cubelets = new List<Cubelet>();

        // consider allowing this to be set at instantiation
        private readonly int _size = 3;
        
        public Cube()
        {
            PopulateCube();
        }

        public void Rotate(Orientation orientation, Direction direction)
        {
            var face = Config.InitialFaces.Single(f => f.Orientation == orientation);
            var cubeletRing = _cubelets
                .Where(c => face.IsOfOrientation(c.Coordinate))
                .Where(c => !(c.Coordinate.Y == 0 && c.Coordinate.X == 0))
                .OrderBy(Clockwise)
                .ToArray();

            var staticCoordinates = cubeletRing
                .Select(c => c.Coordinate)
                .ToArray()
                .DeepClone();

            for (var i = 0; i < cubeletRing.Length; i++)
            {
                var cubelet = cubeletRing[i];

                cubelet.Coordinate = staticCoordinates[(i + 2) % staticCoordinates.Length];
                cubelet.Rotate(orientation, direction);
            }
        }

        private double AntiClockwise(Cubelet cubelet)
        {
            var dividend = cubelet.Coordinate.Y;
            double divisor = cubelet.Coordinate.X;
            if (divisor == 0) divisor = 0.0001; // approximation of division by 0

            return Math.Atan2(dividend, divisor);
        }

        private double Clockwise(Cubelet cubelet)
        {
            return -AntiClockwise(cubelet);
        }

        // Some of this code is repeated
        public void Display()
        {
            // Better to group cubelets instead?
            foreach (var initialFace in Config.InitialFaces)
            {
                var cubeletsInFace = _cubelets.Where(c => initialFace.IsOfOrientation(c.Coordinate));

                var orderedFaces = Config
                    .OrderCubelets[initialFace.Orientation](cubeletsInFace)
                    .Select(group => string.Join(" ", group
                        .Select(cube => cube.GetFace(initialFace.Orientation).Color.Symbol)));

                var orderedString = string.Join("\n", orderedFaces);

                // Can this be returned as a string before being displayed?
                Console.WriteLine(initialFace.Orientation);
                Console.WriteLine(orderedString);
                Console.WriteLine();
            }
        }

        private void PopulateCube()
        {
            // remove magic  numbers
            for(var x = -1; x <= 1; x++)
            {
                for(var y = -1; y <= 1; y++)
                {
                    for(var z = -1; z <= 1; z++)
                    {
                        var coordinate = new Coordinate(x, y, z);
                        var faces = Config.InitialFaces.Select(f => AssignFaceColor(f, coordinate));

                        var cubelet = new Cubelet(coordinate, faces);

                        _cubelets.Add(new Cubelet(coordinate, faces));
                    }
                }
            }
        }

        private CubeletFace AssignFaceColor(CubeletFace face, Coordinate coordinate)
        {
            if (!face.IsOfOrientation(coordinate))
            { 
                var color = new Color(Hue.Null, ' ', face.Color.ConsoleColor);
                return new CubeletFace { Orientation = face.Orientation, Color = color, IsOfOrientation = face.IsOfOrientation };
            }

            return face;
        }
    }
}
