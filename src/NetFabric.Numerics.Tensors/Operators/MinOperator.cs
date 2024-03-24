namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct MinAggregationOperator<T>
    : IAggregationOperator<T, T>
    where T : struct, INumber<T>, IMinMaxValue<T>
{
    public static T Seed
        => T.MaxValue;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.Min(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half)
            ? Vector.ConditionalSelect(Vector.Equals(x, x),
                Vector.ConditionalSelect(Vector.Equals(y, y),
                    Vector.ConditionalSelect(Vector.Equals(x, y),
                        Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), x, y),
                        Vector.Min(x, y)),
                    y),
                x)
            : Vector.Min(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, ref readonly Vector<T> y)
    {
        for (var index = 0; index < Vector<T>.Count; index++)
            x = T.MinNumber(x, y[index]);
        return x;
    }
}

public readonly struct MinNumberAggregationOperator<T>
    : IAggregationOperator<T, T>
    where T : struct, INumber<T>, IMinMaxValue<T>
{
    public static T Seed
        => T.MaxValue;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.MinNumber(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.Min(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, ref readonly Vector<T> y)
    {
        for (var index = 0; index < Vector<T>.Count; index++)
            x = T.MinNumber(x, y[index]);
        return x;
    }
}

public readonly struct MinOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumber<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.Min(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half)
            ? Vector.ConditionalSelect(Vector.Equals(x, x),
                Vector.ConditionalSelect(Vector.Equals(y, y),
                    Vector.ConditionalSelect(Vector.Equals(x, y),
                        Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), x, y),
                        Vector.Min(x, y)),
                    y),
                x)
            : Vector.Min(x, y);
}

public readonly struct MinNumberOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumber<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.MinNumber(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.Min(x, y);
}
