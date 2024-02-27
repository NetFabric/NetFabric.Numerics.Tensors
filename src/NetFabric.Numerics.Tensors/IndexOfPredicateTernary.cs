namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static int IndexOfPredicate<T, TPredicateOperator>(ReadOnlySpan<T> x, T y, T z)
        where T : struct
        where TPredicateOperator : struct, ITernaryToScalarOperator<T, T, T, bool>
    {
        var indexSource = nint.Zero;

        if (TPredicateOperator.IsVectorizable && 
            Vector.IsHardwareAccelerated && 
            Vector<T>.IsSupported)
        {
            var vectors = MemoryMarshal.Cast<T, Vector<T>>(x);
            ref var vectorsRef = ref MemoryMarshal.GetReference(vectors);
            var yVector = new Vector<T>(y);
            var zVector = new Vector<T>(z);

            var indexVector = nint.Zero;
            for (; indexVector < vectors.Length; indexVector++)
            {
                ref var currentVector = ref Unsafe.Add(ref vectorsRef, indexVector);
                if (TPredicateOperator.Invoke(ref currentVector, ref yVector, ref zVector))
                {
                    for (var indexElement = 0; indexElement < Vector<T>.Count; indexElement++)
                    {
                        if (TPredicateOperator.Invoke(currentVector[indexElement], y, z))
                            return ((int)indexVector * Vector<T>.Count) + indexElement;
                    }
                }
            }

            indexSource = indexVector * Vector<T>.Count;
        }

        ref var xRef = ref MemoryMarshal.GetReference(x);
        for (; indexSource < x.Length; indexSource++)
        {
            if (TPredicateOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y, z))
                return (int)indexSource;
        }

        return -1;
    }
}