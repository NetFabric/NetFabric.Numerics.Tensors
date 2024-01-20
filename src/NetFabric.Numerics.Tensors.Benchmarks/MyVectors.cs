using System.Numerics;

namespace NetFabric.Numerics.Tensors.Benchmarks;

public readonly record struct MyVector2<T>(T X, T Y)
    : IAdditiveIdentity<MyVector2<T>, MyVector2<T>>, IAdditionOperators<MyVector2<T>, MyVector2<T>, MyVector2<T>>
    where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    public static MyVector2<T> AdditiveIdentity 
        => new(T.AdditiveIdentity, T.AdditiveIdentity);

    public static MyVector2<T> operator +(MyVector2<T> left, MyVector2<T> right)
        => new(left.X + right.X, left.Y + right.Y);
}

public readonly record struct MyVector3<T>(T X, T Y, T Z)
    : IAdditiveIdentity<MyVector3<T>, MyVector3<T>>, IAdditionOperators<MyVector3<T>, MyVector3<T>, MyVector3<T>>
    where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    public static MyVector3<T> AdditiveIdentity 
        => new(T.AdditiveIdentity, T.AdditiveIdentity, T.AdditiveIdentity);

    public static MyVector3<T> operator +(MyVector3<T> left, MyVector3<T> right)
        => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
}

public readonly record struct MyVector4<T>(T X, T Y, T Z, T W)
    : IAdditiveIdentity<MyVector4<T>, MyVector4<T>>, IAdditionOperators<MyVector4<T>, MyVector4<T>, MyVector4<T>>
    where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    public static MyVector4<T> AdditiveIdentity 
        => new(T.AdditiveIdentity, T.AdditiveIdentity, T.AdditiveIdentity, T.AdditiveIdentity);

    public static MyVector4<T> operator +(MyVector4<T> left, MyVector4<T> right)
        => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
}

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

    public static T Sum<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
    {
        var sum = T.AdditiveIdentity;
        foreach (var item in source)
            sum += item;
        return sum;
    }
}