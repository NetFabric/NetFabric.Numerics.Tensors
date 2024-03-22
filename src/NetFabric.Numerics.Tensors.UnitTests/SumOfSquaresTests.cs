using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class SumOfSquaresTests
{
    public static TheoryData<float[]> SumOfSquaresNaNData
        => new() {
            new[] { float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN },
            new[] { float.NaN, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
            new[] { 1.0f, float.NaN, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
            new[] { 1.0f, 1.0f, float.NaN, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
            new[] { 1.0f, 1.0f, 1.0f, float.NaN, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
            new[] { 1.0f, 1.0f, 1.0f, 1.0f, float.NaN, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
            new[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, float.NaN, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
            new[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, float.NaN, 1.0f, 1.0f, 1.0f, 1.0f },
            new[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, float.NaN, 1.0f, 1.0f, 1.0f },
        };

    [Theory]
    [MemberData(nameof(SumOfSquaresNaNData))]
    public static void SumOfSquares_With_NaN_Should_Return_NaN(float[] source)
    {
        // arrange
        var expected = TensorPrimitives.SumOfSquares(source);

        // act
        var result = TensorOperations.SumOfSquares<float>(source);

        // assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<int> SumOfSquaresData
        => new() { 
            { 0 }, { 1 }, { 2 }, { 3 }, { 4 }, { 5 }, { 6 }, { 7 }, { 8 }, { 9 }, { 10 }, { 100 },
        };

    static void SumOfSquares_Should_Succeed<T>(int count)
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
        var result = TensorOperations.SumOfSquares<T>(source);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(SumOfSquaresData))]
    public void SumOfSquares_Short_Should_Succeed(int count)
        => SumOfSquares_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(SumOfSquaresData))]
    public void SumOfSquares_Int_Should_Succeed(int count)
        => SumOfSquares_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(SumOfSquaresData))]
    public void SumOfSquares_Long_Should_Succeed(int count)
        => SumOfSquares_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(SumOfSquaresData))]
    public void SumOfSquares_Half_Should_Succeed(int count)
        => SumOfSquares_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(SumOfSquaresData))]
    public void SumOfSquares_Float_Should_Succeed(int count)
        => SumOfSquares_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(SumOfSquaresData))]
    public void SumOfSquares_Double_Should_Succeed(int count)
        => SumOfSquares_Should_Succeed<double>(count);
}