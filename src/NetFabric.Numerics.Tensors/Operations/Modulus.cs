namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Computes the modulus of each element in the left span with the corresponding element in the right span,
    /// and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="left">The span containing the left operands.</param>
    /// <param name="right">The span containing the right operands.</param>
    /// <param name="destination">The span to store the result in.</param>
    /// <exception cref="ArgumentException">Thrown when the spans have different lengths.</exception>
    public static void Modulus<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IModulusOperators<T, T, T>
        => Tensor.Apply<T, ModulusOperator<T>>(left, right, destination);
}