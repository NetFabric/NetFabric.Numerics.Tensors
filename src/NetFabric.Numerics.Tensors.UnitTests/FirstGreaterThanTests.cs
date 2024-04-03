namespace NetFabric.Numerics.Tensors.UnitTests;

public class FirstGreaterThanTests
{
    public static TheoryData<int[], int, int?> FirstGreaterThanData
        => new() {
            { Array.Empty<int>(), 0, null },
            { new[] { 1, 2, 3 }, 0, 1 },
            { new[] { 1, 2, 3 }, 1, 2 },
            { new[] { 1, 2, 3 }, 2, 3 },
            { new[] { 1, 2, 3 }, 3, null },
            { new[] { 0, 1, 0, 0, 2, 0, 0, 3, 0, 0, 4, 0, 0, 5 }, 0, 1 },
            { new[] { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 2, 0, 0, 3 }, 0, 1 },
            { new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 0, 1 },
            { new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0, null },
        };

    [Theory]
    [MemberData(nameof(FirstGreaterThanData))]
    public static void FirstGreaterThan_Should_Succeed(int[] source, int value, int? expected)
    {
        // arrange

        // act
        var result = TensorOperations.FirstGreaterThanNumber(source, value);

        // assert
        Assert.Equal(expected, result);
    }

}