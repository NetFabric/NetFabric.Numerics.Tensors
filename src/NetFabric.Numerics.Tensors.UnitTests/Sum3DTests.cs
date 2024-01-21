using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class Sum3DTests
{
    [Fact]
    public void Sum_Size_Not_Multiple_Of_3_Throws()
    {
        // arrange
        var source = new short[4];

        // act
        void action() => Tensor.Sum3D<short>(source);

        // assert
        Assert.Throws<ArgumentException>("source", action);
    }
    
    public static TheoryData<int> SumData
        => new() { 
            { 0 }, { 3 }, { 6 }, { 9 }, { 12 }, { 15 }, { 300 },
        };

    static void Sum_Should_Succeed<T>(int count)
        where T : struct, INumber<T>
    {
        // arrange
        var source = new MyVector3<T>[count];
        var expected = MyVector3<T>.AdditiveIdentity;
        var random = new Random(42);
        for (var index = 0; index < source.Length; index++)
        {
            var value = new MyVector3<T>(T.CreateChecked(random.Next(10)), T.CreateChecked(random.Next(10)), T.CreateChecked(random.Next(10)));
            source[index] = value;
            expected += value;
        }

        // act
        var result = Tensor.Sum3D<T>(MemoryMarshal.Cast<MyVector3<T>, T>(source));

        // assert
        Assert.Equal(expected, new MyVector3<T>(result));
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Short_Should_Succeed(int count)
        => Sum_Should_Succeed<short>(count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Int_Should_Succeed(int count)
        => Sum_Should_Succeed<int>(count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Long_Should_Succeed(int count)
        => Sum_Should_Succeed<long>(count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Half_Should_Succeed(int count)
        => Sum_Should_Succeed<Half>(count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Float_Should_Succeed(int count)
        => Sum_Should_Succeed<float>(count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void Sum_Double_Should_Succeed(int count)
        => Sum_Should_Succeed<double>(count);
}