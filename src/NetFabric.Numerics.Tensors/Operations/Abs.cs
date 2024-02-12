namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void Abs<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, INumberBase<T>
        => Tensor.Apply<T, AbsOperator<T>>(left, destination);
}