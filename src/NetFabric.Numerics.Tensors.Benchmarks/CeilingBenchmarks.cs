using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class CeilingBenchmarks
{
    Half[]? sourceHalf, resultHalf;
    float[]? sourceFloat, resultFloat;
    double[]? sourceDouble, resultDouble;

    [Params(1_000)]
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
        => Baseline.Ceiling<Half>(sourceHalf!, resultHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void Tensor_Half()
        => Tensor.Ceiling<Half>(sourceHalf!, resultHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public void Baseline_Float()
        => Baseline.Ceiling<float>(sourceFloat!, resultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void Tensor_Float()
        => Tensor.Ceiling<float>(sourceFloat!, resultFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public void Baseline_Double()
        => Baseline.Ceiling<double>(sourceDouble!, resultDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void Tensor_Double()
        => Tensor.Ceiling<double>(sourceDouble!, resultDouble!);
}