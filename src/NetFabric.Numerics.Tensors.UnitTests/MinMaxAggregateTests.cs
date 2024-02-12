namespace NetFabric.Numerics.Tensors.UnitTests;

public class MinMaxAggregateTests
{
    public static TheoryData<int> MinMaxData
        => new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void MinMax_Should_Succeed<T>(int count)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        // arrange
        var source = new T[count];
        var expectedMin = T.MaxValue;
        var expectedMax = T.MinValue;
        var random = new Random();
        for (var index = 0; index < source.Length; index++)
        {
            var value = T.CreateChecked(random.Next(100) - 50);
            source[index] = value;
            if (expectedMin > value)
                expectedMin = value;
            if (expectedMax < value)
                expectedMax = value;
        }
        var expected = (expectedMin, expectedMax);

        // act
        var result = TensorOperations.MinMax<T>(source);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(MinMaxData))]
    public void MinMax_Short_Should_Succeed(int count)
        => MinMax_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(MinMaxData))]
    public void MinMax_Int_Should_Succeed(int count)
        => MinMax_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(MinMaxData))]
    public void MinMax_Long_Should_Succeed(int count)
        => MinMax_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(MinMaxData))]
    public void MinMax_Half_Should_Succeed(int count)
        => MinMax_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(MinMaxData))]
    public void MinMax_Float_Should_Succeed(int count)
        => MinMax_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(MinMaxData))]
    public void MinMax_Double_Should_Succeed(int count)
        => MinMax_Should_Succeed<float>(count);

}