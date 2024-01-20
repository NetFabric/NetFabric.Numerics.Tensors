namespace NetFabric.Numerics;

/// <summary>
/// Represents a unary operator that operates on a single value or vector.
/// </summary>
/// <typeparam name="T">The type of the value or vector.</typeparam>
public interface IUnaryOperator<T>
    where T : struct
{
    /// <summary>
    /// Applies the unary operator to the specified value.
    /// </summary>
    /// <param name="x">The value to apply the operator to.</param>
    /// <returns>The result of applying the operator to the value.</returns>
    static abstract T Invoke(T x);

    /// <summary>
    /// Applies the unary operator to the specified vector.
    /// </summary>
    /// <param name="x">The vector to apply the operator to.</param>
    /// <returns>The result of applying the operator to the vector.</returns>
    static abstract Vector<T> Invoke(ref readonly Vector<T> x);
}

/// <summary>
/// Represents a binary operator that operates on two values or vectors.
/// </summary>
/// <typeparam name="T">The type of the values or vectors.</typeparam>
public interface IBinaryOperator<T>
    where T : struct
{
    /// <summary>
    /// Applies the binary operator to the specified values.
    /// </summary>
    /// <param name="x">The first value to apply the operator to.</param>
    /// <param name="y">The second value to apply the operator to.</param>
    /// <returns>The result of applying the operator to the values.</returns>
    static abstract T Invoke(T x, T y);

    /// <summary>
    /// Applies the binary operator to the specified vectors.
    /// </summary>
    /// <param name="x">The first vector to apply the operator to.</param>
    /// <param name="y">The second vector to apply the operator to.</param>
    /// <returns>The result of applying the operator to the vectors.</returns>
    static abstract Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y);
}

/// <summary>
/// Represents a ternary operator that operates on three values or vectors.
/// </summary>
/// <typeparam name="T">The type of the values or vectors.</typeparam>
public interface ITernaryOperator<T>
    where T : struct
{
    /// <summary>
    /// Applies the ternary operator to the specified values.
    /// </summary>
    /// <param name="x">The first value to apply the operator to.</param>
    /// <param name="y">The second value to apply the operator to.</param>
    /// <param name="z">The third value to apply the operator to.</param>
    /// <returns>The result of applying the operator to the values.</returns>
    static abstract T Invoke(T x, T y, T z);

    /// <summary>
    /// Applies the ternary operator to the specified vectors.
    /// </summary>
    /// <param name="x">The first vector to apply the operator to.</param>
    /// <param name="y">The second vector to apply the operator to.</param>
    /// <param name="z">The third vector to apply the operator to.</param>
    /// <returns>The result of applying the operator to the vectors.</returns>
    static abstract Vector<T> Invoke(ref readonly Vector<T> x, ref readonly Vector<T> y, ref readonly Vector<T> z);
}

/// <summary>
/// Represents an aggregation operator that operates on two values or vectors.
/// </summary>
/// <typeparam name="T">The type of the values or vectors.</typeparam>
public interface IAggregationOperator<T>
    : IBinaryOperator<T>
    where T : struct
{
    /// <summary>
    /// Gets the identity value for the type and operation to be performed.
    /// </summary>
    static virtual T Identity
        => Throw.NotSupportedException<T>();

    /// <summary>
    /// Combines the specified value with the vector to produce a new value.
    /// </summary>
    /// <param name="value">The current value.</param>
    /// <param name="vector">The vector to combine with the value.</param>
    /// <returns>The result of combining the value with the vector.</returns>
    static abstract T Invoke(T value, ref readonly Vector<T> vector);
}
