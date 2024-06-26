namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    /// <summary>
    /// Aggregates the elements of a <see cref="ReadOnlySpan{T}"/> using the specified aggregation operator.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{T, T}"/> interface.</typeparam>
    /// <param name="source">The span of elements to aggregate.</param>
    /// <returns>The result of the aggregation.</returns>
    /// <remarks>This methods does not propagate NaN.</remarks>
    public static T AggregateNumber<T, TAggregateOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TAggregateOperator : struct, IAggregationOperator<T, T>
        => AggregateNumber<T, IdentityOperator<T>, TAggregateOperator>(source);

    /// <summary>
    /// Aggregates the elements of a <see cref="ReadOnlySpan{T}"/> using the specified aggregation operator.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span.</typeparam>
    /// <typeparam name="TTransformOperator">The type of the transform operator that must implement the <see cref="IUnaryOperator{TSource, TTransformed}"/> interface.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{T, T}"/> interface.</typeparam>
    /// <param name="source">The span of elements to aggregate.</param>
    /// <returns>The result of the aggregation.</returns>
    /// <remarks>This methods does not propagate NaN.</remarks>
    public static T AggregateNumber<T, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TTransformOperator : struct, IUnaryOperator<T, T>
        where TAggregateOperator : struct, IAggregationOperator<T, T>
        => AggregateNumber<T, T, T, TTransformOperator, TAggregateOperator>(source);

    /// <summary>
    /// Aggregates the elements of a <see cref="ReadOnlySpan{T1}"/> using the specified transform and aggregation operators.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in the source span.</typeparam>
    /// <typeparam name="TTransformed">The type of the elements after the transform operation.</typeparam>
    /// <typeparam name="TResult">The type of the result of the aggregation.</typeparam>
    /// <typeparam name="TTransformOperator">The type of the transform operator that must implement the <see cref="IUnaryOperator{TSource, TTransformed}"/> interface.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{TTransformed, TResult}"/> interface.</typeparam>
    /// <param name="source">The span of elements to transform and aggregate.</param>
    /// <returns>The result of the aggregation.</returns>
    /// <remarks>
    ///     <para>The transform operator is applied to the source elements before the aggregation operator.</para>
    ///     <para>This methods does not propagate NaN.</para>
    /// </remarks>
    public static TResult AggregateNumber<TSource, TTransformed, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<TSource> source)
        where TSource : struct
        where TTransformed : struct
        where TResult : struct
        where TTransformOperator : struct, IUnaryOperator<TSource, TTransformed>
        where TAggregateOperator : struct, IAggregationOperator<TTransformed, TResult>
    {
        // initialize aggregate
        var aggregate = TAggregateOperator.Seed;
        var indexSource = 0;

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
                for (var indexVector = 0; indexVector < sourceVectors.Length; indexVector++)
                {
                    var transformedVector = TTransformOperator.Invoke(ref Unsafe.Add(ref sourceVectorsRef, indexVector));
                    resultVector = TAggregateOperator.Invoke(ref resultVector, ref transformedVector);
                }

                // aggregate the aggregate vector into the aggregate
                aggregate = TAggregateOperator.Invoke(aggregate, ref resultVector);

                // skip the source elements already aggregated
                indexSource = sourceVectors.Length * Vector<TSource>.Count;
            }
        }

        // aggregate the remaining elements in the source
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        var remaining = source.Length - indexSource;
        if (remaining >= 8)
        {
            var partial1 = TAggregateOperator.Seed;
            var partial2 = TAggregateOperator.Seed;
            var partial3 = TAggregateOperator.Seed;
            for (; indexSource < source.Length - 3; indexSource += 4)
            {
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
                partial1 = TAggregateOperator.Invoke(partial1, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1)));
                partial2 = TAggregateOperator.Invoke(partial2, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 2)));
                partial3 = TAggregateOperator.Invoke(partial3, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 3)));
            }
            aggregate = TAggregateOperator.Invoke(aggregate, partial1);
            aggregate = TAggregateOperator.Invoke(aggregate, partial2);
            aggregate = TAggregateOperator.Invoke(aggregate, partial3);
            
            remaining = source.Length - indexSource;
        }

        switch(remaining)
        {
            case 7:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 2)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 3)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 4)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 5)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 6)));
                break;
            case 6:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 2)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 3)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 4)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 5)));
                break;
            case 5:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 2)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 3)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 4)));
                break;
            case 4:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 2)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 3)));
                break;
            case 3:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 2)));
                break;
            case 2:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1)));
                break;
            case 1:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
                break;
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }

        return aggregate;
    }

    /// <summary>
    /// Aggregates the elements of two <see cref="ReadOnlySpan{T}"/> using the specified transform and aggregation operators.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source spans.</typeparam>
    /// <typeparam name="TTransformOperator">The type of the transform operator that must implement the <see cref="IBinaryOperator{T, T, T}"/> interface.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{T, T}"/> interface.</typeparam>
    /// <param name="x">The first span of elements to transform and aggregate.</param>
    /// <param name="y">The second span of elements to transform and aggregate.</param>
    /// <returns>The result of the aggregation.</returns>
    /// <remarks>
    ///     <para>The transform operator is applied to the source elements before the aggregation operator.</para>
    ///     <para>This methods does not propagate NaN.</para>
    /// </remarks>
    public static T AggregateNumber<T, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct
        where TTransformOperator : struct, IBinaryOperator<T, T, T>
        where TAggregateOperator : struct, IAggregationOperator<T, T>
        => AggregateNumber<T, T, T, T, TTransformOperator, TAggregateOperator>(x, y);

    /// <summary>
    /// Aggregates the elements of two <see cref="ReadOnlySpan{T}"/> using the specified transform and aggregation operators.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the first source span.</typeparam>
    /// <typeparam name="T2">The type of the elements in the second source span.</typeparam>
    /// <typeparam name="TTransformed">The type of the elements after the transform operation.</typeparam>
    /// <typeparam name="TResult">The type of the result of the aggregation.</typeparam>
    /// <typeparam name="TTransformOperator">The type of the transform operator that must implement the <see cref="IBinaryOperator{T, T, T}"/> interface.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{T, T}"/> interface.</typeparam>
    /// <param name="x">The first span of elements to transform and aggregate.</param>
    /// <param name="y">The second span of elements to transform and aggregate.</param>
    /// <returns>The result of the aggregation.</returns>
    /// <remarks>
    ///     <para>The transform operator is applied to the source elements before the aggregation operator.</para>
    ///     <para>This methods does not propagate NaN.</para>
    /// </remarks>
    public static TResult AggregateNumber<T1, T2, TTransformed, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y)
        where T1 : struct
        where T2 : struct
        where TTransformed : struct
        where TResult : struct
        where TTransformOperator : struct, IBinaryOperator<T1, T2, TTransformed>
        where TAggregateOperator : struct, IAggregationOperator<TTransformed, TResult>
    {
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(y), "source spans must have the same size.");

        // initialize aggregate
        var aggregate = TAggregateOperator.Seed;
        var indexSource = 0;

        // aggregate using hardware acceleration if available
        if (TTransformOperator.IsVectorizable &&
            TAggregateOperator.IsVectorizable && 
            Vector.IsHardwareAccelerated && 
            Vector<T1>.IsSupported &&
            Vector<T2>.IsSupported &&
            Vector<TTransformed>.IsSupported &&
            Vector<TResult>.IsSupported)
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
                for (var indexVector = 0; indexVector < xVectors.Length; indexVector++)
                {
                    var transformedVector = TTransformOperator.Invoke(ref Unsafe.Add(ref xVectorsRef, indexVector), ref Unsafe.Add(ref yVectorsRef, indexVector));
                    resultVector = TAggregateOperator.Invoke(ref resultVector, ref transformedVector);
                }

                // aggregate the aggregate vector into the aggregate
                aggregate = TAggregateOperator.Invoke(aggregate, ref resultVector);

                // skip the source elements already aggregated
                indexSource = xVectors.Length * Vector<T1>.Count;
            }
        }

        // aggregate the remaining elements in the source
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        var remaining = x.Length - indexSource;
        if (remaining >= 8)
        {
            var partial1 = TAggregateOperator.Seed;
            var partial2 = TAggregateOperator.Seed;
            var partial3 = TAggregateOperator.Seed;
            for (; indexSource < x.Length - 3; indexSource += 4)
            {
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
                partial1 = TAggregateOperator.Invoke(partial1, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1)));
                partial2 = TAggregateOperator.Invoke(partial2, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2)));
                partial3 = TAggregateOperator.Invoke(partial3, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), Unsafe.Add(ref yRef, indexSource + 3)));
            }
            aggregate = TAggregateOperator.Invoke(aggregate, partial1);
            aggregate = TAggregateOperator.Invoke(aggregate, partial2);
            aggregate = TAggregateOperator.Invoke(aggregate, partial3);
            remaining = x.Length - indexSource;
        }

        switch(remaining)
        {
            case 7:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), Unsafe.Add(ref yRef, indexSource + 3)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 4), Unsafe.Add(ref yRef, indexSource + 4)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 5), Unsafe.Add(ref yRef, indexSource + 5)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 6), Unsafe.Add(ref yRef, indexSource + 6)));
                break;
            case 6:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), Unsafe.Add(ref yRef, indexSource + 3)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 4), Unsafe.Add(ref yRef, indexSource + 4)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 5), Unsafe.Add(ref yRef, indexSource + 5)));
                break;
            case 5:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), Unsafe.Add(ref yRef, indexSource + 3)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 4), Unsafe.Add(ref yRef, indexSource + 4)));
                break;
            case 4:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), Unsafe.Add(ref yRef, indexSource + 3)));
                break;
            case 3:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2)));
                break;
            case 2:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1)));
                break;
            case 1:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
                break;
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }

        return aggregate;
    }

}

