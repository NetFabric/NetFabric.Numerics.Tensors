namespace NetFabric.Numerics.Tensors.UnitTests;

public class ProductOfAdditionsTests
{
    public static TheoryData<int> ProductOfAdditionsData
        => new() { 
            { 0 }, { 1 }, { 2 }, { 3 }, { 4 }, { 5 }, { 6 }, { 7 }, { 8 }, { 9 }, { 10 }, { 100 },
        };

    static void ProductOfAdditions_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var x = new T[count];
        var y = new T[count];
        var expected = T.One;
        var random = new Random(42);
        for (var index = 0; index < x.Length; index++)
        {
            var value = T.CreateChecked(random.Next(10));
            x[index] = value;
            y[index] = value + T.CreateChecked(1);
            expected *= value + value + T.CreateChecked(1);
        }

        // act
        var result = Tensor.ProductOfAdditions<T>(x, y);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(ProductOfAdditionsData))]
    public void ProductOfAdditions_Int_Should_Succeed(int count)
        => ProductOfAdditions_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(ProductOfAdditionsData))]
    public void ProductOfAdditions_Long_Should_Succeed(int count)
        => ProductOfAdditions_Should_Succeed<long>(count);

    // [Theory]
    // [MemberData(nameof(ProductOfAdditionsData))]
    // public void ProductOfAdditions_Float_Should_Succeed(int count)
    //     => ProductOfAdditions_Should_Succeed<float>(count);

    // [Theory]
    // [MemberData(nameof(ProductOfAdditionsData))]
    // public void ProductOfAdditions_Double_Should_Succeed(int count)
    //     => ProductOfAdditions_Should_Succeed<double>(count);
}