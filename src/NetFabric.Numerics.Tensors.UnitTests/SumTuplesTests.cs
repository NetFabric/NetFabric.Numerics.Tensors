using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class SumTuplesTests
{
    public static TheoryData<int, int> SumData
        => new() { 
            //{ 2, 0 },  { 2, 1 },  { 2, 2 },  { 2, 3 },  { 2, 4 },  { 2, 5 },  { 2, 6 },  
            { 3, 0 },  { 3, 1 },  { 3, 2 },  { 3, 3 },  { 3, 4 },  { 3, 5 },  { 3, 6 },  
        };

    static void SumTuples_Should_Succeed<T>(int tupleSize, int count)
        where T : struct, INumber<T>
    {
        // arrange
        var source = new T[count * tupleSize];
        var expected = new T[tupleSize];
        ref var sourceRef = ref MemoryMarshal.GetReference<T>(source);
        ref var expectedRef = ref MemoryMarshal.GetReference<T>(expected);
        var value = T.Zero;
        for (var index = 0; index + tupleSize <= source.Length; index += tupleSize)
        {
            for (var indexTuple = 0; indexTuple < tupleSize; indexTuple++)
            {
                Unsafe.Add(ref sourceRef, index + indexTuple) = value;
                Unsafe.Add(ref expectedRef, indexTuple) += value;
                value++;
            }
        }

        // act
        var result = Tensor.SumTuples<T>(source, tupleSize);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumTuples_Short_Should_Succeed(int tupleSize, int count)
        => SumTuples_Should_Succeed<short>(tupleSize, count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumTuples_Int_Should_Succeed(int tupleSize, int count)
        => SumTuples_Should_Succeed<int>(tupleSize, count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumTuples_Long_Should_Succeed(int tupleSize, int count)
        => SumTuples_Should_Succeed<long>(tupleSize, count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumTuples_Half_Should_Succeed(int tupleSize, int count)
        => SumTuples_Should_Succeed<Half>(tupleSize, count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumTuples_Float_Should_Succeed(int tupleSize, int count)
        => SumTuples_Should_Succeed<float>(tupleSize, count);

    [Theory]
    [MemberData(nameof(SumData))]
    public void SumTuples_Double_Should_Succeed(int tupleSize, int count)
        => SumTuples_Should_Succeed<double>(tupleSize, count);
}