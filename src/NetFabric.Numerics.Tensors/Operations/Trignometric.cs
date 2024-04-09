namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Calculates the arc cosine of each element in the input tensor and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    /// <remarks>
    /// The arc cosine function returns the angle whose cosine is the specified number.
    /// </remarks>
    public static void Acos<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, AcosOperator<T>>(source, destination);

    /// <summary>
    /// Calculates the arc cosine of each element in the input tensor multiplied by π and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    /// <remarks>
    /// The arc cosine function multiplied by π returns the angle whose cosine is the specified number.
    /// </remarks>
    public static void AcosPi<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, AcosPiOperator<T>>(source, destination);

    /// <summary>
    /// Calculates the arc sine of each element in the input tensor and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    /// <remarks>
    /// The arc sine function returns the angle whose sine is the specified number.
    /// </remarks>
    public static void Asin<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, AsinOperator<T>>(source, destination);

    /// <summary>
    /// Calculates the arc sine of each element in the input tensor multiplied by π and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    /// <remarks>
    /// The arc sine function multiplied by π returns the angle whose sine is the specified number.
    /// </remarks>
    public static void AsinPi<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, AsinPiOperator<T>>(source, destination);

    /// <summary>
    /// Calculates the arc tangent of each element in the input tensor and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    /// <remarks>
    /// The arc tangent function returns the angle whose tangent is the specified number.
    /// </remarks>
    public static void Atan<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, AtanOperator<T>>(source, destination);

    /// <summary>
    /// Calculates the arc tangent of each element in the input tensor multiplied by π and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    /// <remarks>
    /// The arc tangent function multiplied by π returns the angle whose tangent is the specified number.
    /// </remarks>
    public static void AtanPi<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>    
        => Tensor.Apply<T, AtanPiOperator<T>>(source, destination);

    /// <summary>
    /// Calculates the cosine of each element in the input tensor and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    /// <remarks>
    /// The cosine function returns the ratio of the adjacent side to the hypotenuse side of a right-angled triangle.
    /// </remarks>
    public static void Cos<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, CosOperator<T>>(source, destination);

    /// <summary>
    /// Calculates the cosine of each element in the input tensor multiplied by π and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    /// <remarks>
    /// The cosine function multiplied by π returns the ratio of the adjacent side to the hypotenuse side of a right-angled triangle.
    /// </remarks>
    public static void CosPi<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T> 
        => Tensor.Apply<T, CosPiOperator<T>>(source, destination);

    /// <summary>
    /// Calculates the sine of each element in the input tensor and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    /// <remarks>
    /// The sine function returns the ratio of the opposite side to the hypotenuse side of a right-angled triangle.
    /// </remarks>
    public static void Sin<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, SinOperator<T>>(source, destination);

    /// <summary>
    /// Calculates the sine of each element in the input tensor multiplied by π and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    /// <remarks>
    /// The sine function multiplied by π returns the ratio of the opposite side to the hypotenuse side of a right-angled triangle.
    /// </remarks>
    public static void SinPi<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, SinPiOperator<T>>(source, destination);

    /// <summary>
    /// Calculates the tangent of each element in the input tensor and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    /// <remarks>
    /// The tangent function returns the ratio of the opposite side to the adjacent side of a right-angled triangle.
    /// </remarks>
    public static void Tan<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, TanOperator<T>>(source, destination);

    /// <summary>
    /// Calculates the tangent of each element in the input tensor multiplied by π and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    /// <remarks>
    /// The tangent function multiplied by π returns the ratio of the opposite side to the adjacent side of a right-angled triangle.
    /// </remarks>
    public static void TanPi<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, TanPiOperator<T>>(source, destination);

    /// <summary>
    /// Calculates the sine and cosine of each element in the input tensor and stores the results in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    /// <remarks>
    /// The sine and cosine functions return the ratios of the opposite side and adjacent side, respectively, to the hypotenuse side of a right-angled triangle.
    /// </remarks>
    public static void SinCos<T>(ReadOnlySpan<T> source, Span<(T Sin, T Cos)> destination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply<T, (T Sin, T Cos), SinCosOperator<T>>(source, destination);

    /// <summary>
    /// Calculates the sine and cosine of each element in the input tensor and stores the results in separate destination tensors.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="sinDestination">The destination tensor for the sine values.</param>
    /// <param name="cosDestination">The destination tensor for the cosine values.</param>
    /// <remarks>
    /// The sine and cosine functions return the ratios of the opposite side and adjacent side, respectively, to the hypotenuse side of a right-angled triangle.
    /// </remarks>
    public static void SinCos<T>(ReadOnlySpan<T> source, Span<T> sinDestination, Span<T> cosDestination)
        where T : struct, ITrigonometricFunctions<T>
        => Tensor.Apply2<T, SinOperator<T>, CosOperator<T>>(source, sinDestination, cosDestination);

    /// <summary>
    /// Converts each element in the input tensor from degrees to radians and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    public static void DegreesToRadians<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, INumberBase<T>, IFloatingPointConstants<T>
        => MultiplyDivide(source, T.Pi, T.CreateChecked(180), destination);

    /// <summary>
    /// Converts each element in the input tensor from degrees to gradians and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    public static void DegreesToGradians<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, INumberBase<T>
        => MultiplyDivide(source, T.CreateChecked(200), T.CreateChecked(180), destination);

    /// <summary>
    /// Converts each element in the input tensor from degrees to revolutions and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    public static void DegreesToRevolutions<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, INumberBase<T>, IFloatingPointConstants<T>
        => Divide(source, T.CreateChecked(360), destination);

    /// <summary>
    /// Converts each element in the input tensor from radians to degrees and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    public static void RadiansToDegrees<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, INumberBase<T>, IFloatingPointConstants<T>
        => MultiplyDivide(source, T.CreateChecked(180), T.Pi, destination);

    /// <summary>
    /// Converts each element in the input tensor from radians to gradians and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    public static void RadiansToGradians<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, INumberBase<T>, IFloatingPointConstants<T>
        => MultiplyDivide(source, T.CreateChecked(200), T.Pi, destination);

    /// <summary>
    /// Converts each element in the input tensor from radians to revolutions and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    public static void RadiansToRevolutions<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, INumberBase<T>, IFloatingPointConstants<T>
        => Divide(source, T.CreateChecked(2) * T.Pi, destination);

    /// <summary>
    /// Converts each element in the input tensor from gradians to degrees and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    public static void GradiansToDegrees<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, INumberBase<T>
        => MultiplyDivide(source, T.CreateChecked(180), T.CreateChecked(200), destination);

    /// <summary>
    /// Converts each element in the input tensor from gradians to radians and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    public static void GradiansToRadians<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, INumberBase<T>, IFloatingPointConstants<T>
        => MultiplyDivide(source, T.Pi, T.CreateChecked(200), destination);

    /// <summary>
    /// Converts each element in the input tensor from gradians to revolutions and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    public static void GradiansToRevolutions<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, INumberBase<T>
        => Divide(source, T.CreateChecked(400), destination);

    /// <summary>
    /// Converts each element in the input tensor from revolutions to degrees and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    public static void RevolutionsToDegrees<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, INumberBase<T>
        => Multiply(source, T.CreateChecked(360), destination);

    /// <summary>
    /// Converts each element in the input tensor from revolutions to radians and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    public static void RevolutionsToRadians<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, INumberBase<T>, IFloatingPointConstants<T>
        => Multiply(source, T.CreateChecked(2) * T.Pi, destination);

    /// <summary>
    /// Converts each element in the input tensor from revolutions to gradians and stores the result in the destination tensor.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the tensors.</typeparam>
    /// <param name="source">The input tensor.</param>
    /// <param name="destination">The destination tensor.</param>
    public static void RevolutionsToGradians<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : struct, INumberBase<T>
        => Multiply(source, T.CreateChecked(400), destination);
}