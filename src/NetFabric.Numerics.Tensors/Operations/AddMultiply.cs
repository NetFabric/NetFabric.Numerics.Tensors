namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Adds each element of a span to a scalar value then multiply by another scalar value and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The scalar value to add.</param>
    /// <param name="z">The scalar value to multiply.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the length of the destination is less than the length of the source.</exception>
    public static void AddMultiply<T>(ReadOnlySpan<T> x, T y, T z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Adds each element of a span to a scalar value then multiply by another scalar value and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of two values to add.</param>
    /// <param name="z">The tuple of two values to multiply.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the length of the destination is less than the length of the source.</exception>
    /// <remarks>
    /// This method can be used to calculate the addition and multiplication of 2D vectors.
    /// </remarks>
    public static void AddMultiply<T>(ReadOnlySpan<T> x, (T, T) y, (T, T) z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Adds each element of a span to a scalar value then multiply by another scalar value and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of three values to add.</param>
    /// <param name="z">The tuple of three values to multiply.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the length of the destination is less than the length of the source.</exception>
    /// <remarks>
    /// This method can be used to calculate the addition and multiplication of 3D vectors.
    /// </remarks>
    public static void AddMultiply<T>(ReadOnlySpan<T> x, (T, T, T) y, (T, T, T) z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Adds each element of a span to a scalar value then multiply by then respective element of another span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The scalar value to add.</param>
    /// <param name="z">The span of values to multiply.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the length of the destination is less than the length of the source.</exception>
    public static void AddMultiply<T>(ReadOnlySpan<T> x, T y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Adds each element of a span to a scalar value then multiply by then respective element of another span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of two values to add.</param>
    /// <param name="z">The span of values to multiply.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the length of the destination is less than the length of the source.</exception>
    /// <remarks>
    /// This method can be used to calculate the addition and multiplication of 2D vectors.
    /// </remarks>
    public static void AddMultiply<T>(ReadOnlySpan<T> x, (T, T) y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Adds each element of a span to a scalar value then multiply by then respective element of another span and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The tuple of three values to add.</param>
    /// <param name="z">The span of values to multiply.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the length of the destination is less than the length of the source.</exception>
    /// <remarks>
    /// This method can be used to calculate the addition and multiplication of 3D vectors.
    /// </remarks>
    public static void AddMultiply<T>(ReadOnlySpan<T> x, (T, T, T) y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Adds each element of a span to the respective element of another span then multiply by a scalar value and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The span of values to add.</param>
    /// <param name="z">The scalar value to multiply.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the length of the destination is less than the length of the source.</exception>
    public static void AddMultiply<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, T z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Adds each element of a span to the respective element of another span then multiply by a scalar value and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The span of values to add.</param>
    /// <param name="z">The tuple of two values to multiply.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the length of the destination is less than the length of the source.</exception>
    /// <remarks>
    /// This method can be used to calculate the addition and multiplication of 2D vectors.
    /// </remarks>
    public static void AddMultiply<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, (T, T) z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Adds each element of a span to the respective element of another span then multiply by a scalar value and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The span of values to add.</param>
    /// <param name="z">The tuple of three values to multiply.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the length of the destination is less than the length of the source.</exception>
    /// <remarks>
    /// This method can be used to calculate the addition and multiplication of 3D vectors.
    /// </remarks>
    public static void AddMultiply<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, (T, T, T) z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);

    /// <summary>
    /// Adds each element of a span to respective element on a second span then multiply by the respective element of a third element and stores the result in the destination span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="x">The source span.</param>
    /// <param name="y">The span of values to add.</param>
    /// <param name="z">The span of values to multiply.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException">Thrown when the length of the destination is less than the length of the source.</exception>
    public static void AddMultiply<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, AddMultiplyOperator<T>>(x, y, z, destination);
}