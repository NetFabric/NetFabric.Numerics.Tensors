namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct
        where TOperator : struct, IBinaryOperator<T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if (SpansOverlapAndAreNotSame(y, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with y.");

        Apply<T, T, TOperator>(x, y, destination);
    }

    public static void Apply<TSource, TResult, TOperator>(ReadOnlySpan<TSource> x, ReadOnlySpan<TSource> y, Span<TResult> destination)
        where TSource : struct
        where TResult : struct
        where TOperator : struct, IBinaryOperator<TSource, TResult>
    {
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(y), "x and y spans must have the same length.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        // Initialize the index to 0.
        var index = nint.Zero;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (Vector.IsHardwareAccelerated &&
            Vector<TSource>.IsSupported &&
            Vector<TResult>.IsSupported &&
            x.Length >= Vector<TSource>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<TSource, Vector<TSource>>(x);
            var yVectors = MemoryMarshal.Cast<TSource, Vector<TSource>>(y);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var yVectorsRef = ref MemoryMarshal.GetReference(yVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            for (var indexVector = nint.Zero; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    Unsafe.Add(ref xVectorsRef, indexVector),
                    Unsafe.Add(ref yVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            index = x.Length - (x.Length % Vector<TSource>.Count);
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; index + 3 < x.Length; index += 4)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref xRef, index), Unsafe.Add(ref yRef, index));
            Unsafe.Add(ref destinationRef, index + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 1), Unsafe.Add(ref yRef, index + 1));
            Unsafe.Add(ref destinationRef, index + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 2), Unsafe.Add(ref yRef, index + 2));
            Unsafe.Add(ref destinationRef, index + 3) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 3), Unsafe.Add(ref yRef, index + 3));
        }

        switch(x.Length - (int)index)
        {
            case 3:
                Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref xRef, index), Unsafe.Add(ref yRef, index));
                Unsafe.Add(ref destinationRef, index + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 1), Unsafe.Add(ref yRef, index + 1));
                Unsafe.Add(ref destinationRef, index + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 2), Unsafe.Add(ref yRef, index + 2));
                break;

            case 2:
                Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref xRef, index), Unsafe.Add(ref yRef, index));
                Unsafe.Add(ref destinationRef, index + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 1), Unsafe.Add(ref yRef, index + 1));
                break;

            case 1:
                Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref xRef, index), Unsafe.Add(ref yRef, index));
                break;
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct
        where TOperator : struct, IBinaryOperator<T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");

        Apply<T, T, TOperator>(x, y, destination);
    }

    public static void Apply<TSource, TResult, TOperator>(ReadOnlySpan<TSource> x, TSource y, Span<TResult> destination)
        where TSource : struct
        where TResult : struct
        where TOperator : struct, IBinaryOperator<TSource, TResult>
    {
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        // Initialize the index to 0.
        var index = nint.Zero;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (Vector.IsHardwareAccelerated &&
            Vector<TSource>.IsSupported &&
            Vector<TResult>.IsSupported &&
            x.Length >= Vector<TSource>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<TSource, Vector<TSource>>(x);
            var valueVector = new Vector<TSource>(y);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            for (var indexVector = nint.Zero; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    Unsafe.Add(ref xVectorsRef, indexVector),
                    valueVector);
            }

            // Update the index to the end of the last complete vector.
            index = x.Length - (x.Length % Vector<TSource>.Count);
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

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ValueTuple<T, T> y, Span<T> destination)
        where T : struct
        where TOperator : struct, IBinaryOperator<T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");

        Apply<T, T, TOperator>(x, y, destination);
    }

    public static void Apply<TSource, TResult, TOperator>(ReadOnlySpan<TSource> x, ValueTuple<TSource, TSource> y, Span<TResult> destination)
        where TSource : struct
        where TResult : struct
        where TOperator : struct, IBinaryOperator<TSource, TResult>
    {
        if (x.Length % 2 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have an even size.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        // Initialize the index to 0.
        var index = nint.Zero;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (Vector.IsHardwareAccelerated &&
            Vector<TSource>.IsSupported &&
            Vector<TResult>.IsSupported &&
            Vector<TSource>.Count > 2 &&
            Vector<TSource>.Count % 2 is 0 &&
            x.Length >= Vector<TSource>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<TSource, Vector<TSource>>(x);
            var valueVector = GetVector(y);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            for (var indexVector = nint.Zero; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    Unsafe.Add(ref xVectorsRef, indexVector),
                    valueVector);
            }

            // Update the index to the end of the last complete vector.
            index = x.Length - (x.Length % Vector<TSource>.Count);
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; index + 3 < x.Length; index += 4)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref xRef, index), y.Item1);
            Unsafe.Add(ref destinationRef, index + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 1), y.Item2);
            Unsafe.Add(ref destinationRef, index + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 2), y.Item1);
            Unsafe.Add(ref destinationRef, index + 3) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 3), y.Item2);
        }

        if(x.Length > (int)index)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref xRef, index), y.Item1);
            Unsafe.Add(ref destinationRef, index + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 1), y.Item2);
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ValueTuple<T, T, T> y, Span<T> destination)
        where T : struct
        where TOperator : struct, IBinaryOperator<T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");

        Apply<T, T, TOperator>(x, y, destination);
    }

    public static void Apply<TSource, TResult, TOperator>(ReadOnlySpan<TSource> x, ValueTuple<TSource, TSource, TSource> y, Span<TResult> destination)
        where TSource : struct
        where TResult : struct
        where TOperator : struct, IBinaryOperator<TSource, TResult>
    {
        if (x.Length % 3 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have a size multiple of 3.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (var index = nint.Zero; index < x.Length; index += 3)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref xRef, index), y.Item1);
            Unsafe.Add(ref destinationRef, index + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 1), y.Item2);
            Unsafe.Add(ref destinationRef, index + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 2), y.Item3);
        }
    }

}