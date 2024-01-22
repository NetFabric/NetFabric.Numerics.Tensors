namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that computes the ceiling value on a tensor of floating-points.
/// </summary>
/// <typeparam name="T">The type of the tensor elements.</typeparam>
public readonly struct CeilingOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IFloatingPoint<T>
{
    /// <summary>
    /// <inheritedoc />
    /// </summary>
    public static bool IsVectorizable
        => false; 

    /// <summary>
    /// Applies the ceiling operation to the specified value.
    /// </summary>
    /// <param name="x">The value to apply the ceiling operation to.</param>
    /// <returns>The result of the ceiling operation.</returns>
    public static T Invoke(T x)
        => T.Ceiling(x);

    /// <summary>
    /// Applies the ceiling operation to the specified vector.
    /// </summary>
    /// <param name="x">The vector to apply the ceiling operation to.</param>
    /// <returns>The result of the ceiling operation.</returns>
#pragma warning disable IDE0060 // Remove unused parameter
    public static Vector<T> Invoke(ref readonly Vector<T> x)
#pragma warning restore IDE0060 // Remove unused parameter
        => Throw.InvalidOperationException<Vector<T>>();
}

/// <summary>
/// Represents an operator that computes the ceiling value on a tensor of single-precision floating-points.
/// </summary>
public readonly struct CeilingSingleOperator
    : IUnaryOperator<float, float>
{
    /// <summary>
    /// Applies the ceiling operation to the specified value.
    /// </summary>
    /// <param name="x">The value to apply the ceiling operation to.</param>
    /// <returns>The result of the ceiling operation.</returns>
    public static float Invoke(float x)
        => float.Ceiling(x);

    /// <summary>
    /// Applies the ceiling operation to the specified vector.
    /// </summary>
    /// <param name="x">The vector to apply the ceiling operation to.</param>
    /// <returns>The result of the ceiling operation.</returns>
    public static Vector<float> Invoke(ref readonly Vector<float> x)
        => Vector.Ceiling(x);
}

/// <summary>
/// Represents an operator that computes the ceiling value on a tensor of double-precision floating-points.
/// </summary>
public readonly struct CeilingDoubleOperator
    : IUnaryOperator<double, double>
{
    /// <summary>
    /// Applies the ceiling operation to the specified value.
    /// </summary>
    /// <param name="x">The value to apply the ceiling operation to.</param>
    /// <returns>The result of the ceiling operation.</returns>
    public static double Invoke(double x)
        => double.Ceiling(x);

    /// <summary>
    /// Applies the ceiling operation to the specified vector.
    /// </summary>
    /// <param name="x">The vector to apply the ceiling operation to.</param>
    /// <returns>The result of the ceiling operation.</returns>
    public static Vector<double> Invoke(ref readonly Vector<double> x)
        => Vector.Ceiling(x);
}