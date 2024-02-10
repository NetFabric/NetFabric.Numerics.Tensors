# Expanding the Library's Functionality

While `NetFabric.Numerics.Tensors` offers a range of primitive operations, combining them may not always be efficient, leading to multiple iterations over the sources. Anticipating and implementing all potential combinations can be a challenging task. To address this, `NetFabric.Numerics.Tensors` supports the implementation of custom operators. This feature allows you to define the specific operation for each element of the source while still taking advantage of the high-performance reusable iteration code.

## Harnessing Pre-existing Operations

Before delving into crafting custom operations, assess whether you can make use of any of the predefined operations. For example, the conversion of degrees into radians adheres to the formula `(value * PI) / 180.0`. The `MultiplyDivide` operation proves handy here, as it multiplies the first two parameters and divides by the third. Implement it as follows:

```csharp
public static void DegreesToRadians<T>(ReadOnlySpan<T> left, Span<T> destination)
    where T : struct, INumberBase<T>, IFloatingPointConstants<T>
    => MultiplyDivide(left, T.Pi, T.CreateChecked(180), destination);
```

Beyond the fundamental operations like `Negate`, `Add`, `Subtract`, `Multiply`, and `Divide`, you can explore composed operations such as `AddMultiply`, `MultiplyAdd`, and `MultiplyDivide`.

## Defining custom operators

### Application

The `Apply<T, TOperator>()` method facilitates the execution of operations that involve one, two, or three source `ReadOnlySpan<T>`, with the results stored in the destination `Span<T>`. The operation can occur in-place if the destination is the same as one of the sources. Additionally, it accommodates a value or a tuple in place of the second or third source spans.

This method requires two generic parameters. The first specifies the type of the span elements, while the second defines the operator to be applied. Operators must adhere to one of the following interfaces:

```csharp
public interface IUnaryOperator<T, TResult> 
    : IOperator
    where T : struct
    where TResult : struct
{
    static abstract TResult Invoke(T x);
    static abstract Vector<TResult> Invoke(ref readonly Vector<T> x);
}

public interface IBinaryOperator<T1, T2, TResult> 
    : IOperator
    where T1 : struct
    where T2 : struct
    where TResult : struct
{
    static abstract TResult Invoke(T1 x, T2 y);
    static abstract Vector<TResult> Invoke(ref readonly Vector<T1> x, ref readonly Vector<T2> y);
}

public interface IBinaryScalar<T1, T2, TResult> 
    : IOperator
    where T1 : struct
    where TResult : struct
{
    static abstract TResult Invoke(T1 x, T2 y);
    static abstract Vector<TResult> Invoke(ref readonly Vector<T1> x, T2 y);
}

public interface ITernaryOperator<T1, T2, T3, TResult> 
    : IOperator
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where TResult : struct
{
    static abstract TResult Invoke(T1 x, T2 y, T3 z);
    static abstract Vector<TResult> Invoke(ref readonly Vector<T1> x, ref readonly Vector<T2> y, ref readonly Vector<T3> z);
}
```

It's essential to note that these interfaces make use of [static virtual members](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/static-virtual-interface-members), a feature introduced in .NET 7. No instance of the operator is required to utilize the methods, and operators are pure, devoid of internal state.

Each operator must implement two `Invoke` methods that define the operation between elements of type `T` or vectors of type `Vector<T>`. Each interface specifies a different number of parameters.

Consider the square operator as an example, which functions as a unary operator designed to operate on a single source. It implements the `IUnaryOperator<T>` interface. The generic type `T` is restricted to `struct` and must implement `IMultiplyOperators<T, T, T`, indicating that only value types with the `*` operator implemented are suitable. The `Invoke` methods straightforwardly execute the square operation for either a single `T` value or a `Vector<T>` of values:

```csharp
public readonly struct SquareOperator<T> 
    : IUnaryOperator<T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    public static T Invoke(T x)
        => x * x;

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => x * x;
}
```

