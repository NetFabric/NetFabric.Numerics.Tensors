namespace NetFabric.Numerics.Tensors;

/// <summary>
/// Represents an operator that can be applied to the elements of a tensor.
/// </summary>
public interface IOperator
{
    /// <summary>
    /// Gets a value indicating whether the operation can be vectorized.
    /// </summary>
    static virtual bool IsVectorizable => true;
}

/// <summary>
/// Represents an operator that performs an operation with a single parameter.
/// </summary>
/// <typeparam name="T">The type of the parameter.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface IUnaryOperator<T, TResult> 
    : IOperator
    where T : struct
    where TResult : struct
{
    /// <summary>
    /// Performs the unary operation using the specified value.
    /// </summary>
    /// <param name="x">The value to use.</param>
    /// <returns>The result of using the operation.</returns>
    static abstract TResult Invoke(T x);

    /// <summary>
    /// Performs the unary operation using the specified vector.
    /// </summary>
    /// <param name="x">The vector to use.</param>
    /// <returns>The result of using the operation.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<T> x);
}

/// <summary>
/// Represents an operator that performs an operation with two parameters.
/// </summary>
/// <typeparam name="T1">The type of the first parameter.</typeparam>
/// <typeparam name="T2">The type of the second parameter.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface IBinaryOperator<T1, T2, TResult> 
    : IOperator
    where T1 : struct
    where T2 : struct
    where TResult : struct
{
    /// <summary>
    /// Performs the binary operation using the specified values.
    /// </summary>
    /// <param name="x">The first value to use.</param>
    /// <param name="y">The second value to use.</param>
    /// <returns>The result of using the operation.</returns>
    static abstract TResult Invoke(T1 x, T2 y);

    /// <summary>
    /// Performs the binary operation using the specified vectors.
    /// </summary>
    /// <param name="x">The first vector to use.</param>
    /// <param name="y">The second vector to use.</param>
    /// <returns>The result of using the operation.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<T1> x, ref readonly Vector<T2> y);
}

/// <summary>
/// Represents an operator that performs an operation with a vector and a scalar.
/// </summary>
/// <typeparam name="T1">The type of the vector elements.</typeparam>
/// <typeparam name="T2">The type of the scalar.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface IBinaryScalarOperator<T1, T2, TResult> 
    : IOperator
    where T1 : struct
    where TResult : struct
{
    /// <summary>
    /// Performs the binary operation using the specified values.
    /// </summary>
    /// <param name="x">The vector to use.</param>
    /// <param name="y">The scalar to use.</param>
    /// <returns>The result of using the operation.</returns>
    static abstract TResult Invoke(T1 x, T2 y);

    /// <summary>
    /// Performs the binary operation using the specified vector and scalar.
    /// </summary>
    /// <param name="x">The vector to use.</param>
    /// <param name="y">The scalar to use.</param>
    /// <returns>The result of using the operation.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<T1> x, T2 y);
}

/// <summary>
/// Represents an operator that performs an operation with three parameters.
/// </summary>
/// <typeparam name="T1">The type of the first parameter.</typeparam>
/// <typeparam name="T2">The type of the second parameter.</typeparam>
/// <typeparam name="T3">The type of the third parameter.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface ITernaryOperator<T1, T2, T3, TResult> 
    : IOperator
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where TResult : struct
{
    /// <summary>
    /// Performs the ternary operation using the specified values.
    /// </summary>
    /// <param name="x">The first value to use.</param>
    /// <param name="y">The second value to use.</param>
    /// <param name="z">The third value to use.</param>
    /// <returns>The result of using the operation.</returns>
    static abstract TResult Invoke(T1 x, T2 y, T3 z);

    /// <summary>
    /// Performs the ternary operation using the specified vectors.
    /// </summary>
    /// <param name="x">The first vector to use.</param>
    /// <param name="y">The second vector to use.</param>
    /// <param name="z">The third vector to use.</param>
    /// <returns>The result of using the operation.</returns>
    static abstract Vector<TResult> Invoke(ref readonly Vector<T1> x, ref readonly Vector<T2> y, ref readonly Vector<T3> z);
}

