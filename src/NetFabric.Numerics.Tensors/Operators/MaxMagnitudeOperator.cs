namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct MaxMagnitudeAggregationOperator<T>
    : IAggregationOperator<T, T>
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
            : Vector.ConditionalSelect(Vector.Equals(xMag, yMag),
                Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), y, x),
                Vector.ConditionalSelect(Vector.GreaterThan(xMag, yMag), x, y));
    }
}

public readonly struct MaxMagnitudeNumberAggregationOperator<T>
    : IAggregationOperator<T, T>
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

public readonly struct MaxMagnitudeOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumberBase<T>
{
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
            : Vector.ConditionalSelect(Vector.Equals(xMag, yMag),
                Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), y, x),
                Vector.ConditionalSelect(Vector.GreaterThan(xMag, yMag), x, y));
    }
}

public readonly struct MaxMagnitudeNumberOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumberBase<T>
{
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
