using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[Config(typeof(VectorizationConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[MemoryDiagnoser]
[MemoryRandomization]
public class AddMultiplyBenchmarks
{
    short[]? sourceShort, otherShort, anotherShort, resultShort;
    int[]? sourceInt, otherInt, anotherInt, resultInt;
    long[]? sourceLong, otherLong, anotherLong, resultLong;
    Half[]? sourceHalf, otherHalf, anotherHalf, resultHalf;
    float[]? sourceFloat, otherFloat, anotherFloat, resultFloat;
    double[]? sourceDouble, otherDouble, anotherDouble, resultDouble;

    [Params(100)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        sourceShort = new short[Count];
        otherShort = new short[Count];
        anotherShort = new short[Count];
        resultShort = new short[Count];
        sourceInt = new int[Count];
        otherInt = new int[Count];
        anotherInt = new int[Count];
        resultInt = new int[Count];
        sourceLong = new long[Count];
        otherLong = new long[Count];
        anotherLong = new long[Count];
        resultLong = new long[Count];
        sourceHalf = new Half[Count];
        otherHalf = new Half[Count];
        anotherHalf = new Half[Count];
        resultHalf = new Half[Count];
        sourceFloat = new float[Count];
        otherFloat = new float[Count];
        anotherFloat = new float[Count];
        resultFloat = new float[Count];
        sourceDouble = new double[Count];
        otherDouble = new double[Count];
        anotherDouble = new double[Count];
        resultDouble = new double[Count];

        var random = new Random(42);
        for(var index = 0; index < Count; index++)
        {
            var value = random.Next(10);
            sourceShort[index] = (short)value;
            otherShort[index] = (short)value;
            anotherShort[index] = (short)value;
            sourceInt[index] = value;
            otherInt[index] = value;
            anotherInt[index] = value;
            sourceLong[index] = value;
            otherLong[index] = value;
            anotherLong[index] = value;
            sourceHalf[index] = (Half)value;
            otherHalf[index] = (Half)value;
            anotherHalf[index] = (Half)value;
            sourceFloat[index] = value;
            otherFloat[index] = value;
            anotherFloat[index] = value;
            sourceDouble[index] = value;
            otherDouble[index] = value;
            anotherDouble[index] = value;
        }
    }

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public void Baseline_Short()
        => Baseline.AddMultiply<short>(sourceShort!, otherShort!, anotherShort!, resultShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public void System_Short()
        => TensorPrimitives.AddMultiply<short>(sourceShort!, otherShort!, anotherShort!, resultShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public void NetFabric_Short()
        => TensorOperations.AddMultiply<short>(sourceShort!, otherShort!, anotherShort!, resultShort!);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public void Baseline_Int()
        => Baseline.AddMultiply<int>(sourceInt!, otherInt!, anotherInt!, resultInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public void System_Int()
        => TensorPrimitives.AddMultiply<int>(sourceInt!, otherInt!, anotherInt!, resultInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public void NetFabric_Int()
        => TensorOperations.AddMultiply<int>(sourceInt!, otherInt!, anotherInt!, resultInt!);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public void Baseline_Long()
        => Baseline.AddMultiply<long>(sourceLong!, otherLong!, anotherLong!, resultLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public void System_Long()
        => TensorPrimitives.AddMultiply<long>(sourceLong!, otherLong!, anotherLong!, resultLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public void NetFabric_Long()
        => TensorOperations.AddMultiply<long>(sourceLong!, otherLong!, anotherLong!, resultLong!);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public void Baseline_Half()
        => Baseline.AddMultiply<Half>(sourceHalf!, otherHalf!, anotherHalf!, resultHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void System_Half()
        => TensorPrimitives.AddMultiply<Half>(sourceHalf!, otherHalf!, anotherHalf!, resultHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void NetFabric_Half()
        => TensorOperations.AddMultiply<Half>(sourceHalf!, otherHalf!, anotherHalf!, resultHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public void Baseline_Float()
        => Baseline.AddMultiply<float>(sourceFloat!, otherFloat!, anotherFloat!, resultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void System_Float()
        => TensorPrimitives.AddMultiply(sourceFloat!, otherFloat!, anotherFloat!, resultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void NetFabric_Float()
        => TensorOperations.AddMultiply<float>(sourceFloat!, otherFloat!, anotherFloat!, resultFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public void Baseline_Double()
        => Baseline.AddMultiply<double>(sourceDouble!, otherDouble!, anotherDouble!, resultDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void System_Double()
        => TensorPrimitives.AddMultiply<double>(sourceDouble!, otherDouble!, anotherDouble!, resultDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void NetFabric_Double()
        => TensorOperations.AddMultiply<double>(sourceDouble!, otherDouble!, anotherDouble!, resultDouble!);
}