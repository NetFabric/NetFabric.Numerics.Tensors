namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static T Aggregate<T, TOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TOperator : struct, IAggregationOperator<T, T>
        => Aggregate<T, T, T, IdentityOperator<T>, TOperator>(source);

    public static TResult Aggregate<T, TResult, TOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TResult : struct
        where TOperator : struct, IAggregationOperator<T, TResult>
        => Aggregate<T, T, TResult, IdentityOperator<T>, TOperator>(source);

    public static TResult Aggregate<T1, T2, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T1> source)
        where T1 : struct
        where T2 : struct
        where TResult : struct
        where TTransformOperator : struct, IUnaryOperator<T1, T2>
        where TAggregateOperator : struct, IAggregationOperator<T2, TResult>
    {
        // initialize aggregate
        var aggregate = TAggregateOperator.Seed;
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
                for(var index = 0; index < Vector<TResult>.Count; index++)
                {
                    aggregate = TAggregateOperator.Invoke(aggregate, resultVector[index]);
                }

                // skip the source elements already aggregated
                indexSource = indexVector * Vector<T1>.Count;
            }
        }

        // aggregate the remaining elements in the source
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        var remaining = source.Length - (int)indexSource;
        if (remaining >= 4)
        {
            var partial1 = TAggregateOperator.Seed;
            var partial2 = TAggregateOperator.Seed;
            var partial3 = TAggregateOperator.Seed;
            for (; indexSource + 3 < source.Length; indexSource += 4)
            {
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
                partial1 = TAggregateOperator.Invoke(partial1, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1)));
                partial2 = TAggregateOperator.Invoke(partial2, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 2)));
                partial3 = TAggregateOperator.Invoke(partial3, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 3)));
            }
            aggregate = TAggregateOperator.Invoke(aggregate, partial1);
            aggregate = TAggregateOperator.Invoke(aggregate, partial2);
            aggregate = TAggregateOperator.Invoke(aggregate, partial3);
            remaining = source.Length - (int)indexSource;
        }

        switch(remaining)
        {
            case 3:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 2)));
                goto case 2;
            case 2:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1)));
                goto case 1;
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

    public static TResult Aggregate<T1, T2, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T1> y)
        where T1 : struct
        where T2 : struct
        where TResult : struct
        where TTransformOperator : struct, IBinaryOperator<T1, T1, T2>
        where TAggregateOperator : struct, IAggregationOperator<T2, TResult>
    {
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(y), "source spans must have the same size.");

        // initialize aggregate
        var aggregate = TAggregateOperator.Seed;
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
                for(var index = 0; index < Vector<TResult>.Count; index++)
                {
                    aggregate = TAggregateOperator.Invoke(aggregate, resultVector[index]);
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
            var partial1 = TAggregateOperator.Seed;
            var partial2 = TAggregateOperator.Seed;
            var partial3 = TAggregateOperator.Seed;
            for (; indexSource + 3 < x.Length; indexSource += 4)
            {
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
                partial1 = TAggregateOperator.Invoke(partial1, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1)));
                partial2 = TAggregateOperator.Invoke(partial2, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2)));
                partial3 = TAggregateOperator.Invoke(partial3, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), Unsafe.Add(ref yRef, indexSource + 3)));
            }
            aggregate = TAggregateOperator.Invoke(aggregate, partial1);
            aggregate = TAggregateOperator.Invoke(aggregate, partial2);
            aggregate = TAggregateOperator.Invoke(aggregate, partial3);
            remaining = x.Length - (int)indexSource;
        }

        switch(remaining)
        {
            case 3:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2)));
                goto case 2;
            case 2:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1)));
                goto case 1;
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

