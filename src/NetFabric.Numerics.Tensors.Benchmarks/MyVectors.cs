using System.Numerics;

namespace NetFabric.Numerics.Tensors.Benchmarks;

public readonly record struct MyVector2<T>(T X, T Y)
    : IAdditiveIdentity<MyVector2<T>, MyVector2<T>>, IAdditionOperators<MyVector2<T>, MyVector2<T>, MyVector2<T>>
    where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    public MyVector2((T, T) tuple)
        : this(tuple.Item1, tuple.Item2)
    { }

    public static MyVector2<T> AdditiveIdentity 
        => new(T.AdditiveIdentity, T.AdditiveIdentity);

    public static MyVector2<T> operator +(MyVector2<T> left, MyVector2<T> right)
        => new(left.X + right.X, left.Y + right.Y);
}

public readonly record struct MyVector3<T>(T X, T Y, T Z)
    : IAdditiveIdentity<MyVector3<T>, MyVector3<T>>, IAdditionOperators<MyVector3<T>, MyVector3<T>, MyVector3<T>>
    where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    public MyVector3((T, T, T) tuple)
        : this(tuple.Item1, tuple.Item2, tuple.Item3)
    { }

    public static MyVector3<T> AdditiveIdentity 
        => new(T.AdditiveIdentity, T.AdditiveIdentity, T.AdditiveIdentity);

    public static MyVector3<T> operator +(MyVector3<T> left, MyVector3<T> right)
        => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
}

public readonly record struct MyVector4<T>(T X, T Y, T Z, T W)
    : IAdditiveIdentity<MyVector4<T>, MyVector4<T>>, IAdditionOperators<MyVector4<T>, MyVector4<T>, MyVector4<T>>
    where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    public MyVector4((T, T, T, T) tuple)
        : this(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4)
    { }

    public static MyVector4<T> AdditiveIdentity 
        => new(T.AdditiveIdentity, T.AdditiveIdentity, T.AdditiveIdentity, T.AdditiveIdentity);

    public static MyVector4<T> operator +(MyVector4<T> left, MyVector4<T> right)
        => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
}