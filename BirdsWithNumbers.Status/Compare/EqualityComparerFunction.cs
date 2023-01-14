using System;
using System.Diagnostics.CodeAnalysis;

namespace BirdsWithNumbers.Status.Compare;

/// <summary>
/// Represents a generic EqualityComparer object that uses callbacks to do the comparison.
/// </summary>
/// <typeparam name="T">The type of data to compare.</typeparam>
public class EqualityComparerFunction<T> : IEqualityComparer<T>
{
    private readonly Func<T, T, bool> equals;
    private readonly Func<T, int> hash;

    /// <summary>
    /// Initializes a new instance of this object.
    /// </summary>
    /// <param name="equals">The equals callback.</param>
    /// <param name="hash">The hashcode callback.</param>
    public EqualityComparerFunction(
        [DisallowNull] Func<T, T, bool> equals,
        [DisallowNull] Func<T, int> hash)
	{
        this.equals = equals;
        this.hash = hash;
	}

    /// <summary>
    /// Determines if x equals y.
    /// </summary>
    /// <param name="x">The left clause.</param>
    /// <param name="y">The right clause.</param>
    /// <returns>True if x equals y, false otherwise.</returns>
    public bool Equals(T? x, T? y)
    {
        if (x == null && y == null)
        {
            return true;
        }

        if( x == null || y == null)
        {
            return false;
        }

        return equals(x, y);
    }

    /// <summary>
    /// Gets the hash code for obj.
    /// </summary>
    /// <param name="obj">The object to hash.</param>
    /// <returns>The hash code for obj.</returns>
    public int GetHashCode([DisallowNull] T obj)
    {
        return hash(obj);
    }
}

