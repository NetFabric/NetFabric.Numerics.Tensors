namespace NetFabric.Numerics.Tensors;

public static partial class Tensor
{
    /// <summary>
    /// Returns the first element in the <paramref name="x"/> span that satisfies the specified condition.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    /// <typeparam name="TPredicateOperator">The type of the unary operator used to evaluate the condition.</typeparam>
    /// <param name="x">The span to search.</param>
    /// <returns>The first element that satisfies the condition, or <c>null</c> if no element is found.</returns>
    public static T? First<T, TPredicateOperator>(ReadOnlySpan<T> x)
        where T : struct
        where TPredicateOperator : struct, IUnaryToScalarOperator<T, bool>
        => First<T, T, IdentityOperator<T>, TPredicateOperator>(x);

    /// <summary>
    /// Returns the first transformed element in the <paramref name="x"/> span that satisfies the specified condition.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements in the span.</typeparam>
    /// <typeparam name="TTransformed">The type of the transformed elements in the span.</typeparam>
    /// <typeparam name="TTransformOperator">The type of the unary operator used to transform the elements.</typeparam>
    /// <typeparam name="TPredicateOperator">The type of the unary operator used to evaluate the condition.</typeparam>
    /// <param name="x">The span to search.</param>
    /// <returns>The first transformed element that satisfies the condition, or <c>null</c> if no element is found.</returns>
    /// <remarks>The elements of the <paramref name="x"/> span are transformed by the <typeparamref name="TTransformOperator"/> operator before being passed as parameter of <typeparamref name="TPredicateOperator"/> operator.</remarks>
    public static TTransformed? First<TSource, TTransformed, TTransformOperator, TPredicateOperator>(ReadOnlySpan<TSource> x)
        where TSource : struct
        where TTransformed : struct
        where TTransformOperator : struct, IUnaryOperator<TSource, TTransformed>
        where TPredicateOperator : struct, IUnaryToScalarOperator<TTransformed, bool>
    {
        return (Vector.IsHardwareAccelerated && 
            Vector<TSource>.IsSupported && 
            Vector<TTransformed>.IsSupported && 
            TTransformOperator.IsVectorizable && 
            TPredicateOperator.IsVectorizable)
                ? VectorOperation(x)
                : ScalarOperation(x);

        static TTransformed? ScalarOperation(ReadOnlySpan<TSource> x)
        {
            for (var index = 0; index < x.Length; index++)
            {
                var transformedItem = TTransformOperator.Invoke(x[index]);
                if (TPredicateOperator.Invoke(transformedItem))
                    return transformedItem;
            }
            return null;
        }

        static TTransformed? VectorOperation(ReadOnlySpan<TSource> x)
        {
            var indexSource = 0;
            var vectors = MemoryMarshal.Cast<TSource, Vector<TSource>>(x);
            if (vectors.Length > 0)
            {
                // aggregate the source vectors into the aggregate vector
                ref var vectorsRef = ref MemoryMarshal.GetReference(vectors);
                for (var indexVector = 0; indexVector < vectors.Length; indexVector++)
                {
                    var transformedVector = TTransformOperator.Invoke(ref Unsafe.Add(ref vectorsRef, indexVector));
                    if (TPredicateOperator.Invoke(ref transformedVector))
                    {
                        for (var indexElement = 0; indexElement < Vector<TTransformed>.Count; indexElement++)
                        {
                            if (TPredicateOperator.Invoke(transformedVector[indexElement]))
                                return transformedVector[indexElement];
                        }
                    }
                }

                indexSource = vectors.Length * Vector<TTransformed>.Count;
            }

            ref var xRef = ref MemoryMarshal.GetReference(x);
            for (; indexSource < x.Length; indexSource++)
            {
                var transformedItem = TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource));
                if (TPredicateOperator.Invoke(transformedItem))
                    return transformedItem;
            }

