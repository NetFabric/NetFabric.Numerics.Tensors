using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class SumCheckedTests
{
    public static TheoryData<int[]> SumCheckedOverflowData
        => new() {
            new[] { int.MaxValue, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            new[] { 1, int.MaxValue, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            new[] { 1, 1, int.MaxValue, 1, 1, 1, 1, 1, 1, 1, 1 },
            new[] { 1, 1, 1, int.MaxValue, 1, 1, 1, 1, 1, 1, 1 },
            new[] { 1, 1, 1, 1, int.MaxValue, 1, 1, 1, 1, 1, 1 },
            new[] { 1, 1, 1, 1, 1, int.MaxValue, 1, 1, 1, 1, 1 },
            new[] { 1, 1, 1, 1, 1, 1, int.MaxValue, 1, 1, 1, 1 },
            new[] { 1, 1, 1, 1, 1, 1, 1, int.MaxValue, 1, 1, 1 },
        };

    [Theory]
    [MemberData(nameof(SumCheckedOverflowData))]
    public static void SumChecked_With_Overflow_Should_Throw(int[] source)
    {
        // arrange

        // act
        void action() => TensorOperations.SumChecked<int>(source);

        // assert
        _ = Assert.Throws<OverflowException>(action);
    }

    public static TheoryData<int> SumCheckedData
        => new() { 
            { 0 }, { 1 }, { 2 }, { 3 }, { 4 }, { 5 }, { 6 }, { 7 }, { 8 }, { 9 }, { 10 }, { 100 },
        };

    static void SumChecked_Should_Succeed<T>(int count)
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
        var result = TensorOperations.SumChecked<T>(source);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(SumCheckedData))]
    public void SumChecked_Short_Should_Succeed(int count)
        => SumChecked_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(SumCheckedData))]
    public void SumChecked_Int_Should_Succeed(int count)
        => SumChecked_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(SumCheckedData))]
    public void SumChecked_Long_Should_Succeed(int count)
        => SumChecked_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(SumCheckedData))]
    public void SumChecked_Half_Should_Succeed(int count)
        => SumChecked_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(SumCheckedData))]
    public void SumChecked_Float_Should_Succeed(int count)
        => SumChecked_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(SumCheckedData))]
    public void SumChecked_Double_Should_Succeed(int count)
        => SumChecked_Should_Succeed<double>(count);
}