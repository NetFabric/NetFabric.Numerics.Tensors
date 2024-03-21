namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    /// <summary>
    /// Applies the specified operator to the elements of three <see cref="ReadOnlySpan{T}"/> and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source spans and destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The first span of elements to apply the operator to.</param>
    /// <param name="y">The second span of elements to apply the operator to.</param>
    /// <param name="z">The third span of elements to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or the destination is too small, or at least one source span overlaps with the destination but they are not identical.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
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

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/> and two scalar values, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the first source span.</typeparam>
    /// <typeparam name="T2">The type of the elements in the second source span.</typeparam>
    /// <typeparam name="T3">The type of the elements in the third source span.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The first span of elements to apply the operator to.</param>
    /// <param name="y">The second span of elements to apply the operator to.</param>
    /// <param name="z">The third span of elements to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or the destination is too small.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
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
            Vector<T2>.IsSupported &&
            Vector<T3>.IsSupported &&
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
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }
    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/> a scalar value and another <see cref="ReadOnlySpan{T}"/>, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source spans, scalar value and destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator to.</param>
    /// <param name="y">The scalar value to apply the operator to.</param>
    /// <param name="z">The span of elements to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or the destination is too small, or at least one source span overlaps with the destination but they are not identical.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
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

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/> a scalar value and another <see cref="ReadOnlySpan{T}"/>, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the source span in the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the scalar value in the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the elements in the source span in the third parameter.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator to.</param>
    /// <param name="y">The scalar value to apply the operator to.</param>
    /// <param name="z">The span of elements to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or the destination is too small.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
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
            Vector<T2>.IsSupported &&
            Vector<T3>.IsSupported &&
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
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }
    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/>, a tuple of two scalar values and another <see cref="ReadOnlySpan{T}"/>, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source spans, the tuple and destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator to.</param>
    /// <param name="y">The tuple of scalar values to apply the operator to.</param>
    /// <param name="z">The span of elements to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or don't have an even length, or the destination is too small, or at least one source span overlaps with the destination but they are not identical.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, (T, T) y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if (SpansOverlapAndAreNotSame(z, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with z.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/>, a tuple of two scalar values and another <see cref="ReadOnlySpan{T}"/>, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the source span in the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the elements in the tuple in the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the elements in the source span in the third parameter.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator to.</param>
    /// <param name="y">The tuple of scalar values to apply the operator to.</param>
    /// <param name="z">The span of elements to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or don't have an even length, or the destination is too small.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, (T2, T2) y, ReadOnlySpan<T3> z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
    {
        if (x.Length % 2 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have an even length.");
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
            Vector<T2>.IsSupported &&
            Vector<T3>.IsSupported &&
            Vector<TResult>.IsSupported &&
            Vector<T1>.Count > 2 &&
            Vector<T1>.Count % 2 is 0 &&
            x.Length >= Vector<T1>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var yVector = VectorFactory.Create(y);
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

        switch (x.Length - (int)indexSource)
        {
            case 2:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y.Item1, Unsafe.Add(ref zRef, indexSource));
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y.Item2, Unsafe.Add(ref zRef, indexSource + 1));
                break;
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }
    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/>, a tuple of three scalar values and another <see cref="ReadOnlySpan{T}"/>, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source spans, the tuple and destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator to.</param>
    /// <param name="y">The tuple of scalar values to apply the operator to.</param>
    /// <param name="z">The span of elements to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or don't have a length multiple of 3, or the destination is too small, or at least one source span overlaps with the destination but they are not identical.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, (T, T, T) y, ReadOnlySpan<T> z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if (SpansOverlapAndAreNotSame(z, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with z.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/>, a tuple of three scalar values and another <see cref="ReadOnlySpan{T}"/>, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the source span in the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the elements in the tuple in the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the elements in the source span in the third parameter.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator to.</param>
    /// <param name="y">The tuple of scalar values to apply the operator to.</param>
    /// <param name="z">The span of elements to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or don't have a length multiple of 3, or the destination is too small.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, (T2, T2, T2) y, ReadOnlySpan<T3> z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
    {
        if (x.Length % 3 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have a length multiple of 3.");
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

    /// <summary>
    /// Applies the specified operator to the elements of two <see cref="ReadOnlySpan{T}"/> and a scalar values, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source spans, scalar values and destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The first span of elements to apply the operator to.</param>
    /// <param name="y">The second span of elements to apply the operator to.</param>
    /// <param name="z">The scalar value to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or the destination is too small, or at least one source span overlaps with the destination but they are not identical.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
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

    /// <summary>
    /// Applies the specified operator to the elements of two <see cref="ReadOnlySpan{T}"/> and a scalar values, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the first source span.</typeparam>
    /// <typeparam name="T2">The type of the elements in the second source span.</typeparam>
    /// <typeparam name="T3">The type of the scalar value.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The first span of elements to apply the operator to.</param>
    /// <param name="y">The second span of elements to apply the operator to.</param>
    /// <param name="z">The scalar value to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or the destination is too small.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
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
            Vector<T2>.IsSupported &&
            Vector<T3>.IsSupported &&
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
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }
    }

    /// <summary>
    /// Applies the specified operator to the elements of two <see cref="ReadOnlySpan{T}"/> and a tuple of two scalar values, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source spans, the tuple and destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The first span of elements to apply the operator to.</param>
    /// <param name="y">The second span of elements to apply the operator to.</param>
    /// <param name="z">The tuple of scalar values to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or its length is not even, or the destination is too small, or at least one source span overlaps with the destination but they are not identical.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, (T, T) z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if (SpansOverlapAndAreNotSame(y, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with y.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    /// <summary>
    /// Applies the specified operator to the elements of two <see cref="ReadOnlySpan{T}"/> and a tuple of two scalar values, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the first source span.</typeparam>
    /// <typeparam name="T2">The type of the elements in the second source span.</typeparam>
    /// <typeparam name="T3">The type of the elements in the tuple.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The first span of elements to apply the operator to.</param>
    /// <param name="y">The second span of elements to apply the operator to.</param>
    /// <param name="z">The tuple of scalar values to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or its length is not even, or the destination is too small.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y, (T3, T3) z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
    {
        if (x.Length % 2 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have an even length.");
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
            Vector<T2>.IsSupported &&
            Vector<T3>.IsSupported &&
            Vector<TResult>.IsSupported &&
            Vector<T1>.Count > 2 &&
            Vector<T1>.Count % 2 is 0 &&
            x.Length >= Vector<T1>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var yVectors = MemoryMarshal.Cast<T2, Vector<T2>>(y);
            var zVector = VectorFactory.Create(z);
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

        switch (x.Length - (int)indexSource)
        {
            case 2:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource), z.Item1);
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1), z.Item2);
                break;
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }
    }

    /// <summary>
    /// Applies the specified operator to the elements of two <see cref="ReadOnlySpan{T}"/> and a tuple of three scalar values, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source spans, the tuple and destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The first span of elements to apply the operator to.</param>
    /// <param name="y">The second span of elements to apply the operator to.</param>
    /// <param name="z">The tuple of scalar values to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or its length is not a multiple of 3, or the destination is too small, or at least one source span overlaps with the destination but they are not identical.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, (T, T, T) z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");
        if (SpansOverlapAndAreNotSame(y, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with y.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    /// <summary>
    /// Applies the specified operator to the elements of two <see cref="ReadOnlySpan{T}"/> and a tuple of three scalar values, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the first source span.</typeparam>
    /// <typeparam name="T2">The type of the elements in the second source span.</typeparam>
    /// <typeparam name="T3">The type of the elements in the tuple.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The first span of elements to apply the operator to.</param>
    /// <param name="y">The second span of elements to apply the operator to.</param>
    /// <param name="z">The tuple of scalar values to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source spans do not have the same length, or its length is not a multiple of 3, or the destination is too small.</exception>
    /// <remarks>If the destination is the same as one of the source spans, the operation is performed in-place.</remarks>
    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y, (T3, T3, T3) z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
    {
        if (x.Length % 3 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have a length multiple of 3.");
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(x), "x and y spans must have the same length.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        var indexSource = nint.Zero;
        for (; indexSource < x.Length; indexSource += 3)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource), z.Item1);
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1), z.Item2);
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2), z.Item3);
        }

        switch (x.Length - (int)indexSource)
        {
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }
    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/> and two scalar values, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span, scalar values and destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator to.</param>
    /// <param name="y">The first scalar value to apply the operator to.</param>
    /// <param name="z">The second scalar value to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The destination is too small, or at least one source span overlaps with the destination but they are not identical.</exception>
    /// <remarks>If the destination is the same as the source span, the operation is performed in-place.</remarks>
    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, T y, T z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/> and two scalar values, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the source span.</typeparam>
    /// <typeparam name="T2">The type of the first scalar value.</typeparam>
    /// <typeparam name="T3">The type of the second scalar value.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator to.</param>
    /// <param name="y">The first scalar value to apply the operator to.</param>
    /// <param name="z">The second scalar value to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The destination is too small.</exception>
    /// <remarks>If the destination is the same as the source span, the operation is performed in-place.</remarks>
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
            Vector<T2>.IsSupported &&
            Vector<T3>.IsSupported &&
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
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }
    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/> and two tuples of two scalar values, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span, the tuples and destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator to.</param>
    /// <param name="y">The first tuple of scalar values to apply the operator to.</param>
    /// <param name="z">The second tuple of scalar values to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source span doesn't have an even length, the destination is too small, or at least one source span overlaps with the destination but they are not identical.</exception>
    /// <remarks>If the destination is the same as the source span, the operation is performed in-place.</remarks>
    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, (T, T) y, (T, T) z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/> and two tuples of two scalar values, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the source span.</typeparam>
    /// <typeparam name="T2">The type of the elements in the tuples.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator to.</param>
    /// <param name="y">The first tuple of scalar values to apply the operator to.</param>
    /// <param name="z">The second tuple of scalar values to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source span doesn't have an even length, the destination is too small.</exception>
    /// <remarks>If the destination is the same as the source span, the operation is performed in-place.</remarks>
    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, (T2, T2) y, (T3, T3) z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
    {
        if (x.Length % 2 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have an even length.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        // Initialize the index to 0.
        var indexSource = nint.Zero;

        // Check if hardware acceleration and Vector<T> support are available,
        // and if the length of the x is greater than the Vector<T>.Count.
        if (TOperator.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<T1>.IsSupported &&
            Vector<T2>.IsSupported &&
            Vector<T3>.IsSupported &&
            Vector<TResult>.IsSupported &&
            Vector<T1>.Count > 2 &&
            Vector<T1>.Count % 2 is 0 &&
            x.Length >= Vector<T1>.Count)
        {
            // Cast the spans to vectors for hardware acceleration.
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var yVector = VectorFactory.Create(y);
            var zVector = VectorFactory.Create(z);
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

        switch (x.Length - (int)indexSource)
        {
            case 2:
                Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y.Item1, z.Item1);
                Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y.Item2, z.Item2);
                break;
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }
    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/> and two tuples of three scalar values, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span, the tuples and destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator to.</param>
    /// <param name="y">The first tuple of scalar values to apply the operator to.</param>
    /// <param name="z">The second tuple of scalar values to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source span doesn't have a length multiple of 3, the destination is too small, or at least one source span overlaps with the destination but they are not identical.</exception>
    /// <remarks>If the destination is the same as the source span, the operation is performed in-place.</remarks>
    public static void Apply<T, TOperator>(ReadOnlySpan<T> x, (T, T, T) y, (T, T, T) z, Span<T> destination)
        where T : struct
        where TOperator : struct, ITernaryOperator<T, T, T, T>
    {
        if (SpansOverlapAndAreNotSame(x, destination))
            Throw.ArgumentException(nameof(destination), "Destination span overlaps with x.");

        Apply<T, T, T, T, TOperator>(x, y, z, destination);
    }

    /// <summary>
    /// Applies the specified operator to the elements of a <see cref="ReadOnlySpan{T}"/> and two tuples of three scalar values, and stores the result in the destination <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the source span.</typeparam>
    /// <typeparam name="T2">The type of the elements in the first tuples</typeparam>
    /// <typeparam name="T3">The type of the elements in the second tuple.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the destination span.</typeparam>
    /// <typeparam name="TOperator">The type of the operator that must implement the <see cref="ITernaryOperator{T, T, T, T}"/> interface.</typeparam>
    /// <param name="x">The span of elements to apply the operator to.</param>
    /// <param name="y">The first tuple of scalar values to apply the operator to.</param>
    /// <param name="z">The second tuple of scalar values to apply the operator to.</param>
    /// <param name="destination">The span to store the result of the operation.</param>
    /// <exception cref="ArgumentException">The source span doesn't have a length multiple of 3, the destination is too small.</exception>
    /// <remarks>If the destination is the same as the source span, the operation is performed in-place.</remarks>
    public static void Apply<T1, T2, T3, TResult, TOperator>(ReadOnlySpan<T1> x, (T2, T2, T2) y, (T3, T3, T3) z, Span<TResult> destination)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where TResult : struct
        where TOperator : struct, ITernaryOperator<T1, T2, T3, TResult>
    {
        if (x.Length % 3 is not 0)
            Throw.ArgumentException(nameof(x), "x span must have a length multiple of 3.");
        if (x.Length > destination.Length)
            Throw.ArgumentException(nameof(destination), "Destination span is too small.");

        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var destinationRef = ref MemoryMarshal.GetReference(destination);
        var indexSource = nint.Zero;
        for (; indexSource < x.Length; indexSource += 3)
        {
            Unsafe.Add(ref destinationRef, indexSource) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y.Item1, z.Item1);
            Unsafe.Add(ref destinationRef, indexSource + 1) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), y.Item2, z.Item2);
            Unsafe.Add(ref destinationRef, indexSource + 2) = TOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), y.Item3, z.Item3);
        }

        switch (x.Length - (int)indexSource)
        {
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }
    }
}