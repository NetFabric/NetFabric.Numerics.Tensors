namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that computes the absolute value of a tensor.
/// </summary>
/// <typeparam name="T">The type of the tensor elements.</typeparam>
public readonly struct AbsOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, INumberBase<T>
{
    /// <summary>
    /// Computes the absolute value of a scalar.
    /// </summary>
    /// <param name="x">The scalar value.</param>
    /// <returns>The absolute value of the scalar.</returns>
    public static T Invoke(T x)
        => T.Abs(x);

    /// <summary>
    /// Computes the absolute value of a vector.
    /// </summary>
    /// <param name="x">The vector.</param>
    /// <returns>A new vector with the absolute values of the elements.</returns>
    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Vector.Abs(x);
}