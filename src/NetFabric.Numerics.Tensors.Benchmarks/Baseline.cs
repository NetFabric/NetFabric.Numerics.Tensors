using System.Numerics;

namespace NetFabric.Numerics.Tensors.Benchmarks;

public static class Baseline
{
    public static void Negate<T>(ReadOnlySpan<T> source, Span<T> result)
        where T : struct, IUnaryNegationOperators<T, T>
    {
        if (source.Length > result.Length)
            Throw.ArgumentException(nameof(source), "result spans is too small.");

        for(var index = 0; index < source.Length; index++)
            result[index] = -source[index];
    }

    public static void Add<T>(ReadOnlySpan<T> source, ReadOnlySpan<T> other, Span<T> result)
        where T : struct, IAdditionOperators<T, T, T>
    {
        if (source.Length != other.Length)
            Throw.ArgumentException(nameof(source), "source and other spans must have the same length.");
        if (source.Length > result.Length)
            Throw.ArgumentException(nameof(source), "result spans is too small.");

        for(var index = 0; index < source.Length; index++)
            result[index] = source[index] + other[index];
    }

    public static void Add<T>(ReadOnlySpan<T> source, T value, Span<T> result)
        where T : struct, IAdditionOperators<T, T, T>
    {
        for(var index = 0; index < source.Length; index++)
            result[index] = source[index] + value;
    }

    public static void AddMultiply<T>(ReadOnlySpan<T> source, ReadOnlySpan<T> other, ReadOnlySpan<T> another, Span<T> result)
        where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
    {
        if (source.Length != other.Length)
            Throw.ArgumentException(nameof(source), "source and other spans must have the same length.");
        if (source.Length != another.Length)
            Throw.ArgumentException(nameof(source), "source and another spans must have the same length.");
        if (source.Length > result.Length)
            Throw.ArgumentException(nameof(source), "result spans is too small.");

        for(var index = 0; index < source.Length; index++)
            result[index] = (source[index] + other[index]) * another[index];
    }

    public static T Sum<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
    {
        var sum = T.AdditiveIdentity;
        foreach (var item in source)
            sum += item;
        return sum;
    }
}