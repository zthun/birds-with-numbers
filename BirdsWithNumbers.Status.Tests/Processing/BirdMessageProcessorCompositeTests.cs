using System;
using BirdsWithNumbers.Status.Messaging;
using BirdsWithNumbers.Status.Processing;
using Moq;

namespace BirdsWithNumbers.Status.Tests.Processing;

public class BirdMessageProcessorCompositeTests
{
	private Mock<IBirdMessageProcessor> processorA;
	private Mock<IBirdMessageProcessor> processorB;

	private BirdMessageProcessorComposite CreateTestTarget()
	{
		return new BirdMessageProcessorComposite(new IBirdMessageProcessor[]
		{
			processorA.Object,
			processorB.Object
		});
	}

	public BirdMessageProcessorCompositeTests()
	{
		processorA = new Mock<IBirdMessageProcessor>();
		processorB = new Mock<IBirdMessageProcessor>();
	}

	[Fact]
	public void ShouldInvokeAddOnAllInternalProcessors()
	{
		// Arrange
		var target = CreateTestTarget();
		var expected = new BirdMessage();

		// Act
		target.Add(expected);

		// Assert
		processorA.Verify((p) => p.Add(expected));
		processorB.Verify((p) => p.Add(expected));
	}
}


