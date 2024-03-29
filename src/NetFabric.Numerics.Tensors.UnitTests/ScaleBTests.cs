﻿namespace NetFabric.Numerics.Tensors.UnitTests;

public class ScaleBTests
{
    public static TheoryData<int> ScaleBData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void ScaleB_Should_Succeed<T>(int count)
        where T : struct, IFloatingPointIeee754<T>
    {
        // arrange
        var n = 2;
        var source = new T[count];
        var result = new T[count];
        var expected = new T[count];
        var random = new Random(42);
        for (var index = 0; index < source.Length; index++)
        {
            var value = T.CreateChecked(random.Next(100));
            source[index] = value;
            expected[index] = T.ScaleB(value, n);
        }

        // act
        TensorOperations.ScaleB<T>(source, n, result);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(ScaleBData))]
    public void ScaleB_Half_Should_Succeed(int count)
        => ScaleB_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(ScaleBData))]
    public void ScaleB_Float_Should_Succeed(int count)
        => ScaleB_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(ScaleBData))]
    public void ScaleB_Double_Should_Succeed(int count)
        => ScaleB_Should_Succeed<double>(count);

}