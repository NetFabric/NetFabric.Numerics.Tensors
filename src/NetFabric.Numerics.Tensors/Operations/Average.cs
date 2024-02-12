namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static T Average<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IDivisionOperators<T, T, T>
        => source.Length is 0
            ? Throw.InvalidOperationException<T>()
            : Sum(source) / T.CreateChecked(source.Length);

    public static ValueTuple<T, T> Average2D<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IDivisionOperators<T, T, T>
    {
        if (source.Length is 0)
            Throw.InvalidOperationException();

        var result = Sum2D(source);
        var count = T.CreateChecked(source.Length);
        result.Item1 /= count;
        result.Item2 /= count;
        return result;
    }

    public static ValueTuple<T, T, T> Average3D<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IDivisionOperators<T, T, T>
    {
        if (source.Length is 0)
            Throw.InvalidOperationException();

        var result = Sum3D(source);
        var count = T.CreateChecked(source.Length);
        result.Item1 /= count;
        result.Item2 /= count;
        result.Item3 /= count;
        return result;
    }

    public static ValueTuple<T, T, T, T> Average4D<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IDivisionOperators<T, T, T>
    {
        if (source.Length is 0)
            Throw.InvalidOperationException();

        var result = Sum4D(source);
        var count = T.CreateChecked(source.Length);
        result.Item1 /= count;
        result.Item2 /= count;
        result.Item3 /= count;
        result.Item4 /= count;
        return result;
    }

}