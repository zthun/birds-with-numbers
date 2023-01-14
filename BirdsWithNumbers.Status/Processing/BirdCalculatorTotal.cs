using System;
using BirdsWithNumbers.Status.Messaging;

namespace BirdsWithNumbers.Status.Processing
{
	/// <summary>
	/// Calculates the total number of messages received.
	/// </summary>
	public class BirdCalculatorTotal : IBirdCalculator<long>
	{
		/// <summary>
		/// Gets the current count.
		/// </summary>
		public long Current { get; private set; } = 0;

		/// <summary>
		/// Adds 1 to the current count.
		/// </summary>
		/// <param name="message">The processed message.  Not actually used</param>
        public void Add(BirdMessage message)
        {
			++Current;
        }
    }
}

