using System.ComponentModel.DataAnnotations;

namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static int IndexOfPredicate<T, TPredicateOperator>(ReadOnlySpan<T> x, T y, T z)
        where T : struct
        where TPredicateOperator : struct, ITernaryToScalarOperator<T, T, T, bool>
    {
        var indexSource = 0;

        if (TPredicateOperator.IsVectorizable && 
            Vector.IsHardwareAccelerated && 
            Vector<T>.IsSupported)
        {
            var vectors = MemoryMarshal.Cast<T, Vector<T>>(x);
            if(vectors.Length > 0)
            {
                ref var vectorsRef = ref MemoryMarshal.GetReference(vectors);
                var yVector = new Vector<T>(y);
                var zVector = new Vector<T>(z);

                var indexVector = 0;
                for (; indexVector < vectors.Length; indexVector++)
                {
                    ref var currentVector = ref Unsafe.Add(ref vectorsRef, indexVector);
                    if (TPredicateOperator.Invoke(ref currentVector, ref yVector, ref zVector))
                    {
                        for (var indexElement = 0; indexElement < Vector<T>.Count; indexElement++)
                        {
                            if (TPredicateOperator.Invoke(currentVector[indexElement], y, z))
                                return (indexVector * Vector<T>.Count) + indexElement;
                        }
                    }
                }

                indexSource = indexVector * Vector<T>.Count;
            }
        }

        ref var xRef = ref MemoryMarshal.GetReference(x);
        for (; indexSource < x.Length; indexSource++)
        {
            if (TPredicateOperator.Invoke(Unsafe.Add(ref xRef, indexSource), y, z))
                return indexSource;
        }

        return -1;
    }
}