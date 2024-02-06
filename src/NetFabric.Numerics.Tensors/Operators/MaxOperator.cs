namespace NetFabric.Numerics;

readonly struct MaxOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumber<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.Max(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.ConditionalSelect(Vector.Equals(x, y),
                Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), y, x),
                Vector.Max(x, y));
}

readonly struct MaxPropagateNaNOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumber<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.MaxNumber(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.ConditionalSelect(Vector.Equals(x, x),
                Vector.ConditionalSelect(Vector.Equals(y, y),
                    Vector.ConditionalSelect(Vector.Equals(x, y),
                        Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), y, x),
                        Vector.Max(x, y)),
                    y),
                x);
}

readonly struct MaxMagnitudeOperator<T>
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
        return
            Vector.ConditionalSelect(Vector.Equals(xMag, yMag),
                Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), y, x),
                Vector.ConditionalSelect(Vector.GreaterThan(xMag, yMag), x, y));
    }
}

readonly struct MaxMagnitudePropagateNaNOperator<T>
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
            Vector.ConditionalSelect(Vector.Equals(x, x),
                Vector.ConditionalSelect(Vector.Equals(y, y),
                    Vector.ConditionalSelect(Vector.Equals(xMag, yMag),
                        Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), y, x),
                        Vector.ConditionalSelect(Vector.GreaterThan(xMag, yMag), x, y)),
                    y),
                x);
    }
}
