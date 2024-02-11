namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static void Modulus<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IModulusOperators<T, T, T>
        => Apply<T, ModulusOperator<T>>(left, right, destination);
}