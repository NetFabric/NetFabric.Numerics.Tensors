namespace NetFabric.Numerics.Tensors.UnitTests;

public class ProductOfAdditionsTuple3DTests
{
    public static TheoryData<int> ProductOfAdditions3DData
        => new() { 
            { 1 }, { 2 }, { 3 }, { 4 }, { 5 }, { 6 }, { 7 }, { 8 }, { 9 }, { 10 }, { 100 },
        };

    [Theory]
    [MemberData(nameof(ProductOfAdditions3DData))]
    static void ProductOfAdditions4D_Using_Tuple3D_Should_Succeed(int count)
    {
        // arrange
        var x = new ValueTuple<int, int, int>[count];
        var y = new ValueTuple<int, int, int>[count];
        var expected = new ValueTuple<int, int, int, int>(1, 1, 1, 0);
        var random = new Random(42);
        for (var index = 0; index < x.Length; index++)
        {
            var value = new ValueTuple<int, int, int>(random.Next(10), random.Next(10), random.Next(10));
            var value2 = new ValueTuple<int, int, int>(random.Next(10), random.Next(10), random.Next(10));
            x[index] = value;
            y[index] = value2;
            expected = new ((value.Item1 + value2.Item1) * expected.Item1, (value.Item2 + value2.Item2) * expected.Item2, (value.Item3 + value2.Item3) * expected.Item3, 0);
        }

        // act
        var result = TensorOperations.ProductOfAdditions4D<int>(
            MemoryMarshal.Cast<ValueTuple<int, int, int>, int>(x),
            MemoryMarshal.Cast<ValueTuple<int, int, int>, int>(y));

        // assert
        Assert.Equal(expected, Assert.NotNull(result));
    }
}