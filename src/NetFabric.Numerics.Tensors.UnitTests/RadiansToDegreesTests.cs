namespace NetFabric.Numerics.Tensors.UnitTests;

public class RadiansToDegreesTests
{
    public static TheoryData<int> RadiansToDegreesData
        => new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

    static void RadiansToDegrees_Should_Succeed<T>(int count)
        where T : struct, IFloatingPoint<T>, ITrigonometricFunctions<T>
    {
        // arrange
        var source = new T[count];
        var result = new T[count];
        var expected = new T[count];
        var random = new Random(42);
        for (var index = 0; index < source.Length; index++)
        {
            var value = T.CreateChecked(random.NextDouble() * 10.0);
            source[index] = value;
            expected[index] = T.RadiansToDegrees(value);
        }

        // act
        TensorOperations.RadiansToDegrees(source.AsSpan(), result.AsSpan());

        // assert
        AssertCollection.AreEqual(expected, result, T.CreateChecked(0.0001));
    }

    [Theory]
    [MemberData(nameof(RadiansToDegreesData))]
    public void RadiansToDegrees_Float_Should_Succeed(int count)
        => RadiansToDegrees_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(RadiansToDegreesData))]
    public void RadiansToDegrees_Double_Should_Succeed(int count)
        => RadiansToDegrees_Should_Succeed<double>(count);
}