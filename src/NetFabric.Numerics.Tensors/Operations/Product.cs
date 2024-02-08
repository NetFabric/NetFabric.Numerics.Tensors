namespace NetFabric.Numerics;

public static partial class Tensor
{
    public static T? Product<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => source.IsEmpty 
            ? null
            : Aggregate<T, ProductOperator<T>>(source);

    public static ValueTuple<T, T>? Product2D<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => source.IsEmpty 
            ? null
            : Aggregate2D<T, ProductOperator<T>>(source);

    public static ValueTuple<T, T, T>? Product3D<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => source.IsEmpty 
            ? null
            : Aggregate3D<T, ProductOperator<T>>(source);

    public static ValueTuple<T, T, T, T>? Product4D<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => source.IsEmpty 
            ? null
            : Aggregate4D<T, ProductOperator<T>>(source);

    public static T? ProductOfAdditions<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Aggregate<T, T, T, AddOperator<T>, ProductOperator<T>>(x, y);

    public static ValueTuple<T, T>? ProductOfAdditions2D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Aggregate2D<T, T, T, AddOperator<T>, ProductOperator<T>>(x, y);

    public static ValueTuple<T, T, T>? ProductOfAdditions3D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Aggregate3D<T, T, T, AddOperator<T>, ProductOperator<T>>(x, y);

    public static ValueTuple<T, T, T, T>? ProductOfAdditions4D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Aggregate4D<T, T, T, AddOperator<T>, ProductOperator<T>>(x, y);

    public static T? ProductOfSubtractions<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Aggregate<T, T, T, SubtractOperator<T>, ProductOperator<T>>(x, y);

    public static ValueTuple<T, T>? ProductOfSubtractions2D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Aggregate2D<T, T, T, SubtractOperator<T>, ProductOperator<T>>(x, y);

    public static ValueTuple<T, T, T>? ProductOfSubtractions3D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Aggregate3D<T, T, T, SubtractOperator<T>, ProductOperator<T>>(x, y);

    public static ValueTuple<T, T, T, T>? ProductOfSubtractions4D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Aggregate4D<T, T, T, SubtractOperator<T>, ProductOperator<T>>(x, y);

}