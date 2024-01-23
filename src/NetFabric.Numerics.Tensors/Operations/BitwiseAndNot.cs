namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void BitwiseAndNot<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Apply<T, BitwiseAndNotOperator<T>>(x, y, destination);

    public static void BitwiseAndNot<T>(ReadOnlySpan<T> x, ValueTuple<T, T> y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Apply<T, BitwiseAndNotOperator<T>>(x, y, destination);

    public static void BitwiseAndNot<T>(ReadOnlySpan<T> x, ValueTuple<T, T, T> y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Apply<T, BitwiseAndNotOperator<T>>(x, y, destination);

   public static void BitwiseAndNot<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Apply<T, BitwiseAndNotOperator<T>>(x, y, destination);
}