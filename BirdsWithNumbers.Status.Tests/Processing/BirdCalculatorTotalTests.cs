using BirdsWithNumbers.Status.Messaging;
using BirdsWithNumbers.Status.Processing;

namespace BirdsWithNumbers.Status.Tests.Processing;

public class BirdCalculatorTotalTests
{
    private static BirdCalculatorTotal CreateTestTarget()
    {
        return new BirdCalculatorTotal();
    }

    [Fact]
    public void ShouldIncrementTheCountWhenANewMessageComesIn()
    {
        // Arrange.
        var target = CreateTestTarget();
        var expected = 5L;

        // Act.
        for(var i = 0; i < expected; ++i) { target.Add(new BirdMessage()); }
        var actual = target.Current;

        // Assert
        Assert.Equal(expected, actual);
    }
}
