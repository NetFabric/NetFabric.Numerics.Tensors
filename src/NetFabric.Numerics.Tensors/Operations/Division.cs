namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Divide<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Apply<T, DivideOperator<T>>(left, right, destination);

    public static void Divide<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Apply<T, DivideOperator<T>>(left, right, destination);

    public static void Divide<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Apply<T, DivideOperator<T>>(left, right, destination);

    public static void Divide<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Apply<T, DivideOperator<T>>(left, right, destination);
    
    public static void CheckedDivide<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Apply<T, CheckedDivideOperator<T>>(left, right, destination);

    public static void CheckedDivide<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Apply<T, CheckedDivideOperator<T>>(left, right, destination);

    public static void CheckedDivide<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Apply<T, CheckedDivideOperator<T>>(left, right, destination);

    public static void CheckedDivide<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IDivisionOperators<T, T, T>
        => Apply<T, CheckedDivideOperator<T>>(left, right, destination);
}