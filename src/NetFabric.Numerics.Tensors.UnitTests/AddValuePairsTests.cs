namespace NetFabric.Numerics.Tensors.UnitTests;

public class AddValuePairsTests
{
    public static TheoryData<int> AddData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void Add_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var x = new MyVector2<T>[count];
        var y = (T.CreateChecked(42), T.CreateChecked(24));
        var result = new MyVector2<T>[count];
        var expected = new MyVector2<T>[count];
        var random = new Random(42);
        for (var index = 0; index < count; index++)
        { 
            var value = random.Next(100);
            x[index] = new(T.CreateChecked(value), T.CreateChecked(value + 1));
            expected[index] = new(
                T.CreateChecked(value + 42), 
                T.CreateChecked(value + 25));
        }

        // act
        TensorOperations.Add(MemoryMarshal.Cast<MyVector2<T>, T>(x), y, MemoryMarshal.Cast<MyVector2<T>, T>(result));

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Short_Should_Succeed(int count)
        => Add_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Int_Should_Succeed(int count)
        => Add_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Long_Should_Succeed(int count)
        => Add_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Half_Should_Succeed(int count)
        => Add_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Float_Should_Succeed(int count)
        => Add_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(AddData))]
    public void Add_Double_Should_Succeed(int count)
        => Add_Should_Succeed<double>(count);

}