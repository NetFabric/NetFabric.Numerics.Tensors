namespace NetFabric.Numerics.Tensors.UnitTests;

public class IndexOfFirstGreaterThanTests
{
    public static TheoryData<int[], int, int> IndexOfFirstGreaterThanData
        => new() {
            { Array.Empty<int>(), 0, -1 },
            { new[] { 1, 2, 3 }, 0, 0 },
            { new[] { 1, 2, 3 }, 1, 1 },
            { new[] { 1, 2, 3 }, 2, 2 },
            { new[] { 1, 2, 3 }, 3, -1 },
            { new[] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1 }, 0, 1 },
            { new[] { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1 }, 0, 7 },
            { new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 0, 13 },
            { new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0, -1 },
        };

    [Theory]
    [MemberData(nameof(IndexOfFirstGreaterThanData))]
    public static void IndexOfFirstGreaterThan_Should_Succeed(int[] source, int value, int expected)
    {
        // arrange

        // act
        var result = TensorOperations.IndexOfFirstGreaterThan(source, value);

        // assert
        Assert.Equal(expected, result);
    }

}