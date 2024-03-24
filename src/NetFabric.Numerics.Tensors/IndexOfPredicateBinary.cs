namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static int IndexOfPredicate<T, TPredicateOperator>(ReadOnlySpan<T> x, T y)
        where T : struct
        where TPredicateOperator : struct, IBinaryToScalarOperator<T, T, bool>
    {
        return (Vector.IsHardwareAccelerated && 
            Vector<T>.IsSupported && 
            TPredicateOperator.IsVectorizable)
                ? VectorOperation(x, y)
                : ScalarOperation(x, y);

        static int ScalarOperation(ReadOnlySpan<T> x, T y)
        {
            for (var index = 0; index < x.Length; index++)
            {
                if (TPredicateOperator.Invoke(x[index], y))
                    return index;
            }
            return -1;
        }

        static int VectorOperation(ReadOnlySpan<T> x, T y)
        {
            var indexSource = 0;
            var vectors = MemoryMarshal.Cast<T, Vector<T>>(x);
            if (vectors.Length > 0)
            {
                ref var vectorsRef = ref MemoryMarshal.GetReference(vectors);
                var yVector = new Vector<T>(y);

                var indexVector = 0;
                for (; indexVector < vectors.Length; indexVector++)
                {
                    ref var currentVector = ref Unsafe.Add(ref vectorsRef, indexVector);
                    if (TPredicateOperator.Invoke(ref currentVector, ref yVector))
                    {
                        for (var indexElement = 0; indexElement < Vector<T>.Count; indexElement++)
                        {
                            if (TPredicateOperator.Invoke(currentVector[indexElement], y))
                                return (indexVector * Vector<T>.Count) + indexElement;
                        }
                    }
                }

                indexSource = indexVector * Vector<T>.Count;
            }

            ref var xRef = ref MemoryMarshal.GetReference(x);
            for (; indexSource < x.Length; indexSource++)
            {
                if (TPredicateOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y))
                    return indexSource;
            }

            return -1;
        }
    } 
}