Similarly, consider an addition operator, a binary operator that works on two sources, the addends. It implements the `IBinaryOperator<T, T, T>` interface. The generic type `T` is confined to `struct` and must implement `IAdditionOperators<T, T, T>`, indicating that only value types with the `+` operator implemented are eligible. The `Invoke` methods straightforwardly perform the addition operation for either a single `T` value or a `Vector<T>` of values:

```csharp
readonly struct AddOperator<T> 
    : IBinaryOperator<T, T, T>
    where T : struct, IAdditionOperators<T, T, T>
{
    public static T Invoke(T x, T y)
        => x + y;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x + y;
}
```

Furthermore, consider an operator calculating the addition followed by multiplication of values. This is a ternary operator that handles three sources: the two addends plus the multiplier. it implements the `ITernaryOperator<T, T, T, T>` interface. The generic type `T` is constrained to `struct`, `IAdditionOperators<T, T, T>`, and `IMultiplyOperators<T, T, T>`, indicating that only value types with the `+` and `*` operators implemented are applicable. The `Invoke` methods straightforwardly perform the addition operation followed by multiplication for either a single `T` value or a `Vector<T>` of values:

```csharp
readonly struct AddMultiplyOperator<T> 
    : ITernaryOperator<T, T, T, T>
    where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
{
    public static T Invoke(T x, T y, T z)
        => (x + y) * z;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y, ref readonly Vector<T> z)
        => (x + y) * z;
}
```

### Aggregation

The `Aggregate<T, TOperator>()` method serves as a tool for consolidating a source span of data into either a single value or a tuple of values.

This method requires two generic parameters. The first specifies the type of the span elements, while the second defines the operator to be applied. Operators must adhere to the following interface:

```csharp
public interface IAggregationOperator<T, TResult> 
    : IBinaryOperator<TResult, T, TResult>
    where T : struct
    where TResult : struct
{
    static virtual TResult Seed 
        => Throw.NotSupportedException<TResult>();

    static abstract TResult Invoke(TResult x, TResult y);
}
```

Each operator must implement a property that returns the seed value for the operation, which initializes the aggregation process. Additionally, operators must implement the two `Invoke` methods required by the `IBinaryOperator<T, T, T>` interface, along with an additional one `Invoke` methods that aggregate the final result.

Consider, for instance, an operator that calculates the sum of all elements in the source. This serves as an aggregation operator, providing a value. It implements the `IAggregationOperator<T, T>` interface. The generic type `T` is restricted to `struct`, `IAdditiveIdentity<T, T>`, and `IAdditionOperators<T, T, T>`, signifying that only value types with both the additive identity and the `+` operator implemented are suitable. The `Seed` initializes the sum using the additive identity. The `Invoke` methods handle the addition of `T` and `Vector<T>` values.

```csharp
readonly struct SumOperator<T> : IAggregationOperator<T, T>
    where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    public static T Seed
        => T.AdditiveIdentity;

    public static T Invoke(T x, ref readonly Vector<T> y)
        => x + Vector.Sum(y);

    public static T Invoke(T x, T y)
        => x + y;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x + y;
}
```

## Operators Unsuitable for Vectorization

It's crucial to acknowledge that certain operators may lack full compatibility or support with `Vector<T>`. While alternative optimizations can still be applied, vectorization will have to be disabled for these operations.

All operator interfaces inherit from the following interface:

```csharp
public interface IOperator
{
    static virtual bool IsVectorizable
        => true;
}
```

This signifies that all operator interfaces include a boolean property indicating whether the operation is vectorizable. By default, the property returns `true`, making overloading unnecessary unless explicitly intending to return `false`.

Consider the left shift operation (`<<`) as an example. While `Vector<T>` supports it only for signed and unsigned integer primitives, any type implementing `IShiftOperators<TSelf, TOther, TResult>` can support left shift, including third-party-developed types. To cover all scenarios, a generic non-vectorizable operator can be implemented, alongside specific operators for each vectorizable type. The following example illustrates this approach, focusing on the `sbyte` type:

