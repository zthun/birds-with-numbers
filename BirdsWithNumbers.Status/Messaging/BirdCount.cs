using System;
using BirdsWithNumbers.Status.Compare;

namespace BirdsWithNumbers.Status.Messaging
{
	/// <summary>
	/// Represents an object that is counted.
	/// </summary>
	/// <typeparam name="T">The type of data being counted</typeparam>
	public sealed class BirdCount<T>
	{
		/// <summary>
		/// Gets an equality comparer that can be used to compare the data of two BirdCount objects.
		/// </summary>
		public static readonly IEqualityComparer<BirdCount<T>> DataEquality =
			new EqualityComparerFunction<BirdCount<T>>((x, y) => Object.Equals(x.Data, y.Data), (x) => x.Data?.GetHashCode() ?? 0);


		/// <summary>
		/// The data being counted.
		/// </summary>
		public T Data { get; private set; }

		/// <summary>
		/// The count of the data.
		/// </summary>
		public long Count { get; set; }

		/// <summary>
		/// Initializes a BirdCount with initial data and a count of 1.
		/// </summary>
		/// <param name="data">The data being counted.</param>
		public BirdCount(T data)
			: this(data, 1)
		{

		}

		/// <summary>
		/// Initializes a new instance of this object.
		/// </summary>
		/// <param name="data">The data being counted.</param>
		public BirdCount(T data, long count)
		{
			this.Data = data;
			this.Count = count;
		}

		/// <summary>
		/// Returns the string representation of this object.
		/// </summary>
		/// <returns>
		///	The string representation of this object.
		/// </returns>
        public override string ToString()
        {
			return String.Format("{0}({1})", Data, Count);
        }
    }
}

