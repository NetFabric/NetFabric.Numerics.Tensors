namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static T Sum<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>
        => Tensor.Aggregate<T, SumOperator<T>>(source);

    public static T SumNumber<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.AggregateNumber<T, SumOperator<T>>(source);

    public static (T, T) Sum2D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.AggregateNumber2D<T, SumOperator<T>>(source);

    public static (T, T, T) Sum3D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.AggregateNumber3D<T, SumOperator<T>>(source);

    public static (T, T, T, T) Sum4D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.AggregateNumber4D<T, SumOperator<T>>(source);
}