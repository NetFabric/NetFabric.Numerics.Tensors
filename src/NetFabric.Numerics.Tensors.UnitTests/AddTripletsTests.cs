namespace NetFabric.Numerics.Tensors.UnitTests;

public class AddTripletsTests
{
    public static TheoryData<int> AddData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void Add_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var x = new MyVector3<T>[count];
        var y = new MyVector3<T>[count];
        var result = new MyVector3<T>[count];
        var expected = new MyVector3<T>[count];
        var random = new Random(42);
        for (var index = 0; index < count; index++)
        { 
            var value = random.Next(100);
            x[index] = new(T.CreateChecked(value), T.CreateChecked(value + 1), T.CreateChecked(value + 2));
            y[index] = new(T.CreateChecked(value + 3), T.CreateChecked(value + 4), T.CreateChecked(value + 5));
            expected[index] = new(
                T.CreateChecked(2 * value + 3), 
                T.CreateChecked(2 * value + 5),
                T.CreateChecked(2 * value + 7));
        }

        // act
        TensorOperations.Add(
            MemoryMarshal.Cast<MyVector3<T>, T>(x),
            MemoryMarshal.Cast<MyVector3<T>, T>(y),
            MemoryMarshal.Cast<MyVector3<T>, T>(result));

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