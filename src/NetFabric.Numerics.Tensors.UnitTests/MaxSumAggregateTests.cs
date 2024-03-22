using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class MaxSumAggregateTests
{
    public static TheoryData<float[]> MaxSumNaNData
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
    [MemberData(nameof(MaxSumNaNData))]
    public static void MaxSum_With_NaN_Should_Return_NaN(float[] source)
    {
        // arrange
        var expected = TensorPrimitives.Max(source);

        // act
        var result = TensorOperations.MaxSum<float>(source, source);

        // assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<int> MaxData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void MaxSum_Should_Succeed<T>(int count)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        // arrange
        var left = new T[count];
        var right = new T[count];
        var expected = T.MinValue;
        var random = new Random(42);
        for (var index = 0; index < left.Length; index++)
        {
            var valueLeft = T.CreateChecked(random.Next(100) - 50);
            var valueRight = T.CreateChecked(random.Next(100) - 50);
            left[index] = valueLeft;
            right[index] = valueRight;
            var sum = valueLeft + valueRight;
            if (sum > expected)
                expected = sum;
        }

        // act
        var result = TensorOperations.MaxSum<T>(left, right);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(MaxData))]
    public void MaxSum_Short_Should_Succeed(int count)
        => MaxSum_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(MaxData))]
    public void MaxSum_Int_Should_Succeed(int count)
        => MaxSum_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(MaxData))]
    public void MaxSum_Long_Should_Succeed(int count)
        => MaxSum_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(MaxData))]
    public void MaxSum_Half_Should_Succeed(int count)
        => MaxSum_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(MaxData))]
    public void MaxSum_Float_Should_Succeed(int count)
        => MaxSum_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(MaxData))]
    public void MaxSum_Double_Should_Succeed(int count)
        => MaxSum_Should_Succeed<float>(count);

}