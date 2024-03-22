using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class SumNumberTests
{
    public static TheoryData<int> SumNumberData
        => new() { 
            { 0 }, { 1 }, { 2 }, { 3 }, { 4 }, { 5 }, { 6 }, { 7 }, { 8 }, { 9 }, { 10 }, { 100 },
        };

    static void SumNumber_Should_Succeed<T>(int count)
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
            expected += value;
        }

        // act
        var result = TensorOperations.SumNumber<T>(source);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(SumNumberData))]
    public void SumNumber_Short_Should_Succeed(int count)
        => SumNumber_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(SumNumberData))]
    public void SumNumber_Int_Should_Succeed(int count)
        => SumNumber_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(SumNumberData))]
    public void SumNumber_Long_Should_Succeed(int count)
        => SumNumber_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(SumNumberData))]
    public void SumNumber_Half_Should_Succeed(int count)
        => SumNumber_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(SumNumberData))]
    public void SumNumber_Float_Should_Succeed(int count)
        => SumNumber_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(SumNumberData))]
    public void SumNumber_Double_Should_Succeed(int count)
        => SumNumber_Should_Succeed<double>(count);
}