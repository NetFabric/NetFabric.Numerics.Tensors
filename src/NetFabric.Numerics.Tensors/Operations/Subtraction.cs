namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void Subtract<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, SubtractOperator<T>>(left, right, destination);

    public static void Subtract<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, SubtractOperator<T>>(left, right, destination);

    public static void Subtract<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, SubtractOperator<T>>(left, right, destination);

    public static void Subtract<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, SubtractOperator<T>>(left, right, destination);
    
    public static void CheckedSubtract<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, CheckedSubtractOperator<T>>(left, right, destination);

    public static void CheckedSubtract<T>(ReadOnlySpan<T> left, (T, T) right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, CheckedSubtractOperator<T>>(left, right, destination);

    public static void CheckedSubtract<T>(ReadOnlySpan<T> left, (T, T, T) right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, CheckedSubtractOperator<T>>(left, right, destination);

    public static void CheckedSubtract<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Tensor.Apply<T, CheckedSubtractOperator<T>>(left, right, destination);
}