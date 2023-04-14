// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using RubiksCube;

var cube = new Cube();
cube.Display();

cube.Rotate(Orientation.Front, Direction.Clockwise);

cube.Display();
