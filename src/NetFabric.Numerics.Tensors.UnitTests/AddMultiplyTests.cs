namespace NetFabric.Numerics.Tensors.UnitTests;

public class AddMultiplyTests
{
    public static TheoryData<int> AddData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void AddMultiply_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var source = Enumerable.Range(0, count);
        var x = source
            .Select(value => T.CreateChecked(value))
            .ToArray();
        var y = source
            .Select(value => T.CreateChecked(value + 1))
            .ToArray();
        var z = source
            .Select(value => T.CreateChecked(value + 2))
            .ToArray();
        var result = new T[count];
        var expected = source
            .Select(value => T.CreateChecked((value + value + 1) * (value + 2)))
            .ToArray();

        // act
        Tensor.AddMultiply<T>(x, y, z, result);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Short_Should_Succeed(int count)
        => AddMultiply_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Int_Should_Succeed(int count)
        => AddMultiply_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Long_Should_Succeed(int count)
        => AddMultiply_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Half_Should_Succeed(int count)
        => AddMultiply_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Float_Should_Succeed(int count)
        => AddMultiply_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Double_Should_Succeed(int count)
        => AddMultiply_Should_Succeed<double>(count);

}