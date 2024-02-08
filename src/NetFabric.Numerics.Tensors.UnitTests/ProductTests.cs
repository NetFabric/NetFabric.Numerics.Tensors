namespace NetFabric.Numerics.Tensors.UnitTests;

public class ProductTests
{
    [Fact]
    public static void Product_Empty_Should_Return_Null()
    {
        // arrange
        var source = Array.Empty<int>();

        // act
        var result = Tensor.Product<int>(source);

        // assert
        Assert.Null(result);
    }

    public static TheoryData<int> ProductData
        => new() { 
            { 1 }, { 2 }, { 3 }, { 4 }, { 5 }, { 6 }, { 7 }, { 8 }, { 9 }, { 10 }, { 100 },
        };

    static void Product_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var source = new T[count];
        var expected = T.One;
        var random = new Random(42);
        for (var index = 0; index < source.Length; index++)
        {
            var value = T.CreateChecked(random.Next(10));
            source[index] = value;
            expected *= value;
        }

        // act
        var result = Tensor.Product<T>(source);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(ProductData))]
    public void Product_Int_Should_Succeed(int count)
        => Product_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(ProductData))]
    public void Product_Long_Should_Succeed(int count)
        => Product_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(ProductData))]
    public void Product_Float_Should_Succeed(int count)
        => Product_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(ProductData))]
    public void Product_Double_Should_Succeed(int count)
        => Product_Should_Succeed<double>(count);
}