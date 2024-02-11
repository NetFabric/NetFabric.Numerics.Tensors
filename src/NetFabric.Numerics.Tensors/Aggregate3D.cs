namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static ValueTuple<T, T, T> Aggregate3D<T, TOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TOperator : struct, IAggregationOperator<T, T>
        => Aggregate3D<T, T, T, IdentityOperator<T>, TOperator>(source);

    public static ValueTuple<TResult, TResult, TResult> Aggregate3D<T, TResult, TOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TResult : struct
        where TOperator : struct, IAggregationOperator<T, TResult>
        => Aggregate3D<T, T, TResult, IdentityOperator<T>, TOperator>(source);

    public static ValueTuple<TResult, TResult, TResult> Aggregate3D<T1, T2, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T1> source)
        where T1 : struct
        where T2 : struct
        where TResult : struct
        where TTransformOperator : struct, IUnaryOperator<T1, T2>
        where TAggregateOperator : struct, IAggregationOperator<T2, TResult>
    {
        if (source.Length % 3 is not 0)
            Throw.ArgumentException(nameof(source), "source span must have a size multiple of 3.");

        // initialize aggregate
        var aggregateX = TAggregateOperator.Seed;
        var aggregateY = TAggregateOperator.Seed;
        var aggregateZ = TAggregateOperator.Seed;
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
            if (sourceVectors.Length > 0)
            {
                // initialize aggregate vectors
                // use 3 vectors as 3 times the number of items in a vector is a multiple of 3
                var values = new TResult[Vector<TResult>.Count * 3];
                Array.Fill(values, TAggregateOperator.Seed);
                var resultValues = values.AsSpan();
                var resultVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(resultValues);

                // aggregate the source vectors into the aggregate vectors
                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                ref var resultVectorsRef = ref MemoryMarshal.GetReference(resultVectors);
                var indexVector = nint.Zero;
                for (; indexVector + 2 < sourceVectors.Length; indexVector += 3)
                {
                    var transformedVector0 = TTransformOperator.Invoke(ref Unsafe.Add(ref sourceVectorsRef, indexVector));
                    Unsafe.Add(ref resultVectorsRef, 0) = TAggregateOperator.Invoke(ref Unsafe.Add(ref resultVectorsRef, 0), ref transformedVector0);
                    var transformedVector1 = TTransformOperator.Invoke(ref Unsafe.Add(ref sourceVectorsRef, indexVector + 1));
                    Unsafe.Add(ref resultVectorsRef, 1) = TAggregateOperator.Invoke(ref Unsafe.Add(ref resultVectorsRef, 1), ref transformedVector1);
                    var transformedVector2 = TTransformOperator.Invoke(ref Unsafe.Add(ref sourceVectorsRef, indexVector + 2));
                    Unsafe.Add(ref resultVectorsRef, 2) = TAggregateOperator.Invoke(ref Unsafe.Add(ref resultVectorsRef, 2), ref transformedVector2);
                }

                // aggregate the aggregate vector into the aggregate
                ref var resultValuesRef = ref MemoryMarshal.GetReference(resultValues);
                for (var index = nint.Zero; index + 2 < Vector<TResult>.Count * 3; index += 3)
                {
                    aggregateX = TAggregateOperator.Invoke(aggregateX, Unsafe.Add(ref resultValuesRef, index));
                    aggregateY = TAggregateOperator.Invoke(aggregateY, Unsafe.Add(ref resultValuesRef, index + 1));
                    aggregateZ = TAggregateOperator.Invoke(aggregateZ, Unsafe.Add(ref resultValuesRef, index + 2));
                }

                // skip the source elements already aggregated
                indexSource = indexVector * Vector<T1>.Count;
            }
        }

        // aggregate the remaining elements in the source
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        for (; indexSource + 2 < source.Length; indexSource += 3)
        {
            aggregateX = TAggregateOperator.Invoke(aggregateX, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
            aggregateY = TAggregateOperator.Invoke(aggregateY, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1)));
            aggregateZ = TAggregateOperator.Invoke(aggregateZ, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 2)));
        }

        switch (source.Length - (int)indexSource)
        {
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }

        return (aggregateX, aggregateY, aggregateZ);
    }

    public static ValueTuple<TResult, TResult, TResult> Aggregate3D<T1, T2, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T1> y)
        where T1 : struct
        where T2 : struct
        where TResult : struct
        where TTransformOperator : struct, IBinaryOperator<T1, T1, T2>
        where TAggregateOperator : struct, IAggregationOperator<T2, TResult>
    {
        if (x.Length % 3 is not 0)
            Throw.ArgumentException(nameof(x), "source spans must have a size multiple of 3.");
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(y), "source spans must have the same size.");

        // initialize aggregate
        var aggregateX = TAggregateOperator.Seed;
        var aggregateY = TAggregateOperator.Seed;
        var aggregateZ = TAggregateOperator.Seed;
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
            if (xVectors.Length > 0)
            {
                // initialize aggregate vectors
                // use 3 vectors as 3 times the number of items in a vector is a multiple of 3
                var values = new TResult[Vector<TResult>.Count * 3];
                Array.Fill(values, TAggregateOperator.Seed);
                var resultValues = values.AsSpan();
                var resultVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(resultValues);

                // aggregate the source vectors into the aggregate vectors
                ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
                ref var yVectorsRef = ref MemoryMarshal.GetReference(yVectors);
                ref var resultVectorsRef = ref MemoryMarshal.GetReference(resultVectors);
                var indexVector = nint.Zero;
                for (; indexVector + 2 < xVectors.Length; indexVector += 3)
                {
                    var transformedVector0 = TTransformOperator.Invoke(ref Unsafe.Add(ref xVectorsRef, indexVector), ref Unsafe.Add(ref yVectorsRef, indexVector));
                    Unsafe.Add(ref resultVectorsRef, 0) = TAggregateOperator.Invoke(ref Unsafe.Add(ref resultVectorsRef, 0), ref transformedVector0);
                    var transformedVector1 = TTransformOperator.Invoke(ref Unsafe.Add(ref xVectorsRef, indexVector + 1), ref Unsafe.Add(ref yVectorsRef, indexVector + 1));
                    Unsafe.Add(ref resultVectorsRef, 1) = TAggregateOperator.Invoke(ref Unsafe.Add(ref resultVectorsRef, 1), ref transformedVector1);
                    var transformedVector2 = TTransformOperator.Invoke(ref Unsafe.Add(ref xVectorsRef, indexVector + 2), ref Unsafe.Add(ref yVectorsRef, indexVector + 2));
                    Unsafe.Add(ref resultVectorsRef, 2) = TAggregateOperator.Invoke(ref Unsafe.Add(ref resultVectorsRef, 2), ref transformedVector2);
                }

                // aggregate the aggregate vector into the aggregate
                ref var resultValuesRef = ref MemoryMarshal.GetReference(resultValues);
                for (var index = nint.Zero; index + 2 < Vector<TResult>.Count * 3; index += 3)
                {
                    aggregateX = TAggregateOperator.Invoke(aggregateX, Unsafe.Add(ref resultValuesRef, index));
                    aggregateY = TAggregateOperator.Invoke(aggregateY, Unsafe.Add(ref resultValuesRef, index + 1));
                    aggregateZ = TAggregateOperator.Invoke(aggregateZ, Unsafe.Add(ref resultValuesRef, index + 2));
                }

                // skip the source elements already aggregated
                indexSource = indexVector * Vector<T1>.Count;
            }
        }

        // aggregate the remaining elements in the source
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        for (; indexSource + 2 < x.Length; indexSource += 3)
        {
            aggregateX = TAggregateOperator.Invoke(aggregateX, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
            aggregateY = TAggregateOperator.Invoke(aggregateY, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1)));
            aggregateZ = TAggregateOperator.Invoke(aggregateZ, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2)));
        }

        switch (x.Length - (int)indexSource)
        {
            case 0:
                break;
            default:
                Throw.Exception("Should not happen!");
                break;
        }

        return (aggregateX, aggregateY, aggregateZ);
    }
}