/// <summary>
/// Represents an aggregation operator that returns a value.
/// </summary>
/// <typeparam name="T">The type of the source elements.</typeparam>
/// <typeparam name="TResult">The type of the result value.</typeparam>
public interface IAggregationOperator<T, TResult> 
    : IBinaryOperator<TResult, T, TResult>
    where T : struct
    where TResult : struct
{
    /// <summary>
    /// Gets the seed value used to initialize the aggregation.
    /// </summary>
    /// <returns>The seed value.</returns>
    static virtual TResult Seed => Throw.NotSupportedException<TResult>();

    /// <summary>
    /// Performs the binary operatorion using the specified values.
    /// </summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The result of using the operation.</returns>
    static abstract TResult Invoke(TResult x, TResult y);

    /// <summary>
    /// Performs the binary operation using the specified values.
    /// </summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">A vector of values</param>
    /// <returns>The result of using the operation.</returns>
    /// <remarks>It's guaranteed that none of the parameters will contain NaN.</remarks>
    static abstract TResult Invoke(TResult x, ref readonly Vector<TResult> y);
}

/// <summary>
/// Represents operator with one parameter that returns a scalar.
/// </summary>
/// <typeparam name="T">The type of the source elements.</typeparam>
/// <typeparam name="TResult">The type of the result value.</typeparam>
public interface IUnaryToScalarOperator<T, TResult> 
    : IOperator
    where T : struct
{
    /// <summary>
    /// Performs the binary operation using the specified values.
    /// </summary>
    /// <param name="x">The value to use.</param>
    /// <returns>The result of using the operation.</returns>
    static abstract TResult Invoke(T x);

    /// <summary>
    /// Performs the binary operation using the specified vectors.
    /// </summary>
    /// <param name="x">The vector to use.</param>
    /// <returns>The result of using the operation.</returns>
    static abstract TResult Invoke(ref readonly Vector<T> x);
}

/// <summary>
/// Represents operator with two parameters that returns a scalar.
/// </summary>
/// <typeparam name="T1">The type of the first parameter.</typeparam>
/// <typeparam name="T2">The type of the second parameter.</typeparam>
/// <typeparam name="TResult">The type of the result value.</typeparam>
public interface IBinaryToScalarOperator<T1, T2, TResult> 
    : IOperator
    where T1 : struct
    where T2 : struct
{
    /// <summary>
    /// Performs the binary operation using the specified values.
    /// </summary>
    /// <param name="x">The first value to use.</param>
    /// <param name="y">The second value to use.</param>
    /// <returns>The result of using the operation.</returns>
    static abstract TResult Invoke(T1 x, T2 y);

    /// <summary>
    /// Performs the binary operation using the specified vectors.
    /// </summary>
    /// <param name="x">The first vector to use.</param>
    /// <param name="y">The second vector to use.</param>
    /// <returns>The result of using the operation.</returns>
    static abstract TResult Invoke(ref readonly Vector<T1> x, ref readonly Vector<T2> y);
}

/// <summary>
/// Represents operator with three parameters that returns a scalar.
/// </summary>
/// <typeparam name="T1">The type of the first parameter.</typeparam>
/// <typeparam name="T2">The type of the second parameter.</typeparam>
/// <typeparam name="T3">The type of the third parameter.</typeparam>
/// <typeparam name="TResult">The type of the result value.</typeparam>
public interface ITernaryToScalarOperator<T1, T2, T3, TResult> 
    : IOperator
    where T1 : struct
    where T2 : struct
    where T3 : struct
{
    /// <summary>
    /// Performs the binary operation using the specified values.
    /// </summary>
    /// <param name="x">The first value to use.</param>
    /// <param name="y">The second value to use.</param>
    /// <param name="z">The third value to use.</param>
    /// <returns>The result of using the operation.</returns>
    static abstract TResult Invoke(T1 x, T2 y, T3 z);

    /// <summary>
    /// Performs the binary operation using the specified vectors.
    /// </summary>
    /// <param name="x">The first vector to use.</param>
    /// <param name="y">The second vector to use.</param>
    /// <param name="z">The third vector to use.</param>
    /// <returns>The result of using the operation.</returns>
    static abstract TResult Invoke(ref readonly Vector<T1> x, ref readonly Vector<T2> y, ref readonly Vector<T3> z);
}