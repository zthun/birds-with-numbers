using System;

namespace BirdsWithNumbers.Status.Messaging;

/// <summary>
/// Represents a general message that comes from a social media platform.
/// </summary>
public class BirdMessage
{
	/// <summary>
	/// Gets or sets the id for this message.
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// Gets or sets the message.
	/// </summary>
	public string Message { get; set; }

	/// <summary>
	/// Gets or sets the discovered HashTags.
	/// </summary>
	public IEnumerable<string> HashTags { get; set; }

	/// <summary>
	/// Initializes a new instance of this object.
	/// </summary>
	public BirdMessage()
	{
		Id = String.Empty;
		Message = String.Empty;
		HashTags = Array.Empty<string>();
	}
}

