// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

// Modeled in accordance with https://rubiks-cube-solver.com/
// Use 'floating hidden sides' for verification

using RubiksCube;

var cube = new Cube();

var instructions = new Instruction[]
{
    new Instruction(Orientation.Front, Direction.Clockwise),
    new Instruction(Orientation.Right, Direction.AntiClockwise),
    new Instruction(Orientation.Top, Direction.Clockwise),
    new Instruction(Orientation.Back, Direction.AntiClockwise),
    new Instruction(Orientation.Left, Direction.Clockwise),
    new Instruction(Orientation.Bottom, Direction.AntiClockwise),
};

Console.WriteLine("Initial State ...\n");
cube.Display();

foreach (var instruction in instructions)
{
    var orientation = instruction.Orientation;
    var direction = instruction.Direction;

    Console.WriteLine($"{direction} {orientation} Rotation ...\n");

    cube.Rotate(orientation, direction);
    cube.Display();
}