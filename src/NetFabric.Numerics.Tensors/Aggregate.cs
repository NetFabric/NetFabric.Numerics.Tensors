namespace NetFabric.Numerics;

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
        var aggregate = TAggregateOperator.Identity;
        var sourceIndex = nint.Zero;

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

            // check if there are multiple vectors to aggregate
            if (sourceVectors.Length > 1)
            {
                // initialize aggregate vector
                var resultVector = new Vector<TResult>(TAggregateOperator.Identity);

                // aggregate the source vectors into the aggregate vector
                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                for (var indexVector = nint.Zero; indexVector < sourceVectors.Length; indexVector++)
                {
                    resultVector = TAggregateOperator.Invoke(ref resultVector, TTransformOperator.Invoke(ref Unsafe.Add(ref sourceVectorsRef, indexVector)));
                }

                // aggregate the aggregate vector into the aggregate
                aggregate = TAggregateOperator.Invoke(aggregate, ref resultVector);

                // skip the source elements already aggregated
                sourceIndex = source.Length - (source.Length % Vector<T1>.Count);
            }
        }

        // aggregate the remaining elements in the source
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        var remaining = source.Length - (int)sourceIndex;
        if (remaining >= 4)
        {
            var partial1 = TAggregateOperator.Identity;
            var partial2 = TAggregateOperator.Identity;
            var partial3 = TAggregateOperator.Identity;
            for (; sourceIndex + 3 < source.Length; sourceIndex += 4)
            {
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, sourceIndex)));
                partial1 = TAggregateOperator.Invoke(partial1, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, sourceIndex + 1)));
                partial2 = TAggregateOperator.Invoke(partial2, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, sourceIndex + 2)));
                partial3 = TAggregateOperator.Invoke(partial3, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, sourceIndex + 3)));
            }
            aggregate = TAggregateOperator.Invoke(aggregate, partial1);
            aggregate = TAggregateOperator.Invoke(aggregate, partial2);
            aggregate = TAggregateOperator.Invoke(aggregate, partial3);
            remaining = source.Length - (int)sourceIndex;
        }

        switch(remaining)
        {
            case 3:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, sourceIndex)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, sourceIndex + 1)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, sourceIndex + 2)));
                break;
            case 2:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, sourceIndex)));
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, sourceIndex + 1)));
                break;
            case 1:
                aggregate = TAggregateOperator.Invoke(aggregate, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, sourceIndex)));
                break;
        }

        return aggregate;
    }
}

