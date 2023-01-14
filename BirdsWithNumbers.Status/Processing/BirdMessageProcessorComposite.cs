using System;
using System.Diagnostics.CodeAnalysis;
using BirdsWithNumbers.Status.Messaging;

namespace BirdsWithNumbers.Status.Processing;

/// <summary>
/// Represents a composite bird processor that Adds messages to an internal list of other processors.
/// </summary>
public class BirdMessageProcessorComposite : IBirdMessageProcessor
{
    public IEnumerable<IBirdMessageProcessor> Processors { get; private set; }

	public BirdMessageProcessorComposite([DisallowNull] IEnumerable<IBirdMessageProcessor> processors)
	{
        this.Processors = processors;
	}

    public void Add(BirdMessage message)
    {
        foreach(IBirdMessageProcessor processor in Processors)
        {
            processor.Add(message);
        }
    }
}

