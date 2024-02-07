namespace NetFabric.Numerics.Tensors.UnitTests;

public class AddMultiplyValueTests
{
    public static TheoryData<int> AddData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void AddMultiply_First_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var x = new T[count];
        var y = T.CreateChecked(24);
        var z = new T[count];
        var result = new T[count];
        var expected = new T[count];
        var random = new Random(42);
        for (var index = 0; index < count; index++)
        { 
            var value = random.Next(100);
            x[index] = T.CreateChecked(value);
            z[index] = T.CreateChecked(value + 2);
            expected[index] = T.CreateChecked((value + 24) * (value + 2));
        }

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        Assert.Equal(expected, result);
    }

    static void AddMultiply_Second_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var x = new T[count];
        var y = new T[count];
        var z = T.CreateChecked(42);
        var result = new T[count];
        var expected = new T[count];
        var random = new Random(42);
        for (var index = 0; index < count; index++)
        { 
            var value = random.Next(100);
            x[index] = T.CreateChecked(value);
            y[index] = T.CreateChecked(value + 1);
            expected[index] = T.CreateChecked((value + value + 1) * 42);
        }


        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        Assert.Equal(expected, result);
    }

    static void AddMultiply_Both_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var source = Enumerable.Range(0, count);
        var x = source
            .Select(value => T.CreateChecked(value))
            .ToArray();
        var y = T.CreateChecked(24);
        var z = T.CreateChecked(42);
        var result = new T[count];
        var expected = source
            .Select(value => T.CreateChecked((value + 24) * 42))
            .ToArray();

        // act
        Tensor.AddMultiply(x, y, z, result);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Short_Should_Succeed(int count)
        => AddMultiply_First_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Short_Should_Succeed(int count)
        => AddMultiply_Second_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Short_Should_Succeed(int count)
        => AddMultiply_Both_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Int_Should_Succeed(int count)
        => AddMultiply_First_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Int_Should_Succeed(int count)
        => AddMultiply_Second_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Int_Should_Succeed(int count)
        => AddMultiply_Both_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Long_Should_Succeed(int count)
        => AddMultiply_First_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Long_Should_Succeed(int count)
        => AddMultiply_Second_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Long_Should_Succeed(int count)
        => AddMultiply_Both_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Float_Should_Succeed(int count)
        => AddMultiply_First_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Float_Should_Succeed(int count)
        => AddMultiply_Second_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Float_Should_Succeed(int count)
        => AddMultiply_Both_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_First_Double_Should_Succeed(int count)
        => AddMultiply_First_Should_Succeed<double>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Double_Should_Succeed(int count)
        => AddMultiply_Second_Should_Succeed<double>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Double_Should_Succeed(int count)
        => AddMultiply_Both_Should_Succeed<double>(count);

}