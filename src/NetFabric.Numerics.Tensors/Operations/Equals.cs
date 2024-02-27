namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static int IndexOfEquals<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.IndexOfPredicate<T, EqualsAnyOperator<T>>(source, value);
}
