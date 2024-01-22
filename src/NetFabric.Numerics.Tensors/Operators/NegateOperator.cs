namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that computes the unary negation of a tensor.
/// </summary>
/// <typeparam name="T">The type of the tensor elements.</typeparam>
public readonly struct NegateOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IUnaryNegationOperators<T, T>
{
    /// <summary>
    /// Negates a value of type T.
    /// </summary>
    /// <param name="x">The value to negate.</param>
    /// <returns>The negated value.</returns>
    public static T Invoke(T x)
        => -x;

    /// <summary>
    /// Negates a vector.
    /// </summary>
    /// <param name="x">The vector to negate.</param>
    /// <returns>The negated vector.</returns>
    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => -x;
}