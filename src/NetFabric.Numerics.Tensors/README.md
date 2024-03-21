# NetFabric.Numerics.Tensors

Dealing with SIMD in .NET for optimized code can be complex, but this library offers a practical solution. It provides a reusable and highly-optimized iterations on `Span<T>`, enabling the application of both pre-defined and custom operations to each element.

Using generics, the library accommodates any type embracing [.NET generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math).

Within the library, you'll find pre-defined operations such as `Sqrt()`, `Sin()`, `Negate()`, `Add()`, `Divide()`, `Multiply()`, `AddMultiply()`, `Sum()`, `Average()`, and many more.

For custom operations, the library allows the definition of operators through interfaces like `IUnaryOperator<T>`, `IBinaryOperator<T>`, `ITernaryOperator<T>`, or `IAggregationOperator<T>`. These operators can be applied seamlessly using the `Apply()` or `Aggregate()` methods.

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

In addition to per-element operations, the library also supports various aggregation operations, including:

```csharp
var sum = TensorOperations.Sum(values);
var average = TensorOperations.Average(values);
var product = TensorOperations.Product(values);
var min = TensorOperations.Min(values);
var max = TensorOperations.Max(values);
var (min, max) = TensorOperations.MinMax(values);
```

The library offers functionality to retrieve the index of the first element meeting a specific condition:

```csharp
var indexOfEqual = IndexOfEquals(values, 0);
var indexOfGreaterThan = IndexOfGreaterThan(values, 0);
var indexOfGreaterThanOrEqual = IndexOfGreaterThanOrEqual(values, 0);
var indexOfLessThan = IndexOfLessThan(values, 0);
var indexOfLessThanOrEqual = IndexOfLessThanOrEqual(values, 0);
```

## Custom Operations

While `NetFabric.Numerics.Tensors` provides various pre-defined operations, combining them might not be efficient. Custom operators can be implemented, allowing the definition of specific operations for each element of the source, while still benefiting from high-performance reusable iteration code. 
