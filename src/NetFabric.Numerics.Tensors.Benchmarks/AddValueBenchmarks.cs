using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class AddValueBenchmarks
{
    short[]? sourceShort, resultShort;
    int[]? sourceInt, resultInt;
    long[]? sourceLong, resultLong;
    Half[]? sourceHalf, resultHalf;
    float[]? sourceFloat, resultFloat;
    double[]? sourceDouble, resultDouble;

    [Params(10_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        sourceShort = new short[Count];
        resultShort = new short[Count];
        sourceInt = new int[Count];
        resultInt = new int[Count];
        sourceLong = new long[Count];
        resultLong = new long[Count];
        sourceHalf = new Half[Count];
        resultHalf = new Half[Count];
        sourceFloat = new float[Count];
        resultFloat = new float[Count];
        sourceDouble = new double[Count];
        resultDouble = new double[Count];

        var random = new Random(42);
        for(var index = 0; index < Count; index++)
        {
            var value = random.Next(10);
            sourceShort[index] = (short)value;
            sourceInt[index] = value;
            sourceLong[index] = value;
            sourceHalf[index] = (Half)value;
            sourceFloat[index] = value;
            sourceDouble[index] = value;
        }
    }

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public void Baseline_Short()
        => Baseline.Add<short>(sourceShort!, 42, resultShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public void Tensor_Short()
        => Tensor.Add<short>(sourceShort!, 42, resultShort!);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public void Baseline_Int()
        => Baseline.Add<int>(sourceInt!, 42, resultInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public void Tensor_Int()
        => Tensor.Add<int>(sourceInt!, 42, resultInt!);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public void Baseline_Long()
        => Baseline.Add<long>(sourceLong!, 42, resultLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public void Tensor_Long()
        => Tensor.Add<long>(sourceLong!, 42, resultLong!);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public void Baseline_Half()
        => Baseline.Add<Half>(sourceHalf!, (Half)42, resultHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void Tensor_Half()
        => Tensor.Add<Half>(sourceHalf!, (Half)42, resultHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public void Baseline_Float()
        => Baseline.Add<float>(sourceFloat!, 42, resultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void Tensor_Float()
        => Tensor.Add<float>(sourceFloat!, 42, resultFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public void Baseline_Double()
        => Baseline.Add<double>(sourceDouble!, 42, resultDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void Tensor_Double()
        => Tensor.Add<double>(sourceDouble!, 42, resultDouble!);
}