namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static ValueTuple<T, T, T> Aggregate3D<T, TOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TOperator : struct, IAggregationOperator<T, T>
        => Aggregate3D<T, T, TOperator>(source);

    public static ValueTuple<TResult, TResult, TResult> Aggregate3D<TSource, TResult, TOperator>(ReadOnlySpan<TSource> source)
        where TSource : struct
        where TResult : struct
        where TOperator : struct, IAggregationOperator<TSource, TResult>
    {
        if (source.Length % 3 is not 0)
            Throw.ArgumentException(nameof(source), "source span must have a size multiple of 3.");

        // initialize aggregate
        var aggregateX = TOperator.Identity;
        var aggregateY = TOperator.Identity;
        var aggregateZ = TOperator.Identity;
        var sourceIndex = nint.Zero;

        // aggregate using hardware acceleration if available
        if (TOperator.IsVectorizable && 
            Vector.IsHardwareAccelerated && 
            Vector<TSource>.IsSupported &&
            Vector<TResult>.IsSupported)
        {
            // convert source span to vector span without copies
            var sourceVectors = MemoryMarshal.Cast<TSource, Vector<TSource>>(source);

            // check if there are multiple vectors to aggregate
            if (sourceVectors.Length is >=6)
            {
                // initialize aggregate vectors
                // use 3 vectors as 3 times the number of items in a vector is a multiple of 3
                var values = new TResult[Vector<TResult>.Count * 3];
                Array.Fill(values, TOperator.Identity);
                var resultValues = values.AsSpan();
                var resultVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(resultValues);

                // aggregate the source vectors into the aggregate vectors
                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                ref var resultVectorsRef = ref MemoryMarshal.GetReference(resultVectors);
                for (var index = nint.Zero; index + 2 < sourceVectors.Length; index += 3)
                {
                    Unsafe.Add(ref resultVectorsRef, 0) =  TOperator.Invoke(ref Unsafe.Add(ref resultVectorsRef, 0), ref Unsafe.Add(ref sourceVectorsRef, index));
                    Unsafe.Add(ref resultVectorsRef, 1) =  TOperator.Invoke(ref Unsafe.Add(ref resultVectorsRef, 1), ref Unsafe.Add(ref sourceVectorsRef, index + 1));
                    Unsafe.Add(ref resultVectorsRef, 2) =  TOperator.Invoke(ref Unsafe.Add(ref resultVectorsRef, 2), ref Unsafe.Add(ref sourceVectorsRef, index + 2));
                }

                // aggregate the values from the aggregate vectors into the aggregates
                ref var resultValuesRef = ref MemoryMarshal.GetReference(resultValues);
                for(var index = nint.Zero; index + 2 < Vector<TResult>.Count * 3; index += 3)
                {
                    aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref resultValuesRef, index));
                    aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref resultValuesRef, index + 1));
                    aggregateZ = TOperator.Invoke(aggregateZ, Unsafe.Add(ref resultValuesRef, index + 2));
                }

                // skip the source elements already aggregated
                sourceIndex = source.Length - (source.Length % (Vector<TSource>.Count * 3));
            }
        }

        // aggregate the remaining elements in the source
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        for (; sourceIndex + 2 < source.Length; sourceIndex += 3)
        {
            aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref sourceRef, sourceIndex));
            aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref sourceRef, sourceIndex + 1));
            aggregateZ = TOperator.Invoke(aggregateZ, Unsafe.Add(ref sourceRef, sourceIndex + 2));
        }

        return (aggregateX, aggregateY, aggregateZ);
    }
}