namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static T Min<T>(ReadOnlySpan<T> left)
        where T : struct, INumber<T>, IMinMaxValue<T>
        => AggregatePropagateNaN<T, MinOperator<T>>(left);

    public static void Min<T>(ReadOnlySpan<T> left, T right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half))
            Apply<T, MinPropagateNaNOperator<T>>(left, right, destination);
        else
            Apply<T, MinOperator<T>>(left, right, destination);
    }

    public static void Min<T>(ReadOnlySpan<T> left, ValueTuple<T, T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half))
            Apply<T, MinPropagateNaNOperator<T>>(left, right, destination);
        else
            Apply<T, MinOperator<T>>(left, right, destination);
    }

    public static void Min<T>(ReadOnlySpan<T> left, ValueTuple<T, T, T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half))
            Apply<T, MinPropagateNaNOperator<T>>(left, right, destination);
        else
            Apply<T, MinOperator<T>>(left, right, destination);
    }

    public static void Min<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right, Span<T> destination)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        if (typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half))
            Apply<T, MinPropagateNaNOperator<T>>(left, right, destination);
        else
            Apply<T, MinOperator<T>>(left, right, destination);
    }

}