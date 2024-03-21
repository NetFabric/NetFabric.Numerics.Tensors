namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static T Sum<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.Aggregate<T, SumOperator<T>>(source);

    public static (T, T) Sum2D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.Aggregate2D<T, SumOperator<T>>(source);

    public static (T, T, T) Sum3D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.Aggregate3D<T, SumOperator<T>>(source);

    public static (T, T, T, T) Sum4D<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.Aggregate4D<T, SumOperator<T>>(source);

    public static T SumOfSquares<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplyOperators<T, T, T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.Aggregate<T, T, T, SquareOperator<T>, SumOperator<T>>(source);
}