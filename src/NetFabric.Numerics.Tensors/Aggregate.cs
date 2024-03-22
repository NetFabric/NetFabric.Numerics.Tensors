namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    /// <summary>
    /// Aggregates the elements of a <see cref="ReadOnlySpan{T}"/> using the specified operator propagating NaN values.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source span that must implement the <see cref="INumberBase{T}"/> interface.</typeparam>
    /// <typeparam name="TAggregateOperator">The type of the aggregation operator that must implement the <see cref="IAggregationOperator{T, T}"/> interface.</typeparam>
    /// <param name="source">The span of elements to aggregate.</param>
    /// <returns>The result of the aggregation.</returns>
    /// <remarks>This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if any of the elements is NaN.</remarks>
    public static T Aggregate<T, TAggregateOperator>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>
        where TAggregateOperator : struct, IAggregationOperator<T, T>
    {
        // initialize aggregate
        var aggregate = TAggregateOperator.Seed;
        var indexSource = nint.Zero;

        // aggregate using hardware acceleration if available
        if (TAggregateOperator.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported)
        {
            // convert source span to vector span without copies
            var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);

            // check if there is at least one vector to aggregate
            if (sourceVectors.Length > 0)
            {
                // initialize aggregate vector
                var resultVector = new Vector<T>(TAggregateOperator.Seed);

                // aggregate the source vectors into the aggregate vector
                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                var indexVector = nint.Zero;
                for (; indexVector < sourceVectors.Length; indexVector++)
                {
                    var currentVector = Unsafe.Add(ref sourceVectorsRef, indexVector);
                    if (Vector.EqualsAll(currentVector, currentVector)) // check if vector contains NaN
                    {
                        resultVector = TAggregateOperator.Invoke(ref resultVector, ref currentVector);
                    }
                    else
                    {
                        for (var index = 0; index < Vector<T>.Count; index++)
                        {
                            var current = currentVector[index];
                            if (T.IsNaN(current))
                                return current;
                        }
                        Throw.Exception("Should not happen!");
                    }
                }

                // aggregate the aggregate vector into the aggregate
                for (var index = 0; index < Vector<T>.Count; index++)
                {
                    aggregate = TAggregateOperator.Invoke(aggregate, resultVector[index]);
                }

                // skip the source elements already aggregated
                indexSource = indexVector * Vector<T>.Count;
            }
        }

        // aggregate the remaining elements in the source
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        for (; indexSource < source.Length; indexSource++)
        {
            var current = Unsafe.Add(ref sourceRef, indexSource);
            if (T.IsNaN(current))
                return current;

            aggregate = TAggregateOperator.Invoke(aggregate, current);
        }

        return aggregate;
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
        // initialize aggregate
        var aggregate1 = TAggregateOperator1.Seed;
        var aggregate2 = TAggregateOperator2.Seed;
        var indexSource = nint.Zero;

        // aggregate using hardware acceleration if available
        if (TAggregateOperator1.IsVectorizable &&
            TAggregateOperator2.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported)
        {
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
                var indexVector = nint.Zero;
                for (; indexVector < sourceVectors.Length; indexVector++)
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
                indexSource = indexVector * Vector<T>.Count;
            }
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

