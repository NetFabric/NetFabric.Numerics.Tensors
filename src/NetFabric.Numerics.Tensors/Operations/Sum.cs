namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Computes the sum of the elements in the specified <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> containing the elements to sum.</param>
    /// <returns>The sum of the elements in the <see cref="ReadOnlySpan{T}"/>.</returns>
    /// <remarks>
    /// This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if the transformation and aggregation of any of the elements result in NaN.
    /// </remarks>
    public static T Sum<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>
        => Tensor.Aggregate<T, SumOperator<T>>(source);

    /// <summary>
    /// Computes the sum of the elements in the specified <see cref="ReadOnlySpan{T}"/> using number operators.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> containing the elements to sum.</param>
    /// <returns>The sum of the elements in the <see cref="ReadOnlySpan{T}"/>.</returns>
    /// <remarks>This methods does not propagate NaN.</remarks>
    public static T SumNumber<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.AggregateNumber<T, SumOperator<T>>(source);

    /// <summary>
    /// Computes the sum of the elements in the specified 2D <see cref="ReadOnlySpan{T}"/> using number operators.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="source">The 2D <see cref="ReadOnlySpan{T}"/> containing the elements to sum.</param>
    /// <returns>A tuple containing the sum of the elements in the rows and columns of the 2D <see cref="ReadOnlySpan{T}"/>.</returns>
    public static (T, T) Sum2D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.AggregateNumber2D<T, SumOperator<T>>(source);

    /// <summary>
    /// Computes the sum of the elements in the specified 3D <see cref="ReadOnlySpan{T}"/> using number operators.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="source">The 3D <see cref="ReadOnlySpan{T}"/> containing the elements to sum.</param>
    /// <returns>A tuple containing the sum of the elements in the rows, columns, and depth of the 3D <see cref="ReadOnlySpan{T}"/>.</returns>
    public static (T, T, T) Sum3D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.AggregateNumber3D<T, SumOperator<T>>(source);

    /// <summary>
    /// Computes the sum of the elements in the specified 4D <see cref="ReadOnlySpan{T}"/> using number operators.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="source">The 4D <see cref="ReadOnlySpan{T}"/> containing the elements to sum.</param>
    /// <returns>A tuple containing the sum of the elements in the rows, columns, depth, and time of the 4D <see cref="ReadOnlySpan{T}"/>.</returns>
    public static (T, T, T, T) Sum4D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.AggregateNumber4D<T, SumOperator<T>>(source);
}