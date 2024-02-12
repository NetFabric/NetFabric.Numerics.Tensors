namespace NetFabric.Numerics.Tensors.UnitTests;

public class Sum2DTests
{
    [Fact]
    public void Sum_Size_Not_Multiple_Of_2_Throws()
    {
        // arrange
        var source = new short[3];

        // act
        void action() => TensorOperations.Sum2D<short>(source);

        // assert
        Assert.Throws<ArgumentException>("source", action);
    }
    
    public static TheoryData<int> SumData
        => new() { 
            { 0 }, { 2 }, { 4 }, { 6 }, { 8 }, { 10 }, { 100 },
        };

    static void Sum_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var source = new MyVector2<T>[count];
        var expected = MyVector2<T>.AdditiveIdentity;
        var random = new Random(42);
        for (var index = 0; index < source.Length; index++)
        {
            var value = new MyVector2<T>(T.CreateChecked(random.Next(10)), T.CreateChecked(random.Next(10)));
            source[index] = value;
            expected += value;
        }

        // act
        var result = TensorOperations.Sum2D<T>(MemoryMarshal.Cast<MyVector2<T>, T>(source));

        // assert
        Assert.Equal(expected, new MyVector2<T>(result));
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Short_Should_Succeed(int count)
        => Sum_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Int_Should_Succeed(int count)
        => Sum_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Long_Should_Succeed(int count)
        => Sum_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Half_Should_Succeed(int count)
        => Sum_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Float_Should_Succeed(int count)
        => Sum_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Double_Should_Succeed(int count)
        => Sum_Should_Succeed<double>(count);
}