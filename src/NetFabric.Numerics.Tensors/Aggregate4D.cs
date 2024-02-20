namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    /// <summary>
    /// Aggregates the elements of a source <see cref="ReadOnlySpan{T}"/> containing contiguous 4D data using the specified aggregation operator.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{T, T}"/> interface.</typeparam>
    /// <param name="source">The source span containing the 4D data to aggregate.</param>
    /// <returns>A tuple containing the aggregated values.</returns>
    public static ValueTuple<T, T, T, T> Aggregate4D<T, TAggregateOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TAggregateOperator : struct, IAggregationOperator<T, T>
        => Aggregate4D<T, T, T, IdentityOperator<T>, TAggregateOperator>(source);

    /// <summary>
    /// Aggregates the elements of a source <see cref="ReadOnlySpan{T}"/> containing contiguous 4D data using the specified transform and aggregation operators.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the source span.</typeparam>
    /// <typeparam name="T2">The type of the elements in the transformed data.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <typeparam name="TTransformOperator">The type of the transform operator that must implement the <see cref="IUnaryOperator{T, T}"/> interface.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{T, T}"/> interface.</typeparam>
    /// <param name="source">The source span containing the 4D data to aggregate.</param>
    /// <remarks>The transform operator is applied to the source elements before the aggregation operator.</remarks>
    public static ValueTuple<TResult, TResult, TResult, TResult> Aggregate4D<T1, T2, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T1> source)
        where T1 : struct
        where T2 : struct
        where TResult : struct
        where TTransformOperator : struct, IUnaryOperator<T1, T2>
        where TAggregateOperator : struct, IAggregationOperator<T2, TResult>
    {
        if (source.Length % 4 is not 0)
            Throw.ArgumentException(nameof(source), "source span must have a size multiple of 4.");

        // initialize aggregate
        var aggregateX = TAggregateOperator.Seed;
        var aggregateY = TAggregateOperator.Seed;
        var aggregateZ = TAggregateOperator.Seed;
        var aggregateW = TAggregateOperator.Seed;
        var indexSource = nint.Zero;

        // aggregate using hardware acceleration if available
        if (TTransformOperator.IsVectorizable &&
            TAggregateOperator.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<T1>.IsSupported &&
            Vector<T2>.IsSupported &&
            Vector<TResult>.IsSupported)
        {
            // convert source span to vector span without copies
            var sourceVectors = MemoryMarshal.Cast<T1, Vector<T1>>(source);

            // check if there is at least one vector to aggregate
            if (sourceVectors.Length > 1)
            {
                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                var indexVector = nint.Zero;
                if (Vector<T1>.Count % 4 is 0)
                {
                    // use one aggregator vector

                    // initialize aggregate vector
                    var resultVector = new Vector<TResult>(TAggregateOperator.Seed);

                    // aggregate the source vectors into the aggregate vector
                    for (; indexVector < sourceVectors.Length; indexVector++)
                    {
                        var transformedVector = TTransformOperator.Invoke(ref Unsafe.Add(ref sourceVectorsRef, indexVector));
                        resultVector = TAggregateOperator.Invoke(ref resultVector, ref transformedVector);
                    }

                    // aggregate the aggregate vector into the aggregate
                    for (var index = 0; index + 3 < Vector<TResult>.Count; index += 4)
                    {
                        aggregateX = TAggregateOperator.Invoke(aggregateX, resultVector[index]);
                        aggregateY = TAggregateOperator.Invoke(aggregateY, resultVector[index + 1]);
                        aggregateZ = TAggregateOperator.Invoke(aggregateZ, resultVector[index + 2]);
                        aggregateW = TAggregateOperator.Invoke(aggregateW, resultVector[index + 3]);
                    }
                }
                else
                {
                    // use two aggregator vectors

                    // initialize aggregate vectors
                    var values = new TResult[Vector<TResult>.Count * 2];
                    Array.Fill(values, TAggregateOperator.Seed);
                    var resultValues = values.AsSpan();
                    var resultVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(resultValues);

                    // aggregate the source vectors into the aggregate vectors
                    ref var resultVectorsRef = ref MemoryMarshal.GetReference(resultVectors);
                    for (; indexVector + 1 < sourceVectors.Length; indexVector += 2)
                    {
                        var transformedVector0 = TTransformOperator.Invoke(ref Unsafe.Add(ref sourceVectorsRef, indexVector));
                        Unsafe.Add(ref resultVectorsRef, 0) = TAggregateOperator.Invoke(ref Unsafe.Add(ref resultVectorsRef, 0), ref transformedVector0);
                        var transformedVector1 = TTransformOperator.Invoke(ref Unsafe.Add(ref sourceVectorsRef, indexVector + 1));
                        Unsafe.Add(ref resultVectorsRef, 1) = TAggregateOperator.Invoke(ref Unsafe.Add(ref resultVectorsRef, 1), ref transformedVector1);
                    }

                    // aggregate the aggregate vector into the aggregate
                    ref var resultValuesRef = ref MemoryMarshal.GetReference(resultValues);
                    for (var index = nint.Zero; index + 3 < Vector<TResult>.Count * 4; index += 4)
                    {
                        aggregateX = TAggregateOperator.Invoke(aggregateX, Unsafe.Add(ref resultValuesRef, index));
                        aggregateY = TAggregateOperator.Invoke(aggregateY, Unsafe.Add(ref resultValuesRef, index + 1));
                        aggregateZ = TAggregateOperator.Invoke(aggregateZ, Unsafe.Add(ref resultValuesRef, index + 2));
                        aggregateW = TAggregateOperator.Invoke(aggregateW, Unsafe.Add(ref resultValuesRef, index + 3));
                    }
                }

                // skip the source elements already aggregated
                indexSource = indexVector * Vector<T1>.Count;
            }
        }

        // aggregate the remaining elements in the source
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        for (; indexSource + 3 < source.Length; indexSource += 4)
        {
            aggregateX = TAggregateOperator.Invoke(aggregateX, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
            aggregateY = TAggregateOperator.Invoke(aggregateY, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1)));
            aggregateZ = TAggregateOperator.Invoke(aggregateZ, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 2)));
            aggregateW = TAggregateOperator.Invoke(aggregateW, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 3)));
        }

        return (aggregateX, aggregateY, aggregateZ, aggregateW);
    }

    /// <summary>
    /// Aggregates the elements of two source <see cref="ReadOnlySpan{T}"/> containing contiguous 4D data using the specified aggregation operator.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source spans.</typeparam>
    /// <typeparam name="TTransformOperator">The type of the transform operator that must implement the <see cref="IBinaryOperator{T, T, T}"/> interface.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{T, T}"/> interface.</typeparam>
    /// <param name="x">The source span containing the first set of contiguous 4D data to transform and aggregate.</param>
    /// <param name="y">The source span containing the second set of contiguous 4D data to transform and aggregate.</param>
    /// <returns>A tuple containing the transformed and aggregated results.</returns>
    /// <remarks>The transform operator is applied to the source elements before the aggregation operator.</remarks>
    public static ValueTuple<T, T, T, T> Aggregate4D<T, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct
        where TTransformOperator : struct, IBinaryOperator<T, T, T>
        where TAggregateOperator : struct, IAggregationOperator<T, T>
        => Aggregate4D<T, T, T, T, TTransformOperator, TAggregateOperator>(x, y);

    /// <summary>
    /// Aggregates the elements of two source <see cref="ReadOnlySpan{T}"/> containing contiguous 4D data using the specified aggregation operator.
    /// </summary>
    /// <typeparam name="TSource1">The type of the elements in the first source span.</typeparam>
    /// <typeparam name="TSource2">The type of the elements in the second source span.</typeparam>
    /// <typeparam name="TTransformed">The type of the elements in the transformed data.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <typeparam name="TTransformOperator">The type of the transform operator that must implement the <see cref="IBinaryOperator{T, T, T}"/> interface.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{T, T}"/> interface.</typeparam>
    /// <param name="x">The source span containing the first set of contiguous 4D data to transform and aggregate.</param>
    /// <param name="y">The source span containing the second set of contiguous 4D data to transform and aggregate.</param>
    /// <returns>A tuple containing the transformed and aggregated results.</returns>
    /// <remarks>The transform operator is applied to the source elements before the aggregation operator.</remarks>
    public static ValueTuple<TResult, TResult, TResult, TResult> Aggregate4D<TSource1, TSource2, TTransformed, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<TSource1> x, ReadOnlySpan<TSource2> y)
        where TSource1 : struct
        where TSource2 : struct
        where TTransformed : struct
        where TResult : struct
        where TTransformOperator : struct, IBinaryOperator<TSource1, TSource2, TTransformed>
        where TAggregateOperator : struct, IAggregationOperator<TTransformed, TResult>
    {
        if (x.Length % 4 is not 0)
            Throw.ArgumentException(nameof(x), "source spans must have a size multiple of 4.");
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(y), "source spans must have the same size.");

        // initialize aggregate
        var aggregateX = TAggregateOperator.Seed;
        var aggregateY = TAggregateOperator.Seed;
        var aggregateZ = TAggregateOperator.Seed;
        var aggregateW = TAggregateOperator.Seed;
        var indexSource = nint.Zero;

        // aggregate using hardware acceleration if available
        if (TTransformOperator.IsVectorizable &&
            TAggregateOperator.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<TSource1>.IsSupported &&
            Vector<TSource2>.IsSupported &&
            Vector<TTransformed>.IsSupported &&
            Vector<TResult>.IsSupported)
        {
            // convert source span to vector span without copies
            var xVectors = MemoryMarshal.Cast<TSource1, Vector<TSource1>>(x);
            var yVectors = MemoryMarshal.Cast<TSource2, Vector<TSource2>>(y);

            // check if there is at least one vector to aggregate
            if (xVectors.Length > 1)
            {
                ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
                ref var yVectorsRef = ref MemoryMarshal.GetReference(yVectors);
                var indexVector = nint.Zero;
                if (Vector<TSource1>.Count % 4 is 0)
                {
                    // initialize aggregate vector
                    var resultVector = new Vector<TResult>(TAggregateOperator.Seed);

                    // aggregate the source vectors into the aggregate vector
                    for (; indexVector < xVectors.Length; indexVector++)
                    {
                        var transformedVector = TTransformOperator.Invoke(ref Unsafe.Add(ref xVectorsRef, indexVector), ref Unsafe.Add(ref yVectorsRef, indexVector));
                        resultVector = TAggregateOperator.Invoke(ref resultVector, ref transformedVector);
                    }

                    // aggregate the aggregate vector into the aggregate
                    for (var index = 0; index + 3 < Vector<TResult>.Count; index += 4)
                    {
                        aggregateX = TAggregateOperator.Invoke(aggregateX, resultVector[index]);
                        aggregateY = TAggregateOperator.Invoke(aggregateY, resultVector[index + 1]);
                        aggregateZ = TAggregateOperator.Invoke(aggregateZ, resultVector[index + 2]);
                        aggregateW = TAggregateOperator.Invoke(aggregateW, resultVector[index + 3]);
                    }
                }
                else
                {
                    // use two aggregator vectors

                    // initialize aggregate vectors
                    var values = new TResult[Vector<TResult>.Count * 2];
                    Array.Fill(values, TAggregateOperator.Seed);
                    var resultValues = values.AsSpan();
                    var resultVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(resultValues);

                    // aggregate the source vectors into the aggregate vectors
                    ref var resultVectorsRef = ref MemoryMarshal.GetReference(resultVectors);
                    for (; indexVector + 1 < xVectors.Length; indexVector += 2)
                    {
                        var transformedVector0 = TTransformOperator.Invoke(ref Unsafe.Add(ref xVectorsRef, indexVector), ref Unsafe.Add(ref yVectorsRef, indexVector));
                        Unsafe.Add(ref resultVectorsRef, 0) = TAggregateOperator.Invoke(ref Unsafe.Add(ref resultVectorsRef, 0), ref transformedVector0);
                        var transformedVector1 = TTransformOperator.Invoke(ref Unsafe.Add(ref xVectorsRef, indexVector), ref Unsafe.Add(ref yVectorsRef, indexVector));
                        Unsafe.Add(ref resultVectorsRef, 1) = TAggregateOperator.Invoke(ref Unsafe.Add(ref resultVectorsRef, 1), ref transformedVector1);
                    }

                    // aggregate the aggregate vector into the aggregate
                    ref var resultValuesRef = ref MemoryMarshal.GetReference(resultValues);
                    for (var index = nint.Zero; index + 3 < Vector<TResult>.Count * 4; index += 4)
                    {
                        aggregateX = TAggregateOperator.Invoke(aggregateX, Unsafe.Add(ref resultValuesRef, index));
                        aggregateY = TAggregateOperator.Invoke(aggregateY, Unsafe.Add(ref resultValuesRef, index + 1));
                        aggregateZ = TAggregateOperator.Invoke(aggregateZ, Unsafe.Add(ref resultValuesRef, index + 2));
                        aggregateW = TAggregateOperator.Invoke(aggregateW, Unsafe.Add(ref resultValuesRef, index + 3));
                    }
                }

                // skip the source elements already aggregated
                indexSource = indexVector * Vector<TSource1>.Count;
            }
        }

        // aggregate the remaining elements in the source
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        for (; indexSource + 3 < x.Length; indexSource += 4)
        {
            aggregateX = TAggregateOperator.Invoke(aggregateX, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
            aggregateY = TAggregateOperator.Invoke(aggregateY, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1)));
            aggregateZ = TAggregateOperator.Invoke(aggregateZ, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2)));
            aggregateW = TAggregateOperator.Invoke(aggregateW, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), Unsafe.Add(ref yRef, indexSource + 3)));
        }

        return (aggregateX, aggregateY, aggregateZ, aggregateW);
    }
}
