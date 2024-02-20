namespace NetFabric.Numerics.Tensors.UnitTests;

public class ProductOfAdditions2DTests
{
    [Fact]
    public static void ProductOfAdditions2D_Empty_Should_Return_Null()
    {
        // arrange
        var x = Array.Empty<MyVector2<int>>();
        var y = Array.Empty<MyVector2<int>>();

        // act
        var result = TensorOperations.ProductOfAdditions3D<int>(
            MemoryMarshal.Cast<MyVector2<int>, int>(x),
            MemoryMarshal.Cast<MyVector2<int>, int>(y));

        // assert
        Assert.Null(result);
    }

    public static TheoryData<int> ProductOfAdditions2DData
        => new() { 
            { 1 }, { 2 }, { 3 }, { 4 }, { 5 }, { 6 }, { 7 }, { 8 }, { 9 }, { 10 }, { 100 },
        };

    static void ProductOfAdditions2D_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var x = new MyVector2<T>[count];
        var y = new MyVector2<T>[count];
        var expected = new MyVector2<T>(T.One, T.One);
        var random = new Random(42);
        for (var index = 0; index < x.Length; index++)
        {
            var value = new MyVector2<T>(T.CreateChecked(random.Next(10)), T.CreateChecked(random.Next(10)));
            var value2 = new MyVector2<T>(T.CreateChecked(random.Next(10)), T.CreateChecked(random.Next(10)));
            x[index] = value;
            y[index] = value2;
            expected = new((value.X + value2.X) * expected.X, (value.Y + value2.Y) * expected.Y);
        }

        // act
        var result = TensorOperations.ProductOfAdditions2D<T>(
            MemoryMarshal.Cast<MyVector2<T>, T>(x),
            MemoryMarshal.Cast<MyVector2<T>, T>(y));

        // assert
        Assert.Equal(expected, new MyVector2<T>(Assert.NotNull(result)));
    }

    [Theory]
    [MemberData(nameof(ProductOfAdditions2DData))]
    public void ProductOfAdditions2D_Int_Should_Succeed(int count)
        => ProductOfAdditions2D_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(ProductOfAdditions2DData))]
    public void ProductOfAdditions2D_Long_Should_Succeed(int count)
        => ProductOfAdditions2D_Should_Succeed<long>(count);

    // [Theory]
    // [MemberData(nameof(ProductOfAdditions2DData))]
    // public void ProductOfAdditions2D_Float_Should_Succeed(int count)
    //     => ProductOfAdditions2D_Should_Succeed<float>(count);

    // [Theory]
    // [MemberData(nameof(ProductOfAdditions2DData))]
    // public void ProductOfAdditions2D_Double_Should_Succeed(int count)
    //     => ProductOfAdditions2D_Should_Succeed<double>(count);
}