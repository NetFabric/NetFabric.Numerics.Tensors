namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, Span<T> destination)
        where T : struct
        where TOperator : struct, IUnaryOperator<T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");

        Apply<T, T, TOperator>(x, destination);
    }

    public static void Apply<TSource, TResult, TOperator>(ReadOnlySpan<TSource> x, Span<TResult> destination)
        where TSource : struct
        where TResult : struct
        where TOperator : struct, IUnaryOperator<TSource, TResult>
    {
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        // Initialize the index to 0.
        var index = nint.Zero;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (TOperator.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<TSource>.IsSupported &&
            Vector<TResult>.IsSupported &&
            x.Length >= Vector<TSource>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var sourceVectors = MemoryMarshal.Cast<TSource, Vector<TSource>>(x);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            for (var indexVector = nint.Zero; indexVector < sourceVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(ref Unsafe.Add(ref sourceVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            index = x.Length - (x.Length % Vector<TSource>.Count);
        }

        // Iterate through the remaining elements.
        ref var sourceRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; index + 3 < x.Length; index += 4)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref sourceRef, index));
            Unsafe.Add(ref destinationRef, index + 1) = TOperator.Invoke(Unsafe.Add(ref sourceRef, index + 1));
            Unsafe.Add(ref destinationRef, index + 2) = TOperator.Invoke(Unsafe.Add(ref sourceRef, index + 2));
            Unsafe.Add(ref destinationRef, index + 3) = TOperator.Invoke(Unsafe.Add(ref sourceRef, index + 3));
        }

        switch(x.Length - (int)index)
        {
            case 3:
                Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref sourceRef, index));
                Unsafe.Add(ref destinationRef, index + 1) = TOperator.Invoke(Unsafe.Add(ref sourceRef, index + 1));
                Unsafe.Add(ref destinationRef, index + 2) = TOperator.Invoke(Unsafe.Add(ref sourceRef, index + 2));
                break;

            case 2:
                Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref sourceRef, index));
                Unsafe.Add(ref destinationRef, index + 1) = TOperator.Invoke(Unsafe.Add(ref sourceRef, index + 1));
                break;

            case 1:
                Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref sourceRef, index));
                break;
        }
    }

}