namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Determines whether the specified value is contained within the source span.
    /// </summary>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <param name="source">The source span to search.</param>
    /// <param name="value">The value to search for.</param>
    /// <returns><c>true</c> if the value is found; otherwise, <c>false</c>.</returns>
    public static bool Contains<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.First<T, EqualsAnyOperator<T>>(source, value) is not null;

    /// <summary>
    /// Returns the index of the first occurrence of the specified value within the source span.
    /// </summary>
    /// <typeparam name="T">The type of elements in the span.</typeparam>
    /// <param name="source">The source span to search.</param>
    /// <param name="value">The value to search for.</param>
    /// <returns>The index of the first occurrence of the value within the span, if found; otherwise, -1.</returns>
    public static int IndexOfFirstEquals<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.IndexOfFirst<T, EqualsAnyOperator<T>>(source, value);
}
