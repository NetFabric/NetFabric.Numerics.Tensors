using BenchmarkDotNet.Attributes;

namespace NetFabric.Numerics.Tensors.Benchmarks;

public class SumBenchmarks
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
        arrayShort = new short[Count];
        arrayInt = new int[Count];
        arrayLong = new long[Count];
        arrayHalf = new Half[Count];
        arrayFloat = new float[Count];
        arrayDouble = new double[Count];

        for(var index = 0; index < Count; index++)
        {
            arrayShort[index] = (short)index;
            arrayInt[index] = index;
            arrayLong[index] = index;
            arrayHalf[index] = (Half)index;
            arrayFloat[index] = index;
            arrayDouble[index] = index;
        }
    }

    [Benchmark]
    public ReadOnlySpan<short> Sum_Short()
        => Tensor.Sum<short>(arrayShort!);

    [Benchmark]
    public ReadOnlySpan<int> Sum_Int()
        => Tensor.Sum<int>(arrayInt!);

    [Benchmark]
    public ReadOnlySpan<long> Sum_Long()
        => Tensor.Sum<long>(arrayLong!);

    [Benchmark]
    public ReadOnlySpan<Half> Sum_Half()
        => Tensor.Sum<Half>(arrayHalf!);

    [Benchmark]
    public ReadOnlySpan<float> Sum_Float()
        => Tensor.Sum<float>(arrayFloat!);

    [Benchmark]
    public ReadOnlySpan<double> Sum_Double()
        => Tensor.Sum<double>(arrayDouble);
}