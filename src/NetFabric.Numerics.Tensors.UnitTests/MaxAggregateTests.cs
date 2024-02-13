using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class MaxAggregateTests
{
    public static TheoryData<float[]> MaxNaNData
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
    [MemberData(nameof(MaxNaNData))]
    public static void Max_With_NaN_Should_Return_NaN(float[] source)
    {
        // arrange
        var expected = TensorPrimitives.Max(source);

        // act
        var result = TensorOperations.Max<float>(source);

        // assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<int> MaxData
        => new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void Max_Should_Succeed<T>(int count)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        // arrange
        var source = new T[count];
        var expected = T.MinValue;
        var random = new Random();
        for (var index = 0; index < source.Length; index++)
        {
            var value = T.CreateChecked(random.Next(100) - 50);
            source[index] = value;
            if (expected < value)
                expected = value;
        }

        // act
        var result = TensorOperations.Max<T>(source);

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