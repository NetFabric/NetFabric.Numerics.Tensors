namespace NetFabric.Numerics.Tensors.UnitTests;

public class AddMultiplyValueTripletsTests
{
    public static TheoryData<int> AddData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void AddMultiply_First_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var x = new MyVector3<T>[count];
        var y = (T.CreateChecked(24), T.CreateChecked(25), T.CreateChecked(26));
        var z = new MyVector3<T>[count];
        var result = new MyVector3<T>[count];
        var expected = new MyVector3<T>[count];
        var random = new Random(42);
        for (var index = 0; index < count; index++)
        { 
            var value = random.Next(100);
            x[index] = new(T.CreateChecked(value), T.CreateChecked(value + 1), T.CreateChecked(value + 2));
            z[index] = new(T.CreateChecked(value + 3), T.CreateChecked(value + 4), T.CreateChecked(value + 5));
            expected[index] = new(
                T.CreateChecked((value + 24) * (value + 3)), 
                T.CreateChecked((value + 26) * (value + 4)),
                T.CreateChecked((value + 28) * (value + 5)));
        }

        // act
        TensorOperations.AddMultiply(
            MemoryMarshal.Cast<MyVector3<T>, T>(x),
            y,
            MemoryMarshal.Cast<MyVector3<T>, T>(z),
            MemoryMarshal.Cast<MyVector3<T>, T>(result));

        // assert
        Assert.Equal(expected, result);
    }

    static void AddMultiply_Second_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var x = new MyVector3<T>[count];
        var y = new MyVector3<T>[count];
        var z = (T.CreateChecked(42), T.CreateChecked(43), T.CreateChecked(44));
        var result = new MyVector3<T>[count];
        var expected = new MyVector3<T>[count];
        var random = new Random(42);
        for (var index = 0; index < count; index++)
        { 
            var value = random.Next(100);
            x[index] = new(T.CreateChecked(value), T.CreateChecked(value + 1), T.CreateChecked(value + 2));
            y[index] = new(T.CreateChecked(value + 3), T.CreateChecked(value + 4), T.CreateChecked(value + 5));
            expected[index] = new(
                T.CreateChecked((value + value + 3) * 42), 
                T.CreateChecked((value + value + 5) * 43),
                T.CreateChecked((value + value + 7) * 44));
        }

        // act
        TensorOperations.AddMultiply(
            MemoryMarshal.Cast<MyVector3<T>, T>(x),
            MemoryMarshal.Cast<MyVector3<T>, T>(y),
            z,
            MemoryMarshal.Cast<MyVector3<T>, T>(result));

        // assert
        Assert.Equal(expected, result);
    }

    static void AddMultiply_Both_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var x = new MyVector3<T>[count];
        var y = (T.CreateChecked(24), T.CreateChecked(25), T.CreateChecked(26));
        var z = (T.CreateChecked(42), T.CreateChecked(43), T.CreateChecked(44));
        var result = new MyVector3<T>[count];
        var expected = new MyVector3<T>[count];
        var random = new Random(42);
        for (var index = 0; index < count; index++)
        { 
            var value = random.Next(100);
            x[index] = new(T.CreateChecked(value), T.CreateChecked(value + 1), T.CreateChecked(value + 2));
            expected[index] = new(
                T.CreateChecked((value + 24) * 42), 
                T.CreateChecked((value + 26) * 43),
                T.CreateChecked((value + 28) * 44));
        }


        // act
        TensorOperations.AddMultiply(
            MemoryMarshal.Cast<MyVector3<T>, T>(x),
            y,
            z,
            MemoryMarshal.Cast<MyVector3<T>, T>(result));

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
    public void AddMultiply_First_Half_Should_Succeed(int count)
        => AddMultiply_First_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Second_Half_Should_Succeed(int count)
        => AddMultiply_Second_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void AddMultiply_Both_Half_Should_Succeed(int count)
        => AddMultiply_Both_Should_Succeed<Half>(count);

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