namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Computes the square of each element in the input span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="destination">The destination span to store the squared values.</param>
    /// <exception cref="ArgumentException">Thrown when the input and destination spans have different lengths.</exception>
    public static void Square<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, SquareOperator<T>>(x, destination);

    /// <summary>
    /// Computes the cube of each element in the input span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="destination">The destination span to store the cubed values.</param>
    /// <exception cref="ArgumentException">Thrown when the input and destination spans have different lengths.</exception>
    public static void Cube<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, CubeOperator<T>>(x, destination);

    /// <summary>
    /// Computes the power of each element in the input span with the specified exponent and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input span.</param>
    /// <param name="y">The exponent.</param>
    /// <param name="destination">The destination span to store the powered values.</param>
    /// <exception cref="ArgumentException">Thrown when the input and destination spans have different lengths.</exception>
    public static void Pow<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IPowerFunctions<T>
        => Tensor.ApplyScalar<T, T, T, PowOperator<T>>(x, y, destination);
}