# NetFabric.Numerics.Tensors

Working with SIMD in .NET to optimize code can be challenging, but this library offers a straightforward solution. It delivers reusable and highly optimized iterations over `Span<T>`, allowing for the application of both pre-defined and custom operations to each element or aggregation of elements.

Using generics, the library accommodates any type embracing [.NET generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math).

Within the library, you'll find pre-defined operations such as `Sqrt()`, `Sin()`, `Negate()`, `Add()`, `Divide()`, `Multiply()`, `AddMultiply()`, `Sum()`, `Average()`, `Min()`, `Max()`, and many more.

For custom operations, the library allows the definition of operators through interfaces like `IUnaryOperator<T>`, `IBinaryOperator<T>`, `ITernaryOperator<T>`, or `IAggregationOperator<T>`. These operators can be applied seamlessly using the `Apply()`, `Aggregate()`, `IndexOfAggregate()`, `First()`, or `IndexOfFirst()` methods.

## Usage

The library includes methods tailored for operations involving one, two, or three `ReadOnlySpan<T>`. Results are provided in a `Span<T>`, with the condition that the destination `Span<T>` must be of the same size or larger than the sources. Inplace operations are supported when the destination parameter matches any of the sources.

For example, given a variable `data` of type `Span<int>`, the following code snippet replaces each element of the span with its square root:

```csharp
TensorOperations.Sqrt(data, data);
```

Note that since `data` serves as both the source and destination, the operation is performed in-place.

Given variables `x`, `y`, and `result`, all of type `Span<float>` and of the same size, the following example updates each element in `result` with the sum of the corresponding elements in `x` and `y`:

```csharp
TensorOperations.Add(x, y, result);
```

In addition to working with individual elements, this library supports various aggregation operations. For example:

```csharp
var sum = TensorOperations.Sum(data);
var min = TensorOperations.Min(data);
var (min, max) = TensorOperations.MinMax(data);
```

It also helps find the index of specific elements:

```csharp
var index = TensorOperations.IndexOfMin(data);
```

Additionally, it can quickly locate the first element meeting certain criteria:

```csharp
var value = FirstGreaterThan(data, 0);
```

And it simplifies finding the index of such elements:

```csharp
var index = IndexOfFirstGreaterThan(data, 0);
```

## Custom Operations

While `NetFabric.Numerics.Tensors` provides various pre-defined operations, combining them might not be efficient. Custom operators can be implemented, allowing the definition of specific operations for each element of the source, while still benefiting from high-performance reusable iteration code.

The aggregation methods support transformation operators that allow composite operations to be performed in a single data iteration.
