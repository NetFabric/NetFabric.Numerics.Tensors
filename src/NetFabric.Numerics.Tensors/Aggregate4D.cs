namespace NetFabric.Numerics
{
    public static partial class Tensor
    {
        public static ReadOnlySpan<T> Aggregate4D<T, TOperator>(ReadOnlySpan<T> source)
            where T : struct
            where TOperator : struct, IAggregationOperator<T>
        {
            if (source.Length % 4 is not 0)
                Throw.ArgumentException(nameof(source), "source span must have a size multiple of 4.");

            // initialize aggregate
            var aggregateX = TOperator.Identity;
            var aggregateY = TOperator.Identity;
            var aggregateZ = TOperator.Identity;
            var aggregateW = TOperator.Identity;
            var sourceIndex = nint.Zero;

            // aggregate the remaining elements in the source
            ref var sourceRef = ref MemoryMarshal.GetReference(source);
            for (; sourceIndex + 3 < source.Length; sourceIndex += 4)
            {
                aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref sourceRef, sourceIndex));
                aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref sourceRef, sourceIndex + 1));
                aggregateZ = TOperator.Invoke(aggregateZ, Unsafe.Add(ref sourceRef, sourceIndex + 2));
                aggregateW = TOperator.Invoke(aggregateW, Unsafe.Add(ref sourceRef, sourceIndex + 3));
            }

            return new[] { aggregateX, aggregateY, aggregateZ, aggregateW };
        }
    }
}