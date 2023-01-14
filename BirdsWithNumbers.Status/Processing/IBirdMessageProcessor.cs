using System;
using BirdsWithNumbers.Status.Messaging;

namespace BirdsWithNumbers.Status.Processing
{
	/// <summary>
	/// Represents an object that processes a BirdMessage.
	/// </summary>
	public interface IBirdMessageProcessor
	{
		/// <summary>
		/// Processes the received message.
		/// </summary>
		/// <param name="message">The message to process.</param>
		void Add(BirdMessage message);
	}
}

