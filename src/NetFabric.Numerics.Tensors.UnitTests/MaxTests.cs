using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class MaxTests
{
    public static TheoryData<float[]> MaxNaNData
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
    [MemberData(nameof(MaxNaNData))]
    public static void Max_With_NaN_Should_Succeed(float[] source)
    {
        // arrange
        var threshold = 1.5f;
        var thresholdArray = new float[source.Length];
        Array.Fill(thresholdArray, threshold);

        var result = new float[source.Length];
        var reversedResult = new float[source.Length];

        var expected = new float[source.Length];
        TensorPrimitives.Max(source, thresholdArray, expected);

        var reversedExpected = new float[source.Length];
        TensorPrimitives.Max(thresholdArray, source, reversedExpected);

        // act
        Tensor.Max(source, threshold, result);
        Tensor.Max<float>(thresholdArray, source, reversedResult);

        // assert
        Assert.Equal(expected, result);
        Assert.Equal(reversedExpected, reversedResult);
    }

    public static TheoryData<int> MaxData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void Max_Should_Succeed<T>(int count)
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
            expected[index] = T.Max(value, threshold);
        }

        // act
        Tensor.Max(source, threshold, result);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(MaxData))]
    public void Max_Short_Should_Succeed(int count)
        => Max_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(MaxData))]
    public void Max_Int_Should_Succeed(int count)
        => Max_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(MaxData))]
    public void Max_Long_Should_Succeed(int count)
        => Max_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(MaxData))]
    public void Max_Half_Should_Succeed(int count)
        => Max_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(MaxData))]
    public void Max_Float_Should_Succeed(int count)
        => Max_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(MaxData))]
    public void Max_Double_Should_Succeed(int count)
        => Max_Should_Succeed<float>(count);

}