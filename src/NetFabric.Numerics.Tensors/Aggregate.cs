namespace NetFabric.Numerics
{
    public static partial class Tensor
    {
        public static T Aggregate<T, TOperator>(ReadOnlySpan<T> source)
            where T : struct
            where TOperator : struct, IAggregationOperator<T>
        {
            // initialize aggregate
            var aggregate = TOperator.Identity;
            var sourceIndex = nint.Zero;

            // aggregate using hardware acceleration if available
            if (Vector.IsHardwareAccelerated && Vector<T>.IsSupported)
            {
                // convert source span to vector span without copies
                var sourceVectors = MemoryMarshal.Cast<T, Vector<T>>(source);

                // check if there are multiple vectors to aggregate
                if (sourceVectors.Length > 1)
                {
                    // initialize aggregate vector
                    var resultVector = new Vector<T>(TOperator.Identity);

                    // aggregate the source vectors into the aggregate vector
                    ref var sourceVectorsRef = ref MemoryMarshal.GetReference(sourceVectors);
                    for (var indexVector = nint.Zero; indexVector < sourceVectors.Length; indexVector++)
                    {
                        resultVector = TOperator.Invoke(resultVector, Unsafe.Add(ref sourceVectorsRef, indexVector));
                    }

                    // aggregate the aggregate vector into the aggregate
                    aggregate = TOperator.Invoke(aggregate, resultVector);

                    // skip the source elements already aggregated
                    sourceIndex = source.Length - (source.Length % Vector<T>.Count);
                }
            }

            // aggregate the remaining elements in the source
            ref var sourceRef = ref MemoryMarshal.GetReference(source);
            var remaining = source.Length - (int)sourceIndex;
            if (remaining >= 4)
            {
                var partial1 = TOperator.Identity;
                var partial2 = TOperator.Identity;
                var partial3 = TOperator.Identity;
                for (; sourceIndex + 3 < source.Length; sourceIndex += 4)
                {
                    aggregate = TOperator.Invoke(aggregate, Unsafe.Add(ref sourceRef, sourceIndex));
                    partial1 = TOperator.Invoke(partial1, Unsafe.Add(ref sourceRef, sourceIndex + 1));
                    partial2 = TOperator.Invoke(partial2, Unsafe.Add(ref sourceRef, sourceIndex + 2));
                    partial3 = TOperator.Invoke(partial3, Unsafe.Add(ref sourceRef, sourceIndex + 3));
                }
                aggregate = TOperator.Invoke(aggregate, partial1);
                aggregate = TOperator.Invoke(aggregate, partial2);
                aggregate = TOperator.Invoke(aggregate, partial3);
                remaining = source.Length - (int)sourceIndex;
            }

            switch(remaining)
            {
                case 3:
                    aggregate = TOperator.Invoke(aggregate, Unsafe.Add(ref sourceRef, sourceIndex));
                    aggregate = TOperator.Invoke(aggregate, Unsafe.Add(ref sourceRef, sourceIndex + 1));
                    aggregate = TOperator.Invoke(aggregate, Unsafe.Add(ref sourceRef, sourceIndex + 2));
                    break;
                case 2:
                    aggregate = TOperator.Invoke(aggregate, Unsafe.Add(ref sourceRef, sourceIndex));
                    aggregate = TOperator.Invoke(aggregate, Unsafe.Add(ref sourceRef, sourceIndex + 1));
                    break;
                case 1:
                    aggregate = TOperator.Invoke(aggregate, Unsafe.Add(ref sourceRef, sourceIndex));
                    break;
            }

            return aggregate;
        }
    }
}