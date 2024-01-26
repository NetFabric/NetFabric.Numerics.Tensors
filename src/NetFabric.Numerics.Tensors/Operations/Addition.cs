namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Add<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Apply<T, AddOperator<T>>(left, right, destination);

    public static void Add<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Apply<T, AddOperator<T>>(left, right, destination);

    public static void Add<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Apply<T, AddOperator<T>>(left, right, destination);

    public static void Add<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Apply<T, AddOperator<T>>(left, right, destination);
    
    public static void CheckedAdd<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Apply<T, CheckedAddOperator<T>>(left, right, destination);

    public static void CheckedAdd<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Apply<T, CheckedAddOperator<T>>(left, right, destination);

    public static void CheckedAdd<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Apply<T, CheckedAddOperator<T>>(left, right, destination);

    public static void CheckedAdd<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Apply<T, CheckedAddOperator<T>>(left, right, destination);
}