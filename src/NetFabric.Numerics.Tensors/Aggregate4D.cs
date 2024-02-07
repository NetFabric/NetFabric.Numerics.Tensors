namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static ValueTuple<T, T, T, T> Aggregate4D<T, TOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TOperator : struct, IAggregationOperator<T, T>
        => Aggregate4D<T, T, T, IdentityOperator<T>, TOperator>(source);

    public static ValueTuple<TResult, TResult, TResult, TResult> Aggregate4D<T, TResult, TOperator>(ReadOnlySpan<T> source)
        where T : struct
        where TResult : struct
        where TOperator : struct, IAggregationOperator<T, TResult>
        => Aggregate4D<T, T, TResult, IdentityOperator<T>, TOperator>(source);

    public static ValueTuple<TResult, TResult, TResult, TResult> Aggregate4D<T1, T2, TResult, TTransformOperator, TAggregateOperator>(ReadOnlySpan<T1> source)
        where T1 : struct
        where T2 : struct
        where TResult : struct
        where TTransformOperator : struct, IUnaryOperator<T1, T2>
        where TAggregateOperator : struct, IAggregationOperator<T2, TResult>
    {
        if (source.Length % 4 is not 0)
            Throw.ArgumentException(nameof(source), "source span must have a size multiple of 4.");

        // initialize aggregate
        var aggregateX = TAggregateOperator.Identity;
        var aggregateY = TAggregateOperator.Identity;
        var aggregateZ = TAggregateOperator.Identity;
        var aggregateW = TAggregateOperator.Identity;
        var sourceIndex = nint.Zero;

        // aggregate the remaining elements in the source
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        for (; sourceIndex + 3 < source.Length; sourceIndex += 4)
        {
            aggregateX = TAggregateOperator.Invoke(aggregateX, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, sourceIndex)));
            aggregateY = TAggregateOperator.Invoke(aggregateY, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, sourceIndex + 1)));
            aggregateZ = TAggregateOperator.Invoke(aggregateZ, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, sourceIndex + 2)));
            aggregateW = TAggregateOperator.Invoke(aggregateW, TTransformOperator.Invoke(Unsafe.Add(ref sourceRef, sourceIndex + 3)));
        }

        return (aggregateX, aggregateY, aggregateZ, aggregateW);
    }
}
