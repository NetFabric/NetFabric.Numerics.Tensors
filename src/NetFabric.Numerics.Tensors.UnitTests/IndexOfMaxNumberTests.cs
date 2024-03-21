using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class IndexOfMaxNumberTests
{
    public static TheoryData<int> MaxData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void IndexOfMaxNumber_Should_Succeed<T>(int count)
        where T : struct, INumber<T>, IMinMaxValue<T>
    {
        // arrange
        var source = new T[count];
        var max = T.MinValue;
        var expected = -1;
        var random = new Random(42);
        for (var index = 0; index < source.Length; index++)
        {
            var value = T.CreateChecked(random.Next(100) - 50);
            source[index] = value;
            if (value > max)
            {
                max = value;
                expected = index;
            }
        }

        // act
        var result = TensorOperations.IndexOfMaxNumber<T>(source);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(MaxData))]
    public void IndexOfMaxNumber_Short_Should_Succeed(int count)
        => IndexOfMaxNumber_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(MaxData))]
    public void IndexOfMaxNumber_Int_Should_Succeed(int count)
        => IndexOfMaxNumber_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(MaxData))]
    public void IndexOfMaxNumber_Long_Should_Succeed(int count)
        => IndexOfMaxNumber_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(MaxData))]
    public void IndexOfMaxNumber_Half_Should_Succeed(int count)
        => IndexOfMaxNumber_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(MaxData))]
    public void IndexOfMaxNumber_Float_Should_Succeed(int count)
        => IndexOfMaxNumber_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(MaxData))]
    public void IndexOfMaxNumber_Double_Should_Succeed(int count)
        => IndexOfMaxNumber_Should_Succeed<float>(count);

}