namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void Multiply<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyOperator<T>>(left, right, destination);

    public static void Multiply<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyOperator<T>>(left, right, destination);

    public static void Multiply<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyOperator<T>>(left, right, destination);

    public static void Multiply<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyOperator<T>>(left, right, destination);
    
    public static void CheckedMultiply<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, CheckedMultiplyOperator<T>>(left, right, destination);

    public static void CheckedMultiply<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, CheckedMultiplyOperator<T>>(left, right, destination);

    public static void CheckedMultiply<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, CheckedMultiplyOperator<T>>(left, right, destination);

    public static void CheckedMultiply<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, CheckedMultiplyOperator<T>>(left, right, destination);
}