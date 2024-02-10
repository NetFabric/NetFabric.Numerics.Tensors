namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void MinMagnitude<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half))
            Apply<T, MinMagnitudePropagateNaNOperator<T>>(left, right, destination);
        else
            Apply<T, MinMagnitudeOperator<T>>(left, right, destination);
    }

    public static void MinMagnitude<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half))
            Apply<T, MinMagnitudePropagateNaNOperator<T>>(left, right, destination);
        else
            Apply<T, MinMagnitudeOperator<T>>(left, right, destination);
    }

    public static void MinMagnitude<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half))
            Apply<T, MinMagnitudePropagateNaNOperator<T>>(left, right, destination);
        else
            Apply<T, MinMagnitudeOperator<T>>(left, right, destination);
    }

    public static void MinMagnitude<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half))
            Apply<T, MinMagnitudePropagateNaNOperator<T>>(left, right, destination);
        else
            Apply<T, MinMagnitudeOperator<T>>(left, right, destination);
    }
}