namespace NetFabric.Numerics.Tensors.UnitTests;

public class SinCosTests
{
    public static TheoryData<int> SinCosData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void SinCos_Should_Succeed<T>(int count)
        where T : struct, ITrigonometricFunctions<T>
    {
        // arrange
        var source = new T[count];
        var sinResult = new T[count];
        var cosResult = new T[count];
        var sinExpected = new T[count];
        var cosExpected = new T[count];
        var random = new Random(42);
        for (var index = 0; index < source.Length; index++)
        {
            var value = T.CreateChecked(random.NextDouble() * 10.0);
            source[index] = value;
            sinExpected[index] = T.Sin(value);
            cosExpected[index] = T.Cos(value);
        }

        // act
        Tensor.SinCos(source.AsSpan(), sinResult.AsSpan(), cosResult.AsSpan());

        // assert
        Assert.Equal(sinExpected, sinResult);
        Assert.Equal(cosExpected, cosResult);
    }

    [Theory]
    [MemberData(nameof(SinCosData))]
    public void SinCos_Half_Should_Succeed(int count)
        => SinCos_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(SinCosData))]
    public void SinCos_Float_Should_Succeed(int count)
        => SinCos_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(SinCosData))]
    public void SinCos_Double_Should_Succeed(int count)
        => SinCos_Should_Succeed<double>(count);
}