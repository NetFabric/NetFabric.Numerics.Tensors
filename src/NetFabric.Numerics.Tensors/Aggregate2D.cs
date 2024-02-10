namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static ValueTuple<T, T> Aggregate2D<T, TOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TOperator : struct, IAggregationOperator<T, T>
        => Aggregate2D<T, T, T, IdentityOperator<T>, TOperator>(source);

    public static ValueTuple<TResult, TResult> Aggregate2D<T, TResult, TOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TResult : struct
        where TOperator : struct, IAggregationOperator<T, TResult>
        => Aggregate2D<T, T, TResult, IdentityOperator<T>, TOperator>(source);

    public static ValueTuple<TResult, TResult> Aggregate2D<T1, T2, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T1> source)
        where T1 : struct
        where T2 : struct
        where TResult : struct
        where TTransformOperator : struct, IUnaryOperator<T1, T2>
        where TAggregateOperator : struct, IAggregationOperator<T2, TResult>
    {
        if (source.Length % 2 is not 0)
            Throw.ArgumentException(nameof(source), "source span must have a size multiple of 2.");

        // initialize aggregate
        var aggregateX = TAggregateOperator.Seed;
        var aggregateY = TAggregateOperator.Seed;
        var indexSource = nint.Zero;

        // aggregate the remaining elements in the source
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        var remaining = source.Length;
        if (remaining >= 4)
        {
            var partialX1 = TAggregateOperator.Seed;
            var partialY1 = TAggregateOperator.Seed;
            for (; indexSource + 3 < source.Length; indexSource += 4)
            {
                aggregateX = TAggregateOperator.Invoke(aggregateX, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
                aggregateY = TAggregateOperator.Invoke(aggregateY, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1)));
                partialX1 = TAggregateOperator.Invoke(partialX1, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 2)));
                partialY1 = TAggregateOperator.Invoke(partialY1, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 3)));
            }
            aggregateX = TAggregateOperator.Invoke(aggregateX, partialX1);
            aggregateY = TAggregateOperator.Invoke(aggregateY, partialY1);
            remaining = source.Length - (int)indexSource;
        }

        switch(remaining)
        {
            case 2:
                aggregateX = TAggregateOperator.Invoke(aggregateX, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource)));
                aggregateY = TAggregateOperator.Invoke(aggregateY, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, indexSource + 1)));
                break;
        }

        return (aggregateX, aggregateY);
    }

    public static ValueTuple<TResult, TResult> Aggregate2D<T1, T2, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T1> x, ReadOnlySpan<T1> y)
        where T1 : struct
        where T2 : struct
        where TResult : struct
        where TTransformOperator : struct, IBinaryOperator<T1, T1, T2>
        where TAggregateOperator : struct, IAggregationOperator<T2, TResult>
    {
        if (x.Length % 2 is not 0)
            Throw.ArgumentException(nameof(x), "source spans must have a size multiple of 2.");
        if (x.Length != y.Length)
            Throw.ArgumentException(nameof(y), "source spans must have the same size.");

        // initialize aggregate
        var aggregateX = TAggregateOperator.Seed;
        var aggregateY = TAggregateOperator.Seed;
        var indexSource = nint.Zero;

        // aggregate the remaining elements in the source
        ref var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        var remaining = x.Length;
        if (remaining >= 4)
        {
            var partialX1 = TAggregateOperator.Seed;
            var partialY1 = TAggregateOperator.Seed;
            for (; indexSource + 3 < x.Length; indexSource += 4)
            {
                aggregateX = TAggregateOperator.Invoke(aggregateX, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
                aggregateY = TAggregateOperator.Invoke(aggregateY, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1)));
                partialX1 = TAggregateOperator.Invoke(partialX1, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 2), Unsafe.Add(ref yRef, indexSource + 2)));
                partialY1 = TAggregateOperator.Invoke(partialY1, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 3), Unsafe.Add(ref yRef, indexSource + 3)));
            }
            aggregateX = TAggregateOperator.Invoke(aggregateX, partialX1);
            aggregateY = TAggregateOperator.Invoke(aggregateY, partialY1);
            remaining = x.Length - (int)indexSource;
        }

        switch(remaining)
        {
            case 2:
                aggregateX = TAggregateOperator.Invoke(aggregateX, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource), Unsafe.Add(ref yRef, indexSource)));
                aggregateY = TAggregateOperator.Invoke(aggregateY, TTransformOperator.Invoke(Unsafe.Add(ref xRef, indexSource + 1), Unsafe.Add(ref yRef, indexSource + 1)));
                break;
        }

        return (aggregateX, aggregateY);
    }
}