```csharp
readonly struct ShiftLeftOperator<T, TResult> : IBinaryScalar<T, int, TResult>
    where T : struct, IShiftOperators<T, int, TResult>
    where TResult : struct
{
    public static bool IsVectorizable
        => false;

    public static TResult Invoke(T value, int count)
        => value << count;

    public static Vector<TResult> Invoke(ref readonly Vector<T> value, int count)
        => Throw.NotSupportedException<Vector<TResult>>();
}

readonly struct ShiftLeftSByteOperator : IBinaryScalar<sbyte, int, sbyte>
{
    public static sbyte Invoke(sbyte value, int count)
        => (sbyte)(value << count);

    public static Vector<sbyte> Invoke(ref readonly Vector<sbyte> value, int count)
        => Vector.ShiftLeft(value, count);
}
```

Note that the operator implements `IBinaryScalar<T1, T2, TResult`. This signifies a binary operator where the vectorization method accepts a value instead of a vector as the second parameter.

## Using Custom Operators

To leverage the custom operators, simply utilize either the `Apply()` or `Aggregate()` methods, providing the required generic parameters and method parameters.

For simplifying the process, it's recommended to create methods that abstract this complexity. Consider, for example, an operation akin to `Add`; you can provide the following overloads:

```csharp
public static void Add<T>(ReadOnlySpan<T> x, T y, Span<T> destination)
    where T : struct, IAdditionOperators<T, T, T>
    => Apply<T, AddOperator<T>>(x, y, destination);

public static void Add<T>(ReadOnlySpan<T> x, ValueTuple<T, T> y, Span<T> destination)
    where T : struct, IAdditionOperators<T, T, T>
    => Apply<T, AddOperator<T>>(x, y, destination);

public static void Add<T>(ReadOnlySpan<T> x, ValueTuple<T, T, T> y, Span<T> destination)
    where T : struct, IAdditionOperators<T, T, T>
    => Apply<T, AddOperator<T>>(x, y, destination);

public static void Add<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
    where T : struct, IAdditionOperators<T, T, T>
    => Apply<T, AddOperator<T>>(x, y, destination);
```

These overloads not only support adding values from two spans but also adding either a constant or a tuple of constants to the values in a span.

For an aggregate operation like `Sum`, consider providing the following overloads:

```csharp
public static T Sum<T>(ReadOnlySpan<T> source)
    where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
    => Aggregate<T, SumOperator<T>>(source);

public static ValueTuple<T, T> Sum2D<T>(ReadOnlySpan<T> source)
    where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
    => Aggregate2D<T, SumOperator<T>>(source);

public static ValueTuple<T, T, T> Sum3D<T>(ReadOnlySpan<T> source)
    where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
    => Aggregate3D<T, SumOperator<T>>(source);

public static ValueTuple<T, T, T, T> Sum4D<T>(ReadOnlySpan<T> source)
    where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
    => Aggregate4D<T, SumOperator<T>>(source);
```

For an operation akin to `ShiftLeft`, which offers implementations for specific types, provide the following overloads. The compiler will automatically select the suitable overload based on the type in use:

```csharp
public static void ShiftLeft<T>(ReadOnlySpan<T> value, int count, Span<T> destination)
    where T : struct, IShiftOperators<T, int, T>
    => ShiftLeft<T, T>(value, count, destination);

public static void ShiftLeft<T, TResult>(ReadOnlySpan<T> value, int count, Span<TResult> destination)
    where T : struct, IShiftOperators<T, int, TResult>
    where TResult : struct
    => ApplyScalar<T, int, TResult, ShiftLeftOperator<T, TResult>>(value, count, destination);

public static void ShiftLeft(ReadOnlySpan<sbyte> value, int count, Span<sbyte> destination)
    => ApplyScalar<sbyte, int, sbyte, ShiftLeftSByteOperator>(value, count, destination);
```

For brevity, only the overload for the `sbyte` type is presented here.

`NetFabric.Numerics.Tensors` provides the overloads for these operators, but you can effortlessly implement your own operator and apply it in a similar manner.