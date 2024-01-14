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

            ref var sourceRef = ref MemoryMarshal.GetReference<T>(source);
            ref var resultRef = ref MemoryMarshal.GetReference<T>(result);

            nint index = 0;

            // aggregate
            if (Vector.IsHardwareAccelerated && Vector<T>.IsSupported)
            {
                var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);
                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);

                var intrinsic = (tupleSize.IsPowerOfTwo())
                    ? IntrinsicPowerOfTwo(ref sourceVectorsRef, sourceVectors.Length, ref resultRef, result.Length)
                    : IntrinsicNonPowerOfTwo(ref sourceVectorsRef, sourceVectors.Length, ref resultRef, result.Length);

                // skip the source elements already aggregated
                if (intrinsic)
                    index = source.Length - (source.Length % Vector<T>.Count);
            }

            // aggregate the remaining elements in the source
            for (; index + tupleSize <= source.Length; index += result.Length)
            {
                for (nint indexResult = 0; indexResult < result.Length; indexResult++)
                {
                    Unsafe.Add(ref resultRef, indexResult) = 
                        TOperator.Invoke(
                            Unsafe.Add(ref resultRef, indexResult), 
                            Unsafe.Add(ref sourceRef, index + indexResult));
                }
            }

            return result;

            static bool IntrinsicPowerOfTwo(ref Vector<T> sourceVectorsRef, int sourceVectorsLength, ref T resultRef, int resultLength)
            {
                if (sourceVectorsLength < 2 || Vector<T>.Count / resultLength < 1)
                    return false;
            
                var resultVector = new Vector<T>(TOperator.Identity);
                ref var resultVectorRef = ref Unsafe.As<Vector<T>, T>(ref Unsafe.AsRef(in resultVector));

                // aggregate the source vectors into the result vector
                for (nint indexVector = 0; indexVector < sourceVectorsLength; indexVector++)
                {
                    resultVector = 
                        TOperator.Invoke(
                            resultVector, 
                            Unsafe.Add(ref sourceVectorsRef, indexVector));
                }

                // aggregate the result vectors into the result
                nint indexResult = 0;
                for (var indexVector = 0; indexVector < Vector<T>.Count; indexVector++)
                {
                    Unsafe.Add(ref resultRef, indexResult) = 
                        TOperator.Invoke(
                            Unsafe.Add(ref resultRef, indexResult), 
                            Unsafe.Add(ref resultVectorRef, indexVector));

                    indexResult++;
                    if(indexResult == resultLength)
                        indexResult = 0;
                }

                return true;
            } 

            static bool IntrinsicNonPowerOfTwo(ref Vector<T> sourceVectorsRef, int sourceVectorsLength, ref T resultRef, int resultLength)
            {
                // use as many vectors as the number of elements in the tuple
                // this guarantees alignment and allows to use the same code for all tuple sizes
                // but only used these if source fills more than the number of elements in the tuple
                // and the number of vectors filled is a multiple of the number of elements in the tuple
                if (sourceVectorsLength < 2 * resultLength || sourceVectorsLength % resultLength is not 0)
                    return false;
            
                var resultVectors = GetVectors(resultLength, TOperator.Identity);
                ref var resultVectorsRef = ref MemoryMarshal.GetReference(resultVectors);

                // aggregate the source vectors into the result vectors
                for (nint indexVector = 0; indexVector + resultLength <= sourceVectorsLength; indexVector += resultLength)
                {
                    for (nint indexTuple = 0; indexTuple < resultLength; indexTuple++)
                    {
                        Unsafe.Add(ref resultVectorsRef, indexTuple) = 
                            TOperator.Invoke(
                                Unsafe.Add(ref resultVectorsRef, indexTuple), 
                                Unsafe.Add(ref sourceVectorsRef, indexVector + indexTuple));
                    }
                }

                // aggregate the result vectors into the result
                nint indexResult = 0;
                for(var indexResultVector = 0; indexResultVector < resultLength; indexResultVector++)
                {
                    var resultVector = Unsafe.Add(ref resultVectorsRef, indexResultVector);
                    ref var resultVectorRef = ref Unsafe.As<Vector<T>, T>(ref Unsafe.AsRef(in resultVector));
                    for (var indexVector = 0; indexVector < Vector<T>.Count; indexVector++)
                    {
                        Unsafe.Add(ref resultRef, indexResult) = 
                            TOperator.Invoke(
                                Unsafe.Add(ref resultRef, indexResult), 
                                Unsafe.Add(ref resultVectorRef, indexVector));

                        indexResult++;
                        if(indexResult == resultLength)
                            indexResult = 0;
                    }
                }

                return true;
            }
        }
    }
}