namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    public static T? Product<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => source.IsEmpty 
            ? null
            : Tensor.AggregateNumber<T, ProductOperator<T>>(source);

    public static (T, T)? Product2D<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => source.IsEmpty 
            ? null
            : Tensor.AggregateNumber2D<T, ProductOperator<T>>(source);

    public static (T, T, T)? Product3D<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => source.IsEmpty 
            ? null
            : Tensor.AggregateNumber3D<T, ProductOperator<T>>(source);

    public static (T, T, T, T)? Product4D<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => source.IsEmpty 
            ? null
            : Tensor.AggregateNumber4D<T, ProductOperator<T>>(source);

    public static T? ProductOfAdditions<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber<T, AddOperator<T>, ProductOperator<T>>(x, y);

    public static (T, T)? ProductOfAdditions2D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber2D<T, AddOperator<T>, ProductOperator<T>>(x, y);

    public static (T, T, T)? ProductOfAdditions3D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber3D<T, AddOperator<T>, ProductOperator<T>>(x, y);

    public static (T, T, T, T)? ProductOfAdditions4D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber4D<T, AddOperator<T>, ProductOperator<T>>(x, y);

    public static T? ProductOfSubtractions<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber<T, SubtractOperator<T>, ProductOperator<T>>(x, y);

    public static (T, T)? ProductOfSubtractions2D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber2D<T, SubtractOperator<T>, ProductOperator<T>>(x, y);

    public static (T, T, T)? ProductOfSubtractions3D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber3D<T, SubtractOperator<T>, ProductOperator<T>>(x, y);

    public static (T, T, T, T)? ProductOfSubtractions4D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber4D<T, SubtractOperator<T>, ProductOperator<T>>(x, y);

}