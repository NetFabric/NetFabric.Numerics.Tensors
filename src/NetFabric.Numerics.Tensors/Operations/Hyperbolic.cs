namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void Acosh<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IHyperbolicFunctions<T>
        => Tensor.Apply<T, AcoshOperator<T>>(left, destination);

    public static void Asinh<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IHyperbolicFunctions<T>
        => Tensor.Apply<T, AsinhOperator<T>>(left, destination);

    public static void Atanh<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IHyperbolicFunctions<T>
        => Tensor.Apply<T, AtanhOperator<T>>(left, destination);

    public static void Cosh<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IHyperbolicFunctions<T>
        => Tensor.Apply<T, CoshOperator<T>>(left, destination);

    public static void Sinh<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IHyperbolicFunctions<T>
        => Tensor.Apply<T, SinhOperator<T>>(left, destination);

    public static void Tanh<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IHyperbolicFunctions<T>
        => Tensor.Apply<T, TanhOperator<T>>(left, destination);

}