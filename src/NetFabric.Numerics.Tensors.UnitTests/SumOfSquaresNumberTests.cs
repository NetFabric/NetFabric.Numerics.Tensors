using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class SumOfSquaresNumberTests
{
    public static TheoryData<int> SumOfSquaresNumberData
        => new() { 
            { 0 }, { 1 }, { 2 }, { 3 }, { 4 }, { 5 }, { 6 }, { 7 }, { 8 }, { 9 }, { 10 }, { 100 },
        };

    static void SumOfSquaresNumber_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var source = new T[count];
        var expected = T.Zero;
        var random = new Random(42);
        for (var index = 0; index < source.Length; index++)
        {
            var value = T.CreateChecked(random.Next(10));
            source[index] = value;
            expected += value * value;
        }

        // act
        var result = TensorOperations.SumOfSquaresNumber<T>(source);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(SumOfSquaresNumberData))]
    public void SumOfSquaresNumber_Short_Should_Succeed(int count)
        => SumOfSquaresNumber_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(SumOfSquaresNumberData))]
    public void SumOfSquaresNumber_Int_Should_Succeed(int count)
        => SumOfSquaresNumber_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(SumOfSquaresNumberData))]
    public void SumOfSquaresNumber_Long_Should_Succeed(int count)
        => SumOfSquaresNumber_Should_Succeed<long>(count);

    //[Theory]
    //[MemberData(nameof(SumOfSquaresNumberData))]
    //public void SumOfSquaresNumber_Half_Should_Succeed(int count)
    //    => SumOfSquaresNumber_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(SumOfSquaresNumberData))]
    public void SumOfSquaresNumber_Float_Should_Succeed(int count)
        => SumOfSquaresNumber_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(SumOfSquaresNumberData))]
    public void SumOfSquaresNumber_Double_Should_Succeed(int count)
        => SumOfSquaresNumber_Should_Succeed<double>(count);
}