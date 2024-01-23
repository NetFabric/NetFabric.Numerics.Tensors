namespace NetFabric.Numerics.Tensors.UnitTests;

public class ShiftLeftTests
{
    public static TheoryData<int> ShiftLeftData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    [Theory]
    [MemberData(nameof(ShiftLeftData))]
    public void ShiftLeft_Short_Should_Succeed(int count)
    {
        // arrange
        var amount = 8;
        var source = new short[count];
        var result = new short[count];
        var expected = new short[count];
        var random = new Random(42);
        for (var index = 0; index < source.Length; index++)
        {
            var value = (short)random.Next(100);
            source[index] = value;
            expected[index] = (short)(value << amount);
        }

        // act
        Tensor.ShiftLeft(source.AsSpan(), amount, result.AsSpan());

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(ShiftLeftData))]
    public void ShiftLeft_Int_Should_Succeed(int count)
    {
        // arrange
        var amount = 8;
        var source = new int[count];
        var result = new int[count];
        var expected = new int[count];
        var random = new Random(42);
        for (var index = 0; index < source.Length; index++)
        {
            var value = random.Next(100);
            source[index] = value;
            expected[index] = value << amount;
        }

        // act
        Tensor.ShiftLeft(source.AsSpan(), amount, result.AsSpan());

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(ShiftLeftData))]
    public void ShiftLeft_Long_Should_Succeed(int count)
    {
        // arrange
        var amount = 8;
        var source = new long[count];
        var result = new long[count];
        var expected = new long[count];
        var random = new Random(42);
        for (var index = 0; index < source.Length; index++)
        {
            var value = (long)random.Next(100);
            source[index] = value;
            expected[index] = value << amount;
        }

        // act
        Tensor.ShiftLeft(source.AsSpan(), amount, result.AsSpan());

        // assert
        Assert.Equal(expected, result);
    }
}