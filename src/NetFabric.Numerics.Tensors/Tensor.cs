namespace NetFabric.Numerics;

public static partial class Tensor
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool SpansOverlapAndAreNotSame<T>(ReadOnlySpan<T> span1, ReadOnlySpan<T> span2)
        => !Unsafe.AreSame(ref MemoryMarshal.GetReference(span1), ref MemoryMarshal.GetReference(span2)) && span1.Overlaps(span2);

    static bool IsPowerOfTwo(this int number)
        => (number > 0) && ((number & (number - 1)) is 0);

    static Vector<T> GetVector<T>(ValueTuple<T, T> tuple)
        where T : struct
    {
        var array = new T[Vector<T>.Count];
        ref var resultRef = ref MemoryMarshal.GetReference<T>(array);
        for (var indexVector = nint.Zero; indexVector + 1 < array.Length; indexVector += 2)
        {
            Unsafe.Add(ref resultRef, indexVector) = tuple.Item1;
            Unsafe.Add(ref resultRef, indexVector + 1) = tuple.Item2;
        }
        return new Vector<T>(array);
    }
    
    static ReadOnlySpan<Vector<T>> GetVectors<T>(int tupleSize, T value)
        where T : struct
    {
        var results = new Vector<T>[tupleSize].AsSpan();
        foreach (ref var result in results)
            result = new Vector<T>(value);
        return results;
    }

    static ReadOnlySpan<Vector<T>> GetVectors<T>(ReadOnlySpan<T> values)
        where T : struct
    {
        var results = new Vector<T>[values.Length].AsSpan();
        var indexValue = nint.Zero;
        ref var valuesRef = ref MemoryMarshal.GetReference<T>(values);
        foreach (ref var result in results)
            result = GetVector(ref valuesRef, ref indexValue, values.Length);
        return results;

        static Vector<T> GetVector<T>(ref T values, ref nint indexValue, int indexCount)
            where T : struct
        {
            var array = new T[Vector<T>.Count].AsSpan();
            foreach (ref var value in array)
            {
                value = Unsafe.Add(ref values, indexValue);
                indexValue++;
                if (indexValue == indexCount)
                    indexValue = nint.Zero;
            }
            return new Vector<T>(array);
        }
    }

}