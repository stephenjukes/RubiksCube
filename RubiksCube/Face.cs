using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public class Face
    {
        public Orientation Orientation { get; set; }
    }

    public class CubeletFace : Face
    {
        public Color Color { get; set; }

        public override string ToString() => $"{Orientation}: {Color}";
    }
}
