namespace NetFabric.Numerics.Tensors.Operators;

public readonly struct MaxAggregationOperator<T>
    : IAggregationOperator<T, T>
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, ref readonly Vector<T> y)
    {
        for (var index = 0; index < Vector<T>.Count; index++)
            x = T.MaxNumber(x, y[index]);
        return x;
    }
}

public readonly struct MaxNumberAggregationOperator<T>
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, ref readonly Vector<T> y)
    {
        for (var index = 0; index < Vector<T>.Count; index++)
            x = T.MaxNumber(x, y[index]);
        return x;
    }
}

public readonly struct MaxOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumber<T>
{
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

public readonly struct MaxNumberOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumber<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.MaxNumber(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.Max(x, y);
}
