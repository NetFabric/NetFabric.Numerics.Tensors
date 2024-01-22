namespace NetFabric.Numerics
{
    /// <summary>
    /// Represents a floor operator that performs the floor operation on a tensor of floating-points.
    /// </summary>
    /// <typeparam name="T">The type of the tensor elements.</typeparam>
    public readonly struct FloorOperator<T>
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
        => T.Floor(x);

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
    /// Represents a floor operator that performs the floor operation on a tensor of single-precision floating-points.
    /// </summary>
    public readonly struct FloorSingleOperator
        : IUnaryOperator<float, float>
    {
        /// <summary>
        /// Applies the floor operation to the specified single-precision floating-point value.
        /// </summary>
        /// <param name="x">The value to apply the floor operation to.</param>
        /// <returns>The result of the floor operation.</returns>
        public static float Invoke(float x)
            => float.Floor(x);

        /// <summary>
        /// Applies the floor operation to the specified single-precision floating-point vector.
        /// </summary>
        /// <param name="x">The vector to apply the floor operation to.</param>
        /// <returns>The result of the floor operation.</returns>
        public static Vector<float> Invoke(ref readonly Vector<float> x)
            => Vector.Floor(x);
    }

    /// <summary>
    /// Represents a floor operator that performs the floor operation on a tensor of double-precision floating-points.
    /// </summary>
    public readonly struct FloorDoubleOperator
        : IUnaryOperator<double, double>
    {
        /// <summary>
        /// Applies the floor operation to the specified double-precision floating-point value.
        /// </summary>
        /// <param name="x">The value to apply the floor operation to.</param>
        /// <returns>The result of the floor operation.</returns>
        public static double Invoke(double x)
            => double.Floor(x);

        /// <summary>
        /// Applies the floor operation to the specified double-precision floating-point vector.
        /// </summary>
        /// <param name="x">The vector to apply the floor operation to.</param>
        /// <returns>The result of the floor operation.</returns>
        public static Vector<double> Invoke(ref readonly Vector<double> x)
            => Vector.Floor(x);
    }
}