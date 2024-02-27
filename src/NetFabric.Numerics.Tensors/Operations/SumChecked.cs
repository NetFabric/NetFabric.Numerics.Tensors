namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static T SumChecked<T>(ReadOnlySpan<T> source)
        where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
        => Tensor.AggregateChecked<T, SumCheckedOperator<T>>(source);

    //public static ValueTuple<T, T> SumChecked2D<T>(ReadOnlySpan<T> source)
    //    where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
    //    => Tensor.AggregateChecked2D<T, SumCheckedOperator<T>>(source);

    //public static ValueTuple<T, T, T> SumChecked3D<T>(ReadOnlySpan<T> source)
    //    where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
    //    => Tensor.AggregateChecked3D<T, SumCheckedOperator<T>>(source);

    //public static ValueTuple<T, T, T, T> SumChecked4D<T>(ReadOnlySpan<T> source)
    //    where T : struct, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
    //    => Tensor.AggregateChecked4D<T, SumCheckedOperator<T>>(source);
}