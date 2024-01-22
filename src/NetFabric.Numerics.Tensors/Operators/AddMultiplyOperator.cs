namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that computes addition followed by the multiplication of three tensors.
/// </summary>
/// <typeparam name="T">The type of the tensor elements.</typeparam>
public readonly struct AddMultiplyOperator<T>
    : ITernaryOperator<T, T>
    where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
{
    /// <summary>
    /// Invokes the operator on three operands of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    /// <param name="z">The third operand.</param>
    /// <returns>The result of the operation.</returns>
    public static T Invoke(T x, T y, T z)
        => (x + y) * z;

    /// <summary>
    /// Invokes the operator on three vectors of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="x">The first vector operand.</param>
    /// <param name="y">The second vector operand.</param>
    /// <param name="z">The third vector operand.</param>
    /// <returns>The result vector of the operation.</returns>
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y, ref readonly Vector<T> z)
        => (x + y) * z;
}