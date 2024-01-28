using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[Config(typeof(VectorizationConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class NegateBenchmarks
{
    short[]? sourceShort, resultShort;
    int[]? sourceInt, resultInt;
    long[]? sourceLong, resultLong;
    Half[]? sourceHalf, resultHalf;
    float[]? sourceFloat, resultFloat;
    double[]? sourceDouble, resultDouble;

    [Params(1_000)]
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
        => Baseline.Negate<short>(sourceShort!, resultShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public void NetFabric_Short()
        => Tensor.Negate<short>(sourceShort!, resultShort!);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public void Baseline_Int()
        => Baseline.Negate<int>(sourceInt!, resultInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public void NetFabric_Int()
        => Tensor.Negate<int>(sourceInt!, resultInt!);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public void Baseline_Long()
        => Baseline.Negate<long>(sourceLong!, resultLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public void NetFabric_Long()
        => Tensor.Negate<long>(sourceLong!, resultLong!);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public void Baseline_Half()
        => Baseline.Negate<Half>(sourceHalf!, resultHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void NetFabric_Half()
        => Tensor.Negate<Half>(sourceHalf!, resultHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public void Baseline_Float()
        => Baseline.Negate<float>(sourceFloat!, resultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void System_Float()
        => TensorPrimitives.Negate(sourceFloat!, resultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void NetFabric_Float()
        => Tensor.Negate<float>(sourceFloat!, resultFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public void Baseline_Double()
        => Baseline.Negate<double>(sourceDouble!, resultDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void NetFabric_Double()
        => Tensor.Negate<double>(sourceDouble!, resultDouble!);
}