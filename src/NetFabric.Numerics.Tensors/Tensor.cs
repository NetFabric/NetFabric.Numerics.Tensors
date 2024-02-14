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

    static Vector<T> GetVector<T>(ValueTuple<T, T> tuple)
        where T : struct
    {
        var array = new T[Vector<T>.Count];
        ref var arrayRef = ref MemoryMarshal.GetReference<T>(array);
        for (var indexVector = nint.Zero; indexVector + 1 < array.Length; indexVector += 2)
        {
            Unsafe.Add(ref arrayRef, indexVector) = tuple.Item1;
            Unsafe.Add(ref arrayRef, indexVector + 1) = tuple.Item2;
        }
        return new(array);
    }
}