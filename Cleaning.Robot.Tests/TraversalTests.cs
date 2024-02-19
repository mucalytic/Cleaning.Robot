using FluentAssertions;

namespace Cleaning.Robot.Tests;

public class TraversalTests
{
    /* Robot starts off moving to the right.
     * Always turn counter-clockwise if you reach an obstacle.
     * Stop if you reach the start or an already cleaned square.
     */
    private static int Traverse(IReadOnlyList<string> input)
    {
        var robot = new Robot(input);
        do
        {
            if (robot.TryToMove(robot.Direction)) continue;
            robot.RotateAntiClockwise();
        }
        while (robot.CurrentSquare.TryToClean(robot.Direction));
        return robot.ReportNumberOfSquaresCleaned();
    }

    [Theory]
    [InlineData(new[] { "...o.", "s...o", "..o.o" }, 8)]
    [InlineData(new[] { "....o.", "...o..", ".s...o", ".oo...", "......", ".....o" }, 7)]
    [InlineData(new[] { "...o..", "..o...", ".....o", "o.....", ".s..o.", ".o...." }, 14)]
    public void Traverse_ShouldReturnCorrectNumberOfSquaresCleaned(string[] input, int expected) =>
        Traverse(input).Should().Be(expected);
}
