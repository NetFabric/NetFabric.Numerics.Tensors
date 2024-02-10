namespace NetFabric.Numerics;

/// <summary>
/// Represents an operator that can receive values or vectors.
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
/// Represents a unary operator that receives a single value.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface IUnaryOperator<T, TResult> 
    : IOperator
    where T : struct
    where TResult : struct
{
    /// <summary>
    /// Invokes the unary operator using the specified value.
    /// </summary>
    /// <param name="x">The value to use.</param>
    /// <returns>The result of using the operator.</returns>
    static abstract TResult Invoke(T x);

    /// <summary>
    /// Invokes the unary operator using the specified vector.
    /// </summary>
    /// <param name="x">The vector to use.</param>
    /// <returns>The result of using the operator.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<T> x);
}

/// <summary>
/// Represents a binary operator that receives two values.
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
    /// Invokes the binary operator using the specified values.
    /// </summary>
    /// <param name="x">The first value to use.</param>
    /// <param name="y">The second value to use.</param>
    /// <returns>The result of using the operator.</returns>
    static abstract TResult Invoke(T1 x, T2 y);

    /// <summary>
    /// Invokes the binary operator using the specified vectors.
    /// </summary>
    /// <param name="x">The first vector to use.</param>
    /// <param name="y">The second vector to use.</param>
    /// <returns>The result of using the operator.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<T1> x, ref readonly Vector<T2> y);
}

/// <summary>
/// Represents a binary operator that receives two values where the second is always a scalar.
/// </summary>
/// <typeparam name="T1">The type of the first value.</typeparam>
/// <typeparam name="T2">The type of the second value.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface IBinaryScalarOperator<T1, T2, TResult> 
    : IOperator
    where T1 : struct
    where TResult : struct
{
    /// <summary>
    /// Invokes the binary operator using the specified values.
    /// </summary>
    /// <param name="x">The first value to use.</param>
    /// <param name="y">The second value to use.</param>
    /// <returns>The result of using the operator.</returns>
    static abstract TResult Invoke(T1 x, T2 y);

    /// <summary>
    /// Invokes the binary operator using the specified vector and scalar.
    /// </summary>
    /// <param name="x">The vector to use.</param>
    /// <param name="y">The scalar to use.</param>
    /// <returns>The result of using the operator.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<T1> x, T2 y);
}

/// <summary>
/// Represents a ternary operator that receives three values.
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
    /// Invokes the ternary operator using the specified values.
    /// </summary>
    /// <param name="x">The first value to use.</param>
    /// <param name="y">The second value to use.</param>
    /// <param name="z">The third value to use.</param>
    /// <returns>The result of using the operator.</returns>
    static abstract TResult Invoke(T1 x, T2 y, T3 z);

    /// <summary>
    /// Invokes the ternary operator using the specified vectors.
    /// </summary>
    /// <param name="x">The first vector to use.</param>
    /// <param name="y">The second vector to use.</param>
    /// <param name="z">The third vector to use.</param>
    /// <returns>The result of using the operator.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<T1> x, ref readonly Vector<T2> y, ref readonly Vector<T3> z);
}

/// <summary>
/// Represents an aggregation operator that receives two values.
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
    /// Invokes the binary operator using the specified values.
    /// </summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The result of using the operator.</returns>
    static abstract TResult Invoke(TResult x, TResult y);
}
