namespace Cleaning.Robot.Tests;

public class Square(int x, int y, char typeOfSquare)
{
    private readonly Dictionary<Direction, bool> _directionsCleaned = new()
    {
        [new Direction(1, 0)]  = false,
        [new Direction(0, 1)]  = false,
        [new Direction(-1, 0)] = false,
        [new Direction(0, -1)] = false
    };

    public (int X, int Y) Location => (x, y);
    public bool IsStart() => typeOfSquare is 's';
    public bool IsObstacle() => typeOfSquare is 'o';
    
    public bool TryToClean(Direction direction)
    {
        if (_directionsCleaned[direction]) return false;
        _directionsCleaned[direction] = true;
        return true;
    }
    
    public bool IsCleaned() => _directionsCleaned.Any(d => d.Value);
}
