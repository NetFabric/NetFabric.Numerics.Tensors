namespace NetFabric.Numerics;

public static partial class Tensor
{
    // <summary>
    /// Computes the average of a span of values.
    /// </summary>
    /// <typeparam name="T">The type of the values in the span.</typeparam>
    /// <param name="source">The span of values.</param>
    /// <param name="tupleSize">The size of each tuple in the span. Default is 1.</param>
    /// <exception cref="InvalidOperationException">Thrown when the <paramref name="source"/> span is empty.</exception>
    /// <returns>A span containing the average of the values.</returns>
    /// <remarks>
    /// This method computes the average of the values in the <paramref name="source"/> span. The <paramref name="tupleSize"/> parameter
    /// specifies the number of elements that should be averaged together as a tuple. By default, each element is
    /// treated as a single tuple. The method returns a span containing the averages of the values, where each element
    /// in the resulting span represents the average of the corresponding tuples in the input span.
    /// </remarks>
    public static Span<T> Average<T>(ReadOnlySpan<T> source, int tupleSize = 1)
        where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IDivisionOperators<T, T, T>
    {
        if (source.Length is 0)
            Throw.InvalidOperationException();

        var result = Sum(source, tupleSize);
        var count = T.CreateChecked(source.Length);
        foreach(ref var value in result)
            value /= count;
        return result;
    }
}