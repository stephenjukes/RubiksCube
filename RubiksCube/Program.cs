// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using RubiksCube;

var cube = new Cube();

var rotations = new Instruction[]
{
    new Instruction(Orientation.Front, Direction.Clockwise),
};

Console.WriteLine("Initial State ...\n");
cube.Display();

foreach (var rotation in rotations)
{
    var orientation = rotation.Orientation;
    var direction = rotation.Direction;

    Console.WriteLine($"{direction} {orientation} Rotation ...\n");

    cube.Rotate(orientation, direction);
    cube.Display();
}