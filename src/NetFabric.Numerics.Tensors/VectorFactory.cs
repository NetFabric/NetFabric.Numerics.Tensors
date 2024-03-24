static class VectorFactory
{
    public static Vector<T> Create<T>((T, T) tuple)
    {
        var array = GC.AllocateUninitializedArray<T>(Vector<T>.Count);
        ref var arrayRef = ref MemoryMarshal.GetReference<T>(array);
        for (var index = 0; index + 1 < Vector<T>.Count; index += 2)
        {
            Unsafe.Add(ref arrayRef, index) = tuple.Item1;
            Unsafe.Add(ref arrayRef, index + 1) = tuple.Item2;
        }
        return new(array);
    }
}

static class VectorFactory<T> 
    where T : struct, INumberBase<T> 
{
    public static Vector<T> Indices { get; } = CreateIndicesVector();

    static Vector<T> CreateIndicesVector()
    {
        var array = GC.AllocateUninitializedArray<T>(Vector<T>.Count);
        ref var arrayRef = ref MemoryMarshal.GetReference<T>(array);
        for (var index = 0; index < Vector<T>.Count; index++)
            Unsafe.Add(ref arrayRef, index) = T.CreateChecked(index);
        return new(array);
    }
}