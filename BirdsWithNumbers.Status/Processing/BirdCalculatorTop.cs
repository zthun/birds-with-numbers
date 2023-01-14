using System;
using System.Diagnostics.CodeAnalysis;
using BirdsWithNumbers.Status.Messaging;

namespace BirdsWithNumbers.Status.Processing
{
	/// <summary>
	/// Represents a bird calculator that keeps track of top counts.
	/// </summary>
	public class BirdCalculatorTop<T> : IBirdCalculator<IEnumerable<BirdCount<T>>>
	{
        private readonly int top;
        private readonly IBirdCalculator<IEnumerable<BirdCount<T>>> all;

        /// <summary>
        /// Gets the current enumerable list of counts in descending order
        /// by the top count.
        /// </summary>
        public IEnumerable<BirdCount<T>> Current { get; private set; }

        /// <summary>
        /// Initializes a new instance of this object.
        /// </summary>
        /// <param name="top">The maximum number of hashtags to keep in current</param>
        public BirdCalculatorTop(
            int top,
            [DisallowNull] IBirdCalculator<IEnumerable<BirdCount<T>>> all
        )
        {
            this.top = top;
            this.all = all;
            this.Current = Calculate();
        }

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="message">The message to process.</param>
        public void Add(BirdMessage message)
        {
            this.all.Add(message);
            this.Current = Calculate();
        }

        /// <summary>
        /// Calculates the final current order.
        /// </summary>
        /// <returns>The next Current value.</returns>
        private IEnumerable<BirdCount<T>> Calculate()
        {
            return this.all.Current.OrderByDescending((x) => x.Count).Take(top).ToArray();
        }
    }
}

