namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static int IndexOfFirstEqualsNumber<T>(ReadOnlySpan<T> source, T value)
        where T : struct, IComparisonOperators<T, T, bool>
        => Tensor.IndexOfFirstNumber<T, EqualsAnyOperator<T>>(source, value);
}
