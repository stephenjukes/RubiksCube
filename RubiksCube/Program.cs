using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RubiksCube;

// Modeled in accordance with https://rubiks-cube-solver.com/

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();

// Use IOptions if possible
var initialFaceColors = config
    .GetSection("InitialFaceColors")
    .GetChildren()
    .ToDictionary(
        item => ToEnum<Orientation>(item.Key),
        item => ToEnum<Hue>(item.Value));

var cube = new Cube(initialFaceColors);

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

static T ToEnum<T>(string value) where T : struct, IConvertible
{
    _ = Enum.TryParse(value, out T myEnum);
    return myEnum;
}