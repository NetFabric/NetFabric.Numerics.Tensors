namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    public static int IndexOfFirstNumber<T, TPredicateOperator>(ReadOnlySpan<T> x, T y)
        where T : struct
        where TPredicateOperator : struct, IBinaryToScalarOperator<T, T, bool>
        => IndexOfFirstNumber<T, T, IdentityOperator<T>, TPredicateOperator>(x, y);

    public static int IndexOfFirstNumber<TSource, TTransformed, TTransformOperator, TPredicateOperator>(ReadOnlySpan<TSource> x, TTransformed y)
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

        static int ScalarOperation(ReadOnlySpan<TSource> x, TTransformed y)
        {
            for (var index = 0; index < x.Length; index++)
            {
                if (TPredicateOperator.Invoke(TTransformOperator.Invoke(x[index]), y))
                    return index;
            }
            return -1;
        }

        static int VectorOperation(ReadOnlySpan<TSource> x, TTransformed y)
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
                                return (indexVector * Vector<TTransformed>.Count) + indexElement;
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
                    return indexSource;
            }

            return -1;
        }
    } 

    public static int IndexOfFirstNumber<T, TTransformOperator, TPredicateOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, T z)
        where T : struct
        where TTransformOperator : struct, IBinaryOperator<T, T, T>
        where TPredicateOperator : struct, IBinaryToScalarOperator<T, T, bool>
        => IndexOfFirstNumber<T, T, T, TTransformOperator, TPredicateOperator>(x, y, z);

    public static int IndexOfFirstNumber<T1, T2, TTransformed, TTransformOperator, TPredicateOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y, TTransformed z)
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

        static int ScalarOperation(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y, TTransformed z)
        {
            for (var index = 0; index < x.Length; index++)
            {
                if (TPredicateOperator.Invoke(TTransformOperator.Invoke(x[index], y[index]), z))
                    return index;
            }
            return -1;
        }

        static int VectorOperation(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y, TTransformed z)
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
                                return (indexVector * Vector<TTransformed>.Count) + indexElement;
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
                    return indexSource;
            }

            return -1;
        }
    } 
}