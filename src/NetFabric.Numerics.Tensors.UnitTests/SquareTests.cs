namespace NetFabric.Numerics.Tensors.UnitTests;

public class SquareTests
{
    public static TheoryData<int> SquareData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void Square_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var source = new T[count];
        var result = new T[count];
        var expected = new T[count];
        var random = new Random(42);
        for (var index = 0; index < source.Length; index++)
        {
            var value = T.CreateChecked(random.Next(100));
            source[index] = value;
            expected[index] = T.CreateChecked(float.Pow(float.CreateChecked(value), 2.0f));
        }

        // act
        Tensor.Square<T>(source, result);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(SquareData))]
    public void Square_Short_Should_Succeed(int count)
        => Square_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(SquareData))]
    public void Square_Int_Should_Succeed(int count)
        => Square_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(SquareData))]
    public void Square_Long_Should_Succeed(int count)
        => Square_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(SquareData))]
    public void Square_Half_Should_Succeed(int count)
        => Square_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(SquareData))]
    public void Square_Float_Should_Succeed(int count)
        => Square_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(SquareData))]
    public void Square_Double_Should_Succeed(int count)
        => Square_Should_Succeed<double>(count);

}