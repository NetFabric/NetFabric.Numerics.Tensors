﻿namespace NetFabric.Numerics.Tensors.UnitTests;

public class BitwiseAndNotTests
{
    public static TheoryData<int> BitwiseAndNotData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void BitwiseAndNot_Should_Succeed<T>(int count)
        where T : struct, INumber<T>, IBitwiseOperators<T, T, T>
    {
        // arrange
        var source = new T[count];
        var other = new T[count];
        var result = new T[count];
        var expected = new T[count];
        var random = new Random(42);
        for (var index = 0; index < source.Length; index++)
        {
            var first = T.CreateChecked(random.Next(10));
            source[index] = first;
            var second = T.CreateChecked(random.Next(10));
            other[index] = second;
            expected[index] = first & ~second;
        }

        // act
        TensorOperations.BitwiseAndNot(source.AsSpan(), other, result.AsSpan());

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(BitwiseAndNotData))]
    public void BitwiseAndNot_Short_Should_Succeed(int count)
        => BitwiseAndNot_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(BitwiseAndNotData))]
    public void BitwiseAndNot_Int_Should_Succeed(int count)
        => BitwiseAndNot_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(BitwiseAndNotData))]
    public void BitwiseAndNot_Long_Should_Succeed(int count)
        => BitwiseAndNot_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(BitwiseAndNotData))]
    public void BitwiseAndNot_Half_Should_Succeed(int count)
        => BitwiseAndNot_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(BitwiseAndNotData))]
    public void BitwiseAndNot_Float_Should_Succeed(int count)
        => BitwiseAndNot_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(BitwiseAndNotData))]
    public void BitwiseAndNot_Double_Should_Succeed(int count)
        => BitwiseAndNot_Should_Succeed<double>(count);
}