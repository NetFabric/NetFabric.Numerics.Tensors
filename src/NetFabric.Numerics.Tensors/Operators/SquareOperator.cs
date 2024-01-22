namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that computes the square of a tensor.
/// </summary>
/// <typeparam name="T">The type of the tensor elements.</typeparam>
public readonly struct SquareOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    /// <summary>
    /// Applies the square operator to the specified value.
    /// </summary>
    /// <param name="x">The value to square.</param>
    /// <returns>The squared value.</returns>
    public static T Invoke(T x)
        => x * x;

    /// <summary>
    /// Applies the square operator to the specified vector.
    /// </summary>
    /// <param name="x">The vector to square.</param>
    /// <returns>The squared vector.</returns>
    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => x * x;
}