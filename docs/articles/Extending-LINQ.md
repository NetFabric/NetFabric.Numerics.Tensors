# Extending LINQ

LINQ, which stands for Language-Integrated Query, offers a versatile set of methods for processing collections efficiently. While LINQ includes optimizations for specific scenarios, as a library developer, it's beneficial to extend LINQ methods with overloads to support new numeric types. This ensures that your types are processed more efficiently within LINQ operations.

For instance, let's consider a scenario where your library implements a 2D vector based on generic math, as shown below (simplified for this example):

```csharp
public readonly record struct MyVector2<T>(T X, T Y)
    : IAdditiveIdentity<MyVector2<T>, MyVector2<T>>
    , IAdditionOperators<MyVector2<T>, MyVector2<T>, MyVector2<T>>
    where T : struct, INumber<T>
{
    public MyVector2(ValueTuple<T, T> tuple)
        : this(tuple.Item1, tuple.Item2)
    { }

    public static MyVector2<T> AdditiveIdentity
        => new(T.AdditiveIdentity, T.AdditiveIdentity);

    public static MyVector2<T> operator +(MyVector2<T> left, MyVector2<T> right)
        => new(left.X + right.X, left.Y + right.Y);
}
```

LINQ provides a `Sum()` method for specific native numeric types or types that can be converted to them using a transform lambda expression. However, it's advantageous to provide a dedicated `Sum()` method for custom types. We can start by implementing the following:

```csharp
public static class MyVector2
{
    public static MyVector2<T> Sum<T>(this IEnumerable<MyVector2<T>> source)
        where T : struct, INumber<T>
    {
        var sum = MyVector2<T>.AdditiveIdentity;
        foreach (var value in source)
            sum += value;
        return sum;
    }
}
```

Like any other method in the LINQ library, this is an extension method for the `IEnumerable<T>` interface, specifically tailored for `MyVector2<T>`. The compiler automatically selects the appropriate overload based on the extended type.

However, if the source collection is an array or a span, the optimizations provided by `NetFabric.Numerics.Tensors` are not utilized. To address this, we need to provide additional overloads to cover all scenarios:

```csharp
public static class MyVector2
{
    public static MyVector2<T> Sum<T>(this IEnumerable<MyVector2<T>> source)
        where T : struct, INumber<T>
    {
        if (source.TryGetSpan(out var span))
            return Sum(span);

        var sum = MyVector2<T>.AdditiveIdentity;
        foreach (var value in source)
            sum += value;
        return sum;
    }

    public static MyVector2<T> Sum<T>(this List<MyVector2<T>> source)
        where T : struct, INumber<T>
        => Sum(CollectionsMarshal.AsSpan(source));

    public static MyVector2<T> Sum<T>(this MyVector2<T>[] source)
        where T : struct, INumber<T>
        => Sum(source.AsSpan());

    public static MyVector2<T> Sum<T>(this Span<MyVector2<T>> source)
        where T : struct, INumber<T>
        => Sum((ReadOnlySpan<MyVector2<T>>)source);

    public static MyVector2<T> Sum<T>(this ReadOnlySpan<MyVector2<T>> source)
        where T : struct, INumber<T>
        => TensorOperations.Sum(source);
}
```

Note that the method taking a `IEnumerable<MyVector2<T>>` parameter checks if the source collection can be converted to a span using the `TryGetSpan()` method from the `NetFabric` package. If so, it calls the overload accepting a `ReadOnlySpan<MyVector2<T>>`, which in turn invokes the optimized `TensorOperations.Sum()`.

The overload for `List<MyVector2<T>>` uses `CollectionsMarshal.AsSpan()` to obtain its internal span, as explained in the `Working with List<T>` section.

While explicit method calls automatically convert types `T[]` and `Span<T>` to `ReadOnlySpan<T>`, this conversion doesn't occur when calling extension methods. Therefore, providing all these overloads ensures users don't need to perform explicit conversions.

By providing these overloads, the compiler can explicitly call the most suitable overload, bypassing type verifications and providing support for spans, which LINQ does not inherently support.

Offering these overloads ensures that users accustomed to employing LINQ will consistently achieve optimal performance regardless of the scenario.