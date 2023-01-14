using System;
using System.Diagnostics.CodeAnalysis;
using BirdsWithNumbers.Status.Messaging;

namespace BirdsWithNumbers.Status.Processing;

/// <summary>
/// Represents a calculator for statistics
/// </summary>
public interface IBirdCalculator<TInfo> : IBirdMessageProcessor
{
    /// <summary>
    /// The current output statistic
    /// </summary>
    [DisallowNull] TInfo Current { get; }
}
