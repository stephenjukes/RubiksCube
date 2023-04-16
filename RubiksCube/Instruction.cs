namespace RubiksCube
{
    public class Instruction
    {
        public Instruction(Orientation orientation, Direction direction)
        {
            Orientation = orientation;
            Direction = direction;
        }

        public Orientation Orientation { get; set; }

        public Direction Direction { get; set; }
    }
}
