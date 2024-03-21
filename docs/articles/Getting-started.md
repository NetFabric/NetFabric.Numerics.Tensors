# Getting Started with NetFabric.Numerics.Tensors

To kick off your exploration of NetFabric.Numerics.Tensors, grab it from [NuGet](https://www.nuget.org/packages/NetFabric.Numerics.Tensors). Embed it as a dependency in your project, either by executing the command line steps ([outlined on the NuGet page](https://www.nuget.org/packages/NetFabric.Numerics.Tensors)) or through your preferred dependency manager.

Import the library into your source code files as needed – include `using NetFabric.Numerics.Tensors;` in your C# files or `open NetFabric.Numerics.Tensors` if you're working in F#.

This library simplifies operations with one, two, or three `ReadOnlySpan<T>`, designed for specific use cases. The outcomes are presented in a `Span<T>`, specified as the last parameter. Keep in mind that the destination `Span<T>` must match or exceed the size of the sources. In-place operations are supported when the destination aligns with any of the sources.

All predefined operations can be found in the static class `TensorOperations`. The following examples showcase the library's capabilities`.
`

> NOTE: While the examples showcase C#, the underlying concepts should remain accessible to developers using other languages.

For instance, if you have a `data` variable of type `Span<int>`, the following snippet efficiently replaces each element with its square root:

```csharp
TensorOperations.Sqrt(data, data);
```

Given that `data` serves as both the source and destination, the operation unfolds in-place.

Consider variables `x`, `y`, and `result`, all of type `Span<float>` and equal size. This example populates each element in `result` with the sum of corresponding elements from `x` and `y`:

```csharp
TensorOperations.Add(x, y, result);
```

> **NOTE**: The operations do not allocate any memory, ensuring optimal performance. The library is designed to be memory-efficient and performant. All the spans have to be allocated and managed by the user.

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

Dive deeper into the library's capabilities by exploring the API documentation, especially the section under the `TensorOperations` type, for an exhaustive list of available functionalities.

Moreover, this library caters to customization and extensibility, ensuring it aligns seamlessly with your unique scenarios. Uncover the nuances in the following sections to tailor the library to your needs.
