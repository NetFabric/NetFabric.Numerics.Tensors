using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class Sum1DBenchmarks
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

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public short Baseline_Short()
        => arrayShort!.BaselineSum();

    [BenchmarkCategory("Short")]
    [Benchmark]
    public short Tensor_Short()
        => Tensor.Sum<short>(arrayShort!)[0];

        [BenchmarkCategory("Int")]
    [Benchmark]
    public int Baseline_Int()
        => arrayInt!.BaselineSum();

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int Tensor_Int()
        => Tensor.Sum<int>(arrayInt!)[0];

    [BenchmarkCategory("Long")]
    [Benchmark]
    public long Baseline_Long()
        => arrayLong!.BaselineSum();

    [BenchmarkCategory("Long")]
    [Benchmark]
    public long Tensor_Long()
        => Tensor.Sum<long>(arrayLong!)[0];

    [BenchmarkCategory("Half")]
    [Benchmark]
    public Half Baseline_Half()
        => arrayHalf!.BaselineSum();

    [BenchmarkCategory("Half")]
    [Benchmark]
    public Half Tensor_Half()
        => Tensor.Sum<Half>(arrayHalf!)[0];

    [BenchmarkCategory("Float")]
    [Benchmark]
    public float Baseline_Float()
        => arrayFloat!.BaselineSum();

    [BenchmarkCategory("Float")]
    [Benchmark]
    public float Tensor_Float()
        => Tensor.Sum<float>(arrayFloat!)[0];

    [BenchmarkCategory("Double")]
    [Benchmark]
    public double Baseline_Double()
        => arrayDouble!.BaselineSum();

    [BenchmarkCategory("Double")]
    [Benchmark]
    public double Tensor_Double()
        => Tensor.Sum<double>(arrayDouble!)[0];
}