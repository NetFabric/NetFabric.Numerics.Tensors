# Working with Tensors for Structured Data

In the `NetFabric.Numerics.Tensors` library, tensors can efficiently handle any value-type that meets specific requirements. For instance, consider the implementation of a 2D vector, `MyVector2<T>`, which supports the `Sum` operation as it's a value type and implements both `IAdditiveIdentity<T, T>` and `IAdditionOperators<T, T, T>`:

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

However, when it comes to using `Vector<T>` for vectorization, direct support is lacking, preventing the tensor from optimizing the `Sum` operation using vectorization.

It's important to note that since `MyVector2<T>` comprises two fields of the same type and is always a value type, the adjacent storage in memory allows effortless conversion from a span of `MyVector2<T>` to a span of `T` using `MemoryMarshal.Cast<MyVector2<T>, T>()`.

This capability facilitates the implementation of the `Sum` operation for a span of `MyVector2<T>`:

```csharp
/// <summary>
/// Computes the sum of 2D vectors in the span.
/// </summary>
public static MyVector2<T> Sum<T>(this ReadOnlySpan<MyVector2<T>> source)
    where T : struct, INumber<T>, IMinMaxValue<T>
    => new(Tensor.Sum2D(MemoryMarshal.Cast<MyVector2<T>, T>(source)));
```

Here, the tensor efficiently leverages vectorization for improved performance in the `Sum` operation on a span of `MyVector2<T>`, treating it as a span of its internal values. The use of `Sum2D` specifically indicates the intention to calculate the sum of every other item in the span, returning a 2D tuple that is then converted into the result `MyVector2<T>` by using the constructor.

Regarding the `Apply` operation, it is applied to each element while maintaining order in the destination span. Applying `MemoryMarshal.Cast()` to both the sources and the destination is appropriate:

```csharp
/// <summary>
/// Adds a vector to each element in the span and stores the result in another span.
/// </summary>
public static void Add<T>(ReadOnlySpan<MyVector2<T>> angles, MyVector2<T> value, Span<MyVector2<T>> result)
    where T : struct, INumber<T>, IMinMaxValue<T>
    => Tensor.Add(MemoryMarshal.Cast<MyVector2<T>, T>(angles), (value.X, value.Y), MemoryMarshal.Cast<MyVector2<T>, T>(result));

/// <summary>
/// Adds corresponding elements in two spans of vectors and stores the result in another span.
/// </summary>
public static void Add<T>(ReadOnlySpan<MyVector2<T>> left, ReadOnlySpan<MyVector2<T>> right, Span<MyVector2<T>> result)
    where T : struct, INumber<T>, IMinMaxValue<T>
    => Tensor.Add(MemoryMarshal.Cast<MyVector2<T>, T>(left), MemoryMarshal.Cast<MyVector2<T>, T>(right), MemoryMarshal.Cast<MyVector2<T>, T>(result));
```

In these instances, the span of `MyVector2<T>` is converted to a span of `T` to enable the tensor to execute the `Add` operation. Subsequently, the outcome is re-cast to a span of `MyVector2<T>`. Each `MyVector2<T>` within the destination span will encapsulate the outcome of the operation applied to the corresponding `MyVector2<T>` within the source spans.