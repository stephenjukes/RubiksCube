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
        public Face(Orientation orientation, Func<Cubelet, bool> isOfOrientation)
        {
            Orientation = orientation;
            IsOfOrientation = isOfOrientation;
        }

        public Orientation Orientation { get; set; }

        public int OrientationIndex { get; set; }

        public Func<Cubelet, bool> IsOfOrientation { get; set; }
    }

    public class CubeletFace : Face
    {
        public CubeletFace(
            Orientation orientation,
            Color color,
            Func<Cubelet, bool> isOfOrientation
            ) : base(orientation, isOfOrientation)
        {
            Color = color;
        }

        public Color Color { get; set; }
    }
}
