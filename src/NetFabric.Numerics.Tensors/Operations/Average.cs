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
    public static T Average<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IDivisionOperators<T, T, T>
        => source.Length is 0
            ? Throw.InvalidOperationException<T>()
            : Sum(source) / T.CreateChecked(source.Length);

    public static ValueTuple<T, T> Average2D<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IDivisionOperators<T, T, T>
    {
        if (source.Length is 0)
            Throw.InvalidOperationException();

        var result = Sum2D(source);
        var count = T.CreateChecked(source.Length);
        result.Item1 /= count;
        result.Item2 /= count;
        return result;
    }

    public static ValueTuple<T, T, T> Average3D<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IDivisionOperators<T, T, T>
    {
        if (source.Length is 0)
            Throw.InvalidOperationException();

        var result = Sum3D(source);
        var count = T.CreateChecked(source.Length);
        result.Item1 /= count;
        result.Item2 /= count;
        result.Item3 /= count;
        return result;
    }

    public static ValueTuple<T, T, T, T> Average4D<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IDivisionOperators<T, T, T>
    {
        if (source.Length is 0)
            Throw.InvalidOperationException();

        var result = Sum4D(source);
        var count = T.CreateChecked(source.Length);
        result.Item1 /= count;
        result.Item2 /= count;
        result.Item3 /= count;
        result.Item4 /= count;
        return result;
    }

}