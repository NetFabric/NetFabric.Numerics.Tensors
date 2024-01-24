namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Exp<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IExponentialFunctions<T>
        => Apply<T, ExpOperator<T>>(left, destination);

    public static void ExpM1<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IExponentialFunctions<T>
        => Apply<T, ExpM1Operator<T>>(left, destination);

    public static void Exp2<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IExponentialFunctions<T>
        => Apply<T, Exp2Operator<T>>(left, destination);

    public static void Exp2M1<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IExponentialFunctions<T>
        => Apply<T, Exp2M1Operator<T>>(left, destination);

    public static void Exp10<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IExponentialFunctions<T>
        => Apply<T, Exp10Operator<T>>(left, destination);

    public static void Exp10M1<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, IExponentialFunctions<T>
        => Apply<T, Exp10M1Operator<T>>(left, destination);

}