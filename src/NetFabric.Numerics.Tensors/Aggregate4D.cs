namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static ValueTuple<T, T, T, T> Aggregate4D<T, TOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TOperator : struct, IAggregationOperator<T, T>
        => Aggregate4D<T, T, TOperator>(source);

    public static ValueTuple<TResult, TResult, TResult, TResult> Aggregate4D<TSource, TResult, TOperator>(ReadOnlySpan<TSource> source)
        where TSource : struct
        where TResult : struct
        where TOperator : struct, IAggregationOperator<TSource, TResult>
    {
        if (source.Length % 4 is not 0)
            Throw.ArgumentException(nameof(source), "source span must have a size multiple of 4.");

        // initialize aggregate
        var aggregateX = TOperator.Identity;
        var aggregateY = TOperator.Identity;
        var aggregateZ = TOperator.Identity;
        var aggregateW = TOperator.Identity;
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
            if (Vector<TSource>.Count % 4 is 0 && sourceVectors.Length is >=2)
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
                for (var index = 0; index + 3 < Vector<TResult>.Count; index += 4)
                {
                    aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref resultVectorRef, index));
                    aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref resultVectorRef, index + 1));
                    aggregateZ = TOperator.Invoke(aggregateZ, Unsafe.Add(ref resultVectorRef, index + 2));
                    aggregateW = TOperator.Invoke(aggregateW, Unsafe.Add(ref resultVectorRef, index + 3));
                }

                // skip the source elements already aggregated
                sourceIndex = source.Length - (source.Length % Vector<TSource>.Count);
            }
            else if (Vector<TSource>.Count is 2 && sourceVectors.Length is >=4)
            {
                // initialize aggregate vectors
                // use 2 vectors as it will fit exactly 4 values
                var values = new TResult[4];
                Array.Fill(values, TOperator.Identity);
                var resultValues = values.AsSpan();
                var resultVectors = MemoryMarshal.Cast<TResult, Vector<TResult>>(resultValues);

                // aggregate the source vectors into the aggregate vectors
                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                ref var resultVectorsRef = ref MemoryMarshal.GetReference(resultVectors);
                for (var index = nint.Zero; index + 1 < sourceVectors.Length; index += 2)
                {
                    Unsafe.Add(ref resultVectorsRef, 0) =  TOperator.Invoke(ref Unsafe.Add(ref resultVectorsRef, 0), ref Unsafe.Add(ref sourceVectorsRef, index));
                    Unsafe.Add(ref resultVectorsRef, 1) =  TOperator.Invoke(ref Unsafe.Add(ref resultVectorsRef, 1), ref Unsafe.Add(ref sourceVectorsRef, index + 1));
                }

                // aggregate the values from the aggregate vectors into the aggregates
                ref var resultValuesRef = ref MemoryMarshal.GetReference(resultValues);
                aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref resultValuesRef, 0));
                aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref resultValuesRef, 1));
                aggregateZ = TOperator.Invoke(aggregateZ, Unsafe.Add(ref resultValuesRef, 2));
                aggregateW = TOperator.Invoke(aggregateW, Unsafe.Add(ref resultValuesRef, 3));

                // skip the source elements already aggregated
                sourceIndex = source.Length - (source.Length % (Vector<TSource>.Count * 2));
            }
        }

        // aggregate the remaining elements in the source
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        for (; sourceIndex + 3 < source.Length; sourceIndex += 4)
        {
            aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref sourceRef, sourceIndex));
            aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref sourceRef, sourceIndex + 1));
            aggregateZ = TOperator.Invoke(aggregateZ, Unsafe.Add(ref sourceRef, sourceIndex + 2));
            aggregateW = TOperator.Invoke(aggregateW, Unsafe.Add(ref sourceRef, sourceIndex + 3));
        }

        return (aggregateX, aggregateY, aggregateZ, aggregateW);
    }
}