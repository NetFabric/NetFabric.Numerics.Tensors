namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct MinMagnitudeAggregationOperator<T>
    : IAggregationOperator<T, T>
    where T : struct, INumberBase<T>, IMinMaxValue<T>
{
    public static T Seed
        => T.MaxValue;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.MinMagnitude(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
    {
        var xMag = Vector.Abs(x);
        var yMag = Vector.Abs(y);
        return typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half)
            ? Vector.ConditionalSelect(Vector.Equals(x, x),
                Vector.ConditionalSelect(Vector.Equals(y, y),
                    Vector.ConditionalSelect(Vector.Equals(yMag, xMag),
                        Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), x, y),
                        Vector.ConditionalSelect(Vector.LessThan(xMag, yMag), x, y)),
                    y),
                x)
            : Vector.ConditionalSelect(Vector.Equals(yMag, xMag),
                Vector.ConditionalSelect(Vector.LessThan(y, Vector<T>.Zero), y, x),
                Vector.ConditionalSelect(Vector.LessThan(yMag, xMag), y, x));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, ref readonly Vector<T> y)
    {
        for (var index = 0; index < Vector<T>.Count; index++)
            x = T.MinMagnitudeNumber(x, y[index]);
        return x;
    }
}

public readonly struct MinMagnitudeNumberAggregationOperator<T>
    : IAggregationOperator<T, T>
    where T : struct, INumberBase<T>, IMinMaxValue<T>
{
    public static T Seed
        => T.MaxValue;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.MinMagnitudeNumber(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
    {
        var xMag = Vector.Abs(x);
        var yMag = Vector.Abs(y);
        return
            Vector.ConditionalSelect(Vector.Equals(yMag, xMag),
                Vector.ConditionalSelect(Vector.LessThan(y, Vector<T>.Zero), y, x),
                Vector.ConditionalSelect(Vector.LessThan(yMag, xMag), y, x));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, ref readonly Vector<T> y)
    {
        for (var index = 0; index < Vector<T>.Count; index++)
            x = T.MinMagnitudeNumber(x, y[index]);
        return x;
    }
}

public readonly struct MinMagnitudeOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumberBase<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.MinMagnitude(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
    {
        var xMag = Vector.Abs(x);
        var yMag = Vector.Abs(y);
        return typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half)
            ? Vector.ConditionalSelect(Vector.Equals(x, x),
                Vector.ConditionalSelect(Vector.Equals(y, y),
                    Vector.ConditionalSelect(Vector.Equals(yMag, xMag),
                        Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), x, y),
                        Vector.ConditionalSelect(Vector.LessThan(xMag, yMag), x, y)),
                    y),
                x)
            : Vector.ConditionalSelect(Vector.Equals(yMag, xMag),
                Vector.ConditionalSelect(Vector.LessThan(y, Vector<T>.Zero), y, x),
                Vector.ConditionalSelect(Vector.LessThan(yMag, xMag), y, x));
    }
}

public readonly struct MinMagnitudeNumberOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumberBase<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.MinMagnitudeNumber(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
    {
        var xMag = Vector.Abs(x);
        var yMag = Vector.Abs(y);
        return
            Vector.ConditionalSelect(Vector.Equals(yMag, xMag),
                Vector.ConditionalSelect(Vector.LessThan(y, Vector<T>.Zero), y, x),
                Vector.ConditionalSelect(Vector.LessThan(yMag, xMag), y, x));
    }
}
