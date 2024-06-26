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
    /// <remarks>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if the transformation and aggregation of any of the elements result in NaN.</remarks>
    public static T Aggregate<T, TAggregateOperator>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>
        where TAggregateOperator : struct, IAggregationOperator<T, T>
        => Aggregate<T, IdentityOperator<T>, TAggregateOperator>(source);

    /// <summary>
    /// Aggregates the elements of a <see cref="ReadOnlySpan{T}"/> using the specified aggregation operator.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span.</typeparam>
    /// <typeparam name="TTransformOperator">The type of the transform operator that must implement the <see cref="IUnaryOperator{TSource, TTransformed}"/> interface.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{T, T}"/> interface.</typeparam>
    /// <param name="source">The span of elements to aggregate.</param>
    /// <returns>The result of the aggregation.</returns>
    /// <remarks>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if the transformation and aggregation of any of the elements result in NaN.</remarks>
    public static T Aggregate<T, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>
        where TTransformOperator : struct, IUnaryOperator<T, T>
        where TAggregateOperator : struct, IAggregationOperator<T, T>
        => Aggregate<T, T, T, TTransformOperator, TAggregateOperator>(source);

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
    ///     <para>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if the transformation and aggregation of any of the elements result in NaN.</para>
    /// </remarks>
    public static TResult Aggregate<TSource, TTransformed, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<TSource> source)
        where TSource : struct
        where TTransformed : struct
        where TResult : struct, INumberBase<TResult>
        where TTransformOperator : struct, IUnaryOperator<TSource, TTransformed>
        where TAggregateOperator : struct, IAggregationOperator<TTransformed, TResult>
    {
        return (TTransformOperator.IsVectorizable &&
                TAggregateOperator.IsVectorizable &&
                Vector.IsHardwareAccelerated &&
                Vector<TSource>.IsSupported &&
                Vector<TTransformed>.IsSupported &&
                Vector<TResult>.IsSupported)
            ? VectorAggregate(source)
            : ScalarAggregate(source);

        static TResult ScalarAggregate(ReadOnlySpan<TSource> source)
        {
            var aggregate = TAggregateOperator.Seed;
            foreach (var item in source)
            {
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(item));
                if (TResult.IsNaN(aggregate))
                    return aggregate;
            }
            return aggregate;
        }

        static TResult VectorAggregate(ReadOnlySpan<TSource> source)
        {
            var aggregate = TAggregateOperator.Seed;
            var indexSource = 0;

            // convert source span to vector span without copies
            var sourceVectors = MemoryMarshal.Cast<TSource, Vector<TSource>>(source);

            // check if there is at least one vector to aggregate
            if (sourceVectors.Length > 0)
            {
                // initialize aggregate vector
                var aggregateVector = new Vector<TResult>(TAggregateOperator.Seed);

                // aggregate the source vectors into the aggregate vector
                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                for (var indexVector = 0; indexVector < sourceVectors.Length; indexVector++)
                {
                    var transformedVector = TTransformOperator.Invoke(ref Unsafe.Add(ref sourceVectorsRef, indexVector));
                    aggregateVector = TAggregateOperator.Invoke(ref aggregateVector, ref transformedVector);
                    if (!Vector.EqualsAll(aggregateVector, aggregateVector)) // check if vector contains NaN
                    {
                        for (var index = 0; index < Vector<TResult>.Count; index++)
                        {
                            var current = aggregateVector[index];
                            if (TResult.IsNaN(current))
                                return current;
                        }
                        Throw.Exception("Should not happen!");
                    }
                }

                // aggregate the aggregate vector into the aggregate
                aggregate = TAggregateOperator.Invoke(aggregate, ref aggregateVector);

                // skip the source elements already aggregated
                indexSource = sourceVectors.Length * Vector<TResult>.Count;
            }

            // aggregate the remaining elements in the source
            ref var sourceRef = ref MemoryMarshal.GetReference(source);
            for (; indexSource < source.Length; indexSource++)
            {
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
                if (TResult.IsNaN(aggregate))
                    return aggregate;
            }

            return aggregate;
        }
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
    public static T Aggregate<T, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, INumberBase<T>
        where TTransformOperator : struct, IBinaryOperator<T, T, T>
        where TAggregateOperator : struct, IAggregationOperator<T, T>
        => Aggregate<T, T, T, T, TTransformOperator, TAggregateOperator>(x, y);

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
    ///     <para>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if the transformation and aggregation of any of the elements result in NaN.</para>
    /// </remarks>
    public static TResult Aggregate<T1, T2, TTransformed, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y)
        where T1 : struct
        where T2 : struct
        where TTransformed : struct
        where TResult : struct, INumberBase<TResult>
        where TTransformOperator : struct, IBinaryOperator<T1, T2, TTransformed>
        where TAggregateOperator : struct, IAggregationOperator<TTransformed, TResult>
    {
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(y), "source spans must have the same size.");
            
        return (TTransformOperator.IsVectorizable &&
                TAggregateOperator.IsVectorizable &&
                Vector.IsHardwareAccelerated &&
                Vector<T1>.IsSupported &&
                Vector<T2>.IsSupported &&
                Vector<TTransformed>.IsSupported &&
                Vector<TResult>.IsSupported)
            ? VectorAggregate(x, y)
            : ScalarAggregate(x, y);

        static TResult ScalarAggregate(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y)
        {
            var aggregate = TAggregateOperator.Seed;
            for (var index = 0; index < x.Length && index < y.Length; index++)
            {
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(x[index], y[index]));
                if (TResult.IsNaN(aggregate))
                    return aggregate;
            }
            return aggregate;
        }

        static TResult VectorAggregate(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y)
        {
            var aggregate = TAggregateOperator.Seed;
            var indexSource = 0;

            // convert source span to vector span without copies
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var yVectors = MemoryMarshal.Cast<T2, Vector<T2>>(y);

            // check if there is at least one vector to aggregate
            if (xVectors.Length > 0)
            {
                // initialize aggregate vector
                var aggregateVector = new Vector<TResult>(TAggregateOperator.Seed);

                // aggregate the source vectors into the aggregate vector
                ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
                ref var yVectorsRef = ref MemoryMarshal.GetReference(yVectors);
                for (var indexVector = 0; indexVector < xVectors.Length; indexVector++)
                {
                    var transformedVector = TTransformOperator.Invoke(ref Unsafe.Add(ref xVectorsRef, indexVector), ref Unsafe.Add(ref yVectorsRef, indexVector));
                    aggregateVector = TAggregateOperator.Invoke(ref aggregateVector, ref transformedVector);
                    if (!Vector.EqualsAll(aggregateVector, aggregateVector)) // check if vector contains NaN
                    {
                        for (var index = 0; index < Vector<TResult>.Count; index++)
                        {
                            var current = aggregateVector[index];
                            if (TResult.IsNaN(current))
                                return current;
                        }
                        Throw.Exception("Should not happen!");
                    }
                }

                // aggregate the aggregate vector into the aggregate
                aggregate = TAggregateOperator.Invoke(aggregate, ref aggregateVector);

                // skip the source elements already aggregated
                indexSource = xVectors.Length * Vector<TResult>.Count;
            }

            // aggregate the remaining elements in the source
            ref var xRef = ref MemoryMarshal.GetReference(x);
            ref var yRef = ref MemoryMarshal.GetReference(y);
            for (; indexSource < x.Length; indexSource++)
            {
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
                if (TResult.IsNaN(aggregate))
                    return aggregate;
            }

            return aggregate;
        }
    }

    /// <summary>
    /// Aggregates the elements of a <see cref="ReadOnlySpan{T}"/> using the specified two operators propagating NaN values.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span that must implement the <see cref="INumberBase{T}"/> interface.</typeparam>
    /// <typeparam name="TAggregateOperator1">The type of the first aggregation operator.</typeparam>
    /// <typeparam name="TAggregateOperator2">The type of the second aggregation operator.</typeparam>
    /// <param name="source">The span of elements to aggregate.</param>
    /// <returns>A tuple containing the two aggregated results.</returns>
    /// <remarks>
    /// The two operators are applied in parallel to the source elements, allowing two operations to be performed on a single pass of the source elements.
    /// If any of the elements is NaN, the result is NaN.
    /// </remarks>
    public static (T, T) Aggregate2<T, TAggregateOperator1, TAggregateOperator2>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>
        where TAggregateOperator1 : struct, IAggregationOperator<T, T>
        where TAggregateOperator2 : struct, IAggregationOperator<T, T>
    {
        return (TAggregateOperator1.IsVectorizable &&
                TAggregateOperator2.IsVectorizable &&
                Vector.IsHardwareAccelerated &&
                Vector<T>.IsSupported)
            ? VectorAggregate(source)
            : ScalarAggregate(source);

        static (T, T) ScalarAggregate(ReadOnlySpan<T> source)
        {
            var aggregate1 = TAggregateOperator1.Seed;
            var aggregate2 = TAggregateOperator2.Seed;
            foreach (var item in source)
            {
                if (T.IsNaN(item))
                    return (item, item);

                aggregate1 = TAggregateOperator1.Invoke(aggregate1, item);
                aggregate2 = TAggregateOperator2.Invoke(aggregate2, item);
            }
            return (aggregate1, aggregate2);
        }

        static (T, T) VectorAggregate(ReadOnlySpan<T> source)
        {
            var aggregate1 = TAggregateOperator1.Seed;
            var aggregate2 = TAggregateOperator2.Seed;
            var indexSource = 0;

            // convert source span to vector span without copies
            var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);

            // check if there is at least one vector to aggregate
            if (sourceVectors.Length > 0)
            {
                // initialize aggregate vector
                var resultVector1 = new Vector<T>(TAggregateOperator1.Seed);
                var resultVector2 = new Vector<T>(TAggregateOperator2.Seed);

                // aggregate the source vectors into the aggregate vector
                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                for (var indexVector = 0; indexVector < sourceVectors.Length; indexVector++)
                {
                    var currentVector = Unsafe.Add(ref sourceVectorsRef, indexVector);
                    if (Vector.EqualsAll(currentVector, currentVector)) // check if vector contains NaN
                    {
                        resultVector1 = TAggregateOperator1.Invoke(ref resultVector1, ref currentVector);
                        resultVector2 = TAggregateOperator2.Invoke(ref resultVector2, ref currentVector);
                    }
                    else
                    {
                        for (var index = 0; index < Vector<T>.Count; index++)
                        {
                            var current = currentVector[index];
                            if (T.IsNaN(current))
                                return (current, current);
                        }
                        Throw.Exception("Should not happen!");
                    }
                }

                // aggregate the aggregate vector into the aggregate
                for (var index = 0; index < Vector<T>.Count; index++)
                {
                    aggregate1 = TAggregateOperator1.Invoke(aggregate1, resultVector1[index]);
                    aggregate2 = TAggregateOperator2.Invoke(aggregate2, resultVector2[index]);
                }

                // skip the source elements already aggregated
                indexSource = sourceVectors.Length * Vector<T>.Count;
            }

            // aggregate the remaining elements in the source
            ref var sourceRef = ref MemoryMarshal.GetReference(source);
            for (; indexSource < source.Length; indexSource++)
            {
                var current = Unsafe.Add(ref sourceRef, indexSource);
                if (T.IsNaN(current))
                    return (current, current);

                aggregate1 = TAggregateOperator1.Invoke(aggregate1, current);
                aggregate2 = TAggregateOperator2.Invoke(aggregate2, current);
            }

            return (aggregate1, aggregate2);
        }
    }

}

