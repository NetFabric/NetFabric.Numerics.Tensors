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
However, `NetFabric` relies on `Vector<T>` for vectorization, which supports only primitive numeric types. This limitation prevents vectorization when using other types.

It's worth mentioning that because `MyVector2<T>` consists of two fields of the same type and is always a value type, the contiguous storage in memory facilitates easy conversion from a span of `MyVector2<T>` to a span of `T` using `MemoryMarshal.Cast<MyVector2<T>, T>()`. This operation returns a span containing all the vector coordinates laid out contiguously. The `Apply()` and `Aggregate()` methods offer overloads that accommodate up to 4 elements of the same type.

This capability facilitates the implementation of the `Sum` operation for a span of `MyVector2<T>`:

```csharp
public static MyVector2<T> Sum<T>(this ReadOnlySpan<MyVector2<T>> source)
    where T : struct, INumber<T>
    => new(Tensor.Sum2D(MemoryMarshal.Cast<MyVector2<T>, T>(source)));
```

Here, the tensor efficiently leverages vectorization for improved performance in the `Sum` operation on a span of `MyVector2<T>`, treating it as a span of its internal values. The use of `Sum2D` specifically indicates the intention to calculate the sum of every other item in the span, returning a 2D tuple that is then converted into the result `MyVector2<T>` by using the constructor.

Regarding the `Apply` operation, it's used on each element while preserving order in the destination span. Employing `MemoryMarshal.Cast()` on both the sources and the destination is suitable:

```csharp
public static void Add<T>(ReadOnlySpan<MyVector2<T>> left, MyVector2<T> right, Span<MyVector2<T>> result)
    where T : struct, INumber<T>
    => Tensor.Add(MemoryMarshal.Cast<MyVector2<T>, T>(left), (right.X, right.Y), MemoryMarshal.Cast<MyVector2<T>, T>(result));

public static void Add<T>(ReadOnlySpan<MyVector2<T>> left, ReadOnlySpan<MyVector2<T>> right, Span<MyVector2<T>> result)
    where T : struct, INumber<T>
    => Tensor.Add(MemoryMarshal.Cast<MyVector2<T>, T>(left), MemoryMarshal.Cast<MyVector2<T>, T>(right), MemoryMarshal.Cast<MyVector2<T>, T>(result));
```

The first method yields a `result` span of vectors, with each vector containing the sum of the corresponding vectors in the `left` span and a fixed vector `right`. To achieve this, it invokes the `Add` overload, passing a value tuple with two parameters as the second argument.

The second method produces a `result` span where each vector holds the sum of the respective vectors in the `left` and `right` spans.

In these scenarios, the spans of `MyVector2<T>` are transformed into spans of `T` to enable the tensor to perform the `Add` operation. Afterwards, the result is recast to spans of `MyVector2<T>`. Each `MyVector2<T>` within the destination span encapsulates the result of the operation applied to the corresponding `MyVector2<T>` within the source spans.