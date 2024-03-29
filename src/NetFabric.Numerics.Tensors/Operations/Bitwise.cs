namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void BitwiseAnd<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndOperator<T>>(x, y, destination);

    public static void BitwiseAnd<T>(ReadOnlySpan<T> x, (T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndOperator<T>>(x, y, destination);

    public static void BitwiseAnd<T>(ReadOnlySpan<T> x, (T, T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndOperator<T>>(x, y, destination);

    public static void BitwiseAnd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndOperator<T>>(x, y, destination);

    public static void BitwiseAndNot<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndNotOperator<T>>(x, y, destination);

    public static void BitwiseAndNot<T>(ReadOnlySpan<T> x, (T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndNotOperator<T>>(x, y, destination);

    public static void BitwiseAndNot<T>(ReadOnlySpan<T> x, (T, T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndNotOperator<T>>(x, y, destination);

   public static void BitwiseAndNot<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseAndNotOperator<T>>(x, y, destination);

    public static void BitwiseOr<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseOrOperator<T>>(x, y, destination);

    public static void BitwiseOr<T>(ReadOnlySpan<T> x, (T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseOrOperator<T>>(x, y, destination);

    public static void BitwiseOr<T>(ReadOnlySpan<T> x, (T, T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseOrOperator<T>>(x, y, destination);

    public static void BitwiseOr<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, BitwiseOrOperator<T>>(x, y, destination);

    public static void Xor<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, XorOperator<T>>(x, y, destination);

    public static void Xor<T>(ReadOnlySpan<T> x, (T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, XorOperator<T>>(x, y, destination);

    public static void Xor<T>(ReadOnlySpan<T> x, (T, T, T) y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, XorOperator<T>>(x, y, destination);

    public static void Xor<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, XorOperator<T>>(x, y, destination);

    public static void OnesComplement<T>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct, IBitwiseOperators<T, T, T>
        => Tensor.Apply<T, OnesComplementOperator<T>>(x, destination);
}