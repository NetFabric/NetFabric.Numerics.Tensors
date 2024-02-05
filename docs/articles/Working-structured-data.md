# Working with Tensors for Structured Data

In the `NetFabric.Numerics.Tensors` library, tensors can efficiently handle any value-type that meets specific requirements. For instance, consider the implementation of a 2D vector, `MyVector2<T>`, which supports the `Sum` operation by implementing both `IAdditionOperators<T, T, T>` and `IAdditiveIdentity<T, T>`:

```csharp
/// <summary>
/// Represents a 2D vector.
/// </summary>
public readonly record struct MyVector2<T>(T X, T Y)
    : IAdditiveIdentity<MyVector2<T>, MyVector2<T>>
    , IAdditionOperators<MyVector2<T>, MyVector2<T>, MyVector2<T>>
    where T : struct, INumber<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MyVector2{T}"/> struct.
    /// </summary>
    /// <param name="tuple">The tuple representing the vector.</param>
    public MyVector2(ValueTuple<T, T> tuple)
        : this(tuple.Item1, tuple.Item2)
    { }

    /// <summary>
    /// Gets the additive identity for <see cref="MyVector2{T}"/>.
    /// </summary>
    public static MyVector2<T> AdditiveIdentity
        => new(T.AdditiveIdentity, T.AdditiveIdentity);

    /// <summary>
    /// Adds two vectors.
    /// </summary>
    public static MyVector2<T> operator +(MyVector2<T> left, MyVector2<T> right)
        => new(left.X + right.X, left.Y + right.Y);
}
```

However, when it comes to using `Vector<T>`, direct support is lacking, preventing the tensor from optimizing the `Sum` operation using SIMD.

It's important to note that since `MyVector2<T>` comprises two fields of the same type and is always a value type, the adjacent storage in memory allows effortless conversion from `Span<MyVector2<T>>` to `Span<T>` using `MemoryMarshal.Cast<MyVector2<T>, T>()`.

This capability facilitates the implementation of the `Sum` operation for a span of `MyVector2<T>`:

```csharp
/// <summary>
/// Computes the sum of 2D vectors in the span.
/// </summary>
public static MyVector2<T> Sum<T>(this ReadOnlySpan<MyVector2<T>> source)
    where T : struct, INumber<T>, IMinMaxValue<T>
    => new(Tensor.Sum2D(MemoryMarshal.Cast<MyVector2<T>, T>(source)));
```

Here, the tensor efficiently leverages SIMD for improved performance in the `Sum` operation on a span of `MyVector2<T>`, treating it as a span of its internal values. The use of `Sum2D` specifically indicates the intention to calculate the sum of every other item in the span, returning a 2D tuple that is then converted into the result `MyVector2<T>` by using the constructor.

Regarding the `Apply` operation, it is applied to each element while maintaining order in the destination span. Applying `MemoryMarshal.Cast<MyVector2<T>, T>()` to both the sources and the destination is appropriate:

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