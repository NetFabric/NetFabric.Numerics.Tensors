# Extending the Library Functionality

While `NetFabric.Numerics.Tensors` offers a variety of predefined operations, their combination may not be optimal, leading to multiple iterations over the sources. Anticipating and implementing all potential combinations can pose a significant challenge. To address this concern, `NetFabric.Numerics.Tensors` facilitates the implementation of custom operators and the composition of operators. These capabilities empower users to define specific operations while still harnessing the high-performance reusable iteration code.

## Harnessing Pre-defined Operations

Before delving into crafting custom operations, assess whether you can make use of any of the predefined operations. For example, the conversion of degrees into radians adheres to the formula `(value * PI) / 180.0`. The `MultiplyDivide` operation proves handy here, as it multiplies the first two parameters and divides by the third. As an example, the predefined `DegreesToRadians()` operation is implemented as follows:

```csharp
public static void DegreesToRadians<T>(ReadOnlySpan<T> left, Span<T> destination)
    where T : struct, INumberBase<T>, IFloatingPointConstants<T>
    => MultiplyDivide(left, T.Pi, T.CreateChecked(180), destination);
```

Beyond the fundamental operations like `Negate`, `Add`, `Subtract`, `Multiply`, and `Divide`, you can explore composed operations such as `AddMultiply`, `MultiplyAdd`, and `MultiplyDivide`.

## Custom Operator Definitions

All methods enabling operator functionality are housed within the `Tensor` static class. Within this class, there are two primary categories of methods: `Apply` and `Aggregate`. The `Apply` methods are designed to execute operations on one, two, or three source `ReadOnlySpan<T>`, with the results stored in the destination `Span<T>`. Conversely, the `Aggregate` methods aim to condense a source span of data into either a single value or a tuple of values.

### Apply

The `Apply()` method simplifies the execution of operations involving one, two, or three source `ReadOnlySpan<T>`, with the results stored in the destination `Span<T>`. It supports in-place operations if the destination matches one of the sources.

`Apply()` provides overloads that accept a scalar value or a tuple in place of the second or third source spans, when applicable. These overloads save the need to allocate a new span for the second or third source when the operation is performed on a single scalar value or a tuple of values.

This method requires several generics parameters. The initial parameters specify the type of elements in the source span, with the count varying depending on the operator used. Following this is the type of elements in the destination span, and finally, the operator to be applied. In cases where the type remains consistent across all spans, an alternative overload with fewer generics parameters is available for the `Apply()` method.

Operators must adhere to one of the following interfaces:

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

public interface IBinaryScalarOperator<T1, T2, TResult> 
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

> **NOTE:** It's essential to note that these interfaces make use of [static virtual members](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/static-virtual-interface-members), a feature introduced in .NET 7. No instance of the operator is required to utilize the methods, and operators are pure, devoid of internal state.

Each operator is required to implement two `Invoke` methods, defining the operation between elements of type `T` or vectors of type `Vector<T>`. The second method is utilized when both the operator and the hardware support vectorization. Conversely, the first method is employed for every element that is not handled by vectorization.

Each interface specifies a varying number of operator parameters. The utilization of distinct generic types for each parameter ensures the maximum generalization of the operators.

Consider the square operator as an example, which functions as a unary operator designed to operate on a single source. It implements the `IUnaryOperator<T>` interface. The generic type `T` is restricted to `struct` and must implement `IMultiplyOperators<T, T, T`, indicating that only value types with the `*` operator implemented are suitable. The `Invoke` methods straightforwardly execute the square operation for either a single `T` value or a `Vector<T>` of values:

```csharp
public readonly struct SquareOperator<T> 
    : IUnaryOperator<T>
    where T : struct, IMultiplyOperators<T, T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => x * x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => x * x;
}
```

Similarly, consider an addition operator, a binary operator that works on two sources, the addends. It implements the `IBinaryOperator<T, T, T>` interface. The generic type `T` is confined to `struct` and must implement `IAdditionOperators<T, T, T>`, indicating that only value types with the `+` operator implemented are eligible. The `Invoke()` methods straightforwardly perform the addition operation for either a single `T` value or a `Vector<T>` of values:

```csharp
readonly struct AddOperator<T> 
    : IBinaryOperator<T, T, T>
    where T : struct, IAdditionOperators<T, T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => x + y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x + y;
}
```

The shift left operator (`<<`) is a binary operator that operates on a source value and a count. Typically, in this scenario, all elements in the span are shifted by the same amount. Actually, `Vector<T>` only supports this particular scenario. Hence, the shift left operator is implemented as a binary scalar operator, where the second parameter is a scalar value instead of a `Vector<T>`.

The shift left operation is supported by `Vector<T>` only for signed and unsigned integer primitives. Below is the specific implementation for the `sbyte` type.  It implements the `IBinaryScalarOperator<T, T, T>` interface:

```csharp
readonly struct ShiftLeftSByteOperator
    : IBinaryScalarOperator<sbyte, int, sbyte>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte Invoke(sbyte value, int count)
        => (sbyte)(value << count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<sbyte> Invoke(ref readonly Vector<sbyte> value, int count)
        => Vector.ShiftLeft(value, count);
}
```

