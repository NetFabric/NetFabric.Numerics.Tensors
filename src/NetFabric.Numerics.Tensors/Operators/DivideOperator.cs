namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that computes the division of two tensors.
/// </summary>
/// <typeparam name="T">The type of the tensor elements.</typeparam>
public readonly struct DivideOperator<T>
    : IBinaryOperator<T, T>
    where T : struct, IDivisionOperators<T, T, T>
{
    /// <summary>
    /// Divides two values of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="x">The dividend.</param>
    /// <param name="y">The divisor.</param>
    /// <returns>The result of the division.</returns>
    public static T Invoke(T x, T y)
        => x / y;

    /// <summary>
    /// Divides two vectors of type <typeparamref name="T"/> element-wise.
    /// </summary>
    /// <param name="x">The dividend vector.</param>
    /// <param name="y">The divisor vector.</param>
    /// <returns>A new vector containing the element-wise division of <paramref name="x"/> and <paramref name="y"/>.</returns>
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x / y;
}