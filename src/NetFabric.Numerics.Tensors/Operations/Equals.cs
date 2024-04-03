namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static bool Contains<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.First<T, EqualsAnyOperator<T>>(source, value) is not null;

    public static int IndexOfFirstEquals<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.IndexOfFirst<T, EqualsAnyOperator<T>>(source, value);
}
