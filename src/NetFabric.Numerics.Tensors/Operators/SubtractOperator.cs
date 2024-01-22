namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that computes the subtraction of two tensors.
/// </summary>
/// <typeparam name="T">The type of the tensor elements.</typeparam>
public readonly struct SubtractOperator<T>
    : IBinaryOperator<T, T>
    where T : struct, ISubtractionOperators<T, T, T>
{
    /// <summary>
    /// Subtracts two values of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The result of the subtraction.</returns>
    public static T Invoke(T x, T y)
        => x - y;

    /// <summary>
    /// Subtracts two vectors of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="x">The first vector.</param>
    /// <param name="y">The second vector.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x - y;
}