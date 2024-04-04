# NetFabric.Numerics.Tensors

Dealing with SIMD in .NET for optimized code can be complex, but this library offers a practical solution. It provides a reusable and highly-optimized iterations on `Span<T>`, enabling the application of both pre-defined and custom operations to each element.

Using generics, the library accommodates any type embracing [generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math).

Within the library, you'll find pre-defined operations such as `Sqrt()`, `Sin()`, `Negate()`, `Add()`, `Divide()`, `Multiply()`, `AddMultiply()`, `Sum()`, `Average()`, and many more.

For custom operations, the library allows the definition of operators through interfaces like `IUnaryOperator<T>`, `IBinaryOperator<T>`, `ITernaryOperator<T>`, or `IAggregationOperator<T>`. These operators can be applied seamlessly using the `Apply()` or `Aggregate()` methods.

Documentation for this library is available at [NetFabric.Numerics.Tensors Documentation](https://netfabric.github.io/NetFabric.Numerics.Tensors/).

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

### Custom Operations

While `NetFabric.Numerics.Tensors` provides various primitive operations, combining them might not be efficient. Custom operators can be implemented, allowing the definition of specific operations for each element of the source, while still benefiting from high-performance reusable iteration code.

## Credits

This project relies on the following open-source projects:

- [.NET](https://github.com/dotnet)
- [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet)
- [xUnit](https://github.com/xunit/xunit)

## License

This project is licensed under the MIT license. Refer to the [LICENSE](LICENSE) file for details.
