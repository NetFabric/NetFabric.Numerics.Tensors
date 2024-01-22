namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that computes the XOR of two tensors.
/// </summary>
/// <typeparam name="T">The type of the tensor elements.</typeparam>
public readonly struct XorOperator<T>
    : IBinaryOperator<T, T>
    where T : struct, IBitwiseOperators<T, T, T>
{
    /// <summary>
    /// Computes the XOR of two values of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The result of the XOR operation.</returns>
    public static T Invoke(T x, T y)
        => x ^ y;

    /// <summary>
    /// Computes the XOR of two vectors of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="x">The first vector.</param>
    /// <param name="y">The second vector.</param>
    /// <returns>The vector resulting from the XOR operation.</returns>
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.Xor(x, y);
}