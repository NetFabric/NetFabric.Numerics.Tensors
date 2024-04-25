namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Computes the bitwise AND operation between each element of a <see cref="ReadOnlySpan{T}"/> and a scalar value <paramref name="y"/>,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The scalar value to perform the bitwise AND operation with.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void BitwiseAnd<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the bitwise AND operation between each element of a <see cref="ReadOnlySpan{T}"/> and a tuple <paramref name="y"/>,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The tuple value to perform the bitwise AND operation with.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void BitwiseAnd<T>(ReadOnlySpan<T> x, (T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the bitwise AND operation between each element of a <see cref="ReadOnlySpan{T}"/> and a tuple <paramref name="y"/>,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The tuple value to perform the bitwise AND operation with.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void BitwiseAnd<T>(ReadOnlySpan<T> x, (T, T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the bitwise AND operation between each element of two <see cref="ReadOnlySpan{T}"/>s,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The first input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The second input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void BitwiseAnd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the bitwise AND-NOT operation between each element of a <see cref="ReadOnlySpan{T}"/> and a scalar value <paramref name="y"/>,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The scalar value to perform the bitwise AND-NOT operation with.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void BitwiseAndNot<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndNotOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the bitwise AND-NOT operation between each element of a <see cref="ReadOnlySpan{T}"/> and a tuple <paramref name="y"/>,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The tuple value to perform the bitwise AND-NOT operation with.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void BitwiseAndNot<T>(ReadOnlySpan<T> x, (T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndNotOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the bitwise AND-NOT operation between each element of a <see cref="ReadOnlySpan{T}"/> and a tuple <paramref name="y"/>,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The tuple value to perform the bitwise AND-NOT operation with.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void BitwiseAndNot<T>(ReadOnlySpan<T> x, (T, T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndNotOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the bitwise AND-NOT operation between each element of two <see cref="ReadOnlySpan{T}"/>s,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The first input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The second input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void BitwiseAndNot<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndNotOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the bitwise OR operation between each element of a <see cref="ReadOnlySpan{T}"/> and a scalar value <paramref name="y"/>,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The scalar value to perform the bitwise OR operation with.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void BitwiseOr<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseOrOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the bitwise OR operation between each element of a <see cref="ReadOnlySpan{T}"/> and a tuple <paramref name="y"/>,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The tuple value to perform the bitwise OR operation with.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void BitwiseOr<T>(ReadOnlySpan<T> x, (T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseOrOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the bitwise OR operation between each element of a <see cref="ReadOnlySpan{T}"/> and a tuple <paramref name="y"/>,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The tuple value to perform the bitwise OR operation with.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void BitwiseOr<T>(ReadOnlySpan<T> x, (T, T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseOrOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the bitwise OR operation between each element of two <see cref="ReadOnlySpan{T}"/>s,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The first input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The second input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void BitwiseOr<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseOrOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the bitwise XOR operation between each element of a <see cref="ReadOnlySpan{T}"/> and a scalar value <paramref name="y"/>,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The scalar value to perform the bitwise XOR operation with.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void Xor<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, XorOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the bitwise XOR operation between each element of a <see cref="ReadOnlySpan{T}"/> and a tuple <paramref name="y"/>,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The tuple value to perform the bitwise XOR operation with.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void Xor<T>(ReadOnlySpan<T> x, (T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, XorOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the bitwise XOR operation between each element of a <see cref="ReadOnlySpan{T}"/> and a tuple <paramref name="y"/>,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The tuple value to perform the bitwise XOR operation with.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void Xor<T>(ReadOnlySpan<T> x, (T, T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, XorOperator<T>>(x, y, destination);

    /// <summary>
    /// Computes the bitwise XOR operation between each element of two <see cref="ReadOnlySpan{T}"/>s,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The first input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="y">The second input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void Xor<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, XorOperator<T>>(x, y, destination);

    public static void OnesComplement<T>(T[] x, T[] destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, OnesComplementOperator<T>>(x, destination);

    public static void OnesComplement<T>(ReadOnlyMemory<T> x, Memory<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, OnesComplementOperator<T>>(x, destination);

    /// <summary>
    /// Computes the ones' complement (bitwise negation) operation on each element of a <see cref="ReadOnlySpan{T}"/>,
    /// and stores the result in the corresponding position of the <see cref="Span{T}"/> <paramref name="destination"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The input <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="destination">The output <see cref="Span{T}"/> to store the result.</param>
    /// <exception cref="ArgumentException">Thrown when the input spans and the output span have different lengths.</exception>
    public static void OnesComplement<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, OnesComplementOperator<T>>(x, destination);
}