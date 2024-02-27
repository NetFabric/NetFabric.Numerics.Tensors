namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static int IndexOfPredicate<T, TPredicateOperator>(ReadOnlySpan<T> x)
        where T : struct
        where TPredicateOperator : struct, IUnaryToScalarOperator<T, bool>
    {
        var indexSource = nint.Zero;

        if (TPredicateOperator.IsVectorizable && 
            Vector.IsHardwareAccelerated && 
            Vector<T>.IsSupported)
        {
            var vectors = MemoryMarshal.Cast<T, Vector<T>>(x);
            ref var vectorsRef = ref MemoryMarshal.GetReference(vectors);

            var indexVector = nint.Zero;
            for (; indexVector < vectors.Length; indexVector++)
            {
                ref var currentVector = ref Unsafe.Add(ref vectorsRef, indexVector);
                if (TPredicateOperator.Invoke(ref currentVector))
                {
                    for (var indexElement = 0; indexElement < Vector<int>.Count; indexElement++)
                    {
                        if (TPredicateOperator.Invoke(currentVector[indexElement]))
                            return ((int)indexVector * Vector<int>.Count) + indexElement;
                    }
                }
            }

            indexSource = indexVector * Vector<int>.Count;
        }

        ref var xRef = ref MemoryMarshal.GetReference(x);
        for (; indexSource < x.Length; indexSource++)
        {
            if (TPredicateOperator.Invoke(Unsafe.Add(ref xRef, indexSource)))
                return (int)indexSource;
        }

        return -1;
    }
}