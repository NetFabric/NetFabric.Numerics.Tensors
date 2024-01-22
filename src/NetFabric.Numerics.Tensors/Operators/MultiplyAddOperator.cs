namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that computes multipolication followed by the addition of three tensors.
/// </summary>
/// <typeparam name="T">The type of the tensor elements.</typeparam>
public readonly struct MultiplyAddOperator<T>
    : ITernaryOperator<T, T>
    where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
{
    /// <summary>
    /// Multiplies two values and adds a third value.
    /// </summary>
    /// <param name="x">The first value to multiply.</param>
    /// <param name="y">The second value to multiply.</param>
    /// <param name="z">The value to add.</param>
    /// <returns>The result of multiplying <paramref name="x"/> and <paramref name="y"/> and adding <paramref name="z"/>.</returns>
    public static T Invoke(T x, T y, T z)
        => (x * y) + z;

    /// <summary>
    /// Multiplies two vectors element-wise and adds a third vector element-wise.
    /// </summary>
    /// <param name="x">The first vector to multiply.</param>
    /// <param name="y">The second vector to multiply.</param>
    /// <param name="z">The vector to add.</param>
    /// <returns>The result of multiplying <paramref name="x"/> and <paramref name="y"/> element-wise and adding <paramref name="z"/> element-wise.</returns>
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y, ref readonly Vector<T> z)
        => (x * y) + z;
}