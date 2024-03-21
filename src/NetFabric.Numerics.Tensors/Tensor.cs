namespace NetFabric.Numerics.Tensors;

/// <summary>
/// Provides methods to perform operations on <see cref="ReadOnlySpan{T}"/>.
/// </summary>
/// <remarks>
/// This static class contains methods for operating on data represented by <see cref="ReadOnlySpan{T}"/>.
/// The methods are categorized into two groups: element-wise operations (Apply) and aggregation operations (Aggregate).
/// Hardware acceleration is utilized where available to optimize performance.
/// </remarks>
public static partial class Tensor
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool SpansOverlapAndAreNotSame<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> other)
        => !Unsafe.AreSame(ref MemoryMarshal.GetReference(span), ref MemoryMarshal.GetReference(other)) && MemoryExtensions.Overlaps(span, other);
}