> **Note:** When multiple scalars are needed, a value-typed tuple can serve as the parameter.

Furthermore, consider an operator calculating the addition followed by multiplication of values. This is a ternary operator that handles three sources: the two addends plus the multiplier. It implements the `ITernaryOperator<T, T, T, T>` interface. The generic type `T` is constrained to `struct`, `IAdditionOperators<T, T, T>`, and `IMultiplyOperators<T, T, T>`, indicating that only value types with the `+` and `*` operators implemented are applicable. The `Invoke` methods straightforwardly perform the addition operation followed by multiplication for either a single `T` value or a `Vector<T>` of values:

```csharp
readonly struct AddMultiplyOperator<T> 
    : ITernaryOperator<T, T, T, T>
    where T : struct, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
{
    public static T Invoke(T x, T y, T z)
        => (x + y) * z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y, ref readonly Vector<T> z)
        => (x + y) * z;
}
```

For an operation akin to `Add`, the library provides all the following overrides so that the user can choose the most suitable overload based on their requirements. It can take a vector, single value or a tuple in place of the second or third source spans:

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

These overloads that take a tuple as a parameter prove especially valuable when dealing with multi-dimensional data. For further details, refer to the section on "Working with Tensors for Structured Data".

### Apply2

The `Apply2()` method simplifies the execution of two distinct operations within a single iteration of the source, storing the results in two separate destination `Span<T>` instances.

This method requires five generics parameters. The first specifies the type of elements within the source span, while the second and third specify the types of elements within the destination spans. The fourth and fifth parameters specify the operators to be applied. For scenarios where the type remains consistent across all spans, an alternative overload with fewer generics parameters is available for the `Apply2()` method.

For instance, let's consider the `SinCos()` method, which computes the sine and cosine of an angle simultaneously. This library offers two overloads for the `SinCos()` method. The first utilizes `Apply()` to return a span containing tuples of sine and cosine results, while the second employs `Apply2()` to return the results separately in designated spans.

```csharp
public static void SinCos<T>(ReadOnlySpan<T> left, Span<(T Sin, T Cos)> destination)
    where T : struct, ITrigonometricFunctions<T>
    => Apply<T, (T Sin, T Cos), SinCosOperator<T>>(left, destination);

public static void SinCos<T>(ReadOnlySpan<T> left, Span<T> sinDestination, Span<T> cosDestination)
    where T : struct, ITrigonometricFunctions<T>
    => Apply2<T, SinOperator<T>, CosOperator<T>>(left, sinDestination, cosDestination);
```

With the operators implemented as follows:

```csharp
readonly struct SinOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Sin(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct CosOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => T.Cos(x);

    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<T>>();
}

readonly struct SinCosOperator<T>
    : IUnaryOperator<T, (T Sin, T Cos)>
    where T : struct, ITrigonometricFunctions<T>
{
    public static bool IsVectorizable
        => false; 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (T Sin, T Cos) Invoke(T x)
        => T.SinCos(x);

    public static Vector<(T Sin, T Cos)> Invoke(ref readonly Vector<T> x)
        => Throw.InvalidOperationException<Vector<(T Sin, T Cos)>>();
}

```

The user can choose the most suitable overload based on their requirements.

### Aggregate

The `Aggregate()` method is a versatile tool for consolidating a source span of data into either a single value or a tuple of values. It relies on two operators: one for transforming the source elements and another for aggregating the transformed elements.

This method is flexible, accepting five generics parameters. The first specifies the type of elements in the source span, the second specifies the output type of the transform operator, and the third specifies the element type of the destination span. The fourth and fifth parameters specify the transform and aggregation operators, respectively.

For cases where no transformation is necessary and the type remains consistent across all spans, a couple of alternative overloads with fewer generics parameters are provided for the `Aggregate()` method.

It's worth noting that the transform operator must implement either the `IUnaryOperator<T, TResult>` or `IBinaryOperator<T1, T2, TResult>` interface, while the aggregation operator must implement the `IAggregationOperator<TResult, TResult>` interface:

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

Consider, for instance, an operator that calculates the sum of all elements in the source. This serves as an aggregation operator, resulting in a value or a tuple of value: 

```csharp
readonly struct SumOperator<T>
    : IAggregationOperator<T, T>
    where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    public static T Seed 
        => T.AdditiveIdentity;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => x + y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x + y;
}
```

It implements the `IAggregationOperator<T, T>` interface. The generic type `T` is restricted to `struct`, `IAdditiveIdentity<T, T>`, and `IAdditionOperators<T, T, T>`, signifying that only value types with both the additive identity and the `+` operator implemented are suitable. The `Seed` initializes the sum using the additive identity. The `Invoke()` methods straightforwardly perform the addition operation for either a single `T` value or a `Vector<T>` of values.

