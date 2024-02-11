namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static ValueTuple<T, T, T, T> Aggregate4D<T, TOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TOperator : struct, IAggregationOperator<T, T>
        => Aggregate4D<T, T, T, IdentityOperator<T>, TOperator>(source);

    public static ValueTuple<TResult, TResult, TResult, TResult> Aggregate4D<T, TResult, TOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TResult : struct
        where TOperator : struct, IAggregationOperator<T, TResult>
        => Aggregate4D<T, T, TResult, IdentityOperator<T>, TOperator>(source);

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
            if (Vector<T1>.Count % 4 is 0 && sourceVectors.Length > 0)
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
                for (var index = 0; index + 3 < Vector<TResult>.Count; index += 4)
                {
                    aggregateX = TAggregateOperator.Invoke(aggregateX, resultVector[index]);
                    aggregateY = TAggregateOperator.Invoke(aggregateY, resultVector[index + 1]);
                    aggregateZ = TAggregateOperator.Invoke(aggregateZ, resultVector[index + 2]);
                    aggregateW = TAggregateOperator.Invoke(aggregateW, resultVector[index + 3]);
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

    public static ValueTuple<TResult, TResult, TResult, TResult> Aggregate4D<T1, T2, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T1> y)
        where T1 : struct
        where T2 : struct
        where TResult : struct
        where TTransformOperator : struct, IBinaryOperator<T1, T1, T2>
        where TAggregateOperator : struct, IAggregationOperator<T2, TResult>
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
            Vector<T1>.IsSupported &&
            Vector<T2>.IsSupported &&
            Vector<TResult>.IsSupported)
        {
            // convert source span to vector span without copies
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var yVectors = MemoryMarshal.Cast<T1, Vector<T1>>(y);

            // check if there is at least one vector to aggregate
            if (Vector<T1>.Count % 4 is 0 && xVectors.Length > 0)
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
                for (var index = 0; index + 3 < Vector<TResult>.Count; index += 4)
                {
                    aggregateX = TAggregateOperator.Invoke(aggregateX, resultVector[index]);
                    aggregateY = TAggregateOperator.Invoke(aggregateY, resultVector[index + 1]);
                    aggregateZ = TAggregateOperator.Invoke(aggregateZ, resultVector[index + 2]);
                    aggregateW = TAggregateOperator.Invoke(aggregateW, resultVector[index + 3]);
                }

                // skip the source elements already aggregated
                indexSource = indexVector * Vector<T1>.Count;
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
