namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Computes the sum of squares of the elements in the source span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> containing the elements.</param>
    /// <returns>The sum of squares of the elements.</returns>
    public static T SumOfSquares<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>
        => Tensor.Aggregate<T, SquareOperator<T>, SumOperator<T>>(source);

    /// <summary>
    /// Computes the sum of squares of the elements in the source span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> containing the elements.</param>
    /// <returns>The sum of squares of the elements.</returns>
    /// <remarks>
    public static T SumOfSquaresNumber<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplyOperators<T, T, T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.AggregateNumber<T, SquareOperator<T>, SumOperator<T>>(source);
}