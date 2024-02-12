namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void Square<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, SquareOperator<T>>(x, destination);

    public static void Cube<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, CubeOperator<T>>(x, destination);

    public static void Pow<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IPowerFunctions<T>
        => Tensor.ApplyScalar<T, T, T, PowOperator<T>>(x, y, destination);
}