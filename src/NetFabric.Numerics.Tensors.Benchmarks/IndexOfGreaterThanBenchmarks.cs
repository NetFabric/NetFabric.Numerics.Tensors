using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[Config(typeof(VectorizationConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class IndexOfGreaterThanBenchmarks
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
    public int Baseline_Short()
        => Baseline.IndexOfGreaterThan<short>(arrayShort!, 0);

    // [BenchmarkCategory("Short")]
    // [Benchmark]
    // public int System_Short()
    //     => TensorPrimitives.IndexOfGreaterThan<short>(arrayShort!, 0);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public int NetFabric_Short()
        => TensorOperations.IndexOfGreaterThan<short>(arrayShort!, 0);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public int Baseline_Int()
        => Baseline.IndexOfGreaterThan<int>(arrayInt!, 0);

    // [BenchmarkCategory("Int")]
    // [Benchmark]
    // public int System_Int()
    //     => TensorPrimitives.IndexOfGreaterThan<int>(arrayInt!, 0);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int NetFabric_Int()
        => TensorOperations.IndexOfGreaterThan(arrayInt!, 0);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public int Baseline_Long()
        => Baseline.IndexOfGreaterThan<long>(arrayLong!, 0);

    // [BenchmarkCategory("Long")]
    // [Benchmark]
    // public int System_Long()
    //     => TensorPrimitives.IndexOfGreaterThan<long>(arrayLong!, 0);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public int NetFabric_Long()
        => TensorOperations.IndexOfGreaterThan(arrayLong!, 0L);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public int Baseline_Half()
        => Baseline.IndexOfGreaterThan<Half>(arrayHalf!, (Half)0);

    // [BenchmarkCategory("Half")]
    // [Benchmark]
    // public int System_Half()
    //     => TensorPrimitives.IndexOfGreaterThan<Half>(arrayHalf!, (Half)0);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public int NetFabric_Half()
        => TensorOperations.IndexOfGreaterThan<Half>(arrayHalf!, (Half)0);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public int Baseline_Float()
        => Baseline.IndexOfGreaterThan<float>(arrayFloat!, 0.0f);

    // [BenchmarkCategory("Float")]
    // [Benchmark]
    // public int System_Float()
    //     => TensorPrimitives.IndexOfGreaterThan(arrayFloat!, 0.0f);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public int NetFabric_Float()
        => TensorOperations.IndexOfGreaterThan<float>(arrayFloat!, 0.0f);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public int Baseline_Double()
        => Baseline.IndexOfGreaterThan<double>(arrayDouble!, 0.0);

    // [BenchmarkCategory("Double")]
    // [Benchmark]
    // public int System_Double()
    //     => TensorPrimitives.IndexOfGreaterThan<double>(arrayDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public int NetFabric_Double()
        => TensorOperations.IndexOfGreaterThan(arrayDouble!, 0.0);
}