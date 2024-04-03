# Extending the Library Functionality

While `NetFabric.Numerics.Tensors` offers a variety of predefined operations, their combination may not be optimal, leading to multiple iterations over the sources. Anticipating and implementing all potential combinations can pose a significant challenge. To address this concern, `NetFabric.Numerics.Tensors` facilitates the implementation of custom operators and the composition of operators. These capabilities empower users to define specific operations while still harnessing the high-performance reusable iteration code.

## Harnessing Pre-defined Operations

Before delving into crafting custom operations, assess whether you can make use of any of the predefined operations. For example, the conversion of degrees into radians adheres to the formula `(value * PI) / 180.0`. The `MultiplyDivide` operation proves handy here, as it multiplies the first two parameters and divides by the third. As an example, the predefined `DegreesToRadians()` operation is implemented as follows:

```csharp
public static void DegreesToRadians<T>(ReadOnlySpan<T> left, Span<T> destination)
    where T : struct, INumberBase<T>, IFloatingPointConstants<T>
    => MultiplyDivide(left, T.Pi, T.CreateChecked(180), destination);
```

Consider evaluating the potential utilization of combinations of predefined operators with the `Apply()` or `Aggregate()` methods, as elaborated below. The predefined operators are all defined in the `NetFabric.Numerics.Tensors.Operators` namespace.

## Custom Operation Definitions

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

The shift left operation is supported by `Vector<T>` only for signed and unsigned integer primitives. Below is the specific implementation for the `sbyte` type. It implements the `IBinaryScalarOperator<T, T, T>` interface:

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

The `Aggregate()` method is a powerful tool for consolidating a source span of data into either a single value or a tuple of values. It operates using two essential operators: one for transforming the source elements and another for aggregating the transformed elements. This method adheres to the IEEE 754 standard for floating-point arithmetic; if any element's transformation or aggregation results in `NaN`, it returns `NaN`.

It's essential to note that the transform operator must implement either the `IUnaryOperator<T, TResult>` or `IBinaryOperator<T1, T2, TResult>` interface, while the aggregation operator must implement the `IAggregationOperator<TResult, TResult>` interface.

```csharp
public interface IAggregationOperator<T, TResult>
    : IBinaryOperator<TResult, T, TResult>
    where T : struct
    where TResult : struct
{
    static virtual TResult Seed
        => Throw.NotSupportedException<TResult>();

    static abstract TResult Invoke(TResult x, TResult y);

    static abstract TResult Invoke(TResult x, ref readonly Vector<TResult> y);
}
```

Each operator must provide a property returning the seed value, initializing the aggregation process. Additionally, operators must implement the required `Invoke` methods by the `IBinaryOperator<T1, T2, TResult>` interface, along with two extra `Invoke` methods that aggregates the final result. On the last `Invoke` it's guaranteed that the parameters will not contain `NaN`.

For example, consider an operation calculating the sum of all elements in the source. It employs the following aggregation operator:

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, ref readonly Vector<T> y)
        => x + Vector.Sum(y);
}
```

This operator adheres to the `IAggregationOperator<T, T>` interface. The generic type `T` is constrained to `struct`, `IAdditiveIdentity<T, T>`, and `IAdditionOperators<T, T, T>`, indicating that only value types with both the additive identity and the `+` operator implemented are suitable. The `Seed` initializes the sum using the additive identity. The two first `Invoke()` methods straightforwardly perform the addition operation for either a single `T` value or a `Vector<T>` of values. The last `Invoke`, sums the aggregated value to the sum of the elements in the aggregated vector.

The `Sum()` operation can be utilized as follows:

```csharp
public static T Sum<T>(ReadOnlySpan<T> source)
    where T : struct, INumberBase<T>
    => Tensor.Aggregate<T, SumOperator<T>>(source);
```

Here, the generic type `T` is restricted to `INumberBase<T>` as the `Aggregate()` method necessitates the method `T.IsNaN()` for checking if any transformation or aggregation results in `NaN`.

A transform operator can be applied to transform the source elements before aggregation. For instance, consider an operation calculating the sum of the squares of all elements in the source. This operation can be employed to compute the square of the length of any n-dimensional vector:

```csharp
public static T SumOfSquares<T>(ReadOnlySpan<T> source)
    where T : struct, IMultiplyOperators<T, T, T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
    => Tensor.Aggregate<T, SquareOperator<T>, SumOperator<T>>(source);
```

Here, the `SquareOperator<T>` and `SumOperator<T>` operators are utilized. The `SquareOperator<T>` operator is a unary operator that squares the source elements, while the `SumOperator<T>` operator aggregates the transformed elements.

The transform operator can also take two parameters, as shown in the `ProductOfAdditions()` operation, which computes the sum of the products of corresponding elements in two sources:

```csharp
public static T? ProductOfAdditions<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
    where T : struct, IMultiplicativeIdentity<T, T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
    => x.IsEmpty
        ? null
        : Tensor.Aggregate<T, AddOperator<T>, ProductOperator<T>>(x, y);
