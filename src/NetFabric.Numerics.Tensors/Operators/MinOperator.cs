namespace NetFabric.Numerics;

readonly struct MinOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumber<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.Min(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.ConditionalSelect(Vector.Equals(x, y),
                Vector.ConditionalSelect(Vector.LessThan(y, Vector<T>.Zero), y, x),
                Vector.Min(x, y));
}

readonly struct MinPropagateNaNOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumber<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.MinNumber(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.ConditionalSelect(Vector.Equals(x, x),
                Vector.ConditionalSelect(Vector.Equals(y, y),
                    Vector.ConditionalSelect(Vector.Equals(x, y),
                        Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), x, y),
                        Vector.Min(x, y)),
                    y),
                x);
}

readonly struct MinMagnitudeOperator<T>
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
        return
            Vector.ConditionalSelect(Vector.Equals(yMag, xMag),
                Vector.ConditionalSelect(Vector.LessThan(y, Vector<T>.Zero), y, x),
                Vector.ConditionalSelect(Vector.LessThan(yMag, xMag), y, x));
    }
}

readonly struct MinMagnitudePropagateNaNOperator<T>
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
            Vector.ConditionalSelect(Vector.Equals(x, x),
                Vector.ConditionalSelect(Vector.Equals(y, y),
                    Vector.ConditionalSelect(Vector.Equals(yMag, xMag),
                        Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), x, y),
                        Vector.ConditionalSelect(Vector.LessThan(xMag, yMag), x, y)),
                    y),
                x);
    }
}
