namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Xor<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Apply<T, XorOperator<T>>(x, y, destination);
}