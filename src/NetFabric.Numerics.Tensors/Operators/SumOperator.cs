namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that computes the sum aggregation of a tensor.
/// </summary>
/// <typeparam name="T">The type of the tensor elements.</typeparam>
public readonly struct SumOperator<T>
    : IAggregationOperator<T>
    where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    /// <summary>
    /// Gets the identity value for the summation operation.
    /// </summary>
    public static T Identity 
        => T.AdditiveIdentity;

    /// <summary>
    /// Applies the summation operation to a value and a vector.
    /// </summary>
    /// <param name="value">The value to be added to the vector.</param>
    /// <param name="vector">The vector to be summed.</param>
    /// <returns>The result of the summation operation.</returns>
    public static T Invoke(T value, ref readonly Vector<T> vector)
        => value + Vector.Sum(vector);

    /// <summary>
    /// Applies the summation operation to two values.
    /// </summary>
    /// <param name="x">The first value to be added.</param>
    /// <param name="y">The second value to be added.</param>
    /// <returns>The result of the summation operation.</returns>
    public static T Invoke(T x, T y)
        => x + y;

    /// <summary>
    /// Applies the summation operation to two vectors.
    /// </summary>
    /// <param name="x">The first vector to be summed.</param>
    /// <param name="y">The second vector to be summed.</param>
    /// <returns>The result of the summation operation.</returns>
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x + y;
}