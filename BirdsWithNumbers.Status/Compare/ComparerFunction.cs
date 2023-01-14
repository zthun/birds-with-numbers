using System;
using System.Diagnostics.CodeAnalysis;

namespace BirdsWithNumbers.Status.Compare;

/// <summary>
/// Represents a generic comparer that can compare any object given a callback comparer.
/// </summary>
/// <typeparam name="T">
/// The data type to compare.
/// </typeparam>
public sealed class ComparerFunction<T> : IComparer<T>
{
    private readonly Func<T, T, int> compare;

    /// <summary>
    /// Initializes a new instance of this comparer.
    /// </summary>
    /// <param name="compare">The callback function to compare two objects.</param>
	public ComparerFunction([DisallowNull] Func<T, T, int> compare)
	{
        this.compare = compare;
	}

    /// <summary>
    /// Compares x and y.
    /// </summary>
    /// <param name="x">The left parameter.</param>
    /// <param name="y">The right parameter.</param>
    /// <returns>
    /// Less than 0 if x is less than y.
    /// Greater than 0 if x is greater than y.
    /// Zero if x equals y.
    /// If both x and y are null, then 0 is returned.
    /// Null is always considered less than a non null value.
    /// </returns>
    public int Compare(T? x, T? y)
    {
        if (x == null && y == null)
        {
            return 0;
        }

        if (x == null && y != null)
        {
            return -1;
        }

        if (x != null && y == null)
        {
            return 1;
        }

        return this.compare(x!, y!);
    }
}
