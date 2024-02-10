namespace NetFabric.Numerics;

readonly struct BitwiseAndOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IBitwiseOperators<T, T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => x & y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x & y;
}

readonly struct BitwiseAndNotOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IBitwiseOperators<T, T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => x & ~y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.AndNot(x, y);
}

readonly struct BitwiseOrOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IBitwiseOperators<T, T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => x | y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x | y;
}

readonly struct XorOperator<T>
    : IBinaryOperator<T, T, T>
    where T : struct, IBitwiseOperators<T, T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x, T y)
        => x ^ y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => Vector.Xor(x, y);
}


readonly struct OnesComplementOperator<T>
    : IUnaryOperator<T, T>
    where T : struct, IBitwiseOperators<T, T, T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Invoke(T x)
        => ~x;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Invoke(ref readonly Vector<T> x)
        => Vector.OnesComplement(x);
}