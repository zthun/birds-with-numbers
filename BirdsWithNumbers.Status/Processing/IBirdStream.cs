using System;
namespace BirdsWithNumbers.Status.Processing
{
	/// <summary>
	/// Represents an asynchronous stream of data messages. 
	/// </summary>
	public interface IBirdStream
	{
		/// <summary>
		/// Gets whether the stream is processing.
		/// </summary>
		bool Processing { get; }

		/// <summary>
		/// Starts retrieving the message stream.
		/// </summary>
		/// <returns>A Task that continues to run while the stream is processing.</returns>
		void Start();
	}
}

