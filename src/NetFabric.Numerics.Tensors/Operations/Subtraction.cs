namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Subtract<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Apply<T, SubtractOperator<T>>(left, right, destination);

    public static void Subtract<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Apply<T, SubtractOperator<T>>(left, right, destination);

    public static void Subtract<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Apply<T, SubtractOperator<T>>(left, right, destination);

    public static void Subtract<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Apply<T, SubtractOperator<T>>(left, right, destination);
    
    public static void CheckedSubtract<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Apply<T, CheckedSubtractOperator<T>>(left, right, destination);

    public static void CheckedSubtract<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Apply<T, CheckedSubtractOperator<T>>(left, right, destination);

    public static void CheckedSubtract<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Apply<T, CheckedSubtractOperator<T>>(left, right, destination);

    public static void CheckedSubtract<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, ISubtractionOperators<T, T, T>
        => Apply<T, CheckedSubtractOperator<T>>(left, right, destination);
}