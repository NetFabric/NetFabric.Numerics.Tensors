namespace NetFabric.Numerics;

readonly struct SumOperator<T>
    : IAggregationOperator<T, T>
    where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    public static T Identity 
        => T.AdditiveIdentity;

    public static T Invoke(T value, ref readonly Vector<T> vector)
        => value + Vector.Sum(vector);

    public static T Invoke(T x, T y)
        => x + y;

    public static Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y)
        => x + y;
}