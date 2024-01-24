namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, T y, T z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ValueTuple<T, T> y, ValueTuple<T, T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ValueTuple<T, T, T> y, ValueTuple<T, T, T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, T y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ValueTuple<T, T> y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ValueTuple<T, T, T> y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, T z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, ValueTuple<T, T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, ValueTuple<T, T, T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);
}