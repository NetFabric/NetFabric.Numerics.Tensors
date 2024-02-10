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
/// <typeparam name="T">The type of the value.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface IUnaryOperator<T, TResult> 
    : IOperator
    where T : struct
    where TResult : struct
{
    /// <summary>
    /// Applies the unary operator to the specified value.
    /// </summary>
    /// <param name="x">The value to apply the operator to.</param>
    /// <returns>The result of applying the operator to the value.</returns>
    static abstract TResult Invoke(T x);

    /// <summary>
    /// Applies the unary operator to the specified vector.
    /// </summary>
    /// <param name="x">The vector to apply the operator to.</param>
    /// <returns>The result of applying the operator to the vector.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<T> x);
}

/// <summary>
/// Represents a binary operator that operates on two values.
/// </summary>
/// <typeparam name="T1">The type of the first value.</typeparam>
/// <typeparam name="T2">The type of the second value.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface IBinaryOperator<T1, T2, TResult> 
    : IOperator
    where T1 : struct
    where T2 : struct
    where TResult : struct
{
    /// <summary>
    /// Applies the binary operator to the specified values.
    /// </summary>
    /// <param name="x">The first value to apply the operator to.</param>
    /// <param name="y">The second value to apply the operator to.</param>
    /// <returns>The result of applying the operator to the values.</returns>
    static abstract TResult Invoke(T1 x, T2 y);

    /// <summary>
    /// Applies the binary operator to the specified vectors.
    /// </summary>
    /// <param name="x">The first vector to apply the operator to.</param>
    /// <param name="y">The second vector to apply the operator to.</param>
    /// <returns>The result of applying the operator to the vectors.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<T1> x, ref readonly Vector<T2> y);
}

/// <summary>
/// Represents a binary operator that operates on two values.
/// </summary>
/// <typeparam name="T1">The type of the first value.</typeparam>
/// <typeparam name="T2">The type of the second value.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface IGenericBinaryOperator<T1, T2, TResult> 
    : IOperator
    where T1 : struct
    where TResult : struct
{
    /// <summary>
    /// Applies the binary operator to the specified values.
    /// </summary>
    /// <param name="x">The first value to apply the operator to.</param>
    /// <param name="y">The second value to apply the operator to.</param>
    /// <returns>The result of applying the operator to the values.</returns>
    static abstract TResult Invoke(T1 x, T2 y);

    /// <summary>
    /// Applies the binary operator to the specified vectors.
    /// </summary>
    /// <param name="x">The first vector to apply the operator to.</param>
    /// <param name="y">The second vector to apply the operator to.</param>
    /// <returns>The result of applying the operator to the vectors.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<T1> x, T2 y);
}

/// <summary>
/// Represents a ternary operator that operates on three values.
/// </summary>
/// <typeparam name="T1">The type of the first value.</typeparam>
/// <typeparam name="T2">The type of the second value.</typeparam>
/// <typeparam name="T3">The type of the third value.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface ITernaryOperator<T1, T2, T3, TResult> 
    : IOperator
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where TResult : struct
{
    /// <summary>
    /// Applies the ternary operator to the specified values.
    /// </summary>
    /// <param name="x">The first value to apply the operator to.</param>
    /// <param name="y">The second value to apply the operator to.</param>
    /// <param name="z">The third value to apply the operator to.</param>
    /// <returns>The result of applying the operator to the values.</returns>
    static abstract TResult Invoke(T1 x, T2 y, T3 z);

    /// <summary>
    /// Applies the ternary operator to the specified vectors.
    /// </summary>
    /// <param name="x">The first vector to apply the operator to.</param>
    /// <param name="y">The second vector to apply the operator to.</param>
    /// <param name="z">The third vector to apply the operator to.</param>
    /// <returns>The result of applying the operator to the vectors.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<T1> x, ref readonly Vector<T2> y, ref readonly Vector<T3> z);
}

/// <summary>
/// Represents an aggregation operator that operates on two values.
/// </summary>
/// <typeparam name="T">The type of the values.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface IAggregationOperator<T, TResult> 
    : IBinaryOperator<TResult, T, TResult>
    where T : struct
    where TResult : struct
{
    /// <summary>
    /// Gets the seed value used to initialize the aggregation.
    /// </summary>
    /// <returns>The seed value.</returns>
    static virtual TResult Seed
        => Throw.NotSupportedException<TResult>();

    /// <summary>
    /// Applies the binary operator to the specified values.
    /// </summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The result of applying the operator to the values.</returns>
    static abstract TResult Invoke(TResult x, TResult y);
}
