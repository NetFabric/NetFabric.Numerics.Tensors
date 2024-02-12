namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static void Acos<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, AcosOperator<T>>(left, destination);

    public static void AcosPi<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, AcosPiOperator<T>>(left, destination);

    public static void Asin<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, AsinOperator<T>>(left, destination);

    public static void AsinPi<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, AsinPiOperator<T>>(left, destination);

    public static void Atan<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, AtanOperator<T>>(left, destination);

    public static void AtanPi<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>    
        => Tensor.Apply<T, AtanPiOperator<T>>(left, destination);

    public static void Cos<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, CosOperator<T>>(left, destination);

    public static void CosPi<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T> 
        => Tensor.Apply<T, CosPiOperator<T>>(left, destination);

    public static void Sin<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, SinOperator<T>>(left, destination);

    public static void SinPi<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, SinPiOperator<T>>(left, destination);

    public static void Tan<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, TanOperator<T>>(left, destination);

    public static void TanPi<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, TanPiOperator<T>>(left, destination);

    public static void SinCos<T>(ReadOnlySpan<T> left, Span<(T Sin, T Cos)> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, (T Sin, T Cos), SinCosOperator<T>>(left, destination);

    public static void SinCos<T>(ReadOnlySpan<T> left, Span<T> sinDestination, Span<T> cosDestination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply2<T, SinOperator<T>, CosOperator<T>>(left, sinDestination, cosDestination);

    public static void SinCosPi<T>(ReadOnlySpan<T> left, Span<(T SinPi, T CosPi)> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, (T SinPi, T CosPi), SinCosPiOperator<T>>(left, destination);

    public static void DegreesToRadians<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, INumberBase<T>, IFloatingPointConstants<T>
        => MultiplyDivide(left, T.Pi, T.CreateChecked(180), destination);

    public static void DegreesToGradians<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, INumberBase<T>
        => MultiplyDivide(left, T.CreateChecked(200), T.CreateChecked(180), destination);

    public static void DegreesToRevolutions<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, INumberBase<T>, IFloatingPointConstants<T>
        => Divide(left, T.CreateChecked(360), destination);

    public static void RadiansToDegrees<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, INumberBase<T>, IFloatingPointConstants<T>
        => MultiplyDivide(left, T.CreateChecked(180), T.Pi, destination);

    public static void RadiansToGradians<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, INumberBase<T>, IFloatingPointConstants<T>
        => MultiplyDivide(left, T.CreateChecked(200), T.Pi, destination);

    public static void RadiansToRevolutions<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, INumberBase<T>, IFloatingPointConstants<T>
        => Divide(left, T.CreateChecked(2) * T.Pi, destination);

    public static void GradiansToDegrees<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, INumberBase<T>
        => MultiplyDivide(left, T.CreateChecked(180), T.CreateChecked(200), destination);

    public static void GradiansToRadians<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, INumberBase<T>, IFloatingPointConstants<T>
        => MultiplyDivide(left, T.Pi, T.CreateChecked(200), destination);

    public static void GradiansToRevolutions<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, INumberBase<T>
        => Divide(left, T.CreateChecked(400), destination);

    public static void RevolutionsToDegrees<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, INumberBase<T>
        => Multiply(left, T.CreateChecked(360), destination);

    public static void RevolutionsToRadians<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, INumberBase<T>, IFloatingPointConstants<T>
        => Multiply(left, T.CreateChecked(2) * T.Pi, destination);

    public static void RevolutionsToGradians<T>(ReadOnlySpan<T> left, Span<T> destination)
        where T : struct, INumberBase<T>
        => Multiply(left, T.CreateChecked(400), destination);

}