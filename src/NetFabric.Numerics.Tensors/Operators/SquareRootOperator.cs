namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that computes the square root of a tensor.
/// </summary>
/// <typeparam name="T">The type of the tensor elements.</typeparam>
public readonly struct SquareRootOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IRootFunctions<T>
{
    /// <summary>
    /// Computes the square root of a value of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="x">The value to compute the square root of.</param>
    /// <returns>The square root of the input value.</returns>
    public static T Invoke(T x)
        => T.Sqrt(x);

    /// <summary>
    /// Computes the square root of each element in a vector of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="x">The vector to compute the square root of.</param>
    /// <returns>A new vector with the square root of each element.</returns>
    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Vector.SquareRoot(x);
}