namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if (SpansOverlapAndAreNotSame(y, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with y.");
        if (SpansOverlapAndAreNotSame(z, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with z.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y, ReadOnlySpan<T3> z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
    {
        if (x.Length != y.Length || x.Length != z.Length)
            Throw.ArgumentException(nameof(x), "x, y and z spans must have the same length.");
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
            var yVectors = MemoryMarshal.Cast<T2, Vector<T2>>(y);
            var zVectors = MemoryMarshal.Cast<T3, Vector<T3>>(z);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var yVectorsRef = ref MemoryMarshal.GetReference(yVectors);
            ref var zVectorsRef = ref MemoryMarshal.GetReference(zVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            var indexVector = nint.Zero;
            for (; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    ref Unsafe.Add(ref xVectorsRef, indexVector),
                    ref Unsafe.Add(ref yVectorsRef, indexVector),
                    ref Unsafe.Add(ref zVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            indexSource = indexVector * Vector<T1>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        ref var zRef = ref MemoryMarshal.GetReference(z);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; indexSource + 3 < x.Length; indexSource += 4)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource), Unsafe.Add(ref zRef, indexSource));
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1), Unsafe.Add(ref zRef, indexSource + 1));
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2), Unsafe.Add(ref zRef, indexSource + 2));
            Unsafe.Add(ref destinationRef, indexSource + 3) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), Unsafe.Add(ref yRef, indexSource + 3), Unsafe.Add(ref zRef, indexSource + 3));
        }

        switch(x.Length - (int)indexSource)
        {
            case 3:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource), Unsafe.Add(ref zRef, indexSource));
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1), Unsafe.Add(ref zRef, indexSource + 1));
                Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2), Unsafe.Add(ref zRef, indexSource + 2));
                break;

            case 2:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource), Unsafe.Add(ref zRef, indexSource));
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1), Unsafe.Add(ref zRef, indexSource + 1));
                break;

            case 1:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource), Unsafe.Add(ref zRef, indexSource));
                break;
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, T y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if (SpansOverlapAndAreNotSame(z, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with z.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, T2 y, ReadOnlySpan<T3> z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
    {
        if (x.Length != z.Length)
            Throw.ArgumentException(nameof(x), "x and z spans must have the same length.");
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
            var yVector = new Vector<T2>(y);
            var zVectors = MemoryMarshal.Cast<T3, Vector<T3>>(z);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var zVectorsRef = ref MemoryMarshal.GetReference(zVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            var indexVector = nint.Zero;
            for (; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    ref Unsafe.Add(ref xVectorsRef, indexVector),
                    ref yVector,
                    ref Unsafe.Add(ref zVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            indexSource = indexVector * Vector<T1>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var zRef = ref MemoryMarshal.GetReference(z);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; indexSource + 3 < x.Length; indexSource += 4)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y, Unsafe.Add(ref zRef, indexSource));
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y, Unsafe.Add(ref zRef, indexSource + 1));
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), y, Unsafe.Add(ref zRef, indexSource + 2));
            Unsafe.Add(ref destinationRef, indexSource + 3) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), y, Unsafe.Add(ref zRef, indexSource + 3));
        }

        switch(x.Length - (int)indexSource)
        {
            case 3:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y, Unsafe.Add(ref zRef, indexSource));
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y, Unsafe.Add(ref zRef, indexSource + 1));
                Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), y, Unsafe.Add(ref zRef, indexSource + 2));
                break;

            case 2:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y, Unsafe.Add(ref zRef, indexSource));
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y, Unsafe.Add(ref zRef, indexSource + 1));
                break;

            case 1:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y, Unsafe.Add(ref zRef, indexSource));
                break;
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ValueTuple<T, T> y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if (SpansOverlapAndAreNotSame(z, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with z.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, ValueTuple<T2, T2> y, ReadOnlySpan<T3> z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
    {
        if (x.Length % 2 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have an even size.");
        if (x.Length != z.Length)
            Throw.ArgumentException(nameof(x), "x and z spans must have the same length.");
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
            Vector<T1>.Count > 2 &&
            Vector<T1>.Count % 2 is 0 &&
            x.Length >= Vector<T1>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var yVector = GetVector(y);
            var zVectors = MemoryMarshal.Cast<T3, Vector<T3>>(z);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var zVectorsRef = ref MemoryMarshal.GetReference(zVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            var indexVector = nint.Zero;
            for (; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    ref Unsafe.Add(ref xVectorsRef, indexVector),
                    ref yVector,
                    ref Unsafe.Add(ref zVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            indexSource = indexVector * Vector<T1>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var zRef = ref MemoryMarshal.GetReference(z);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; indexSource + 3 < x.Length; indexSource += 4)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y.Item1, Unsafe.Add(ref zRef, indexSource));
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y.Item2, Unsafe.Add(ref zRef, indexSource + 1));
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), y.Item1, Unsafe.Add(ref zRef, indexSource + 2));
            Unsafe.Add(ref destinationRef, indexSource + 3) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), y.Item2, Unsafe.Add(ref zRef, indexSource + 3));
        }

        if(x.Length > (int)indexSource)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y.Item1, Unsafe.Add(ref zRef, indexSource));
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y.Item2, Unsafe.Add(ref zRef, indexSource + 1));
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ValueTuple<T, T, T> y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if (SpansOverlapAndAreNotSame(z, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with z.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, ValueTuple<T2, T2, T2> y, ReadOnlySpan<T3> z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
    {
        if (x.Length % 3 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have a size multiple of 3.");
        if (x.Length != z.Length)
            Throw.ArgumentException(nameof(x), "x and z spans must have the same length.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var zRef = ref MemoryMarshal.GetReference(z);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (var index = nint.Zero; index < x.Length; index += 3)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref xRef, index), y.Item1, Unsafe.Add(ref zRef, index));
            Unsafe.Add(ref destinationRef, index + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 1), y.Item2, Unsafe.Add(ref zRef, index + 1));
            Unsafe.Add(ref destinationRef, index + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 2), y.Item3, Unsafe.Add(ref zRef, index + 2));
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, T z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if (SpansOverlapAndAreNotSame(y, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with y.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y, T3 z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
    {
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(x), "x and y spans must have the same length.");
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
            var yVectors = MemoryMarshal.Cast<T2, Vector<T2>>(y);
            var zVector = new Vector<T3>(z);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var yVectorsRef = ref MemoryMarshal.GetReference(yVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            var indexVector = nint.Zero;
            for (; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    ref Unsafe.Add(ref xVectorsRef, indexVector),
                    ref Unsafe.Add(ref yVectorsRef, indexVector),
                    ref zVector);
            }

            // Update the index to the end of the last complete vector.
            indexSource = indexVector * Vector<T1>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; indexSource + 3 < x.Length; indexSource += 4)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource), z);
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1), z);
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2), z);
            Unsafe.Add(ref destinationRef, indexSource + 3) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), Unsafe.Add(ref yRef, indexSource + 3), z);
        }

        switch(x.Length - (int)indexSource)
        {
            case 3:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource), z);
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1), z);
                Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2), z);
                break;

            case 2:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource), z);
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1), z);
                break;

            case 1:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource), z);
                break;
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, ValueTuple<T, T> z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if (SpansOverlapAndAreNotSame(y, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with y.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y, ValueTuple<T3, T3> z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
    {
        if (x.Length % 2 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have an even size.");
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(x), "x and y spans must have the same length.");
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
            Vector<T1>.Count > 2 &&
            Vector<T1>.Count % 2 is 0 &&
            x.Length >= Vector<T1>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var yVectors = MemoryMarshal.Cast<T2, Vector<T2>>(y);
            var zVector = GetVector(z);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var yVectorsRef = ref MemoryMarshal.GetReference(yVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            var indexVector = nint.Zero;
            for (; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    ref Unsafe.Add(ref xVectorsRef, indexVector),
                    ref Unsafe.Add(ref yVectorsRef, indexVector),
                    ref zVector);
            }

            // Update the index to the end of the last complete vector.
            indexSource = indexVector * Vector<T1>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; indexSource + 3 < x.Length; indexSource += 4)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource), z.Item1);
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1), z.Item2);
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2), z.Item1);
            Unsafe.Add(ref destinationRef, indexSource + 3) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), Unsafe.Add(ref yRef, indexSource + 3), z.Item2);
        }

        if(x.Length > (int)indexSource)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource), z.Item1);
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1), z.Item2);
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, ValueTuple<T, T, T> z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if (SpansOverlapAndAreNotSame(y, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with y.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y, ValueTuple<T3, T3, T3> z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
    {
        if (x.Length % 3 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have a size multiple of 3.");
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(x), "x and y spans must have the same length.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (var index = nint.Zero; index < x.Length; index += 3)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref xRef, index), Unsafe.Add(ref yRef, index), z.Item1);
            Unsafe.Add(ref destinationRef, index + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 1), Unsafe.Add(ref yRef, index + 1), z.Item2);
            Unsafe.Add(ref destinationRef, index + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 2), Unsafe.Add(ref yRef, index + 2), z.Item3);
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, T y, T z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, T2 y, T3 z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
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
            var yVector = new Vector<T2>(y);
            var zVector = new Vector<T3>(z);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            var indexVector = nint.Zero;
            for (; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    ref Unsafe.Add(ref xVectorsRef, indexVector),
                    ref yVector,
                    ref zVector);
            }

            // Update the index to the end of the last complete vector.
            indexSource = indexVector * Vector<T1>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; indexSource + 3 < x.Length; indexSource += 4)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y, z);
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y, z);
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), y, z);
            Unsafe.Add(ref destinationRef, indexSource + 3) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), y, z);
        }

        switch(x.Length - (int)indexSource)
        {
            case 3:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y, z);
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y, z);
                Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), y, z);
                break;

            case 2:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y, z);
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y, z);
                break;

            case 1:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y, z);
                break;
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ValueTuple<T, T> y, ValueTuple<T, T> z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, ValueTuple<T2, T2> y, ValueTuple<T3, T3> z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
    {
        if (x.Length % 2 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have an even size.");
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
            Vector<T1>.Count > 2 &&
            Vector<T1>.Count % 2 is 0 &&
            x.Length >= Vector<T1>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var yVector = GetVector(y);
            var zVector = GetVector(z);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            var indexVector = nint.Zero;
            for (; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    ref Unsafe.Add(ref xVectorsRef, indexVector),
                    ref yVector,
                    ref zVector);
            }

            // Update the index to the end of the last complete vector.
            indexSource = indexVector * Vector<T1>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; indexSource + 3 < x.Length; indexSource += 4)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y.Item1, z.Item1);
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y.Item2, z.Item2);
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), y.Item1, z.Item1);
            Unsafe.Add(ref destinationRef, indexSource + 3) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), y.Item2, z.Item2);
        }

        if(x.Length > (int)indexSource)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y.Item1, z.Item1);
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y.Item2, z.Item2);
        }
    }

    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ValueTuple<T, T, T> y, ValueTuple<T, T, T> z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, ValueTuple<T2, T2, T2> y, ValueTuple<T3, T3, T3> z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
    {
        if (x.Length % 3 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have a size multiple of 3.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (var index = nint.Zero; index < x.Length; index += 3)
        {
            Unsafe.Add(ref destinationRef, index) = TOperator.Invoke(Unsafe.Add(ref xRef, index), y.Item1, z.Item1);
            Unsafe.Add(ref destinationRef, index + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 1), y.Item2, z.Item2);
            Unsafe.Add(ref destinationRef, index + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, index + 2), y.Item3, z.Item3);
        }
    }
}