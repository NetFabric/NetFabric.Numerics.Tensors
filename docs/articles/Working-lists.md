# Utilizing Tensors with Lists

While this library primarily supports spans, tensors can still be employed with lists. The `CollectionsMarshal.AsSpan()` method grants access to the internal array of a `List<T>`, returning a `Span<T>`. Consequently, a list can serve as both a source and/or a destination. However, when utilized as a destination, it's imperative to ensure that all items already exist, rendering it suitable for in-place operations.

Below is an example illustrating how to utilize a list as both a source and a destination:

```csharp
var span = CollectionsMarshal.AsSpan(list);
Tensor.Square(span, span);
```

This code squares all items in the list in-place.