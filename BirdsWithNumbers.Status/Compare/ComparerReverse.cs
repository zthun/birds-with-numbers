using System;
using System.Diagnostics.CodeAnalysis;

namespace BirdsWithNumbers.Status.Compare;

/// <summary>
/// Represents a comparer that reverses the sort order.
/// </summary>
/// <typeparam name="T">The type of data being compared.</typeparam>
public sealed class ComparerReverse<T> : IComparer<T>
{
    private readonly IComparer<T> inner;

    /// <summary>
    /// Initializes a new instance of this comparable.
    /// </summary>
    /// <param name="inner"></param>
    public ComparerReverse([DisallowNull] IComparer<T> inner)
    {
        this.inner = inner;
    }

    /// <summary>
    /// Returns the reverse comparison of x and y.
    /// </summary>
    /// <param name="x">The right argument.</param>
    /// <param name="y">The left argument.</param>
    /// <returns>
    /// Less than 0 if y is less than x.
    /// Greater than 0 if y is greater than x.
    /// Zero if x equals y.
    /// If both x and y are null, then 0 is returned.
    /// Null is always considered greater than a non null value.
    /// </returns>
    public int Compare(T? x, T? y)
    {
        return inner.Compare(x, y) * -1;
    }
}

