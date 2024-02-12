namespace NetFabric.Numerics.Tensors.UnitTests;

public class OnesComplementTests
{
    public static TheoryData<int> OnesComplementData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void OnesComplement_Should_Succeed<T>(int count)
        where T : struct, INumber<T>, IBitwiseOperators<T, T, T>
    {
        // arrange
        var source = new T[count];
        var result = new T[count];
        var expected = new T[count];
        var random = new Random(42);
        for (var index = 0; index < source.Length; index++)
        {
            var value = T.CreateChecked(random.Next(10));
            source[index] = value;
            expected[index] = ~value;
        }

        // act
        TensorOperations.OnesComplement(source.AsSpan(), result.AsSpan());

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(OnesComplementData))]
    public void OnesComplement_Short_Should_Succeed(int count)
        => OnesComplement_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(OnesComplementData))]
    public void OnesComplement_Int_Should_Succeed(int count)
        => OnesComplement_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(OnesComplementData))]
    public void OnesComplement_Long_Should_Succeed(int count)
        => OnesComplement_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(OnesComplementData))]
    public void OnesComplement_Half_Should_Succeed(int count)
        => OnesComplement_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(OnesComplementData))]
    public void OnesComplement_Float_Should_Succeed(int count)
        => OnesComplement_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(OnesComplementData))]
    public void OnesComplement_Double_Should_Succeed(int count)
        => OnesComplement_Should_Succeed<double>(count);
}