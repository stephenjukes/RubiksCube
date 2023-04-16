using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    //public enum Face
    //{
    //    Top,
    //    Bottom,
    //    Front,
    //    Back,
    //    Left,
    //    Right
    //}

    public class Face
    {
        public Orientation Orientation { get; set; }

        public Func<Coordinate, bool> IsOfOrientation { get; set; }
    }

    public class CubeletFace : Face
    {
        public Color Color { get; set; }

        public override string ToString() => $"{Orientation}: {Color}";
    }

    // New class needed since cannot deep clone Func
    public class CubeletFace1
    {
        public Orientation Orientation { get; set; }

        public Color Color { get; set; }

        public override string ToString()
        {
            return $"{Orientation}: {Color}";
        }
    }
}
