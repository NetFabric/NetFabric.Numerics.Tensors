namespace NetFabric.Numerics
{
    public static partial class Tensor
    {
        public static ReadOnlySpan<T> AggregateTuples<T, TOperator>(ReadOnlySpan<T> source, int tupleSize)
            where T : struct
            where TOperator : struct, IAggregationTuplesOperator<T>
        {
            if (tupleSize < 2)
                Throw.ArgumentException(nameof(tupleSize), "tupleSize must be greater than 1.");
            if (source.Length % tupleSize is not 0)
                Throw.ArgumentException(nameof(source), "source span must have a size multiple of tupleSize.");

            return tupleSize.IsPowerOfTwo() 
                ? AggregateTuplesPowerOfTwo(source, tupleSize)
                : AggregateTuplesNonPowerOfTwo(source, tupleSize);

            static ReadOnlySpan<T> AggregateTuplesPowerOfTwo(ReadOnlySpan<T> source, int tupleSize)
            {
                return Array.Empty<T>();
            } 

            static ReadOnlySpan<T> AggregateTuplesNonPowerOfTwo(ReadOnlySpan<T> source, int tupleSize)
            {
                ref var sourceRef = ref MemoryMarshal.GetReference(source);

                // initialize result
                var result = new T[tupleSize];
                Array.Fill(result, TOperator.Identity);

                ref var resultRef = ref MemoryMarshal.GetReference<T>(result);
                if (Vector.IsHardwareAccelerated && Vector<T>.IsSupported)
                {
                    nint index = 0;

                    // use as many vectors as the number of elements in the tuple
                    // this guarantees alignment and allows to use the same code for all tuple sizes
                    // but only used these if source fills more than the number of elements in the tuple
                    // and the number of vectors filled is a multiple of the number of elements in the tuple
                    var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);
                    if (sourceVectors.Length > tupleSize && 
                        sourceVectors.Length % tupleSize is 0)
                    {
                        ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);

                        var resultVectors = GetVectors<T>(tupleSize, TOperator.Identity);
                        ref var resultVectorsRef = ref MemoryMarshal.GetReference(resultVectors);

                        // aggregate the source vectors into the result vectors
                        for (nint indexVector = 0; indexVector + tupleSize - 1 < sourceVectors.Length; indexVector += tupleSize)
                        {
                            for (nint indexTuple = 0; indexTuple < tupleSize; indexTuple++)
                            {
                                Unsafe.Add(ref resultVectorsRef, indexTuple) = 
                                    TOperator.Invoke(
                                        Unsafe.Add(ref resultVectorsRef, indexTuple), 
                                        Unsafe.Add(ref sourceVectorsRef, indexVector + indexTuple));
                            }
                        }

                        // aggregate the result vectors into the result
                        nint indexResult = 0;
                        for(var indexVector = 0; indexVector < Vector<T>.Count; indexVector++)
                        {
                            var resultVector = Unsafe.Add(ref resultVectorsRef, indexVector);
                            for(var indexTuple = 0; indexTuple < tupleSize; indexTuple++)
                            {
                                Unsafe.Add(ref resultRef, indexResult) = TOperator.Invoke(Unsafe.Add(ref resultRef, indexResult), resultVector[indexVector]);
                                indexResult++;
                                if(indexResult == tupleSize)
                                    indexResult = 0;
                            }
                        }

                        // skip the source vectors already aggregated
                        index = source.Length - (source.Length % Vector<T>.Count);
                    }

                    // aggregate the remaining elements in the source
                    for (; index + tupleSize - 1 < source.Length; index += tupleSize)
                    {
                        for (nint indexResult = 0; indexResult < tupleSize; indexResult++)
                        {
                            Unsafe.Add(ref resultRef, indexResult) = TOperator.Invoke(Unsafe.Add(ref resultRef, indexResult), Unsafe.Add(ref sourceRef, index + indexResult));
                        }
                    }
                }
                else
                {
                    // aggregate the elements in the source
                    for (nint index = 0; index + tupleSize - 1 < source.Length; index += tupleSize)
                    {
                        for (nint indexResult = 0; indexResult < tupleSize; indexResult++)
                        {
                            Unsafe.Add(ref resultRef, indexResult) = TOperator.Invoke(Unsafe.Add(ref resultRef, indexResult), Unsafe.Add(ref sourceRef, index + indexResult));
                        }
                    }
                }

                return result;
            }
        }
    }
}