namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static T? FirstGreaterThan<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.AggregatePredicateBinary<T, GreaterThanAnyOperator<T>>(source, value);

    public static T? FirstGreaterThanOrEqual<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.AggregatePredicateBinary<T, GreaterThanOrEqualAnyOperator<T>>(source, value);

    public static T? FirstLessThan<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.AggregatePredicateBinary<T, LessThanAnyOperator<T>>(source, value);

    public static T? FirstLessThanOrEqual<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.AggregatePredicateBinary<T, LessThanOrEqualAnyOperator<T>>(source, value);
}