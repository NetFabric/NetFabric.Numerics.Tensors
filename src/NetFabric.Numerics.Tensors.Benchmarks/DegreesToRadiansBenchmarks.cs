using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[Config(typeof(VectorizationConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class DegreesToRadiansBenchmarks
{
    Half[]? sourceHalf, resultHalf;
    float[]? sourceFloat, resultFloat;
    double[]? sourceDouble, resultDouble;

    [Params(100)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        sourceHalf = new Half[Count];
        resultHalf = new Half[Count];
        sourceFloat = new float[Count];
        resultFloat = new float[Count];
        sourceDouble = new double[Count];
        resultDouble = new double[Count];

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
        => Baseline.DegreesToRadians<Half>(sourceHalf!, resultHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void System_Half()
        => TensorPrimitives.DegreesToRadians<Half>(sourceHalf!, resultHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void NetFabric_Half()
        => TensorOperations.DegreesToRadians<Half>(sourceHalf!, resultHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public void Baseline_Float()
        => Baseline.DegreesToRadians<float>(sourceFloat!, resultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void System_Float()
        => TensorPrimitives.DegreesToRadians<float>(sourceFloat!, resultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void NetFabric_Float()
        => TensorOperations.DegreesToRadians<float>(sourceFloat!, resultFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public void Baseline_Double()
        => Baseline.DegreesToRadians<double>(sourceDouble!, resultDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void System_Double()
        => TensorPrimitives.DegreesToRadians<double>(sourceDouble!, resultDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void NetFabric_Double()
        => TensorOperations.DegreesToRadians<double>(sourceDouble!, resultDouble!);
}