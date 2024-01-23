namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that can be applied to values or vectors.
/// </summary>
public interface IOperator
{
    /// <summary>
    /// Gets a value indicating whether the operator can be vectorized.
    /// </summary>
    static virtual bool IsVectorizable
        => true;
}

/// <summary>
/// Represents a unary operator that operates on a single value.
/// </summary>
/// <typeparam name="TSource">The type of the value.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface IUnaryOperator<TSource, TResult> 
    : IOperator
    where TSource : struct
    where TResult : struct
{
    /// <summary>
    /// Applies the unary operator to the specified value.
    /// </summary>
    /// <param name="x">The value to apply the operator to.</param>
    /// <returns>The result of applying the operator to the value.</returns>
    static abstract TResult Invoke(TSource x);

    /// <summary>
    /// Applies the unary operator to the specified vector.
    /// </summary>
    /// <param name="x">The vector to apply the operator to.</param>
    /// <returns>The result of applying the operator to the vector.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<TSource> x);
}

/// <summary>
/// Represents a binary operator that operates on two values.
/// </summary>
/// <typeparam name="TSource">The type of the values.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface IBinaryOperator<TSource, TResult> 
    : IOperator
    where TSource : struct
    where TResult : struct
{
    /// <summary>
    /// Applies the binary operator to the specified values.
    /// </summary>
    /// <param name="x">The first value to apply the operator to.</param>
    /// <param name="y">The second value to apply the operator to.</param>
    /// <returns>The result of applying the operator to the values.</returns>
    static abstract TResult Invoke(TSource x, TSource y);

    /// <summary>
    /// Applies the binary operator to the specified vectors.
    /// </summary>
    /// <param name="x">The first vector to apply the operator to.</param>
    /// <param name="y">The second vector to apply the operator to.</param>
    /// <returns>The result of applying the operator to the vectors.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<TSource> x, ref readonly Vector<TSource> y);
}

/// <summary>
/// Represents a ternary operator that operates on three values.
/// </summary>
/// <typeparam name="TSource">The type of the values.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface ITernaryOperator<TSource, TResult> 
    : IOperator
    where TSource : struct
    where TResult : struct
{
    /// <summary>
    /// Applies the ternary operator to the specified values.
    /// </summary>
    /// <param name="x">The first value to apply the operator to.</param>
    /// <param name="y">The second value to apply the operator to.</param>
    /// <param name="z">The third value to apply the operator to.</param>
    /// <returns>The result of applying the operator to the values.</returns>
    static abstract TResult Invoke(TSource x, TSource y, TSource z);

    /// <summary>
    /// Applies the ternary operator to the specified vectors.
    /// </summary>
    /// <param name="x">The first vector to apply the operator to.</param>
    /// <param name="y">The second vector to apply the operator to.</param>
    /// <param name="z">The third vector to apply the operator to.</param>
    /// <returns>The result of applying the operator to the vectors.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<TSource> x, ref readonly Vector<TSource> y, ref readonly Vector<TSource> z);
}

/// <summary>
/// Represents an aggregation operator that operates on two values.
/// </summary>
/// <typeparam name="TSource">The type of the values.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface IAggregationOperator<TSource, TResult> 
    : IOperator
    where TSource : struct
    where TResult : struct
{
    /// <summary>
    /// Gets the identity value for the type and operation to be performed.
    /// </summary>
    /// <returns>The identity value.</returns>
    static virtual TResult Identity
        => Throw.NotSupportedException<TResult>();

    /// <summary>
    /// Applies the binary operator to the specified values.
    /// </summary>
    /// <param name="x">The first value to apply the operator to.</param>
    /// <param name="y">The second value to apply the operator to.</param>
    /// <returns>The result of applying the operator to the values.</returns>
    static abstract TResult Invoke(TResult x, TSource y);

    /// <summary>
    /// Applies the binary operator to the specified vectors.
    /// </summary>
    /// <param name="x">The first vector to apply the operator to.</param>
    /// <param name="y">The second vector to apply the operator to.</param>
    /// <returns>The result of applying the operator to the vectors.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<TResult> x, ref readonly Vector<TSource> y);

    /// <summary>
    /// Applies the binary operator to the specified values.
    /// </summary>
    /// <param name="x">The first value to apply the operator to.</param>
    /// <param name="y">The second value to apply the operator to.</param>
    /// <returns>The result of applying the operator to the values.</returns>
    static abstract TResult Invoke(TResult x, TResult y);

    /// <summary>
    /// Applies the binary operator to the specified value and vector.
    /// </summary>
    /// <param name="value">The value to apply the operator to.</param>
    /// <param name="vector">The vector to apply the operator to.</param>
    /// <returns>The result of applying the operator to the value and vector.</returns>
    static abstract TResult Invoke(TResult value, ref readonly Vector<TResult> vector);
}
