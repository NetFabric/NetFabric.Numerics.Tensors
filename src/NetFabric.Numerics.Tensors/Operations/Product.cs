namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    /// <summary>
    /// Computes the product of all elements in the specified <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to compute the product of.</param>
    /// <returns>The product of all elements in the <see cref="ReadOnlySpan{T}"/>.</returns>
    /// <remarks>
    /// If the <see cref="ReadOnlySpan{T}"/> is empty, <see langword="null"/> is returned.
    /// </remarks>
    public static T? Product<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => source.IsEmpty 
            ? null
            : Tensor.AggregateNumber<T, ProductOperator<T>>(source);

    /// <summary>
    /// Computes the product of all elements in the specified 2D <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="source">The 2D <see cref="ReadOnlySpan{T}"/> to compute the product of.</param>
    /// <returns>The product of all elements in the 2D <see cref="ReadOnlySpan{T}"/>.</returns>
    /// <remarks>
    /// If the <see cref="ReadOnlySpan{T}"/> is empty, <see langword="null"/> is returned.
    /// </remarks>
    public static (T, T)? Product2D<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => source.IsEmpty 
            ? null
            : Tensor.AggregateNumber2D<T, ProductOperator<T>>(source);

    /// <summary>
    /// Computes the product of all elements in the specified 3D <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="source">The 3D <see cref="ReadOnlySpan{T}"/> to compute the product of.</param>
    /// <returns>The product of all elements in the 3D <see cref="ReadOnlySpan{T}"/>.</returns>
    /// <remarks>
    /// If the <see cref="ReadOnlySpan{T}"/> is empty, <see langword="null"/> is returned.
    /// </remarks>
    public static (T, T, T)? Product3D<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => source.IsEmpty 
            ? null
            : Tensor.AggregateNumber3D<T, ProductOperator<T>>(source);

    /// <summary>
    /// Computes the product of all elements in the specified 4D <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="source">The 4D <see cref="ReadOnlySpan{T}"/> to compute the product of.</param>
    /// <returns>The product of all elements in the 4D <see cref="ReadOnlySpan{T}"/>.</returns>
    /// <remarks>
    /// If the <see cref="ReadOnlySpan{T}"/> is empty, <see langword="null"/> is returned.
    /// </remarks>
    public static (T, T, T, T)? Product4D<T>(ReadOnlySpan<T> source)
        where T : struct, IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T>
        => source.IsEmpty 
            ? null
            : Tensor.AggregateNumber4D<T, ProductOperator<T>>(source);

    /// <summary>
    /// Computes the product of the element-wise additions of two <see cref="ReadOnlySpan{T}"/> instances.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="x">The first <see cref="ReadOnlySpan{T}"/> to compute the product of additions.</param>
    /// <param name="y">The second <see cref="ReadOnlySpan{T}"/> to compute the product of additions.</param>
    /// <returns>The product of the element-wise additions of the two <see cref="ReadOnlySpan{T}"/> instances.</returns>
    /// <remarks>
    /// If the <paramref name="x"/> <see cref="ReadOnlySpan{T}"/> is empty, <see langword="null"/> is returned.
    /// </remarks>
    public static T? ProductOfAdditions<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber<T, AddOperator<T>, ProductOperator<T>>(x, y);

    /// <summary>
    /// Computes the product of the element-wise additions of two 2D <see cref="ReadOnlySpan{T}"/> instances.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="x">The first 2D <see cref="ReadOnlySpan{T}"/> to compute the product of additions.</param>
    /// <param name="y">The second 2D <see cref="ReadOnlySpan{T}"/> to compute the product of additions.</param>
    /// <returns>The product of the element-wise additions of the two 2D <see cref="ReadOnlySpan{T}"/> instances.</returns>
    /// <remarks>
    /// If the <paramref name="x"/> <see cref="ReadOnlySpan{T}"/> is empty, <see langword="null"/> is returned.
    /// </remarks>
    public static (T, T)? ProductOfAdditions2D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber2D<T, AddOperator<T>, ProductOperator<T>>(x, y);

    /// <summary>
    /// Computes the product of the element-wise additions of two 3D <see cref="ReadOnlySpan{T}"/> instances.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="x">The first 3D <see cref="ReadOnlySpan{T}"/> to compute the product of additions.</param>
    /// <param name="y">The second 3D <see cref="ReadOnlySpan{T}"/> to compute the product of additions.</param>
    /// <returns>The product of the element-wise additions of the two 3D <see cref="ReadOnlySpan{T}"/> instances.</returns>
    /// <remarks>
    /// If the <paramref name="x"/> <see cref="ReadOnlySpan{T}"/> is empty, <see langword="null"/> is returned.
    /// </remarks>
    public static (T, T, T)? ProductOfAdditions3D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber3D<T, AddOperator<T>, ProductOperator<T>>(x, y);

    /// <summary>
    /// Computes the product of the element-wise additions of two 4D <see cref="ReadOnlySpan{T}"/> instances.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="x">The first 4D <see cref="ReadOnlySpan{T}"/> to compute the product of additions.</param>
    /// <param name="y">The second 4D <see cref="ReadOnlySpan{T}"/> to compute the product of additions.</param>
    /// <returns>The product of the element-wise additions of the two 4D <see cref="ReadOnlySpan{T}"/> instances.</returns>
    /// <remarks>
    /// If the <paramref name="x"/> <see cref="ReadOnlySpan{T}"/> is empty, <see langword="null"/> is returned.
    /// </remarks>
    public static (T, T, T, T)? ProductOfAdditions4D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber4D<T, AddOperator<T>, ProductOperator<T>>(x, y);

    /// <summary>
    /// Computes the product of the element-wise subtractions of two <see cref="ReadOnlySpan{T}"/> instances.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="x">The first <see cref="ReadOnlySpan{T}"/> to compute the product of subtractions.</param>
    /// <param name="y">The second <see cref="ReadOnlySpan{T}"/> to compute the product of subtractions.</param>
    /// <returns>The product of the element-wise subtractions of the two <see cref="ReadOnlySpan{T}"/> instances.</returns>
    /// <remarks>
    /// If the <paramref name="x"/> <see cref="ReadOnlySpan{T}"/> is empty, <see langword="null"/> is returned.
    /// </remarks>
    public static T? ProductOfSubtractions<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber<T, SubtractOperator<T>, ProductOperator<T>>(x, y);

    /// <summary>
    /// Computes the product of the element-wise subtractions of two 2D <see cref="ReadOnlySpan{T}"/> instances.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="x">The first 2D <see cref="ReadOnlySpan{T}"/> to compute the product of subtractions.</param>
    /// <param name="y">The second 2D <see cref="ReadOnlySpan{T}"/> to compute the product of subtractions.</param>
    /// <returns>The product of the element-wise subtractions of the two 2D <see cref="ReadOnlySpan{T}"/> instances.</returns>
    /// <remarks>
    /// If the <paramref name="x"/> <see cref="ReadOnlySpan{T}"/> is empty, <see langword="null"/> is returned.
    /// </remarks>
    public static (T, T)? ProductOfSubtractions2D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber2D<T, SubtractOperator<T>, ProductOperator<T>>(x, y);

    /// <summary>
    /// Computes the product of the element-wise subtractions of two 3D <see cref="ReadOnlySpan{T}"/> instances.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="x">The first 3D <see cref="ReadOnlySpan{T}"/> to compute the product of subtractions.</param>
    /// <param name="y">The second 3D <see cref="ReadOnlySpan{T}"/> to compute the product of subtractions.</param>
    /// <returns>The product of the element-wise subtractions of the two 3D <see cref="ReadOnlySpan{T}"/> instances.</returns>
    /// <remarks>
    /// If the <paramref name="x"/> <see cref="ReadOnlySpan{T}"/> is empty, <see langword="null"/> is returned.
    /// </remarks>
    public static (T, T, T)? ProductOfSubtractions3D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber3D<T, SubtractOperator<T>, ProductOperator<T>>(x, y);

    /// <summary>
    /// Computes the product of the element-wise subtractions of two 4D <see cref="ReadOnlySpan{T}"/> instances.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="x">The first 4D <see cref="ReadOnlySpan{T}"/> to compute the product of subtractions.</param>
    /// <param name="y">The second 4D <see cref="ReadOnlySpan{T}"/> to compute the product of subtractions.</param>
    /// <returns>The product of the element-wise subtractions of the two 4D <see cref="ReadOnlySpan{T}"/> instances.</returns>
    /// <remarks>
    /// If the <paramref name="x"/> <see cref="ReadOnlySpan{T}"/> is empty, <see langword="null"/> is returned.
    /// </remarks>
    public static (T, T, T, T)? ProductOfSubtractions4D<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y)
        where T : struct, IMultiplicativeIdentity<T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, T, T>
        => x.IsEmpty 
            ? null
            : Tensor.AggregateNumber4D<T, SubtractOperator<T>, ProductOperator<T>>(x, y);
}