namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void OnesComplement<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Apply<T, OnesComplementOperator<T>>(x, destination);
}