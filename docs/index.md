# NetFabric.Numerics.Tensors

Dealing with SIMD in .NET for optimized code can be complex, but this library offers a practical solution. It provides a reusable and highly-optimized iterations on `Span<T>`, enabling the application of both pre-defined and custom operations to each element.

Using generics, the library accommodates any type embracing [generic math](https://aalmada.github.io/Generic-math-in-dotnet.html).

Within the library, you'll find pre-defined operations such as `Sqrt()`, `Sin()`, `Negate()`, `Add()`, `Divide()`, `Multiply()`, `AddMultiply()`, `Sum()`, `Average()`, and many more.

For custom operations, the library allows the definition of operators through interfaces like `IUnaryOperator<T>`, `IBinaryOperator<T>`, `ITernaryOperator<T>`, or `IAggregationOperator<T>`. These operators can be applied seamlessly using the `Apply()` or `Aggregate()` methods.

## Usage

To use the `NetFabric.Numerics.Tensors` library:

1. Install the library via NuGet: `dotnet add package NetFabric.Numerics.Tensors`
2. Import the library in your code: `using NetFabric.Numerics.Tensors;`
3. Utilize the library's functions for mathematical operations on tensors represented as spans.

> **Note:** Ensure you're on .NET 8 or a later version for compatibility with the `NetFabric.Numerics.Tensors` library.

The library includes methods tailored for operations involving one, two, or three `ReadOnlySpan<T>`. Results are provided in a `Span<T>`, with the condition that the destination `Span<T>` must be of the same size or larger than the sources. Inplace operations are supported when the destination parameter matches any of the sources.

For example, given a variable `data` of type `Span<int>`, the following code snippet replaces each element of the span with its square root:

```csharp
Tensor.Sqrt(data, data);
```

Note that since `data` serves as both the source and destination, the operation is performed in-place.

For variables `x`, `y`, and `result`, all of type `Span<float>` and of the same size, the following example updates each element in `result` with the sum of the corresponding elements in `x` and `y`:

```csharp
Tensor.Add(x, y, result);
```

Beyond per element operations, the library extends support to aggregations. For a `values` variable of type `Span<float>`, the following code snippet calculates the sum, the minimum and the minimum magnitude of all its elements:

```csharp
var sum = Tensor.Sum(values);
var min = Tensor.Min(values);
var minMagnitude = Tensor.MinMagnitude(values);
```

While `NetFabric.Numerics.Tensors` provides various primitive operations, combining them might not be efficient. Custom operators can be implemented, allowing the definition of specific operations for each element of the source, while still benefiting from high-performance reusable iteration code. Check the articles to find how to implement custom operations.