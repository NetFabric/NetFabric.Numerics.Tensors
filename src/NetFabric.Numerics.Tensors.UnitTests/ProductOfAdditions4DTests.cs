namespace NetFabric.Numerics.Tensors.UnitTests;

public class ProductOfAdditions4DTests
{
    [Fact]
    public static void ProductOfAdditions4D_Empty_Should_Return_Null()
    {
        // arrange
        var x = Array.Empty<MyVector4<int>>();
        var y = Array.Empty<MyVector4<int>>();

        // act
        var result = TensorOperations.ProductOfAdditions4D<int>(
            MemoryMarshal.Cast<MyVector4<int>, int>(x),
            MemoryMarshal.Cast<MyVector4<int>, int>(y));

        // assert
        Assert.Null(result);
    }

    public static TheoryData<int> ProductOfAdditions4DData
        => new() { 
            { 1 }, { 2 }, { 3 }, { 4 }, { 5 }, { 6 }, { 7 }, { 8 }, { 9 }, { 10 }, { 100 },
        };

    static void ProductOfAdditions4D_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var x = new MyVector4<T>[count];
        var y = new MyVector4<T>[count];
        var expected = new MyVector4<T>(T.One, T.One, T.One, T.One);
        var random = new Random(42);
        for (var index = 0; index < x.Length; index++)
        {
            var value = new MyVector4<T>(T.CreateChecked(random.Next(10)), T.CreateChecked(random.Next(10)), T.CreateChecked(random.Next(10)), T.CreateChecked(random.Next(10)));
            var value2 = new MyVector4<T>(T.CreateChecked(random.Next(10)), T.CreateChecked(random.Next(10)), T.CreateChecked(random.Next(10)), T.CreateChecked(random.Next(10)));
            x[index] = value;
            y[index] = value2;
            expected = new ((value.X + value2.X) * expected.X, (value.Y + value2.Y) * expected.Y, (value.Z + value2.Z) * expected.Z, (value.W + value2.W) * expected.W);
        }

        // act
        var result = TensorOperations.ProductOfAdditions4D<T>(
            MemoryMarshal.Cast<MyVector4<T>, T>(x),
            MemoryMarshal.Cast<MyVector4<T>, T>(y));

        // assert
        Assert.Equal(expected, new MyVector4<T>(Assert.NotNull(result)));
    }

    [Theory]
    [MemberData(nameof(ProductOfAdditions4DData))]
    public void ProductOfAdditions4D_Int_Should_Succeed(int count)
        => ProductOfAdditions4D_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(ProductOfAdditions4DData))]
    public void ProductOfAdditions4D_Long_Should_Succeed(int count)
        => ProductOfAdditions4D_Should_Succeed<long>(count);

    // [Theory]
    // [MemberData(nameof(ProductOfAdditions4DData))]
    // public void ProductOfAdditions4D_Float_Should_Succeed(int count)
    //     => ProductOfAdditions4D_Should_Succeed<float>(count);

    // [Theory]
    // [MemberData(nameof(ProductOfAdditions4DData))]
    // public void ProductOfAdditions4D_Double_Should_Succeed(int count)
    //     => ProductOfAdditions4D_Should_Succeed<double>(count);
}