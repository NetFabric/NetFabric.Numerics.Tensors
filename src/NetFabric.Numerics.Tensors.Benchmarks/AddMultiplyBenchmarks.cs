using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class AddMultiplyBenchmarks
{
    short[]? sourceShort, otherShort, anotherShort, resultShort;
    int[]? sourceInt, otherInt, anotherInt, resultInt;
    long[]? sourceLong, otherLong, anotherLong, resultLong;
    Half[]? sourceHalf, otherHalf, anotherHalf, resultHalf;
    float[]? sourceFloat, otherFloat, anotherFloat, resultFloat;
    double[]? sourceDouble, otherDouble, anotherDouble, resultDouble;

    [Params(10_000)]
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
            sourceShort[index] = (short)random.Next(10);
            otherShort[index] = (short)random.Next(10);
            anotherShort[index] = (short)random.Next(10);
            sourceInt[index] = random.Next(10);
            otherInt[index] = random.Next(10);
            anotherInt[index] = random.Next(10);
            sourceLong[index] = random.Next(10);
            otherLong[index] = random.Next(10);
            anotherLong[index] = random.Next(10);
            sourceHalf[index] = (Half)random.Next(10);
            otherHalf[index] = (Half)random.Next(10);
            anotherHalf[index] = (Half)random.Next(10);
            sourceFloat[index] = random.Next(10);
            otherFloat[index] = random.Next(10);
            anotherFloat[index] = random.Next(10);
            sourceDouble[index] = random.Next(10);
            otherDouble[index] = random.Next(10);
            anotherDouble[index] = random.Next(10);
        }
    }

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public void Baseline_Short()
        => Baseline.AddMultiply<short>(sourceShort!, otherShort!, anotherShort!, resultShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public void Tensor_Short()
        => Tensor.AddMultiply<short>(sourceShort!, otherShort!, anotherShort!, resultShort!);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public void Baseline_Int()
        => Baseline.AddMultiply<int>(sourceInt!, otherInt!, anotherInt!, resultInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public void Tensor_Int()
        => Tensor.AddMultiply<int>(sourceInt!, otherInt!, anotherInt!, resultInt!);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public void Baseline_Long()
        => Baseline.AddMultiply<long>(sourceLong!, otherLong!, anotherLong!, resultLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public void Tensor_Long()
        => Tensor.AddMultiply<long>(sourceLong!, otherLong!, anotherLong!, resultLong!);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public void Baseline_Half()
        => Baseline.AddMultiply<Half>(sourceHalf!, otherHalf!, anotherHalf!, resultHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void Tensor_Half()
        => Tensor.AddMultiply<Half>(sourceHalf!, otherHalf!, anotherHalf!, resultHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public void Baseline_Float()
        => Baseline.AddMultiply<float>(sourceFloat!, otherFloat!, anotherFloat!, resultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void Tensor_Float()
        => Tensor.AddMultiply<float>(sourceFloat!, otherFloat!, anotherFloat!, resultFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public void Baseline_Double()
        => Baseline.AddMultiply<double>(sourceDouble!, otherDouble!, anotherDouble!, resultDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void Tensor_Double()
        => Tensor.AddMultiply<double>(sourceDouble!, otherDouble!, anotherDouble!, resultDouble!);
}