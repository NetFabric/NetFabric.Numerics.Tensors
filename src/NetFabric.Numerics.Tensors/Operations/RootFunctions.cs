namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static void Cbrt<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IRootFunctions<T>
        => Apply<T, CbrtOperator<T>>(x, destination);

    public static void Hypot<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IRootFunctions<T>
        => Apply<T, HypotOperator<T>>(x, y, destination);

    public static void RootN<T>(ReadOnlySpan<T> x, int n, Span<T> destination)
        where T : struct, IRootFunctions<T>
        => ApplyScalar<T, int, T, RootNOperator<T>>(x, n, destination);

    public static void Sqrt<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IRootFunctions<T>
        => Apply<T, SqrtOperator<T>>(x, destination);
}