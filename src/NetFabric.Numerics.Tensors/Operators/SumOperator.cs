namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that performs the sum aggregation operation on tuples of elements in a tensor.
/// </summary>
/// <typeparam name="T">The type of the tensor elements.</typeparam>
public readonly struct SumOperator<T>
    : IAggregationOperator<T>
    where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    /// <summary>
    /// Gets the identiy value for the sum operation.
    /// </summary>
    public static T Identity 
        => T.AdditiveIdentity;

    /// <summary>
    /// Combines the specified value with the vector to produce a new value.
    /// </summary>
    /// <param name="value">The current value.</param>
    /// <param name="vector">The vector to combine with the value.</param>
    /// <returns>The result of combining the value with the vector.</returns>
    public static T Invoke(T value, ref readonly Vector<T> vector)
        => value + Vector.Sum(vector);

    /// <summary>
    /// Computes the sum of two values.
    /// </summary>
    /// <param name="x">The first value to be added.</param>
    /// <param name="y">The second value to be added.</param>
    /// <returns>The sum of the two values.</returns>
    public static T Invoke(T x, T y)
        => x + y;

    /// <summary>
    /// Computes the sum of two vectors.
    /// </summary>
    /// <param name="x">The first vector to be added.</param>
    /// <param name="y">The second vector to be added.</param>
    /// <returns>The sum of the two vectors.</returns>
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x + y;
}