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
public class IndexOfMinAggregateBenchmarks
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
        => Baseline.IndexOfMin<short>(arrayShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public int System_Short()
        => TensorPrimitives.IndexOfMin<short>(arrayShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public int NetFabric_Short()
        => TensorOperations.IndexOfMin<short>(arrayShort!);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public int Baseline_Int()
        => Baseline.IndexOfMin<int>(arrayInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int System_Int()
        => TensorPrimitives.IndexOfMin<int>(arrayInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int NetFabric_Int()
        => TensorOperations.IndexOfMin<int>(arrayInt!);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public int Baseline_Long()
        => Baseline.IndexOfMin<long>(arrayLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public int System_Long()
        => TensorPrimitives.IndexOfMin<long>(arrayLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public int NetFabric_Long()
        => TensorOperations.IndexOfMin<long>(arrayLong!);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public int Baseline_Half()
        => Baseline.IndexOfMin<Half>(arrayHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public int System_Half()
        => TensorPrimitives.IndexOfMin<Half>(arrayHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public int NetFabric_Half()
        => TensorOperations.IndexOfMin<Half>(arrayHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public int Baseline_Float()
        => Baseline.IndexOfMin<float>(arrayFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public int System_Float()
        => TensorPrimitives.IndexOfMin(arrayFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public int NetFabric_Float()
        => TensorOperations.IndexOfMin<float>(arrayFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public int Baseline_Double()
        => Baseline.IndexOfMin<double>(arrayDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public int System_Double()
        => TensorPrimitives.IndexOfMin<double>(arrayDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public int NetFabric_Double()
        => TensorOperations.IndexOfMin<double>(arrayDouble!);
}