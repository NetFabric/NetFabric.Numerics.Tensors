namespace NetFabric.Numerics;

public static partial class Tensor
{
    /// <summary>
    /// Computes the sum of a span of values.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    /// <param name="source">The input span.</param>
    /// <returns>The sum of the elements.</returns>
    /// <remarks>
    /// This method requires the type <typeparamref name="T"/> to implement the <see cref="IAdditionOperators{T, T, T}"/> and <see cref="IAdditiveIdentity{T, T}"/> interfaces.
    /// </remarks>
    public static T Sum<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Aggregate<T, SumOperator<T>>(source);

    /// <summary>
    /// Computes the sum of pairs of values in a span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    /// <param name="source">The input span.</param>
    /// <returns>A tuple containing the sum of pairs of elements.</returns>
    /// <remarks>
    /// This method can be used to calculate the sum of 2D vectors.
    /// This method requires the type <typeparamref name="T"/> to implement the <see cref="IAdditionOperators{T, T, T}"/> and <see cref="IAdditiveIdentity{T, T}"/> interfaces.
    /// </remarks>
    public static ValueTuple<T, T> SumPairs<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => AggregatePairs<T, SumPairsOperator<T>>(source);

    /// <summary>
    /// Computes the sum of triplets of values in a span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    /// <param name="source">The input span.</param>
    /// <returns>A tuple containing the sum of triplets of elements.</returns>
    /// <remarks>
    /// This method can be used to calculate the sum of 3D vectors.
    /// This method requires the type <typeparamref name="T"/> to implement the <see cref="IAdditionOperators{T, T, T}"/> and <see cref="IAdditiveIdentity{T, T}"/> interfaces.
    /// </remarks>
    public static ReadOnlySpan<T> SumTriplets<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => AggregateTuples<T, SumTuplesOperator<T>>(source, 3);

    /// <summary>
    /// Computes the sum of tuples of values in a span.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tuples.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="tupleSize">The size of the tuples on the source span.</param>
    /// <returns>A span containing the sum of tuples of elements.</returns>
    /// <remarks>
    /// This method can be used to calculate the sum of vectors with number of dimensions greater than one.
    /// This method requires the type <typeparamref name="T"/> to implement the <see cref="IAdditionOperators{T, T, T}"/> and <see cref="IAdditiveIdentity{T, T}"/> interfaces.
    /// </remarks>
    public static ReadOnlySpan<T> SumTuples<T>(ReadOnlySpan<T> source, int tupleSize)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => AggregateTuples<T, SumTuplesOperator<T>>(source, tupleSize);
}