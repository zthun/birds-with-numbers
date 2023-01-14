using System;
namespace BirdsWithNumbers.Status.Error;

/// <summary>
/// Represents an ErrorHandler that stores the history of all errors.
/// </summary>
public class ErrorHandlerHistory : IErrorHandler
{
	private List<Exception> errors = new List<Exception>();

	public IEnumerable<Exception> Errors => errors;

    public void Handle(Exception error)
    {
        errors.Add(error);
    }
}

