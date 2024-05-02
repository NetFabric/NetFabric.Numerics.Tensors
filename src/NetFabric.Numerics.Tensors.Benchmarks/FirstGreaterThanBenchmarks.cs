using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[Config(typeof(VectorizationConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[MemoryDiagnoser]
[MemoryRandomization]
public class FirstGreaterThanBenchmarks
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
            var value = -random.Next(100);
            arrayShort[index] = (short)value;
            arrayInt[index] = value;
            arrayLong[index] = value;
            arrayHalf[index] = (Half)value;
            arrayFloat[index] = value;
            arrayDouble[index] = value;
        }
        arrayShort[Count - 1] = 1;
        arrayInt[Count - 1] = 1;
        arrayLong[Count - 1] = 1L;
        arrayHalf[Count - 1] = (Half)1.0;
        arrayFloat[Count - 1] = 1.0f;
        arrayDouble[Count - 1] = 1.0;
}

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public short? Baseline_Short()
        => Baseline.FirstGreaterThan<short>(arrayShort!, 0);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public short? NetFabric_Short()
        => TensorOperations.FirstGreaterThan<short>(arrayShort!, 0);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public int? Baseline_Int()
        => Baseline.FirstGreaterThan<int>(arrayInt!, 0);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int? NetFabric_Int()
        => TensorOperations.FirstGreaterThan(arrayInt!, 0);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public long? Baseline_Long()
        => Baseline.FirstGreaterThan<long>(arrayLong!, 0);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public long? NetFabric_Long()
        => TensorOperations.FirstGreaterThan(arrayLong!, 0L);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public Half? Baseline_Half()
        => Baseline.FirstGreaterThan<Half>(arrayHalf!, (Half)0);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public Half? NetFabric_Half()
        => TensorOperations.FirstGreaterThan<Half>(arrayHalf!, (Half)0);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public float? Baseline_Float()
        => Baseline.FirstGreaterThan<float>(arrayFloat!, 0.0f);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public float? NetFabric_Float()
        => TensorOperations.FirstGreaterThan<float>(arrayFloat!, 0.0f);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public double? Baseline_Double()
        => Baseline.FirstGreaterThan<double>(arrayDouble!, 0.0);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public double? NetFabric_Double()
        => TensorOperations.FirstGreaterThan(arrayDouble!, 0.0);
}