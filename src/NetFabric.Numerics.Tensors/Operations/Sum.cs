namespace NetFabric.Numerics;

public static partial class Tensor
{
    /// <summary>
    /// Computes the sum of a span of values.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    /// <param name="source">The input span.</param>
    /// <param name="tupleSize">The size of each tuple in the span. Default is 1.</param>
    /// <returns>A span containing the sum of the values.</returns>
    /// <remarks>
    /// This method computes the sum of the values in the <paramref name="source"/> span. The <paramref name="tupleSize"/> parameter
    /// specifies the number of elements that should be summed together as a tuple. By default, each element is
    /// treated as a single tuple. The method returns a span containing the sum of the values, where each element
    /// in the resulting span represents the sum of the corresponding tuples in the input span.
    /// </remarks>
    public static T Sum<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Aggregate<T, SumOperator<T>>(source);

    public static ValueTuple<T, T> Sum2D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Aggregate2D<T, SumOperator<T>>(source);

    public static ValueTuple<T, T, T> Sum3D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Aggregate3D<T, SumOperator<T>>(source);

    public static ValueTuple<T, T, T, T> Sum4D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Aggregate4D<T, SumOperator<T>>(source);
}