Additional variants of the `Aggregate()` method are available: `Aggregate2D()`, `Aggregate3D()`, and `Aggregate4D()`. These specialized methods are tailored to aggregate the source span into a tuple of two, three, or four values, respectively. They prove especially valuable when dealing with multi-dimensional data. For further details, refer to the section on "Working with Tensors for Structured Data".

For an aggregate operation akin to `Sum`, the library provides all the following operations:

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

## AggregatePropagateNaN

The `AggregatePropagateNaN()` method is a specialized version of the `Aggregate()` method, designed to handle operations that propagate `NaN` values, exiting the iteration as soon as possible. It is particularly useful when dealing with floating-point data, as it ensures that the presence of `NaN` values in the source span is reflected in the final result. This method requires the same generics parameters as the `Aggregate()` method.

The use of this method means that the operator doesn't have to deal with propagating `NaN` values, as the `AggregatePropagateNaN()` method handles this automatically. This simplifies the implementation of the operator, as it only needs to focus on the operation itself and considerably improves its performance.

As an example, consider the implementation of the `Min()` aggregation operation:

```csharp
public static T Min<T>(ReadOnlySpan<T> left)
    where T : struct, INumber<T>, IMinMaxValue<T>
    => Tensor.AggregatePropagateNaN<T, MinOperator<T>>(left);
```

The implementation of the `MinOperator<T>` operator is as follows:

```csharp
readonly struct MinOperator<T>
    : IAggregationOperator<T, T>
    where T : struct, INumber<T>, IMinMaxValue<T>
{
    public static T Seed
        => T.MaxValue;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.MinNumber(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.Min(x, y);
}
```

It implements the `IAggregationOperator<T, T>` interface. The generic type `T` is constrained to `struct`, `INumber<T>`, and `IMinMaxValue<T>`, indicating that only value types with the `MinNumber()` method implemented are suitable. The `Seed` initializes the aggregation using the type's maximum value. The `Invoke` methods handle the minimum of `T` and `Vector<T>` values. The methods `T.MinNumber()` and `Vector.Min()` used do not propagate `NaN` values.

For reference, the `MinPropagateNaNOperator` is a specialized version of the `MinOperator<T>` operator. As its name suggests, it propagates `NaN` values. This operator, utilized with the `Apply()` method, is implemented as follows:

```csharp
readonly struct MinPropagateNaNOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, INumber<T>, IMinMaxValue<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => T.Min(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half)
            ? Vector.ConditionalSelect(Vector.Equals(x, x),
                Vector.ConditionalSelect(Vector.Equals(y, y),
                    Vector.ConditionalSelect(Vector.Equals(x, y),
                        Vector.ConditionalSelect(Vector.LessThan(x, Vector<T>.Zero), x, y),
                        Vector.Min(x, y)),
                    y),
                x)
            : Vector.Min(x, y);
```

It uses the `Min()` method provided by the `INumber<T>` interface, which handles the propagation of `NaN` values. The `Invoke()` method for `Vector<T>` values is more complex, as it needs to explicitly handle the propagation of `NaN` values for floating-point types. The method `Vector.ConditionalSelect()` is an equivalent to an `if` statement when dealing with vectors.


### AggregatePropagateNaN2

The `AggregatePropagateNaN2()` method streamlines the execution of two distinct operations within a single iteration of the source, returning the results in a tuple.

This method requires three generic parameters. The first specifies the type of elements within the source span, while the second and third parameters specify the operators to be applied.

For example, let's consider the `MinMax()` method, which computes the minimum and maximum in a span simultaneously. This library provides the following operation:

```csharp
public static (T Min, T Max) MinMax<T>(ReadOnlySpan<T> left)
    where T : struct, INumber<T>, IMinMaxValue<T>
    => Tensor.AggregatePropagateNaN2<T, MinOperator<T>, MaxOperator<T>>(left);
```

The `MinOperator<T>` and `MaxOperator<T>` operators used are the ones previously described in the `Aggregate()` method.

## Operators Unsuitable for Vectorization

It's important to mention that certain operators cannot be vectorized when using `Vector<T>`. While this library can still enhance processing performance through alternative optimizations, vectorization must be disabled for these specific operations.

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
readonly struct ShiftLeftOperator<T, TResult> 
    : IBinaryScalar<T, int, TResult>
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

readonly struct ShiftLeftSByteOperator 
    : IBinaryScalar<sbyte, int, sbyte>
{
    public static sbyte Invoke(sbyte value, int count)
        => (sbyte)(value << count);

    public static Vector<sbyte> Invoke(ref readonly Vector<sbyte> value, int count)
        => Vector.ShiftLeft(value, count);
}
```

The `ShiftLeftOperator<T, TResult>` operator is non-vectorizable, as indicated by the `IsVectorizable` property. The `Invoke()` method for `Vector<T>` values throws a `NotSupportedException`. Conversely, the `ShiftLeftSByteOperator` operator is vectorizable, as it doesn't include the `IsVectorizable` property, and the `Invoke()` method for `Vector<sbyte>` values is implemented.

For an operation akin to `ShiftLeft`, which offers implementations for specific types, the library provides all the following operations. The compiler will automatically select the suitable overload based on the type in use:

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
