namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void ApplyScalar<T1, T2, TResult, TOperator>(ReadOnlySpan<T1> x, T2 y, Span<TResult> destination)
        where T1 : struct
        where TResult : struct
        where TOperator : struct, IBinaryScalarOperator<T1, T2, TResult>
    {
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        // Initialize the index to 0.
        var indexSource = nint.Zero;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (TOperator.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<T1>.IsSupported &&
            Vector<TResult>.IsSupported &&
            x.Length >= Vector<T1>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            var indexVector = nint.Zero;
            for (; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    ref Unsafe.Add(ref xVectorsRef, indexVector),
                    y);
            }

            // Update the index to the end of the last complete vector.
            indexSource = indexVector * Vector<T1>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; indexSource + 3 < x.Length; indexSource += 4)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y);
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y);
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), y);
            Unsafe.Add(ref destinationRef, indexSource + 3) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), y);
        }

        switch(x.Length - (int)indexSource)
        {
            case 3:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y);
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y);
                Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), y);
                break;

            case 2:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y);
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y);
                break;

            case 1:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y);
                break;
        }
    }
}