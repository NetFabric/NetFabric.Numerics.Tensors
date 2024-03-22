namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static T SumOfSquares<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>
        => Tensor.Aggregate<T, SquareOperator<T>, SumOperator<T>>(source);

    public static T SumOfSquaresNumber<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplyOperators<T, T, T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.AggregateNumber<T, SquareOperator<T>, SumOperator<T>>(source);
}