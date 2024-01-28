namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static ValueTuple<T, T> Aggregate2D<T, TOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TOperator : struct, IAggregationOperator<T, T>
        => Aggregate2D<T, T, TOperator>(source);

    public static ValueTuple<TResult, TResult> Aggregate2D<TSource, TResult, TOperator>(ReadOnlySpan<TSource> source)
        where TSource : struct
        where TResult : struct
        where TOperator : struct, IAggregationOperator<TSource, TResult>
    {
        if (source.Length % 2 is not 0)
            Throw.ArgumentException(nameof(source), "source span must have a size multiple of 2.");

        // initialize aggregate
        var aggregateX = TOperator.Identity;
        var aggregateY = TOperator.Identity;
        var sourceIndex = nint.Zero;

        // aggregate using hardware acceleration if available
        if (TOperator.IsVectorizable && 
            Vector.IsHardwareAccelerated && 
            Vector<TSource>.IsSupported &&
            Vector<TResult>.IsSupported &&
            Vector<TSource>.Count is >=2)
        {
            // convert source span to vector span without copies
            var sourceVectors = MemoryMarshal.Cast<TSource, Vector<TSource>>(source);

            // check if there are multiple vectors to aggregate
            if (sourceVectors.Length is >=2)
            {
                // initialize aggregate vector
                var resultVector = new Vector<TResult>(TOperator.Identity);

                // aggregate the source vectors into the aggregate vector
                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                for (var index = nint.Zero; index < sourceVectors.Length; index++)
                {
                    resultVector = TOperator.Invoke(ref resultVector, ref Unsafe.Add(ref sourceVectorsRef, index));
                }

                // aggregate the aggregate vector into the aggregate
                ref var resultVectorRef = ref Unsafe.As<Vector<TResult>, TResult>(ref Unsafe.AsRef(in resultVector));
                for (var index = 0; index + 1 < Vector<TResult>.Count; index += 2)
                {
                    aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref resultVectorRef, index));
                    aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref resultVectorRef, index + 1));
                }

                // skip the source elements already aggregated
                sourceIndex = source.Length - (source.Length % Vector<TSource>.Count);
            }
        }

        // aggregate the remaining elements in the source
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        var remaining = source.Length;
        if (remaining is >=4)
        {
            var partialX1 = TOperator.Identity;
            var partialY1 = TOperator.Identity;
            for (; sourceIndex + 3 < source.Length; sourceIndex += 4)
            {
                aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref sourceRef, sourceIndex));
                aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref sourceRef, sourceIndex + 1));
                partialX1 = TOperator.Invoke(partialX1, Unsafe.Add(ref sourceRef, sourceIndex + 2));
                partialY1 = TOperator.Invoke(partialY1, Unsafe.Add(ref sourceRef, sourceIndex + 3));
            }
            aggregateX = TOperator.Invoke(aggregateX, partialX1);
            aggregateY = TOperator.Invoke(aggregateY, partialY1);
            remaining = source.Length - (int)sourceIndex;
        }

        switch(remaining)
        {
            case 2:
                aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref sourceRef, sourceIndex));
                aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref sourceRef, sourceIndex + 1));
                break;
        }


        return (aggregateX, aggregateY);
    }
}