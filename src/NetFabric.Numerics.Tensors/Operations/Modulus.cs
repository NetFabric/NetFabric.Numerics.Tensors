namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void Modulus<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IModulusOperators<T, T, T>
        => Tensor.Apply<T, ModulusOperator<T>>(left, right, destination);
}