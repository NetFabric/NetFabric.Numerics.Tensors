using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics.Tensors.UnitTests;

public class SumTuplesTests
{
    public static TheoryData<int, int> SumData
        => new() { 
            { 2, 0 }, { 2, 1 }, { 2, 2 }, { 2, 3 }, { 2, 4 }, { 2, 5 }, { 2, 6 }, { 2, 7 }, { 2, 8 }, { 2, 9 }, { 2, 10 }, { 2, 100 },
            { 3, 0 }, { 3, 1 }, { 3, 2 }, { 3, 3 }, { 3, 4 }, { 3, 5 }, { 3, 6 }, { 3, 7 }, { 3, 8 }, { 3, 9 }, { 3, 10 }, { 3, 100 },  
            { 4, 0 }, { 4, 1 }, { 4, 2 }, { 4, 3 }, { 4, 4 }, { 4, 5 }, { 4, 6 }, { 4, 7 }, { 4, 8 }, { 4, 9 }, { 4, 10 }, { 4, 100 }, 
            { 5, 0 }, { 5, 1 }, { 5, 2 }, { 5, 3 }, { 5, 4 }, { 5, 5 }, { 5, 6 }, { 5, 7 }, { 5, 8 }, { 5, 9 }, { 5, 10 }, { 5, 100 }, 
            { 6, 0 }, { 6, 1 }, { 6, 2 }, { 6, 3 }, { 6, 4 }, { 6, 5 }, { 6, 6 }, { 6, 7 }, { 6, 8 }, { 6, 9 }, { 6, 10 }, { 6, 100 }, 
            { 7, 0 }, { 7, 1 }, { 7, 2 }, { 7, 3 }, { 7, 4 }, { 7, 5 }, { 7, 6 }, { 7, 7 }, { 7, 8 }, { 7, 9 }, { 7, 10 }, { 7, 100 }, 
            { 8, 0 }, { 8, 1 }, { 8, 2 }, { 8, 3 }, { 8, 4 }, { 8, 5 }, { 8, 6 }, { 8, 7 }, { 8, 8 }, { 8, 9 }, { 8, 10 }, { 8, 100 }, 
            { 9, 0 }, { 9, 1 }, { 9, 2 }, { 9, 3 }, { 9, 4 }, { 9, 5 }, { 9, 6 }, { 9, 7 }, { 9, 8 }, { 9, 9 }, { 9, 10 }, { 9, 100 }, 
            { 10, 0 }, { 10, 1 }, { 10, 2 }, { 10, 3 }, { 10, 4 }, { 10, 5 }, { 10, 6 }, { 10, 7 }, { 10, 8 }, { 10, 9 }, { 10, 10 }, { 10, 100 },   
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