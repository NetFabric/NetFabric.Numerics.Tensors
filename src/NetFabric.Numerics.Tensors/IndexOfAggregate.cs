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
    /// <remarks>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns the index of the first element to which the transformation and aggregation results in NaN.</remarks>
    public static int IndexOfAggregate<T, TAggregateOperator>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>
        where TAggregateOperator : struct, IAggregationOperator<T, T>
        => IndexOfAggregate<T, IdentityOperator<T>, TAggregateOperator>(source);

    /// <summary>
    /// Aggregates the elements of a <see cref="ReadOnlySpan{T}"/> using the specified aggregation operator.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span.</typeparam>
    /// <typeparam name="TTransformOperator">The type of the transform operator that must implement the <see cref="IUnaryOperator{TSource, TTransformed}"/> interface.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{T, T}"/> interface.</typeparam>
    /// <param name="source">The span of elements to aggregate.</param>
    /// <returns>The result of the aggregation.</returns>
    /// <remarks>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns the index of the first element to which the transformation and aggregation results in NaN.</remarks>
    public static int IndexOfAggregate<T, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>
        where TTransformOperator : struct, IUnaryOperator<T, T>
        where TAggregateOperator : struct, IAggregationOperator<T, T>
        => IndexOfAggregate<T, T, T, TTransformOperator, TAggregateOperator>(source);

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
    /// <para>The transform operator is applied to the source elements before the aggregation operator.</para>
    /// <para>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns the index of the first element to which the transformation and aggregation results in NaN.</para>
    /// </remarks>
    public static int IndexOfAggregate<TSource, TTransformed, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<TSource> source)
        where TSource : struct
        where TTransformed : struct
        where TResult : struct, INumberBase<TResult>
        where TTransformOperator : struct, IUnaryOperator<TSource, TTransformed>
        where TAggregateOperator : struct, IAggregationOperator<TTransformed, TResult>
    {
        if (source.IsEmpty)
            return -1;

        // initialize aggregate
        var aggregate = TAggregateOperator.Seed;
        var indexOfAggregate = -1;
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

            // check if there is more than one vector to aggregate
            if (sourceVectors.Length > 1)
            {
                // initialize aggregate vector
                var aggregateVector = new Vector<TResult>(TAggregateOperator.Seed);
                var aggregateIndicesVector = new Vector<TResult>(TResult.CreateChecked(-1));
                var indicesVector = VectorFactory<TResult>.Indices;
                var indicesIncrementVector = new Vector<TResult>(TResult.CreateChecked(Vector<TResult>.Count));

                // aggregate the source vectors into the aggregate vector
                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                for (var indexVector = 0; indexVector < sourceVectors.Length; indexVector++)
                {
                    var transformedVector = TTransformOperator.Invoke(ref Unsafe.Add(ref sourceVectorsRef, indexVector));
                    var currentVector = TAggregateOperator.Invoke(ref aggregateVector, ref transformedVector);
                    if (Vector.EqualsAll(currentVector, currentVector)) // check if vector contains NaN
                    {
                        var equalsVector = Vector.Equals(currentVector, aggregateVector);
                        aggregateVector = Vector.ConditionalSelect(equalsVector, aggregateVector, currentVector);
                        aggregateIndicesVector = Vector.ConditionalSelect(equalsVector, aggregateIndicesVector, indicesVector);

                        indicesVector += indicesIncrementVector;
                    }
                    else
                    {
                        for (var index = 0; index < Vector<TResult>.Count; index++)
                        {
                            if (TResult.IsNaN(currentVector[index]))
                                return (indexVector * Vector<TResult>.Count) + index;
                        }
                        Throw.Exception("Should not happen!");
                    }
                }

                // aggregate the aggregate vector into the aggregate
                for (var index = 0; index < Vector<TResult>.Count; index++)
                {
                    var current = aggregateVector[index];
                    var currentIndex = int.CreateChecked(aggregateIndicesVector[index]);
                    if (!TAggregateOperator.Invoke(aggregate, current).Equals(aggregate))
                    {
                        aggregate = current;
                        indexOfAggregate = currentIndex;
                    }
                    else if(aggregate.Equals(current) && indexOfAggregate > currentIndex)
                    {
                        indexOfAggregate = currentIndex;
                    } 
                }

                // skip the source elements already aggregated
                indexSource = sourceVectors.Length * Vector<TSource>.Count;
            }
        }

        // aggregate the remaining elements in the source
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        for (; indexSource < source.Length; indexSource++)
        {
            var currentAggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
            if (TResult.IsNaN(currentAggregate))
                return indexSource;

            if (!aggregate.Equals(currentAggregate))
            {
                aggregate = currentAggregate;
                indexOfAggregate = indexSource;
            }
        }

        return indexOfAggregate;
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
    /// <para>The transform operator is applied to the source elements before the aggregation operator.</para>
    /// <para>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns the index of the first element to which the transformation and aggregation results in NaN.</para>
    /// </remarks>
    public static int IndexOfAggregate<T, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, INumberBase<T>
        where TTransformOperator : struct, IBinaryOperator<T, T, T>
        where TAggregateOperator : struct, IAggregationOperator<T, T>
        => IndexOfAggregate<T, T, T, T, TTransformOperator, TAggregateOperator>(x, y);

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
    /// <para>The transform operator is applied to the source elements before the aggregation operator.</para>
    /// <para>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns the index of the first element to which the transformation and aggregation results in NaN.</para>
    /// </remarks>
    public static int IndexOfAggregate<T1, T2, TTransformed, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y)
        where T1 : struct
        where T2 : struct
        where TTransformed : struct
        where TResult : struct, INumberBase<TResult>
        where TTransformOperator : struct, IBinaryOperator<T1, T2, TTransformed>
        where TAggregateOperator : struct, IAggregationOperator<TTransformed, TResult>
    {
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(y), "source spans must have the same size.");

        if (x.IsEmpty)
            return -1;

        // initialize aggregate
        var aggregate = TAggregateOperator.Seed;
        var indexOfAggregate = -1;
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
            // convert source spans to vector span without copies
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var yVectors = MemoryMarshal.Cast<T2, Vector<T2>>(y);

            // check if there is more than one vector to aggregate
            if (xVectors.Length > 1)
            {
                // initialize aggregate vector
                var aggregateVector = new Vector<TResult>(TAggregateOperator.Seed);
                var aggregateIndicesVector = new Vector<TResult>(TResult.CreateChecked(-1));
                var indicesVector = VectorFactory<TResult>.Indices;
                var indicesIncrementVector = new Vector<TResult>(TResult.CreateChecked(Vector<TResult>.Count));

                // aggregate the source vectors into the aggregate vector
                ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
                ref var yVectorsRef = ref MemoryMarshal.GetReference(yVectors);
                for (var indexVector = 0; indexVector < xVectors.Length; indexVector++)
                {
                    var transformedVector = TTransformOperator.Invoke(ref Unsafe.Add(ref xVectorsRef, indexVector), ref Unsafe.Add(ref yVectorsRef, indexVector));
                    var currentVector = TAggregateOperator.Invoke(ref aggregateVector, ref transformedVector);
                    if (Vector.EqualsAll(currentVector, currentVector)) // check if vector contains NaN
                    {
                        var equalsVector = Vector.Equals(currentVector, aggregateVector);
                        aggregateVector = Vector.ConditionalSelect(equalsVector, aggregateVector, currentVector);
                        aggregateIndicesVector = Vector.ConditionalSelect(equalsVector, aggregateIndicesVector, indicesVector);

                        indicesVector += indicesIncrementVector;
                    }
                    else
                    {
                        for (var index = 0; index < Vector<TResult>.Count; index++)
                        {
                            if (TResult.IsNaN(currentVector[index]))
                                return (indexVector * Vector<TResult>.Count) + index;
                        }
                        Throw.Exception("Should not happen!");
                    }
                }

                // aggregate the aggregate vector into the aggregate
                for (var index = 0; index < Vector<TResult>.Count; index++)
                {
                    var current = aggregateVector[index];
                    var currentIndex = int.CreateChecked(aggregateIndicesVector[index]);
                    if (!TAggregateOperator.Invoke(aggregate, current).Equals(aggregate))
                    {
                        aggregate = current;
                        indexOfAggregate = currentIndex;
                    }
                    else if(aggregate.Equals(current) && indexOfAggregate > currentIndex)
                    {
                        indexOfAggregate = currentIndex;
                    } 
                }

                // skip the source elements already aggregated
                indexSource = xVectors.Length * Vector<T1>.Count;
            }

        }

        // aggregate the remaining elements in the source
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        for (; indexSource < x.Length; indexSource++)
        {
            var currentAggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
            if (TResult.IsNaN(currentAggregate))
                return indexSource;

            if (!aggregate.Equals(currentAggregate))
            {
                aggregate = currentAggregate;
                indexOfAggregate = indexSource;
            }
        }

        return indexOfAggregate;
    }

}

