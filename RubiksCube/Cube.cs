using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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
            //AssignColor();
        }

        public void Rotate(Orientation orientation, Direction direction)
        {
            var face = Config.InitialFaces.Single(f => f.Orientation == orientation);
            var cubelets = _cubelets.Where(face.IsOfOrientation);

            var cubeletRing = cubelets
                .Where(c => !(c.Coordinate.Y == 0 && c.Coordinate.X == 0))
                .OrderBy(c =>
                {
                    var dividend = c.Coordinate.Y;
                    double divisor = c.Coordinate.X;
                    if (divisor == 0) divisor = 0.0001;

                    return Math.Atan2(dividend, divisor);
                });
        
            foreach(var c in cubeletRing)
            {
                Console.WriteLine($"{c.Coordinate.X}, {c.Coordinate.Y}: {Math.Atan2(c.Coordinate.Y, (c.Coordinate.X != 0 ? c.Coordinate.X : 000.1))}");
            }


            foreach (var cubelet in cubelets)
            {
                cubelet.Rotate(orientation, direction);
            }
        }

        // Some of this code is repeated
        public void Display()
        {
            foreach (var initialFace in Config.InitialFaces)
            {
                var cubeletFaces = _cubelets
                    .Where(initialFace.IsOfOrientation)
                    .OrderBy(c => c.Coordinate.X)
                    .ThenBy(c => c.Coordinate.Y)
                    .ThenBy(c => c.Coordinate.Z)
                    .Select(c => c.Faces[initialFace.Orientation].Color.Symbol);

                // Can this be returned as a string before being displayed?
                Console.WriteLine(initialFace.Orientation);
                Console.WriteLine(string.Join(" ", cubeletFaces));
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
                        var faces = Config.InitialFaces
                            .ToDictionary(
                                face => face.Orientation,
                                face => face);

                        _cubelets.Add(new Cubelet(coordinate, faces));
                    }
                }
            }
        }

        //private void AssignColor()
        //{
        //    foreach(var initialFace in Config.InitialFaces)
        //    {
        //        var cubeletFaces = _cubelets.Where(initialFace.IsOfOrientation);

        //        foreach(var cubelet in cubeletFaces)
        //        {
        //            cubelet.Faces[initialFace.Orientation] = initialFace.Color;
        //        }
        //    }
        //}
    }
}
