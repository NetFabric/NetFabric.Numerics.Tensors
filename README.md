# NetFabric.Numerics.Tensors

This .NET tensors implementation maximizes the utilization of CPU SIMD features, providing an efficient solution for harnessing advanced CPU capabilities while avoiding the complexities of intricate and hard-to-maintain code.

Tensors are characterized as data stored in memory using a `ReadOnlySpan<T>`. This library supports the conventional approach of having each data element in a separate span, but it also accommodates storing them in the same array. This flexibility enables the use of tensors in object-oriented structures with multiple fields of the same type.

This library utilizes generics to broaden its functionality for any type employing [generic math](https://aalmada.github.io/Generic-math-in-dotnet.html). For optimal performance, it is recommended to convert these types to native numeric types supported by `System.Numerics.Vector<T>`. This conversion is typically facilitated by using `MemoryMarshal.Cast<TFrom, TTo>()`.

The library offers a set of pre-defined operations, including `Square()`, `Negate()`, `Add()`, `Divide()`, `Multiply()`, `AddMultiply()`, `Sum()`, and `Average()`.

For custom operations, developers can define them by implementing operators based on interfaces such as `IUnaryOperator<T>`, `IBinaryOperator<T>`, `ITernaryOperator<T>`, or `IAggregationOperator<T>`. These operators can then be applied using the `Apply()` or `Aggregate()` methods.

Documentation for this library can be found at https://netfabric.github.io/NetFabric.Numerics.Tensors/

## Usage

To use the `NetFabric.Numerics.Tensors` library, follow these steps:

1. Install the library via NuGet: `dotnet add package NetFabric.Numerics.Tensors`
2. Import the library in your code: `using NetFabric.Numerics.Tensors;`
3. Start using the library's functions to execute mathematical operations on tensors represented as spans.

> **Note:** The `NetFabric.Numerics.Tensors` library requires .NET 8 or later.

## Credits

The following open-source projects are used to build and test this project:

- [.NET](https://github.com/dotnet)
- [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet)
- [coverlet](https://github.com/coverlet-coverage/coverlet)
- [xUnit](https://github.com/xunit/xunit)

## License

This project is licensed under the MIT license. See the [LICENSE](LICENSE) file for more info.
