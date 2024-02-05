# Getting Started with NetFabric.Numerics.Tensors

To kick off your exploration of NetFabric.Numerics.Tensors, grab it conveniently from NuGet. Embed it as a dependency in your project, either by executing the command line steps (outlined on the NuGet page) or through your preferred dependency manager.

Import the library into your source code files as needed – include `using NetFabric.Numerics.Tensors;` in your C# files or `open NetFabric.Numerics.Tensors` if you're working in F#.

This library simplifies operations with one, two, or three `ReadOnlySpan<T>`, designed for specific use cases. The outcomes are presented in a `Span<T>`, specified as the last parameter. Keep in mind that the destination `Span<T>` must match or exceed the size of the sources. Inplace operations are supported when the destination aligns with any of the sources.

> NOTE: While the examples showcase C#, the underlying concepts should remain accessible to developers using other languages.

For instance, if you have a `data` variable of type `Span<int>`, the following snippet efficiently replaces each element with its square root:

```csharp
Tensor.Sqrt(data, data);
```

Given that `data` serves as both the source and destination, the operation unfolds inplace.

Consider variables `x`, `y`, and `result`, all of type `Span<float>` and equal size. This example populates each element in `result` with the sum of corresponding elements from `x` and `y`:

```csharp
Tensor.Add(x, y, result);
```

Beyond per element operations, the library extends support to aggregations. For a `values` variable of type `Span<float>`, the following code snippet calculates the sum of all its elements:

```csharp
var sum = Tensor.Sum(values);
```

Dive deeper into the library's capabilities by exploring the API documentation, especially the section under the `Tensor` type, for an exhaustive list of available functionalities.

Moreover, this library caters to customization and extensibility, ensuring it aligns seamlessly with your unique scenarios. Uncover the nuances in the following sections to tailor the library to your needs.