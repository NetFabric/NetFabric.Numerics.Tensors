namespace NetFabric.Numerics
{
    public static partial class Tensor
    {
        public static ReadOnlySpan<T> Aggregate3D<T, TOperator>(ReadOnlySpan<T> source)
            where T : struct
            where TOperator : struct, IAggregationOperator<T>
        {
            if (source.Length % 3 is not 0)
                Throw.ArgumentException(nameof(source), "source span must have a size multiple of 3.");

            // initialize aggregate
            var aggregateX = TOperator.Identity;
            var aggregateY = TOperator.Identity;
            var aggregateZ = TOperator.Identity;
            var sourceIndex = nint.Zero;

            // aggregate the remaining elements in the source
            ref var sourceRef = ref MemoryMarshal.GetReference(source);
            for (; sourceIndex + 2 < source.Length; sourceIndex += 3)
            {
                aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref sourceRef, sourceIndex));
                aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref sourceRef, sourceIndex + 1));
                aggregateZ = TOperator.Invoke(aggregateZ, Unsafe.Add(ref sourceRef, sourceIndex + 2));
            }

            return new[] { aggregateX, aggregateY, aggregateZ };
        }
    }
}