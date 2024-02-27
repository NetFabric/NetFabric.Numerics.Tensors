namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static T? AggregatePredicateBinary<T, TPredicateOperator>(ReadOnlySpan<T> x, T y)
        where T : struct
        where TPredicateOperator : struct, IBinaryToScalarOperator<T, T, bool>
    {
        var indexSource = nint.Zero;

        if (TPredicateOperator.IsVectorizable && 
            Vector.IsHardwareAccelerated && 
            Vector<T>.IsSupported)
        {
            var vectors = MemoryMarshal.Cast<T, Vector<T>>(x);
            ref var vectorsRef = ref MemoryMarshal.GetReference(vectors);
            var valueVector = new Vector<T>(y);

            var indexVector = nint.Zero;
            for (; indexVector < vectors.Length; indexVector++)
            {
                ref var currentVector = ref Unsafe.Add(ref vectorsRef, indexVector);
                if (TPredicateOperator.Invoke(ref currentVector, ref valueVector))
                {
                    for (var index = 0; index < Vector<int>.Count; index++)
                    {
                        if (TPredicateOperator.Invoke(currentVector[index], y))
                            return currentVector[index];
                    }
                }
            }

            indexSource = indexVector * Vector<int>.Count;
        }

        ref var xRef = ref MemoryMarshal.GetReference(x);
        for (; indexSource < x.Length; indexSource++)
        {
            if (TPredicateOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y))
                return Unsafe.Add(ref xRef, indexSource);
        }

        return default;
    }
}