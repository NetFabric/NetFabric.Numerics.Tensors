namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void Log<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ILogarithmicFunctions<T>
        => Tensor.Apply<T, LogOperator<T>>(left, destination);

    public static void Log<T>(ReadOnlySpan<T> left, T newBase, Span<T> destination)
        where T : struct, ILogarithmicFunctions<T>
        => Tensor.ApplyScalar<T, T, T, LogBaseOperator<T>>(left, newBase, destination);

    public static void LogP1<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ILogarithmicFunctions<T>
        => Tensor.Apply<T, LogP1Operator<T>>(left, destination);

    public static void Log2<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ILogarithmicFunctions<T>
        => Tensor.Apply<T, Log2Operator<T>>(left, destination);

    public static void Log2P1<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ILogarithmicFunctions<T>
        => Tensor.Apply<T, Log2P1Operator<T>>(left, destination);

    public static void Log10<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ILogarithmicFunctions<T>
        => Tensor.Apply<T, Log10Operator<T>>(left, destination);

    public static void Log10P1<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ILogarithmicFunctions<T>
        => Tensor.Apply<T, Log10P1Operator<T>>(left, destination);

}