```

The `Aggregate()` method also provides overloads supporting different types for the source, transformed, and return values.

Additional variants of the `Aggregate()` method are available: `Aggregate2D()`, `Aggregate3D()`, and `Aggregate4D()`. These specialized methods are tailored to aggregate the source span into tuples of two, three, or four values, respectively, which is particularly useful for multi-dimensional data. For further details, refer to the "Working with Tensors for Structured Data" section.

### IndexOfAggregate

The `IndexOfAggregate()` method functions similarly to `Aggregate()` but returns the index of the first element matching the value returned by `Aggregate()`.

For instance, the `IndexOfMax()` aggregation retrieves the index of the maximum value in the source span:

```csharp
public static int IndexOfMax<T>(ReadOnlySpan<T> source)
    where T : struct, INumber<T>, IMinMaxValue<T>
    => Tensor.IndexOfAggregate<T, MaxAggregationOperator<T>>(source);
```

The `IndexOfAggregate()` method can utilize a transform operator accepting one or two parameters. Consider its application in the `IndexOfMaxSum()` aggregation operation, determining the index of the first element matching the maximum sum of corresponding elements in two sources:

```csharp
public static int IndexOfMaxSum<T>(ReadOnlySpan<T> left, ReadOnlySpan<T> right)
    where T : struct, INumber<T>, IMinMaxValue<T>
    => Tensor.IndexOfAggregate<T, SumOperator<T>, MaxAggregationOperator<T>>(left, right);
```

Here, two operators are specified as generic parameters. The first operator transforms the source elements, while the second operator aggregates the transformed elements.

### Aggregate2

The `Aggregate2()` method simplifies the execution of two distinct operations within a single iteration of the source, returning the results in a tuple.

This method necessitates three generic parameters. The first determines the type of elements within the source span, while the second and third parameters specify the operators to apply.

For instance, let's examine the `MinMax()` method, which calculates both the minimum and maximum values in a span simultaneously:

```csharp
public static (T Min, T Max) MinMax<T>(ReadOnlySpan<T> source)
    where T : struct, INumber<T>, IMinMaxValue<T>
    => Tensor.Aggregate2<T, MinNumberAggregationOperator<T>, MaxNumberAggregationOperator<T>>(source);
```

### AggregateNumber

The `AggregateNumber` method functions akin to the `Aggregate()` method, but it omits propagating `NaN` values if encountered. This alternative implementation exhibits better performance and is suitable when `NaN` values are guaranteed not to occur in the source spans.

For instance, let's examine the implementation of the `SumNumber()` aggregation operation:

```csharp
public static T SumNumber<T>(ReadOnlySpan<T> source)
    where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
    => Tensor.AggregateNumber<T, SumOperator<T>>(source);
```

This behaves similarly to `Sum()` but without propagating `NaN` values. This principle applies to all methods suffixed with `Number` in this library.

Further variants of the `AggregateNumber()` method exist: `AggregateNumber2D()`, `AggregateNumber3D()`, and `AggregateNumber4D()`.

### First Method

The `First()` method returns the first element in the span that adheres to a specified predicate operator. It yields a nullable type, with `null` indicating that no item satisfying the predicate is found.

The predicate operator must adhere to one of the subsequent interfaces with `TResult` specified as a `bool`:

```csharp
public interface IUnaryToScalarOperator<T, TResult>
    : IOperator
    where T : struct
{
    static abstract TResult Invoke(T x);
    static abstract TResult Invoke(ref readonly Vector<T> x);
}

public interface IBinaryToScalarOperator<T1, T2, TResult>
    : IOperator
    where T1 : struct
    where T2 : struct
{
    static abstract TResult Invoke(T1 x, T2 y);
    static abstract TResult Invoke(ref readonly Vector<T1> x, ref readonly Vector<T2> y);
}

public interface ITernaryToScalarOperator<T1, T2, T3, TResult>
    : IOperator
    where T1 : struct
    where T2 : struct
    where T3 : struct
{
    static abstract TResult Invoke(T1 x, T2 y, T3 z);
    static abstract TResult Invoke(ref readonly Vector<T1> x, ref readonly Vector<T2> y, ref readonly Vector<T3> z);
}
```

For instance, to obtain the first element greater than or equal to a specified scalar value, this library provides the following operation:

```csharp
public static T? FirstGreaterThanOrEqual<T>(ReadOnlySpan<T> source, T value)
    where T : struct, IComparisonOperators<T, T, bool>
    => Tensor.First<T, GreaterThanOrEqualAnyOperator<T>>(source, value);
```

It uses the `GreaterThanOrEqualAnyOperator` operator, which is vectorizable, improving performance when vectorization is available.

### IndexOfFirst Method

The method `IndexOfFirst()`, works similarly to `First()`, but returning the first element in the span that adheres to a specified predicate operator. Returns `-1` if no item satisfying the predicate is found.

For instance, to obtain the index of the first element greater than or equal to a specified scalar value, this library provides the following operation:

```csharp
public static int IndexOfFirstGreaterThanOrEqual<T>(ReadOnlySpan<T> source, T value)
    where T : struct, IComparisonOperators<T, T, bool>
    => Tensor.IndexOfFirst<T, GreaterThanOrEqualAnyOperator<T>>(source, value);
```

It uses exactly the same `GreaterThanOrEqualAnyOperator` operator as in the previous example.

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
