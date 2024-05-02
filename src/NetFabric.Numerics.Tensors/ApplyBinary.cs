namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    /// <summary>
    /// Applies the specified operator to the elements of two <see cref="ReadOnlySpan{T}"/> and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source and destination spans.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="IBinaryOperator{T, T, T}"/> interface.</typeparam>
    /// <param name="x">The first span of elements to apply the operator.</param>
    /// <param name="y">The second span of elements to apply the operator.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or the destination is too small, or at least one source span overlaps with the destination but they are not identical.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> destination)
        where T : struct
        where TOperator : struct, IBinaryOperator<T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if (SpansOverlapAndAreNotSame(y, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with y.");

        Apply<T, T, T, TOperator>(x, y, destination);
    }

    /// <summary>
    /// Applies the specified operator to the elements of two <see cref="ReadOnlySpan{T}"/> and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the first source span.</typeparam>
    /// <typeparam name="T2">The type of the elements in the second source span.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="IBinaryOperator{T1, T2, TResult}"/> interface.</typeparam>
    /// <param name="x">The first span of elements to apply the operator.</param>
    /// <param name="y">The second span of elements to apply the operator.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or the destination is too small.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
    public static void Apply<T1, T2, TResult, TOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where TResult : struct
        where TOperator : struct, IBinaryOperator<T1, T2, TResult>
    {
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(y), "x and y spans must have the same length.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        // Initialize the index to 0.
        var indexSource = 0;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (TOperator.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<T1>.IsSupported &&
            Vector<T2>.IsSupported &&
            Vector<TResult>.IsSupported &&
            x.Length >= Vector<T1>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var yVectors = MemoryMarshal.Cast<T2, Vector<T2>>(y);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var yVectorsRef = ref MemoryMarshal.GetReference(yVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            for (var indexVector = 0; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    ref Unsafe.Add(ref xVectorsRef, indexVector),
                    ref Unsafe.Add(ref yVectorsRef, indexVector));
            }

            // Update the index to the end of the last complete vector.
            indexSource = xVectors.Length * Vector<T1>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; indexSource < x.Length - 3; indexSource += 4)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource));
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1));
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2));
            Unsafe.Add(ref destinationRef, indexSource + 3) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), Unsafe.Add(ref yRef, indexSource + 3));
        }

        switch(x.Length - indexSource)
        {
            case 3:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource));
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1));
                Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2));
                break;
            case 2:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource));
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1));
                break;
            case 1:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource));
                break;
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }
    }


    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/> and a scalar value, storing the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="IBinaryOperator{T, T, T}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator.</param>
    /// <param name="y">The scalar value to apply the operator.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The destination is too small, or the source span overlaps with the destination but they are not identical.</exception>
    /// <remarks>If the destination is the same as the source span, the operation is performed in-place.</remarks>
    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, T y, Span<T> destination)
        where T : struct
        where TOperator : struct, IBinaryOperator<T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");

        Apply<T, T, T, TOperator>(x, y, destination);
    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/> and a scalar value, storing the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the source span.</typeparam>
    /// <typeparam name="T2">The type of the scalar value to apply the operator.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="IBinaryOperator{T1, T2, TResult}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator.</param>
    /// <param name="y">The scalar value to apply the operator.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The destination is too small.</exception>
    /// <remarks>If the destination is the same as the source span, the operation is performed in-place.</remarks>
    public static void Apply<T1, T2, TResult, TOperator>(ReadOnlySpan<T1> x, T2 y, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where TResult : struct
        where TOperator : struct, IBinaryOperator<T1, T2, TResult>
    {
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        // Initialize the index to 0.
        var indexSource = 0;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (TOperator.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<T1>.IsSupported &&
            Vector<T2>.IsSupported &&
            Vector<TResult>.IsSupported &&
            x.Length >= Vector<T1>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var valueVector = new Vector<T2>(y);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            var indexVector = 0;
            for (; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    ref Unsafe.Add(ref xVectorsRef, indexVector),
                    ref valueVector);
            }

            // Update the index to the end of the last complete vector.
            indexSource = indexVector * Vector<T1>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; indexSource < x.Length - 3; indexSource += 4)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y);
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y);
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), y);
            Unsafe.Add(ref destinationRef, indexSource + 3) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), y);
        }

        switch(x.Length - indexSource)
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
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }
    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/> and a tuple of two scalar values, storing the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="IBinaryOperator{T, T, T}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator.</param>
    /// <param name="y">The tuple of scalar values to apply the operator.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The destination is too small, or the length of the source span is not even, or the source span overlaps with the destination but they are not identical.</exception>
    /// <remarks>If the destination is the same as the source span, the operation is performed in-place.</remarks>
    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, (T, T) y, Span<T> destination)
        where T : struct
        where TOperator : struct, IBinaryOperator<T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");

        Apply<T, T, T, TOperator>(x, y, destination);
    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/> and a tuple of two scalar values, storing the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the source span.</typeparam>
    /// <typeparam name="T2">The type of the scalar values to apply the operator.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="IBinaryOperator{T1, T2, TResult}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator.</param>
    /// <param name="y">The tuple of scalar values to apply the operator.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The destination is too small or the length of the source span is not even.</exception>
    /// <remarks>If the destination is the same as the source span, the operation is performed in-place.</remarks>
    public static void Apply<T1, T2, TResult, TOperator>(ReadOnlySpan<T1> x, (T2, T2) y, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where TResult : struct
        where TOperator : struct, IBinaryOperator<T1, T2, TResult>
    {
        if (x.Length % 2 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have an even length.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        // Initialize the index to 0.
        var indexSource = 0;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (TOperator.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<T1>.IsSupported &&
            Vector<T2>.IsSupported &&
            Vector<TResult>.IsSupported &&
            Vector<T1>.Count > 2 &&
            Vector<T1>.Count % 2 is 0 &&
            x.Length >= Vector<T1>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var valueVector = VectorFactory.Create(y);
            var destinationVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(destination);

            // Iterate through the vectors.
            ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
            ref var destinationVectorsRef = ref MemoryMarshal.GetReference(destinationVectors);
            var indexVector = 0;
            for (; indexVector < xVectors.Length; indexVector++)
            {
                Unsafe.Add(ref destinationVectorsRef, indexVector) = TOperator.Invoke(
                    ref Unsafe.Add(ref xVectorsRef, indexVector),
                    ref valueVector);
            }

            // Update the index to the end of the last complete vector.
            indexSource = indexVector * Vector<T1>.Count;
        }

        // Iterate through the remaining elements.
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        for (; indexSource < x.Length - 3; indexSource += 4)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y.Item1);
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y.Item2);
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), y.Item1);
            Unsafe.Add(ref destinationRef, indexSource + 3) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), y.Item2);
        }

        switch (x.Length - indexSource)
        {
            case 2:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y.Item1);
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y.Item2);
                break;
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }

    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/> and a tuple of three scalar values, storing the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="IBinaryOperator{T, T, T}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator.</param>
    /// <param name="y">The tuple of scalar values to apply the operator.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The destination is too small, or the length of the source span is not a multiple of 3, or the source span overlaps with the destination but they are not identical.</exception>
    /// <remarks>If the destination is the same as the source span, the operation is performed in-place.</remarks>
    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, (T, T, T) y, Span<T> destination)
        where T : struct
        where TOperator : struct, IBinaryOperator<T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");

        Apply<T, T, T, TOperator>(x, y, destination);
    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/> and a tuple of three scalar values, storing the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the source span.</typeparam>
    /// <typeparam name="T2">The type of the scalar values to apply the operator.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="IBinaryOperator{T1, T2, TResult}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator.</param>
    /// <param name="y">The tuple of scalar values to apply the operator.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The destination is too small or the length of the source span is not a multiple of 3.</exception>
    /// <remarks>If the destination is the same as the source span, the operation is performed in-place.</remarks>
    public static void Apply<T1, T2, TResult, TOperator>(ReadOnlySpan<T1> x, (T2, T2, T2) y, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where TResult : struct
        where TOperator : struct, IBinaryOperator<T1, T2, TResult>
    {
        if (x.Length % 3 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have a length multiple of 3.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        var indexSource = 0;
        for (; indexSource < x.Length; indexSource += 3)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y.Item1);
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y.Item2);
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), y.Item3);
        }

        switch (x.Length - indexSource)
        {
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }
    }

}