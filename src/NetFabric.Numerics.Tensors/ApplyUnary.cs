namespace NetFabric.Numerics.Tensors;

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

    public static void Apply<T, TResult, TOperator>(ReadOnlySpan<T> x, Span<TResult> destination)
        where T : struct
        where TResult : struct
        where TOperator : struct, IUnaryOperator<T, TResult>
    {
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        // Initialize the index to 0.
        var indexSource = nint.Zero;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (TOperator.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported &&
            Vector<TResult>.IsSupported &&
            x.Length >= Vector<T>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(x);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            var indexVector = nint.Zero;
            for (; indexVector < sourceVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(ref Unsafe.Add(ref sourceVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            indexSource = indexVector * Vector<T>.Count;
        }

        // Iterate through the remaining elements.
        ref var sourceRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; indexSource + 3 < x.Length; indexSource += 4)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource));
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1));
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 2));
            Unsafe.Add(ref destinationRef, indexSource + 3) = TOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 3));
        }

        switch(x.Length - (int)indexSource)
        {
            case 3:
                Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 2));
                goto case 2;
            case 2:
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1));
                goto case 1;
            case 1:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource));
                break;
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }
    }

    public static void Apply2<T, TOperator1, TOperator2>(ReadOnlySpan<T> x, Span<T> destination1, Span<T> destination2)
        where T : struct
        where TOperator1 : struct, IUnaryOperator<T, T>
        where TOperator2 : struct, IUnaryOperator<T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination1))
            Throw.ArgumentException(nameof(destination1), "Destination span overlaps with x.");
        if (SpansOverlapAndAreNotSame(x, destination2))
            Throw.ArgumentException(nameof(destination2), "Destination span overlaps with x.");

        Apply2<T, T, T, TOperator1, TOperator2>(x, destination1, destination2);
    }

    public static void Apply2<T, TResult1, TResult2, TOperator1, TOperator2>(ReadOnlySpan<T> x, Span<TResult1> destination1, Span<TResult2> destination2)
        where T : struct
        where TResult1 : struct
        where TResult2 : struct
        where TOperator1 : struct, IUnaryOperator<T, TResult1>
        where TOperator2 : struct, IUnaryOperator<T, TResult2>
    {
        if (x.Length > destination1.Length)
            Throw.ArgumentException(nameof(destination1), "Destination span is too small.");
        if (x.Length > destination2.Length)
            Throw.ArgumentException(nameof(destination2), "Destination span is too small.");

        // Initialize the index to 0.
        var indexSource = nint.Zero;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (TOperator1.IsVectorizable &&
            TOperator2.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported &&
            Vector<TResult1>.IsSupported &&
            Vector<TResult2>.IsSupported &&
            x.Length >= Vector<T>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(x);
            var destination1Vectors = MemoryMarshal.Cast<TResult1, Vector<TResult1>>(destination1);
            var destination2Vectors = MemoryMarshal.Cast<TResult2, Vector<TResult2>>(destination2);

            // Iterate through the vectors.
            ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
            ref var destination1VectorsRef = ref MemoryMarshal.GetReference(destination1Vectors);
            ref var destination2VectorsRef = ref MemoryMarshal.GetReference(destination2Vectors);
            var indexVector = nint.Zero;
            for (; indexVector < sourceVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destination1VectorsRef, indexVector) = TOperator1.Invoke(ref Unsafe.Add(ref sourceVectorsRef, indexVector));
                Unsafe.Add(ref destination2VectorsRef, indexVector) = TOperator2.Invoke(ref Unsafe.Add(ref sourceVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            indexSource = indexVector * Vector<T>.Count;
        }

        // Iterate through the remaining elements.
        ref var sourceRef = ref MemoryMarshal.GetReference(x);
        ref var destination1Ref = ref MemoryMarshal.GetReference(destination1);
        ref var destination2Ref = ref MemoryMarshal.GetReference(destination2);
        for (; indexSource + 1 < x.Length; indexSource += 2)
        {
            Unsafe.Add(ref destination1Ref, indexSource) = TOperator1.Invoke(Unsafe.Add(ref sourceRef, indexSource));
            Unsafe.Add(ref destination1Ref, indexSource + 1) = TOperator1.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1));

            Unsafe.Add(ref destination2Ref, indexSource) = TOperator2.Invoke(Unsafe.Add(ref sourceRef, indexSource));
            Unsafe.Add(ref destination2Ref, indexSource + 1) = TOperator2.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1));
        }

        switch (x.Length - (int)indexSource)
        {
            case 1:
                Unsafe.Add(ref destination1Ref, indexSource) = TOperator1.Invoke(Unsafe.Add(ref sourceRef, indexSource));
                Unsafe.Add(ref destination2Ref, indexSource) = TOperator2.Invoke(Unsafe.Add(ref sourceRef, indexSource));
                break;
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }
    }

}