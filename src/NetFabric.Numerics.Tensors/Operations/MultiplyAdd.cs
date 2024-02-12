namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, T y, T z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ValueTuple<T, T> y, ValueTuple<T, T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ValueTuple<T, T, T> y, ValueTuple<T, T, T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, T y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ValueTuple<T, T> y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ValueTuple<T, T, T> y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, T z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, ValueTuple<T, T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, ValueTuple<T, T, T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);

    public static void MultiplyAdd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => Tensor.Apply<T, MultiplyAddOperator<T>>(x, y, z, destination);
}