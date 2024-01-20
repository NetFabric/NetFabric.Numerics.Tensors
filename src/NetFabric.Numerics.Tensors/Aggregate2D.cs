namespace NetFabric.Numerics
{
    public static partial class Tensor
    {
        public static ReadOnlySpan<T> Aggregate2D<T, TOperator>(ReadOnlySpan<T> source)
            where T : struct
            where TOperator : struct, IAggregationOperator<T>
        {
            if (source.Length % 2 is not 0)
                Throw.ArgumentException(nameof(source), "source span must have a size multiple of 2.");

            // initialize aggregate
            var aggregateX = TOperator.Identity;
            var aggregateY = TOperator.Identity;
            var sourceIndex = nint.Zero;

            // aggregate the remaining elements in the source
            ref var sourceRef = ref MemoryMarshal.GetReference(source);
            var remaining = source.Length;
            if (remaining >= 8)
            {
                var partialX1 = TOperator.Identity;
                var partialY1 = TOperator.Identity;
                for (; sourceIndex + 3 < source.Length; sourceIndex += 4)
                {
                    aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref sourceRef, sourceIndex));
                    aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref sourceRef, sourceIndex + 1));
                    partialX1 = TOperator.Invoke(partialX1, Unsafe.Add(ref sourceRef, sourceIndex + 2));
                    partialY1 = TOperator.Invoke(partialY1, Unsafe.Add(ref sourceRef, sourceIndex + 3));
                }
                aggregateX = TOperator.Invoke(aggregateX, partialX1);
                aggregateY = TOperator.Invoke(aggregateY, partialY1);
                remaining = source.Length - (int)sourceIndex;
            }

            switch(remaining)
            {
                case 6:
                    aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref sourceRef, sourceIndex));
                    aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref sourceRef, sourceIndex + 1));
                    aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref sourceRef, sourceIndex + 2));
                    aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref sourceRef, sourceIndex + 3));
                    aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref sourceRef, sourceIndex + 4));
                    aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref sourceRef, sourceIndex + 5));
                    break;
                case 4:
                    aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref sourceRef, sourceIndex));
                    aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref sourceRef, sourceIndex + 1));
                    aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref sourceRef, sourceIndex + 2));
                    aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref sourceRef, sourceIndex + 3));
                    break;
                case 2:
                    aggregateX = TOperator.Invoke(aggregateX, Unsafe.Add(ref sourceRef, sourceIndex));
                    aggregateY = TOperator.Invoke(aggregateY, Unsafe.Add(ref sourceRef, sourceIndex + 1));
                    break;
            }


            return new[] { aggregateX, aggregateY };
        }
    }
}