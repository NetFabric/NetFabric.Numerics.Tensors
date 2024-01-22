namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that computes the minimum magnitude between two tensors.
/// </summary>
/// <typeparam name="T">The type of the tensor elements.</typeparam>
public readonly struct MinOperator<T>
    : IBinaryOperator<T, T>
    where T : struct, INumberBase<T>
{
    /// <summary>
    /// Computes the minimum value between two operands.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    /// <returns>The minimum value between the two operands.</returns>
    public static T Invoke(T x, T y)
        => T.MinMagnitude(x, y);

    /// <summary>
    /// Computes the minimum value between two vectors.
    /// </summary>
    /// <param name="x">The first vector.</param>
    /// <param name="y">The second vector.</param>
    /// <returns>A new vector containing the minimum value between the two vectors.</returns>
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.Min(x, y);
}