using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public class Coordinate
    {
        public Coordinate(int x, int y, int z)
        {
            X = x; 
            Y = y; 
            Z = z;
        }

        public int X { get; set; }

        public int Y { get; set; }    
        
        public int Z { get; set; }

        public override string ToString() => $"{X} : {Y} : {Z}";
    }
}
