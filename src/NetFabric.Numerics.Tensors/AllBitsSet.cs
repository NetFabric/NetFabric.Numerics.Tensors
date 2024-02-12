namespace NetFabric.Numerics.Tensors;

static class AllBitsSet<T>
{
    public static T Value
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
#pragma warning disable IDE0046 // Convert to conditional expression
            if (typeof(T) == typeof(Half))
                return (T)(object)BitConverter.Int16BitsToHalf(-1);
            if (typeof(T) == typeof(float))
                return (T)(object)BitConverter.Int32BitsToSingle(-1);
            if (typeof(T) == typeof(double))
                return (T)(object)BitConverter.Int64BitsToDouble(-1);
            if (typeof(T) == typeof(byte))
                return (T)(object)byte.MaxValue;
            if (typeof(T) == typeof(short))
                return (T)(object)(short)-1;
            if (typeof(T) == typeof(int))
                return (T)(object)-1;
            if (typeof(T) == typeof(long))
                return (T)(object)(long)-1;
            if (typeof(T) == typeof(nint))
                return (T)(object)(nint)(-1);
            if (typeof(T) == typeof(nuint))
                return (T)(object)nuint.MaxValue;
            if (typeof(T) == typeof(sbyte))
                return (T)(object)(sbyte)-1;
            if (typeof(T) == typeof(ushort))
                return (T)(object)ushort.MaxValue;
            if (typeof(T) == typeof(uint))
                return (T)(object)uint.MaxValue;
            if (typeof(T) == typeof(ulong))
                return (T)(object)ulong.MaxValue;
            return Throw.NotSupportedException<T>();
#pragma warning restore IDE0046 // Convert to conditional expression
        }
    }
}
