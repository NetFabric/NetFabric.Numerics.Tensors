namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    /// <summary>
    /// Aggregates the elements of a source <see cref="ReadOnlySpan{T}"/> containing contiguous 2D data using the specified aggregation operator.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{T, T}"/> interface.</typeparam>
    /// <param name="source">The source span containing contiguous 2D data to aggregate.</param>
    /// <returns>A tuple containing the aggregated results.</returns>
    public static (T, T) AggregateNumber2D<T, TAggregateOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TAggregateOperator : struct, IAggregationOperator<T, T>
        => AggregateNumber2D<T, T, T, IdentityOperator<T>, TAggregateOperator>(source);

    /// <summary>
    /// Transforms and aggregates a source <see cref="ReadOnlySpan{T}"/> containing contiguous 2D data using the specified transform and aggregation operators.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in the source span.</typeparam>
    /// <typeparam name="TTransformed">The type of the elements in the transformed data.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <typeparam name="TTransformOperator">The type of the transform operator that must implement the <see cref="IUnaryOperator{TSource, TTransformed}"/> interface.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{TTransformed, TResult}"/> interface.</typeparam>
    /// <param name="source">The source span containing contiguous 2D data to transform and aggregate.</param>
    /// <returns>A tuple containing the transformed and aggregated results.</returns>
    /// <remarks>The transform operator is applied to the source elements before the aggregation operator.</remarks>
    public static (TResult, TResult) AggregateNumber2D<TSource, TTransformed, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<TSource> source)
        where TSource : struct
        where TTransformed : struct
        where TResult : struct
        where TTransformOperator : struct, IUnaryOperator<TSource, TTransformed>
        where TAggregateOperator : struct, IAggregationOperator<TTransformed, TResult>
    {
        if (source.Length % 2 is not 0)
            Throw.ArgumentException(nameof(source), "source span must have a size multiple of 2.");

        // initialize aggregate
        var aggregateX = TAggregateOperator.Seed;
        var aggregateY = TAggregateOperator.Seed;
        var indexSource = nint.Zero;

        // aggregate using hardware acceleration if available
        if (TTransformOperator.IsVectorizable &&
            TAggregateOperator.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<TSource>.IsSupported &&
            Vector<TTransformed>.IsSupported &&
            Vector<TResult>.IsSupported)
        {
            // convert source span to vector span without copies
            var sourceVectors = MemoryMarshal.Cast<TSource, Vector<TSource>>(source);

            // check if there is at least one vector to aggregate
            if (sourceVectors.Length > 0)
            {
                // initialize aggregate vector
                var resultVector = new Vector<TResult>(TAggregateOperator.Seed);

                // aggregate the source vectors into the aggregate vector
                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                var indexVector = nint.Zero;
                for (; indexVector < sourceVectors.Length; indexVector++)
                {
                    var transformedVector = TTransformOperator.Invoke(ref Unsafe.Add(ref sourceVectorsRef, indexVector));
                    resultVector = TAggregateOperator.Invoke(ref resultVector, ref transformedVector);
                }

                // aggregate the aggregate vector into the aggregate
                for (var index = 0; index + 1 < Vector<TResult>.Count; index += 2)
                {
                    aggregateX = TAggregateOperator.Invoke(aggregateX, resultVector[index]);
                    aggregateY = TAggregateOperator.Invoke(aggregateY, resultVector[index + 1]);
                }

                // skip the source elements already aggregated
                indexSource = indexVector * Vector<TSource>.Count;
            }
        }

        // aggregate the remaining elements in the source
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        var remaining = source.Length;
        if (remaining >= 4)
        {
            var partialX1 = TAggregateOperator.Seed;
            var partialY1 = TAggregateOperator.Seed;
            for (; indexSource + 3 < source.Length; indexSource += 4)
            {
                aggregateX = TAggregateOperator.Invoke(aggregateX, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
                aggregateY = TAggregateOperator.Invoke(aggregateY, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1)));
                partialX1 = TAggregateOperator.Invoke(partialX1, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 2)));
                partialY1 = TAggregateOperator.Invoke(partialY1, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 3)));
            }
            aggregateX = TAggregateOperator.Invoke(aggregateX, partialX1);
            aggregateY = TAggregateOperator.Invoke(aggregateY, partialY1);

            remaining = source.Length - (int)indexSource;
        }

        switch(remaining)
        {
            case 2:
                aggregateX = TAggregateOperator.Invoke(aggregateX, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
                aggregateY = TAggregateOperator.Invoke(aggregateY, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1)));
                break;
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }

        return (aggregateX, aggregateY);
    }

    /// <summary>
    /// Aggregates the elements of two <see cref="ReadOnlySpan{T}"/> using the specified transform and aggregation operators.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source spans.</typeparam>
    /// <typeparam name="TTransformOperator">The type of the transform operator that must implement the <see cref="IBinaryOperator{T, T, T}"/> interface.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{T, T}"/> interface.</typeparam>
    /// <param name="x">The source span containing the first set of contiguous 2D data to transform and aggregate.</param>
    /// <param name="y">The source span containing the second set of contiguous 2D data to transform and aggregate.</param>
    /// <returns>The result of the aggregation.</returns>
    /// <remarks>The transform operator is applied to the source elements before the aggregation operator.</remarks>
    public static (T, T) AggregateNumber2D<T, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct
        where TTransformOperator : struct, IBinaryOperator<T, T, T>
        where TAggregateOperator : struct, IAggregationOperator<T, T>
        => AggregateNumber2D<T, T, T, T, TTransformOperator, TAggregateOperator>(x, y);

    /// <summary>
    /// Transforms and aggregates the elements of two source <see cref="ReadOnlySpan{T}"/> containing contiguous 2D data using the specified transform and aggregation operators.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the first source span.</typeparam>
    /// <typeparam name="T2">The type of the elements in the second source span.</typeparam>
    /// <typeparam name="TTransformed">The type of the elements in the transformed data.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <typeparam name="TTransformOperator">The type of the transform operator that must implement the <see cref="IBinaryOperator{T, T, T}"/> interface.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{T, T}"/> interface.</typeparam>
    /// <param name="x">The source span containing the first set of contiguous 2D data to transform and aggregate.</param>
    /// <param name="y">The source span containing the second set of contiguous 2D data to transform and aggregate.</param>
    /// <returns>A tuple containing the transformed and aggregated results.</returns>
    /// <remarks>The transform operator is applied to the source elements before the aggregation operator.</remarks>
    public static (TResult, TResult) AggregateNumber2D<T1, T2, TTransformed, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y)
        where T1 : struct
        where T2 : struct
        where TTransformed : struct
        where TResult : struct
        where TTransformOperator : struct, IBinaryOperator<T1, T2, TTransformed>
        where TAggregateOperator : struct, IAggregationOperator<TTransformed, TResult>
    {
        if (x.Length % 2 is not 0)
            Throw.ArgumentException(nameof(x), "source spans must have a size multiple of 2.");
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(y), "source spans must have the same size.");

        // initialize aggregate
        var aggregateX = TAggregateOperator.Seed;
        var aggregateY = TAggregateOperator.Seed;
        var indexSource = nint.Zero;

        // aggregate using hardware acceleration if available
        if (TTransformOperator.IsVectorizable &&
            TAggregateOperator.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<T1>.IsSupported &&
            Vector<T2>.IsSupported &&
            Vector<TTransformed>.IsSupported &&
            Vector<TResult>.IsSupported &&
            Vector<TTransformed>.Count % 2 == 0)
        {
            // convert source span to vector span without copies
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var yVectors = MemoryMarshal.Cast<T2, Vector<T2>>(y);

            // check if there is at least one vector to aggregate
            if (xVectors.Length > 0)
            {
                // initialize aggregate vector
                var resultVector = new Vector<TResult>(TAggregateOperator.Seed);

                // aggregate the source vectors into the aggregate vector
                ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
                ref var yVectorsRef = ref MemoryMarshal.GetReference(yVectors);
                var indexVector = nint.Zero;
                for (; indexVector < xVectors.Length; indexVector++)
                {
                    var transformedVector = TTransformOperator.Invoke(ref Unsafe.Add(ref xVectorsRef, indexVector), ref Unsafe.Add(ref yVectorsRef, indexVector));
                    resultVector = TAggregateOperator.Invoke(ref resultVector, ref transformedVector);
                }

                // aggregate the aggregate vector into the aggregate
                for (var index = 0; index + 1 < Vector<TResult>.Count; index += 2)
                {
                    aggregateX = TAggregateOperator.Invoke(aggregateX, resultVector[index]);
                    aggregateY = TAggregateOperator.Invoke(aggregateY, resultVector[index + 1]);
                }

                // skip the source elements already aggregated
                indexSource = indexVector * Vector<T1>.Count;
            }
        }

        // aggregate the remaining elements in the source
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        var remaining = x.Length - (int)indexSource;
        if (remaining >= 4)
        {
            var partialX1 = TAggregateOperator.Seed;
            var partialY1 = TAggregateOperator.Seed;
            for (; indexSource + 3 < x.Length; indexSource += 4)
            {
                aggregateX = TAggregateOperator.Invoke(aggregateX, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
                aggregateY = TAggregateOperator.Invoke(aggregateY, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1)));
                partialX1 = TAggregateOperator.Invoke(partialX1, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2)));
                partialY1 = TAggregateOperator.Invoke(partialY1, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), Unsafe.Add(ref yRef, indexSource + 3)));
            }
            aggregateX = TAggregateOperator.Invoke(aggregateX, partialX1);
            aggregateY = TAggregateOperator.Invoke(aggregateY, partialY1);

            remaining = x.Length - (int)indexSource;
        }

        switch(remaining)
        {
            case 2:
                aggregateX = TAggregateOperator.Invoke(aggregateX, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
                aggregateY = TAggregateOperator.Invoke(aggregateY, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1)));
                break;
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }

        return (aggregateX, aggregateY);
    }
}
