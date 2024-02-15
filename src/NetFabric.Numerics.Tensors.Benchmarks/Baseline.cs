using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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

    public static void Ceiling<T>(ReadOnlySpan<T> source, Span<T> result)
        where T : struct, IFloatingPoint<T>
    {
        if (source.Length > result.Length)
            Throw.ArgumentException(nameof(source), "result spans is too small.");

        for(var index = 0; index < source.Length; index++)
            result[index] = T.Ceiling(source[index]);
    }

    public static void DegreesToRadians<T>(ReadOnlySpan<T> source, Span<T> result)
        where T : struct, ITrigonometricFunctions<T>
    {
        if (source.Length > result.Length)
            Throw.ArgumentException(nameof(source), "result spans is too small.");

        for(var index = 0; index < source.Length; index++)
            result[index] = T.DegreesToRadians(source[index]);
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

    public static T Product<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
    {
        var sum = T.MultiplicativeIdentity;
        foreach (var item in source)
            sum *= item;
        return sum;
    }

    public static void Min<T>(ReadOnlySpan<T> source, ReadOnlySpan<T> other, Span<T> result)
        where T : struct, INumber<T>
    {
        if (source.Length != other.Length)
            Throw.ArgumentException(nameof(source), "source and other spans must have the same length.");
        if (source.Length > result.Length)
            Throw.ArgumentException(nameof(source), "result spans is too small.");

        for (var index = 0; index < source.Length; index++)
            result[index] = T.Min(source[index], other[index]);
    }

    public static T Min<T>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        var min = T.MaxValue;
        foreach (var item in source)
        {
            if (T.IsNaN(item))
                return item;

            if (item < min)
                min = item; 
        }
        return min;
    }

    public static (T Min, T Max) MinMax<T>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        var min = T.MaxValue;
        var max = T.MinValue;
        foreach (var item in source)
        {
            if (T.IsNaN(item))
                return (item, item);

            if (item < min)
                min = item; 

            if (item > max)
                max = item;     
        }
        return (min, max);
    }
}