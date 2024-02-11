using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[Config(typeof(VectorizationConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class ProductBenchmarks
{
    short[]? arrayShort;
    int[]? arrayInt;
    long[]? arrayLong;
    Half[]? arrayHalf;
    float[]? arrayFloat;
    double[]? arrayDouble;

    [Params(1_000)]
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
            arrayShort[index] = (short)random.Next(10);
            arrayInt[index] = random.Next(10);
            arrayLong[index] = random.Next(10);
            arrayHalf[index] = (Half)random.Next(10);
            arrayFloat[index] = random.Next(10);
            arrayDouble[index] = random.Next(10);
        }
    }

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public short Baseline_Short()
        => Baseline.Product<short>(arrayShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public short LINQ_Short()
        => Enumerable.Aggregate(arrayShort!, (short)1, (sum, item) => (short)(sum * item));

    [BenchmarkCategory("Short")]
    [Benchmark]
    public short? NetFabric_Short()
        => Tensor.Product<short>(arrayShort!);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public int Baseline_Int()
        => Baseline.Product<int>(arrayInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int LINQ_Int()
        => Enumerable.Aggregate(arrayInt!, 1, (sum, item) => sum * item);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int? NetFabric_Int()
        => Tensor.Product<int>(arrayInt!);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public long Baseline_Long()
        => Baseline.Product<long>(arrayLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public long LINQ_Long()
        => Enumerable.Aggregate(arrayLong!, 1L, (sum, item) => sum * item);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public long? NetFabric_Long()
        => Tensor.Product<long>(arrayLong!);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public Half Baseline_Half()
        => Baseline.Product<Half>(arrayHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public Half LINQ_Half()
        => Enumerable.Aggregate(arrayHalf!, (Half)1, (sum, item) => (Half)(sum * item));

    [BenchmarkCategory("Half")]
    [Benchmark]
    public Half? NetFabric_Half()
        => Tensor.Product<Half>(arrayHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public float Baseline_Float()
        => Baseline.Product<float>(arrayFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public float? LINQ_Float()
        => Enumerable.Aggregate(arrayFloat!, 1.0f, (sum, item) => sum * item);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public float System_Float()
        => TensorPrimitives.Product(arrayFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public float? NetFabric_Float()
        => Tensor.Product<float>(arrayFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public double Baseline_Double()
        => Baseline.Product<double>(arrayDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public double LINQ_Double()
        => Enumerable.Aggregate(arrayDouble!, 1.0, (sum, item) => sum * item);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public double? NetFabric_Double()
        => Tensor.Product<double>(arrayDouble!);
}