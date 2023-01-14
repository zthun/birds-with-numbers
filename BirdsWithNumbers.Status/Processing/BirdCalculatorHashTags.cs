using System;
using BirdsWithNumbers.Status.Messaging;

namespace BirdsWithNumbers.Status.Processing
{
	/// <summary>
	/// Represents a calculator that keeps track of the total count of hashtags seen.
	/// </summary>
	public class BirdCalculatorHashTags : IBirdCalculator<IEnumerable<BirdCount<string>>>
	{
		private readonly HashSet<BirdCount<string>> hashTags =
			new HashSet<BirdCount<string>>(BirdCount<string>.DataEquality);

		/// <summary>
		/// Gets the current top k hash tags and how many times they've been seen.
		/// </summary>
		public IEnumerable<BirdCount<string>> Current => hashTags;

		/// <summary>
		/// Adds a new BirdMessage.
		/// </summary>
		/// <param name="message">The message to process</param>
        public void Add(BirdMessage message)
        {
			foreach(string hashTag in message.HashTags)
			{
                var next = new BirdCount<string>(hashTag);
				BirdCount<string>? current;

				if (hashTags.TryGetValue(next, out current))
				{
					current.Count = current.Count + 1;
				}
				else
				{
					hashTags.Add(next);
				}
			}
        }
    }
}

