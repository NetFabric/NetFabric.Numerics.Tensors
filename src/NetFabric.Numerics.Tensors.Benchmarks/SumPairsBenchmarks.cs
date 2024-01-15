using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.Benchmarks;

public class SumPairsBenchmarks
{
    short[]? arrayShort;
    int[]? arrayInt;
    long[]? arrayLong;
    Half[]? arrayHalf;
    float[]? arrayFloat;
    double[]? arrayDouble;

    [Params(10_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        arrayShort = new short[Count * 2];
        arrayInt = new int[Count * 2];
        arrayLong = new long[Count * 2];
        arrayHalf = new Half[Count * 2];
        arrayFloat = new float[Count * 2];
        arrayDouble = new double[Count * 2];

        for(var index = 0; index + 1 < Count; index += 2)
        {
            arrayShort[index] = (short)index;
            arrayShort[index + 1] = (short)(index + 1);
            arrayInt[index] = index;
            arrayInt[index + 1] = index + 1;
            arrayLong[index] = index;
            arrayLong[index + 1] = index + 1;
            arrayHalf[index] = (Half)index;
            arrayHalf[index + 1] = (Half)(index + 1);
            arrayFloat[index] = index;
            arrayFloat[index + 1] = index + 1;
            arrayDouble[index] = index;
            arrayDouble[index + 1] = index + 1;
        }
    }

    [Benchmark]
    public ReadOnlySpan<short> Sum_Short()
        => Tensor.Sum<short>(arrayShort!, 2);

    [Benchmark]
    public ReadOnlySpan<int> Sum_Int()
        => Tensor.Sum<int>(arrayInt!, 2);

    [Benchmark]
    public ReadOnlySpan<long> Sum_Long()
        => Tensor.Sum<long>(arrayLong!, 2);

    [Benchmark]
    public ReadOnlySpan<Half> Sum_Half()
        => Tensor.Sum<Half>(arrayHalf!, 2);

    [Benchmark]
    public ReadOnlySpan<float> Sum_Float()
        => Tensor.Sum<float>(arrayFloat!, 2);

    [Benchmark]
    public ReadOnlySpan<double> Sum_Double()
        => Tensor.Sum<double>(arrayDouble!, 2);
}