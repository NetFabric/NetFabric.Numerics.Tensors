namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static T AggregatePropagateNaN<T, TOperator>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>
        where TOperator : struct, IAggregationOperator<T, T>
    {
        // initialize aggregate
        var aggregate = TOperator.Seed;
        var indexSource = nint.Zero;

        // aggregate using hardware acceleration if available
        if (TOperator.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported)
        {
            // convert source span to vector span without copies
            var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);

            // check if there is at least one vector to aggregate
            if (sourceVectors.Length > 0)
            {
                // initialize aggregate vector
                var resultVector = new Vector<T>(TOperator.Seed);

                // aggregate the source vectors into the aggregate vector
                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                var indexVector = nint.Zero;
                for (; indexVector < sourceVectors.Length; indexVector++)
                {
                    var currentVector = Unsafe.Add(ref sourceVectorsRef, indexVector);
                    if (Vector.EqualsAll(currentVector, currentVector)) // check if vector contains NaN
                    {
                        resultVector = TOperator.Invoke(ref resultVector, ref Unsafe.Add(ref sourceVectorsRef, indexVector));
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
                    aggregate = TOperator.Invoke(aggregate, resultVector[index]);
                }

                // skip the source elements already aggregated
                indexSource = indexVector * Vector<T>.Count;
            }
        }

        // aggregate the remaining elements in the source
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        for (; (nuint)indexSource < (nuint)source.Length; indexSource++)
        {
            var current = Unsafe.Add(ref sourceRef, indexSource);
            if (T.IsNaN(current))
                return current;

            aggregate = TOperator.Invoke(aggregate, current);
        }

        return aggregate;
    }

    public static ValueTuple<T, T> AggregatePropagateNaN2<T, TOperator1, TOperator2>(ReadOnlySpan<T> source)
        where T : struct, INumber<T>
        where TOperator1 : struct, IAggregationOperator<T, T>
        where TOperator2 : struct, IAggregationOperator<T, T>
    {
        // initialize aggregate
        var aggregate1 = TOperator1.Seed;
        var aggregate2 = TOperator2.Seed;
        var indexSource = nint.Zero;

        // aggregate using hardware acceleration if available
        if (TOperator1.IsVectorizable &&
            TOperator2.IsVectorizable &&
            Vector.IsHardwareAccelerated &&
            Vector<T>.IsSupported)
        {
            // convert source span to vector span without copies
            var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);

            // check if there is at least one vector to aggregate
            if (sourceVectors.Length > 0)
            {
                // initialize aggregate vector
                var resultVector1 = new Vector<T>(TOperator1.Seed);
                var resultVector2 = new Vector<T>(TOperator2.Seed);

                // aggregate the source vectors into the aggregate vector
                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                var indexVector = nint.Zero;
                for (; indexVector < sourceVectors.Length; indexVector++)
                {
                    var currentVector = Unsafe.Add(ref sourceVectorsRef, indexVector);
                    if (Vector.EqualsAll(currentVector, currentVector)) // check if vector contains NaN
                    {
                        resultVector1 = TOperator1.Invoke(ref resultVector1, ref Unsafe.Add(ref sourceVectorsRef, indexVector));
                        resultVector2 = TOperator2.Invoke(ref resultVector2, ref Unsafe.Add(ref sourceVectorsRef, indexVector));
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
                    aggregate1 = TOperator1.Invoke(aggregate1, resultVector1[index]);
                    aggregate2 = TOperator2.Invoke(aggregate2, resultVector2[index]);
                }

                // skip the source elements already aggregated
                indexSource = indexVector * Vector<T>.Count;
            }
        }

        // aggregate the remaining elements in the source
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        for (; (nuint)indexSource < (nuint)source.Length; indexSource++)
        {
            var current = Unsafe.Add(ref sourceRef, indexSource);
            if (T.IsNaN(current))
                return (current, current);

            aggregate1 = TOperator1.Invoke(aggregate1, current);
            aggregate2 = TOperator2.Invoke(aggregate2, current);
        }

        return (aggregate1, aggregate2);
    }

}

