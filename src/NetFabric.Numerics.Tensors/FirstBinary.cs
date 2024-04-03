namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static T? First<T, TPredicateOperator>(ReadOnlySpan<T> x, T y)
        where T : struct
        where TPredicateOperator : struct, IBinaryToScalarOperator<T, T, bool>
        => First<T, T, IdentityOperator<T>, TPredicateOperator>(x, y);

    public static TTransformed? First<TSource, TTransformed, TTransformOperator, TPredicateOperator>(ReadOnlySpan<TSource> x, TTransformed y)
        where TSource : struct
        where TTransformed : struct
        where TTransformOperator : struct, IUnaryOperator<TSource, TTransformed>
        where TPredicateOperator : struct, IBinaryToScalarOperator<TTransformed, TTransformed, bool>
    {
        return (Vector.IsHardwareAccelerated && 
            Vector<TSource>.IsSupported && 
            Vector<TTransformed>.IsSupported && 
            TTransformOperator.IsVectorizable && 
            TPredicateOperator.IsVectorizable)
                ? VectorOperation(x, y)
                : ScalarOperation(x, y);

        static TTransformed? ScalarOperation(ReadOnlySpan<TSource> x, TTransformed y)
        {
            for (var index = 0; index < x.Length; index++)
            {
                var transformedItem = TTransformOperator.Invoke(x[index]);
                if (TPredicateOperator.Invoke(transformedItem, y))
                    return transformedItem;
            }
            return null;
        }

        static TTransformed? VectorOperation(ReadOnlySpan<TSource> x, TTransformed y)
        {
            var indexSource = 0;
            var vectors = MemoryMarshal.Cast<TSource, Vector<TSource>>(x);
            if (vectors.Length > 0)
            {
                // aggregate the source vectors into the aggregate vector
                ref var vectorsRef = ref MemoryMarshal.GetReference(vectors);
                var yVector = new Vector<TTransformed>(y);

                var indexVector = 0;
                for (; indexVector < vectors.Length; indexVector++)
                {
                    var transformedVector = TTransformOperator.Invoke(ref Unsafe.Add(ref vectorsRef, indexVector));
                    if (TPredicateOperator.Invoke(ref transformedVector, ref yVector))
                    {
                        for (var indexElement = 0; indexElement < Vector<TTransformed>.Count; indexElement++)
                        {
                            if (TPredicateOperator.Invoke(transformedVector[indexElement], y))
                                return transformedVector[indexElement];
                        }
                    }
                }

                indexSource = indexVector * Vector<TTransformed>.Count;
            }

            ref var xRef = ref MemoryMarshal.GetReference(x);
            for (; indexSource < x.Length; indexSource++)
            {
                var transformedItem = TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource));
                if (TPredicateOperator.Invoke(transformedItem, y))
                    return transformedItem;
            }

            return null;
        }
    } 

    public static T? First<T, TTransformOperator, TPredicateOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, T z)
        where T : struct
        where TTransformOperator : struct, IBinaryOperator<T, T, T>
        where TPredicateOperator : struct, IBinaryToScalarOperator<T, T, bool>
        => First<T, T, T, TTransformOperator, TPredicateOperator>(x, y, z);

    public static TTransformed? First<T1, T2, TTransformed, TTransformOperator, TPredicateOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y, TTransformed z)
        where T1 : struct
        where T2 : struct
        where TTransformed : struct
        where TTransformOperator : struct, IBinaryOperator<T1, T2, TTransformed>
        where TPredicateOperator : struct, IBinaryToScalarOperator<TTransformed, TTransformed, bool>
    {
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(y), "source spans must have the same size.");

        return (Vector.IsHardwareAccelerated && 
            Vector<T1>.IsSupported && 
            Vector<T2>.IsSupported && 
            Vector<TTransformed>.IsSupported && 
            TTransformOperator.IsVectorizable && 
            TPredicateOperator.IsVectorizable)
                ? VectorOperation(x, y, z)
                : ScalarOperation(x, y, z);

        static TTransformed? ScalarOperation(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y, TTransformed z)
        {
            for (var index = 0; index < x.Length; index++)
            {
                var transformedItem = TTransformOperator.Invoke(x[index], y[index]);
                if (TPredicateOperator.Invoke(transformedItem, z))
                    return transformedItem;
            }
            return null;
        }

        static TTransformed? VectorOperation(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y, TTransformed z)
        {
            var indexSource = 0;

            // convert source span to vector span without copies
            var xVectors = MemoryMarshal.Cast<T1, Vector<T1>>(x);
            var yVectors = MemoryMarshal.Cast<T2, Vector<T2>>(y);

            // check if there is at least one vector to aggregate
            if (xVectors.Length > 0)
            {
                // aggregate the source vectors into the aggregate vector
                ref var xVectorsRef = ref MemoryMarshal.GetReference(xVectors);
                ref var yVectorsRef = ref MemoryMarshal.GetReference(yVectors);
                var zVector = new Vector<TTransformed>(z);

                var indexVector = 0;
                for (; indexVector < xVectors.Length; indexVector++)
                {
                    var transformedVector = TTransformOperator.Invoke(ref Unsafe.Add(ref xVectorsRef, indexVector), ref Unsafe.Add(ref yVectorsRef, indexVector));
                    if (TPredicateOperator.Invoke(ref transformedVector, ref zVector))
                    {
                        for (var indexElement = 0; indexElement < Vector<TTransformed>.Count; indexElement++)
                        {
                            if (TPredicateOperator.Invoke(transformedVector[indexElement], z))
                                return transformedVector[indexElement];
                        }
                    }
                }

                indexSource = indexVector * Vector<TTransformed>.Count;
            }

            ref var xRef = ref MemoryMarshal.GetReference(x);
            ref var yRef = ref MemoryMarshal.GetReference(y);
            for (; indexSource < x.Length; indexSource++)
            {
                var transformedItem = TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource));
                if (TPredicateOperator.Invoke(transformedItem, z))
                    return transformedItem;
            }

            return null;
        }
    } 
}