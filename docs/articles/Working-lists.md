## Leveraging Tensors with Lists

The `CollectionsMarshal.AsSpan()` method provides access to the internal array of a `List<T>`, returning a `Span<T>` that seamlessly integrates with tensor operations, functioning as both a source and a destination. However, when used as a destination, it's crucial to ensure that all items already exist, making it suitable for inplace operations.

```csharp
var span = CollectionsMarshal.AsSpan(list);
Tensor.Square(span, span);
```

This code squares all items in the list inplace.
