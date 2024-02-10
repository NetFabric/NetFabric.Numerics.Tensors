namespace NetFabric.Numerics.Tensors.UnitTests;

public class MinAggregateTests
{
    public static TheoryData<int> MinData
        => new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void Min_Should_Succeed<T>(int count)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        // arrange
        var source = new T[count];
        var expected = T.MaxValue;
        var random = new Random();
        for (var index = 0; index < source.Length; index++)
        {
            var value = T.CreateChecked(random.Next(100) - 50);
            source[index] = value;
            if (expected > value)
                expected = value;
        }

        // act
        var result = Tensor.Min<T>(source);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(MinData))]
    public void Min_Short_Should_Succeed(int count)
        => Min_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(MinData))]
    public void Min_Int_Should_Succeed(int count)
        => Min_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(MinData))]
    public void Min_Long_Should_Succeed(int count)
        => Min_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(MinData))]
    public void Min_Half_Should_Succeed(int count)
        => Min_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(MinData))]
    public void Min_Float_Should_Succeed(int count)
        => Min_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(MinData))]
    public void Min_Double_Should_Succeed(int count)
        => Min_Should_Succeed<float>(count);

}