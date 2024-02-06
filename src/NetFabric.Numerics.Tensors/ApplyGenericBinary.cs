namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void ApplyGeneric<T1, T2, TResult, TOperator>(ReadOnlySpan<T1> x, T2 y, Span<TResult> destination)
        where T1 : struct
        where TResult : struct
        where TOperator : struct, IGenericBinaryOperator<T1, T2, TResult>
    {
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        // Initialize the index to 0.
        var index = nint.Zero;

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
            for (var indexVector = nint.Zero; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    ref Unsafe.Add(ref xVectorsRef, indexVector),
                    y);
            }

            // Update the index to the end of the last complete vector.
            index = x.Length - (x.Length % Vector<T1>.Count);
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; index + 3 < x.Length; index += 4)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref xRef, index), y);
            Unsafe.Add(ref destinationRef, index + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 1), y);
            Unsafe.Add(ref destinationRef, index + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 2), y);
            Unsafe.Add(ref destinationRef, index + 3) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 3), y);
        }

        switch(x.Length - (int)index)
        {
            case 3:
                Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref xRef, index), y);
                Unsafe.Add(ref destinationRef, index + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 1), y);
                Unsafe.Add(ref destinationRef, index + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 2), y);
                break;

            case 2:
                Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref xRef, index), y);
                Unsafe.Add(ref destinationRef, index + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 1), y);
                break;

            case 1:
                Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref xRef, index), y);
                break;
        }
    }
}