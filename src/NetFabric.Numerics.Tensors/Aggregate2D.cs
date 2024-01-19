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
            var partialX0 = TOperator.Identity;
            var partialY0 = TOperator.Identity;
            var partialX1 = TOperator.Identity;
            var partialY1 = TOperator.Identity;
            for (; sourceIndex + 3 < source.Length; sourceIndex += 4)
            {
                partialX0 = TOperator.Invoke(partialX0, Unsafe.Add(ref sourceRef, sourceIndex));
                partialY0 = TOperator.Invoke(partialY0, Unsafe.Add(ref sourceRef, sourceIndex + 1));
                partialX1 = TOperator.Invoke(partialX1, Unsafe.Add(ref sourceRef, sourceIndex + 2));
                partialY1 = TOperator.Invoke(partialY1, Unsafe.Add(ref sourceRef, sourceIndex + 3));
            }

            switch(source.Length - sourceIndex)
            {
                case 2:
                    partialX0 = TOperator.Invoke(partialX0, Unsafe.Add(ref sourceRef, sourceIndex));
                    partialY0 = TOperator.Invoke(partialY0, Unsafe.Add(ref sourceRef, sourceIndex + 1));
                    break;
            }

            aggregateX = TOperator.Invoke(aggregateX, partialX0);
            aggregateY = TOperator.Invoke(aggregateY, partialY0);
            aggregateX = TOperator.Invoke(aggregateX, partialX1);
            aggregateY = TOperator.Invoke(aggregateY, partialY1);

            return new[] { aggregateX, aggregateY };
        }
    }
}