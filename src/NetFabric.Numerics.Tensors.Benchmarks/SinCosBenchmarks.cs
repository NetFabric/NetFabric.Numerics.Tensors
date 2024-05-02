using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[Config(typeof(VectorizationConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[MemoryDiagnoser]
[MemoryRandomization]
public class SinCosBenchmarks
{
    Half[]? sourceHalf, sinResultHalf, cosResultHalf;
    float[]? sourceFloat, sinResultFloat, cosResultFloat;
    double[]? sourceDouble, sinResultDouble, cosResultDouble;

    [Params(100)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        sourceHalf = new Half[Count];
        sinResultHalf = new Half[Count];
        cosResultHalf = new Half[Count];
        sourceFloat = new float[Count];
        sinResultFloat = new float[Count];
        cosResultFloat = new float[Count];
        sourceDouble = new double[Count];
        sinResultDouble = new double[Count];
        cosResultDouble = new double[Count];

        var random = new Random(42);
        for(var index = 0; index < Count; index++)
        {
            var value = random.NextDouble() * 10.0;
            sourceHalf[index] = (Half)value;
            sourceFloat[index] = (float)value;
            sourceDouble[index] = value;
        }
    }

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public void Baseline_Half()
        => Baseline.SinCos<Half>(sourceHalf!, sinResultHalf!, cosResultHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void System_Half()
        => TensorPrimitives.SinCos<Half>(sourceHalf!, sinResultHalf!, cosResultHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void NetFabric_Half()
        => TensorOperations.SinCos<Half>(sourceHalf!, sinResultHalf!, cosResultHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public void Baseline_Float()
        => Baseline.SinCos<float>(sourceFloat!, sinResultFloat!, cosResultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void System_Float()
        => TensorPrimitives.SinCos<float>(sourceFloat!, sinResultFloat!, cosResultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void NetFabric_Float()
        => TensorOperations.SinCos<float>(sourceFloat!, sinResultFloat!, cosResultFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public void Baseline_Double()
        => Baseline.SinCos<double>(sourceDouble!, sinResultDouble!, cosResultDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void System_Double()
        => TensorPrimitives.SinCos<double>(sourceDouble!, sinResultDouble!, cosResultDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void NetFabric_Double()
        => TensorOperations.SinCos<double>(sourceDouble!, sinResultDouble!, cosResultDouble!);
}