namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Square<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, SquareOperator<T>>(x, destination);

    public static void Cube<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, CubeOperator<T>>(x, destination);

    public static void Pow<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IPowerFunctions<T>
        => ApplyGeneric<T, T, T, PowOperator<T>>(x, y, destination);
}