namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that computes the addition of two tensors.
/// </summary>
/// <typeparam name="T">The type of the tensor elements.</typeparam>
public readonly struct AddOperator<T>
    : IBinaryOperator<T, T>
    where T : struct, IAdditionOperators<T, T, T>
{
    /// <summary>
    /// Adds two values of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="x">The first value to add.</param>
    /// <param name="y">The second value to add.</param>
    /// <returns>The result of the addition.</returns>
    public static T Invoke(T x, T y)
        => x + y;

    /// <summary>
    /// Adds two vectors of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="x">The first vector to add.</param>
    /// <param name="y">The second vector to add.</param>
    /// <returns>The result of the addition.</returns>
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x + y;
}