            return null;
        }
    } 

    /// <summary>
    /// Returns the first element in the <paramref name="x"/> span that satisfies the specified condition.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    /// <typeparam name="TTransformOperator">The type of the binary operator used to transform the elements.</typeparam>
    /// <typeparam name="TPredicateOperator">The type of the unary operator used to evaluate the condition.</typeparam>
    /// <param name="x">The span to search.</param>
    /// <param name="y">The span to compare against.</param>
    /// <returns>The first element that satisfies the condition, or <c>null</c> if no element is found.</returns>
    public static T? First<T, TTransformOperator, TPredicateOperator>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct
        where TTransformOperator : struct, IBinaryOperator<T, T, T>
        where TPredicateOperator : struct, IUnaryToScalarOperator<T, bool>
        => First<T, T, T, TTransformOperator, TPredicateOperator>(x, y);

    /// <summary>
    /// Returns the first element in the <paramref name="x"/> span that satisfies the specified condition.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the first span.</typeparam>
    /// <typeparam name="T2">The type of the elements in the second span.</typeparam>
    /// <typeparam name="TTransformed">The type of the transformed elements in the span.</typeparam>
    /// <typeparam name="TTransformOperator">The type of the binary operator used to transform the elements.</typeparam>
    /// <typeparam name="TPredicateOperator">The type of the unary operator used to evaluate the condition.</typeparam>
    /// <param name="x">The first span to search.</param>
    /// <param name="y">The second span to search.</param>
    /// <returns>The first element that satisfies the condition, or <c>null</c> if no element is found.</returns>
    /// <remarks>The elements of the <paramref name="x"/> and <paramref name="y"/> spans are transformed by the <typeparamref name="TTransformOperator"/> operator before being passed as parameters of <typeparamref name="TPredicateOperator"/> operator.</remarks>
    public static TTransformed? First<T1, T2, TTransformed, TTransformOperator, TPredicateOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y)
        where T1 : struct
        where T2 : struct
        where TTransformed : struct
        where TTransformOperator : struct, IBinaryOperator<T1, T2, TTransformed>
        where TPredicateOperator : struct, IUnaryToScalarOperator<TTransformed, bool>
    {
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(y), "source spans must have the same size.");

        return (Vector.IsHardwareAccelerated && 
            Vector<T1>.IsSupported && 
            Vector<T2>.IsSupported && 
            Vector<TTransformed>.IsSupported && 
            TTransformOperator.IsVectorizable && 
            TPredicateOperator.IsVectorizable)
                ? VectorOperation(x, y)
                : ScalarOperation(x, y);

        static TTransformed? ScalarOperation(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y)
        {
            for (var index = 0; index < x.Length; index++)
            {
                var transformedItem = TTransformOperator.Invoke(x[index], y[index]);
                if (TPredicateOperator.Invoke(transformedItem))
                    return transformedItem;
            }
            return null;
        }

        static TTransformed? VectorOperation(ReadOnlySpan<T1> x, ReadOnlySpan<T2> y)
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
                for (var indexVector = 0; indexVector < xVectors.Length; indexVector++)
                {
                    var transformedVector = TTransformOperator.Invoke(ref Unsafe.Add(ref xVectorsRef, indexVector), ref Unsafe.Add(ref yVectorsRef, indexVector));
                    if (TPredicateOperator.Invoke(ref transformedVector))
                    {
                        for (var indexElement = 0; indexElement < Vector<TTransformed>.Count; indexElement++)
                        {
                            if (TPredicateOperator.Invoke(transformedVector[indexElement]))
                                return transformedVector[indexElement];
                        }
                    }
                }

                indexSource = xVectors.Length * Vector<TTransformed>.Count;
            }

            ref var xRef = ref MemoryMarshal.GetReference(x);
            ref var yRef = ref MemoryMarshal.GetReference(y);
            for (; indexSource < x.Length; indexSource++)
            {
                var transformedItem = TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource));
                if (TPredicateOperator.Invoke(transformedItem))
                    return transformedItem;
            }

            return null;
        }
    } 
}