using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class MaxMagnitudeTests
{
    public static TheoryData<float[]> MaxMagnitudeNaNData
        => new() { 
            new[] { float.NaN },
            new[] { float.NaN, float.NaN },
            new[] { float.NaN, 1.0f },
            new[] { float.NaN, 1.0f, float.NaN },
            new[] { 1.0f, float.NaN, 1.0f, float.NaN },
            new[] { 2.0f, float.NaN, 2.0f, float.NaN },
            new[] { 1.0f, 1.0f, float.NaN, 1.0f, float.NaN },
            new[] { 2.0f, 2.0f, float.NaN, 2.0f, float.NaN },
            new[] { 1.0f, 1.0f, 1.0f, float.NaN, 1.0f, float.NaN },
            new[] { 2.0f, 2.0f, 2.0f, float.NaN, 2.0f, float.NaN },
            new[] { 1.0f, 1.0f, 1.0f, 1.0f, float.NaN, 1.0f, float.NaN },
            new[] { 2.0f, 2.0f, 2.0f, 2.0f, float.NaN, 2.0f, float.NaN },
            new[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, float.NaN, 1.0f, float.NaN },
            new[] { 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, float.NaN, 2.0f, float.NaN },
            new[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, float.NaN, 1.0f, float.NaN },
            new[] { 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, float.NaN, 2.0f, float.NaN },
            new[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, float.NaN, 1.0f, float.NaN },
            new[] { 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, float.NaN, 2.0f, float.NaN },
        };

    [Theory]
    [MemberData(nameof(MaxMagnitudeNaNData))]
    public static void MaxMagnitude_With_NaN_Should_Succeed(float[] source)
    {
        // arrange
        var threshold = 1.5f;
        var thresholdArray = new float[source.Length];
        Array.Fill(thresholdArray, threshold);

        var result = new float[source.Length];
        var reversedResult = new float[source.Length];

        var expected = new float[source.Length];
        TensorPrimitives.MaxMagnitude(source, thresholdArray, expected);

        var reversedExpected = new float[source.Length];
        TensorPrimitives.MaxMagnitude(thresholdArray, source, reversedExpected);

        // act
        Tensor.MaxMagnitude(source, threshold, result);
        Tensor.MaxMagnitude<float>(thresholdArray, source, reversedResult);

        // assert
        Assert.Equal(expected, result);
        Assert.Equal(reversedExpected, reversedResult);
    }

    public static TheoryData<int> MaxMagnitudeData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void MaxMagnitude_Should_Succeed<T>(int count)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        // arrange
        var source = new T[count];
        var threshold = T.CreateChecked(0);
        var result = new T[count];
        var expected = new T[count];
        var random = new Random(42);
        for (var index = 0; index < source.Length; index++)
        {
            var value = T.CreateChecked(random.Next(100) - 50);
            source[index] = value;
            expected[index] = T.MaxMagnitude(value, threshold);
        }

        // act
        Tensor.MaxMagnitude<T>(source, threshold, result);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(MaxMagnitudeData))]
    public void MaxMagnitude_Short_Should_Succeed(int count)
        => MaxMagnitude_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(MaxMagnitudeData))]
    public void MaxMagnitude_Int_Should_Succeed(int count)
        => MaxMagnitude_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(MaxMagnitudeData))]
    public void MaxMagnitude_Long_Should_Succeed(int count)
        => MaxMagnitude_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(MaxMagnitudeData))]
    public void MaxMagnitude_Half_Should_Succeed(int count)
        => MaxMagnitude_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(MaxMagnitudeData))]
    public void MaxMagnitude_Float_Should_Succeed(int count)
        => MaxMagnitude_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(MaxMagnitudeData))]
    public void MaxMagnitude_Double_Should_Succeed(int count)
        => MaxMagnitude_Should_Succeed<float>(count);

}