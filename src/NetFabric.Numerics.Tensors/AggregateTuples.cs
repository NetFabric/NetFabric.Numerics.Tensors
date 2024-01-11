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

            // initialize result
            var result = new T[tupleSize];
            Array.Fill(result, TOperator.Identity);

            // aggregate
            if (Vector.IsHardwareAccelerated && Vector<T>.IsSupported)
            {
                if (tupleSize.IsPowerOfTwo())
                    IntrinsicPowerOfTwo(source, result);
                else
                    IntrinsicNonPowerOfTwo(source, result);
            }
            else
            {
                Scalar(source, result);
            }

            return result;

            static void Scalar(ReadOnlySpan<T> source, Span<T> result)
            {
                var tupleSize = result.Length;
                ref var sourceRef = ref MemoryMarshal.GetReference(source);
                ref var resultRef = ref MemoryMarshal.GetReference(result);

                for (nint index = 0; index + tupleSize <= source.Length; index += tupleSize)
                {
                    for (nint indexResult = 0; indexResult < tupleSize; indexResult++)
                    {
                        Unsafe.Add(ref resultRef, indexResult) = 
                            TOperator.Invoke(
                                Unsafe.Add(ref resultRef, indexResult), 
                                Unsafe.Add(ref sourceRef, index + indexResult));
                    }
                }
            }

            static void IntrinsicPowerOfTwo(ReadOnlySpan<T> source, Span<T> result)
            {
            } 

            static void IntrinsicNonPowerOfTwo(ReadOnlySpan<T> source, Span<T> result)
            {
                var tupleSize = result.Length;
                ref var sourceRef = ref MemoryMarshal.GetReference(source);
                ref var resultRef = ref MemoryMarshal.GetReference(result);
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

                    var resultVectors = GetVectors(tupleSize, TOperator.Identity);
                    ref var resultVectorsRef = ref MemoryMarshal.GetReference(resultVectors);

                    // aggregate the source vectors into the result vectors
                    for (nint indexVector = 0; indexVector + tupleSize <= sourceVectors.Length; indexVector += tupleSize)
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
                    for(var indexResultVector = 0; indexResultVector < tupleSize; indexResultVector++)
                    {
                        var resultVector = Unsafe.Add(ref resultVectorsRef, indexResultVector);
                        for (var indexVector = 0; indexVector < Vector<T>.Count; indexVector++)
                        {
                            Unsafe.Add(ref resultRef, indexResult) = 
                                TOperator.Invoke(
                                    Unsafe.Add(ref resultRef, indexResult), 
                                    resultVector[indexVector]);

                            indexResult++;
                            if(indexResult == tupleSize)
                                indexResult = 0;
                        }
                    }

                    // skip the source elements already aggregated
                    index = source.Length - (source.Length % Vector<T>.Count);
                }

                // aggregate the remaining elements in the source
                for (; index + tupleSize <= source.Length; index += tupleSize)
                {
                    for (nint indexResult = 0; indexResult < tupleSize; indexResult++)
                    {
                        Unsafe.Add(ref resultRef, indexResult) = 
                            TOperator.Invoke(
                                Unsafe.Add(ref resultRef, indexResult), 
                                Unsafe.Add(ref sourceRef, index + indexResult));
                    }
                }
            }
        }
    }
}