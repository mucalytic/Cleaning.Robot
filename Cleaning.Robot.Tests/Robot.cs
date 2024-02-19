namespace Cleaning.Robot.Tests;

public class Robot
{
    private readonly List<Square> _squares;
    private int[] _directions = [1, 0, -1, 0];

    public Robot(IReadOnlyList<string> input)
    {
        _squares = input.SelectMany((row, y) => row.Select((_, x) => new Square(x, y, input[y][x]))).ToList();
        CurrentSquare = _squares.Single(s => s.IsStart());
    }

    public Square CurrentSquare { get; private set; }
    
    public Direction Direction => new(_directions[0], _directions[1]);

    public void RotateAntiClockwise() =>
        _directions = _directions.Skip(1).Concat(_directions.Take(1)).ToArray();
    
    public bool TryToMove(Direction direction)
    {
        var nextLocation = (CurrentSquare.Location.X + direction.Dx, CurrentSquare.Location.Y + direction.Dy);
        var nextSquare   = _squares.SingleOrDefault(square => square.Location == nextLocation);
        if (nextSquare is null || nextSquare.IsObstacle()) return false;
        CurrentSquare = nextSquare;
        return true;
    }

    public int ReportNumberOfSquaresCleaned() => _squares.Count(square => square.IsCleaned());
}
