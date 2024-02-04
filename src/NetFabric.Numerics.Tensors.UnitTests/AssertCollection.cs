namespace NetFabric.Numerics.Tensors.UnitTests;

static class AssertCollection
{
    public static void AreEqual<T>(ReadOnlySpan<T> expected, ReadOnlySpan<T> actual, T tolerance)
        where T : struct, IFloatingPoint<T>
    {
        if (expected.Length != actual.Length)
            throw new Exception("The lengths of the collections are different.");

        for (var index = 0; index < expected.Length; index++)
        {
            if (T.Abs(expected[index] - actual[index]) > tolerance)
                throw new Exception($"The elements at index {index} are different. Expected: {expected[index]}, Actual: {actual[index]}.");
        }
    }
}