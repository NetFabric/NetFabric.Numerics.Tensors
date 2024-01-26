namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Multiply<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyOperator<T>>(left, right, destination);

    public static void Multiply<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyOperator<T>>(left, right, destination);

    public static void Multiply<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyOperator<T>>(left, right, destination);

    public static void Multiply<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyOperator<T>>(left, right, destination);
    
    public static void CheckedMultiply<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, CheckedMultiplyOperator<T>>(left, right, destination);

    public static void CheckedMultiply<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, CheckedMultiplyOperator<T>>(left, right, destination);

    public static void CheckedMultiply<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, CheckedMultiplyOperator<T>>(left, right, destination);

    public static void CheckedMultiply<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Apply<T, CheckedMultiplyOperator<T>>(left, right, destination);
}