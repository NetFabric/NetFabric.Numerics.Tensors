namespace NetFabric.Numerics.Tensors;

readonly struct MaxOperator<T>
    : IAggregationOperator<T, T>
    where T : struct, INumber<T>, IMinMaxValue<T>
{
    public static T Seed
        => T.MinValue;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.MaxNumber(x, y); 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.Max(x, y);
}

readonly struct MaxPropagateNaNOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumber<T>, IMinMaxValue<T>
{
    public static T Seed
        => T.MinValue;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.Max(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half)
            ? Vector.ConditionalSelect(Vector.Equals(x, x),
                Vector.ConditionalSelect(Vector.Equals(y, y),
                    Vector.ConditionalSelect(Vector.Equals(x, y),
                        Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), y, x),
                        Vector.Max(x, y)),
                    y),
                x)
            : Vector.Max(x, y); 
}

readonly struct MaxMagnitudeOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumberBase<T>, IMinMaxValue<T>
{
    public static T Seed
        => T.MinValue;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.MaxMagnitudeNumber(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
    {
        var xMag = Vector.Abs(x);
        var yMag = Vector.Abs(y);
        return
            Vector.ConditionalSelect(Vector.Equals(xMag, yMag),
                Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), y, x),
                Vector.ConditionalSelect(Vector.GreaterThan(xMag, yMag), x, y));
    }
}

readonly struct MaxMagnitudePropagateNaNOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumberBase<T>, IMinMaxValue<T>
{
    public static T Seed
        => T.MinValue;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.MaxMagnitude(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
    {
        var xMag = Vector.Abs(x);
        var yMag = Vector.Abs(y);
        return typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half)
            ? Vector.ConditionalSelect(Vector.Equals(x, x),
                Vector.ConditionalSelect(Vector.Equals(y, y),
                    Vector.ConditionalSelect(Vector.Equals(xMag, yMag),
                        Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), y, x),
                        Vector.ConditionalSelect(Vector.GreaterThan(xMag, yMag), x, y)),
                    y),
                x)
            : Vector.Max(xMag, yMag);
    }
}
