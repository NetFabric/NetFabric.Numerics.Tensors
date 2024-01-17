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

public static class MyExtensions
{
    public static T BaselineSum<T>(this T[] source)
        where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
        => BaselineSum(source.AsSpan());

    public static T BaselineSum<T>(this Span<T> source)
        where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
        => BaselineSum((ReadOnlySpan<T>)source);

    public static T BaselineSum<T>(this ReadOnlySpan<T> source)
        where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
    {
        var sum = T.AdditiveIdentity;
        foreach (var item in source)
            sum += item;
        return sum;
    }
}