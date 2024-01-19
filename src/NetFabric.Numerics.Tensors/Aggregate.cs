namespace NetFabric.Numerics
{
    public static partial class Tensor
    {
        public static Span<T> Aggregate<T, TOperator>(ReadOnlySpan<T> source, int tupleSize = 1)
            where T : struct
            where TOperator : struct, IAggregationOperator<T>
        {
            if (tupleSize < 1)
                Throw.ArgumentException(nameof(tupleSize), "tupleSize must be greater than 0.");
            if (source.Length % tupleSize is not 0)
                Throw.ArgumentException(nameof(source), "source span must have a size multiple of tupleSize.");

            // initialize result
            var result = new T[tupleSize].AsSpan();
            result.Fill(TOperator.Identity);

            ref var sourceRef = ref MemoryMarshal.GetReference(source);
            ref var resultRef = ref MemoryMarshal.GetReference(result);

            var index = nint.Zero;

            // aggregate
            if (Vector.IsHardwareAccelerated && Vector<T>.IsSupported)
            {
                var intrinsic = Intrinsic(source, ref resultRef, result.Length);

                // skip the source elements already aggregated
                if (intrinsic)
                    index = source.Length - (source.Length % Vector<T>.Count);
            }

            // aggregate the remaining elements in the source
            Scalar(index, ref sourceRef, source.Length, ref resultRef, result.Length);

            return result;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static void Scalar(nint start, ref T sourceRef, int sourceLength, ref T resultRef, int resultLength)
            {
                switch (resultLength)
                {
                    case 1:
                        for (var index = start; index < sourceLength; index++)
                        {
                            Unsafe.Add(ref resultRef, 0) = TOperator.Invoke(Unsafe.Add(ref resultRef, 0), Unsafe.Add(ref sourceRef, index));
                        }
                        break;

                    case 2:
                        for (var index = start; index + 1 < sourceLength; index += 2)
                        {
                            Unsafe.Add(ref resultRef, 0) = TOperator.Invoke(Unsafe.Add(ref resultRef, 0), Unsafe.Add(ref sourceRef, index));
                            Unsafe.Add(ref resultRef, 1) = TOperator.Invoke(Unsafe.Add(ref resultRef, 1), Unsafe.Add(ref sourceRef, index + 1));
                        }      
                        break;    

                    case 3:
                        for (var index = start; index + 2 < sourceLength; index += 3)
                        {
                            Unsafe.Add(ref resultRef, 0) = TOperator.Invoke(Unsafe.Add(ref resultRef, 0), Unsafe.Add(ref sourceRef, index));
                            Unsafe.Add(ref resultRef, 1) = TOperator.Invoke(Unsafe.Add(ref resultRef, 1), Unsafe.Add(ref sourceRef, index + 1));
                            Unsafe.Add(ref resultRef, 2) = TOperator.Invoke(Unsafe.Add(ref resultRef, 2), Unsafe.Add(ref sourceRef, index + 2));
                        }      
                        break;

                    case 4:
                        for (var index = start; index + 3 < sourceLength; index += 4)
                        {
                            Unsafe.Add(ref resultRef, 0) = TOperator.Invoke(Unsafe.Add(ref resultRef, 0), Unsafe.Add(ref sourceRef, index));
                            Unsafe.Add(ref resultRef, 1) = TOperator.Invoke(Unsafe.Add(ref resultRef, 1), Unsafe.Add(ref sourceRef, index + 1));
                            Unsafe.Add(ref resultRef, 2) = TOperator.Invoke(Unsafe.Add(ref resultRef, 2), Unsafe.Add(ref sourceRef, index + 2));
                            Unsafe.Add(ref resultRef, 3) = TOperator.Invoke(Unsafe.Add(ref resultRef, 3), Unsafe.Add(ref sourceRef, index + 3));
                        }      
                        break;          
                    
                    default:
                        for (var index = start; index + resultLength <= sourceLength; index += resultLength)
                        {
                            for (var indexResult = nint.Zero; indexResult < resultLength; indexResult++)
                            {
                                Unsafe.Add(ref resultRef, indexResult) = TOperator.Invoke(Unsafe.Add(ref resultRef, indexResult), Unsafe.Add(ref sourceRef, index + indexResult));
                            }
                        }
                        break;
                }
            }

            static bool Intrinsic(ReadOnlySpan<T> source, ref T resultRef, int resultLength)
            {
                var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);
                ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);

                return resultLength != 1 && resultLength.IsPowerOfTwo()
                    ? IntrinsicPowerOfTwo(ref sourceVectorsRef, sourceVectors.Length, ref resultRef, resultLength)
                    : IntrinsicNonPowerOfTwo(ref sourceVectorsRef, sourceVectors.Length, ref resultRef, resultLength);
            }

            static bool IntrinsicPowerOfTwo(ref Vector<T> sourceVectorsRef, int sourceVectorsLength, ref T resultRef, int resultLength)
            {
                // use one vector
                // but only used these if source fills at least two vectors
                // and the vector if filled with complete tuples
                if (sourceVectorsLength < 2 || Vector<T>.Count / resultLength == 0)
                    return false;
            
                var resultVector = new Vector<T>(TOperator.Identity);
                ref var resultVectorRef = ref Unsafe.As<Vector<T>, T>(ref Unsafe.AsRef(in resultVector));

                // aggregate the source vectors into the result vector
                for (var indexVector = nint.Zero; indexVector < sourceVectorsLength; indexVector++)
                {
                    resultVector = 
                        TOperator.Invoke(
                            resultVector, 
                            Unsafe.Add(ref sourceVectorsRef, indexVector));
                }

                // aggregate the result vector into the result
                var indexResult = nint.Zero;
                for (var indexVector = nint.Zero; indexVector + 1 < Vector<T>.Count; indexVector += 2)
                {
                    Unsafe.Add(ref resultRef, indexResult) = 
                        TOperator.Invoke(
                            Unsafe.Add(ref resultRef, indexResult), 
                            Unsafe.Add(ref resultVectorRef, indexVector));

                    Unsafe.Add(ref resultRef, indexResult + 1) = 
                        TOperator.Invoke(
                            Unsafe.Add(ref resultRef, indexResult + 1), 
                            Unsafe.Add(ref resultVectorRef, indexVector + 1));

                    indexResult += 2;
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
                for (var indexVector = nint.Zero; indexVector + resultLength <= sourceVectorsLength; indexVector += resultLength)
                {
                    for (var indexTuple = nint.Zero; indexTuple < resultLength; indexTuple++)
                    {
                        Unsafe.Add(ref resultVectorsRef, indexTuple) = 
                            TOperator.Invoke(
                                Unsafe.Add(ref resultVectorsRef, indexTuple), 
                                Unsafe.Add(ref sourceVectorsRef, indexVector + indexTuple));
                    }
                }

                // aggregate the result vectors into the result
                var indexResult = nint.Zero;
                for(var indexResultVector = nint.Zero; indexResultVector < resultLength; indexResultVector++)
                {
                    var resultVector = Unsafe.Add(ref resultVectorsRef, indexResultVector);
                    ref var resultVectorRef = ref Unsafe.As<Vector<T>, T>(ref Unsafe.AsRef(in resultVector));
                    for (var indexVector = nint.Zero; indexVector < Vector<T>.Count; indexVector++)
                    {
                        Unsafe.Add(ref resultRef, indexResult) = 
                            TOperator.Invoke(
                                Unsafe.Add(ref resultRef, indexResult), 
                                Unsafe.Add(ref resultVectorRef, indexVector));

                        indexResult++;
                        if(indexResult == resultLength)
                            indexResult = nint.Zero;
                    }
                }

                return true;
            }
        }
    }
}