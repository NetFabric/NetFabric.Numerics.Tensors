namespace NetFabric.Numerics.Tensors;

public static partial class TensorOperations
{
    
    /// <summary>
    /// Computes the average of a span of numbers.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="source">The span of numbers.</param>
    /// <returns>The average of the numbers.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the span is empty.</exception>
    /// <remarks>
    /// <para>
    /// This methods follows the IEEE 754 standard for floating-point arithmetic, it returns NaN if the transformation and aggregation of any of the elements result in NaN.
    /// </para>
    /// <para>
    /// If the <paramref name="source"/> is empty, <see langword="null"/> is returned.
    /// </para>
    /// </remarks>
    public static T? Average<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IDivisionOperators<T, T, T>
        => source.Length is 0
            ? null
            : Sum(source) / T.CreateChecked(source.Length);

    /// <summary>
    /// Computes the average of a span of numbers, treating NaN values as zero.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="source">The span of numbers.</param>
    /// <returns>The average of the numbers.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the span is empty.</exception>
    /// <remarks>
    /// <para>
    /// This methods does not propagate NaN.
    /// </para>
    /// <para>
    /// If the <paramref name="source"/> is empty, <see langword="null"/> is returned.
    /// </para>
    /// </remarks>
    public static T? AverageNumber<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IDivisionOperators<T, T, T>
        => source.Length is 0
            ? null
            : SumNumber(source) / T.CreateChecked(source.Length);

    /// <summary>
    /// Computes the average of a span of 2D numbers.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="source">The span of numbers.</param>
    /// <returns>The average of the numbers as a tuple of two values.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the span is empty.</exception>
    /// <remarks>
    /// <para>
    /// This methods does not propagate NaN.
    /// </para>
    /// <para>
    /// If the <paramref name="source"/> is empty, <see langword="null"/> is returned.
    /// </para>
    /// </remarks>
    public static (T, T)? Average2D<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IDivisionOperators<T, T, T>
    {
        if (source.Length is 0)
            return default;

        var result = Sum2D(source);
        var count = T.CreateChecked(source.Length);
        result.Item1 /= count;
        result.Item2 /= count;
        return result;
    }

    /// <summary>
    /// Computes the average of a span of 3D numbers.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="source">The span of numbers.</param>
    /// <returns>The average of the numbers as a tuple of three values.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the span is empty.</exception>
    /// <remarks>
    /// <para>
    /// This methods does not propagate NaN.
    /// </para>
    /// <para>
    /// If the <paramref name="source"/> is empty, <see langword="null"/> is returned.
    /// </para>
    /// </remarks>
    public static (T, T, T)? Average3D<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IDivisionOperators<T, T, T>
    {
        if (source.Length is 0)
            return default;

        var result = Sum3D(source);
        var count = T.CreateChecked(source.Length);
        result.Item1 /= count;
        result.Item2 /= count;
        result.Item3 /= count;
        return result;
    }

    /// <summary>
    /// Computes the average of a span of 4D numbers.
    /// </summary>
    /// <typeparam name="T">The type of the numbers.</typeparam>
    /// <param name="source">The span of numbers.</param>
    /// <returns>The average of the numbers as a tuple of four values.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the span is empty.</exception>
    /// <remarks>
    /// <para>
    /// This methods does not propagate NaN.
    /// </para>
    /// <para>
    /// If the <paramref name="source"/> is empty, <see langword="null"/> is returned.
    /// </para>
    /// </remarks>
    public static (T, T, T, T) Average4D<T>(ReadOnlySpan<T> source)
        where T : struct, INumberBase<T>, IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>, IDivisionOperators<T, T, T>
    {
        if (source.Length is 0)
            return default;

        var result = Sum4D(source);
        var count = T.CreateChecked(source.Length);
        result.Item1 /= count;
        result.Item2 /= count;
        result.Item3 /= count;
        result.Item4 /= count;
        return result;
    }
}