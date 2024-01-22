namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that computes the maximum magnitude between two tensors.
/// </summary>
/// <typeparam name="T">The type of the tensor elements.</typeparam>
public readonly struct MaxOperator<T>
    : IBinaryOperator<T, T>
    where T : struct, INumberBase<T>
{
    /// <summary>
    /// Computes the maximum magnitude between two values.
    /// </summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The maximum magnitude between the two values.</returns>
    public static T Invoke(T x, T y)
        => T.MaxMagnitude(x, y);

    /// <summary>
    /// Computes the maximum magnitude between two vectors.
    /// </summary>
    /// <param name="x">The first vector.</param>
    /// <param name="y">The second vector.</param>
    /// <returns>A vector containing the maximum magnitude between the two vectors.</returns>
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.Max(x, y);
}