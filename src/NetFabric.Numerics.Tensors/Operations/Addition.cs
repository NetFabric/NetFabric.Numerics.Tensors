namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void Add<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, AddOperator<T>>(left, right, destination);

    public static void Add<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, AddOperator<T>>(left, right, destination);

    public static void Add<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, AddOperator<T>>(left, right, destination);

    public static void Add<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, AddOperator<T>>(left, right, destination);
    
    public static void AddChecked<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, AddCheckedOperator<T>>(left, right, destination);

    public static void AddChecked<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, AddCheckedOperator<T>>(left, right, destination);

    public static void AddChecked<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, AddCheckedOperator<T>>(left, right, destination);

    public static void AddChecked<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, IAdditionOperators<T, T, T>
        => Tensor.Apply<T, AddCheckedOperator<T>>(left, right, destination);
}