using System;
namespace BirdsWithNumbers.Status.Error;

/// <summary>
/// Represents an error handler.
/// </summary>
public interface IErrorHandler
{
    /// <summary>
    /// Handles a specific error.
    /// </summary>
    /// <param name="error">The error to handle.</param>
    void Handle(Exception error);
}

