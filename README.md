# NetFabric.Numerics.Tensors

This library simplifies the optimization of processing large datasets stored in memory as `Span<T>`. Utilizing generics, it extends this functionality to all value-type numerics and harnesses intrinsics (SIMD) acceleration when available.

The library comes with pre-defined operations such as `Square()`, `Negate()`, `Add()`, `Divide()`, `Multiply()`, `AddMultiply()`, `Sum()`, and `Average()`.

Custom operations can be defined by implementing operators based on interfaces like `IUnaryOperator<T>`, `IBinaryOperator<T>`, `ITernaryOperator<T>`, or `IAggregationOperator<T>`.

It supports the conventional approach of having each data element in a different span, but also accommodates having them in the same array. This flexibility allows the use of tensors in object-oriented structures with fields of the same type.

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
