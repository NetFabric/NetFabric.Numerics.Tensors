namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Acos<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Apply<T, AcosOperator<T>>(left, destination);

    public static void AcosPi<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Apply<T, AcosPiOperator<T>>(left, destination);

    public static void Asin<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Apply<T, AsinOperator<T>>(left, destination);

    public static void AsinPi<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Apply<T, AsinPiOperator<T>>(left, destination);

    public static void Atan<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Apply<T, AtanOperator<T>>(left, destination);

    public static void AtanPi<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>    
        => Apply<T, AtanPiOperator<T>>(left, destination);

    public static void Cos<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Apply<T, CosOperator<T>>(left, destination);

    public static void CosPi<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T> 
        => Apply<T, CosPiOperator<T>>(left, destination);

    public static void Sin<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Apply<T, SinOperator<T>>(left, destination);

    public static void SinPi<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Apply<T, SinPiOperator<T>>(left, destination);

    public static void Tan<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Apply<T, TanOperator<T>>(left, destination);

    public static void TanPi<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Apply<T, TanPiOperator<T>>(left, destination);

    public static void SinCos<T>(ReadOnlySpan<T> left, Span<(T Sin, T Cos)> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Apply<T, (T Sin, T Cos), SinCosOperator<T>>(left, destination);

    public static void SinCosPi<T>(ReadOnlySpan<T> left, Span<(T SinPi, T CosPi)> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Apply<T, (T SinPi, T CosPi), SinCosPiOperator<T>>(left, destination);

    public static void DegreesToRadians<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Apply<T, DegreesToRadiansOperator<T>>(left, destination);

    public static void RadiansToDegrees<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Apply<T, RadiansToDegreesOperator<T>>(left, destination);

}