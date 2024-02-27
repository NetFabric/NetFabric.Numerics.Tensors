namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static int IndexOfGreaterThan<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.IndexOfPredicate<T, GreaterThanAnyOperator<T>>(source, value);

    public static int IndexOfGreaterThanOrEqual<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.IndexOfPredicate<T, GreaterThanOrEqualAnyOperator<T>>(source, value);

    public static int IndexOfLessThan<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.IndexOfPredicate<T, LessThanAnyOperator<T>>(source, value);

    public static int IndexOfLessThanOrEqual<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.IndexOfPredicate<T, LessThanOrEqualAnyOperator<T>>(source, value);
}