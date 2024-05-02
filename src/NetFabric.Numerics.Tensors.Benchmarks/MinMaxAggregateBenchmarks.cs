using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[Config(typeof(VectorizationConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[MemoryDiagnoser]
[MemoryRandomization]
public class MinMaxAggregateBenchmarks
{
    short[]? arrayShort;
    int[]? arrayInt;
    long[]? arrayLong;
    Half[]? arrayHalf;
    float[]? arrayFloat;
    double[]? arrayDouble;

    [Params(100)]
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

        var random = new Random(42);
        for(var index = 0; index < Count; index++)
        {
            var value = random.Next(100) - 50;
            arrayShort[index] = (short)value;
            arrayInt[index] = value;
            arrayLong[index] = value;
            arrayHalf[index] = (Half)value;
            arrayFloat[index] = value;
            arrayDouble[index] = value;
        }
    }

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public (short, short) Baseline_Short()
        => Baseline.MinMax<short>(arrayShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public (short, short) NetFabric_Short()
        => TensorOperations.MinMax<short>(arrayShort!);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public (int, int) Baseline_Int()
        => Baseline.MinMax<int>(arrayInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public (int, int) NetFabric_Int()
        => TensorOperations.MinMax<int>(arrayInt!);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public (long, long) Baseline_Long()
        => Baseline.MinMax<long>(arrayLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public (long, long) NetFabric_Long()
        => TensorOperations.MinMax<long>(arrayLong!);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public (Half, Half) Baseline_Half()
        => Baseline.MinMax<Half>(arrayHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public (Half, Half) NetFabric_Half()
        => TensorOperations.MinMax<Half>(arrayHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public (float, float) Baseline_Float()
        => Baseline.MinMax<float>(arrayFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public (float, float) NetFabric_Float()
        => TensorOperations.MinMax<float>(arrayFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public (double, double) Baseline_Double()
        => Baseline.MinMax<double>(arrayDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public (double, double) NetFabric_Double()
        => TensorOperations.MinMax<double>(arrayDouble!);
}