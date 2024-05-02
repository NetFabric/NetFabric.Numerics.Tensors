using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[Config(typeof(VectorizationConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[MemoryDiagnoser]
[MemoryRandomization]
public class IndexOfFirstGreaterThanBenchmarks
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
    public int Baseline_Short()
        => Baseline.IndexOfFirstGreaterThan<short>(arrayShort!, 0);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public int NetFabric_Short()
        => TensorOperations.IndexOfFirstGreaterThan<short>(arrayShort!, 0);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public int Baseline_Int()
        => Baseline.IndexOfFirstGreaterThan<int>(arrayInt!, 0);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int NetFabric_Int()
        => TensorOperations.IndexOfFirstGreaterThan(arrayInt!, 0);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public int Baseline_Long()
        => Baseline.IndexOfFirstGreaterThan<long>(arrayLong!, 0);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public int NetFabric_Long()
        => TensorOperations.IndexOfFirstGreaterThan(arrayLong!, 0L);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public int Baseline_Half()
        => Baseline.IndexOfFirstGreaterThan<Half>(arrayHalf!, (Half)0);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public int NetFabric_Half()
        => TensorOperations.IndexOfFirstGreaterThan<Half>(arrayHalf!, (Half)0);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public int Baseline_Float()
        => Baseline.IndexOfFirstGreaterThan<float>(arrayFloat!, 0.0f);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public int NetFabric_Float()
        => TensorOperations.IndexOfFirstGreaterThan<float>(arrayFloat!, 0.0f);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public int Baseline_Double()
        => Baseline.IndexOfFirstGreaterThan<double>(arrayDouble!, 0.0);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public int NetFabric_Double()
        => TensorOperations.IndexOfFirstGreaterThan(arrayDouble!, 0.0);
}