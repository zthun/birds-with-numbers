using System;
using BirdsWithNumbers.Status.Messaging;
using BirdsWithNumbers.Status.Processing;
using Moq;

namespace BirdsWithNumbers.Status.Tests.Processing;

public class BirdCalculatorTopTests
{
    const string HASHTAG_PHOTOGRAPHY = "photography";
    const string HASHTAG_FUNNY = "funny";
    const string HASHTAG_PETS = "pets";

    private readonly BirdCount<string> funny;
	private readonly BirdCount<string> photography;
	private readonly BirdCount<string> pets;

	private Mock<IBirdCalculator<IEnumerable<BirdCount<string>>>> all;

    public BirdCalculatorTop<string> CreateTestTarget(int top)
    {
        return new BirdCalculatorTop<string>(top, all.Object);
    }

    public BirdCalculatorTopTests()
	{
		funny = new BirdCount<string>(HASHTAG_FUNNY)
		{
			Count = 3
		};

		photography = new BirdCount<string>(HASHTAG_PHOTOGRAPHY)
		{
			Count = 7
		};

		pets = new BirdCount<string>(HASHTAG_PETS)
		{
			Count = 10
		};

		all = new Mock<IBirdCalculator<IEnumerable<BirdCount<string>>>>();
		all.SetupGet((c) => c.Current).Returns(new BirdCount<string>[]
		{
			funny,
			pets,
			photography,
		});
	}

	[Fact]
	public void ShouldAddTheCountToTheInnerCalculator()
	{
        // Arrange
        var target = CreateTestTarget(10);
		var expected = new BirdMessage();

		// Act
		target.Add(expected);

		// Assert
		all.Verify((c) => c.Add(expected));
    }

	[Fact]
	public void ShouldOrderCountsInDescendingOrder()
	{
        // Arrange
        var target = CreateTestTarget(10);
        var expected = $"{pets.Data}({pets.Count});{photography.Data}({photography.Count});{funny.Data}({funny.Count})";

        // Act
        var actual = String.Join(";", target.Current.Select((c) => c.ToString()));

        // Assert
        Assert.Equal(expected, actual);
    }

	[Fact]
	public void ShouldTakeTheTopCountsOnly()
	{
        // Arrange
        var target = CreateTestTarget(2);
        var expected = $"{pets.Data}({pets.Count});{photography.Data}({photography.Count})";

        // Act
        var actual = String.Join(";", target.Current.Select((c) => c.ToString()));

        // Assert
        Assert.Equal(expected, actual);
    }
}

