﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.Benchmarks;

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

    [Params(10_000)]
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
            sourceShort[index] = (short)random.Next(10);
            sourceInt[index] = random.Next(10);
            sourceLong[index] = random.Next(10);
            sourceHalf[index] = (Half)random.Next(10);
            sourceFloat[index] = random.Next(10);
            sourceDouble[index] = random.Next(10);
        }
    }

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public void Baseline_Short()
        => Baseline.Negate<short>(sourceShort!, resultShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public void Tensor_Short()
        => Tensor.Negate<short>(sourceShort!, resultShort!);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public void Baseline_Int()
        => Baseline.Negate<int>(sourceInt!, resultInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public void Tensor_Int()
        => Tensor.Negate<int>(sourceInt!, resultInt!);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public void Baseline_Long()
        => Baseline.Negate<long>(sourceLong!, resultLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public void Tensor_Long()
        => Tensor.Negate<long>(sourceLong!, resultLong!);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public void Baseline_Half()
        => Baseline.Negate<Half>(sourceHalf!, resultHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void Tensor_Half()
        => Tensor.Negate<Half>(sourceHalf!, resultHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public void Baseline_Float()
        => Baseline.Negate<float>(sourceFloat!, resultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void Tensor_Float()
        => Tensor.Negate<float>(sourceFloat!, resultFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public void Baseline_Double()
        => Baseline.Negate<double>(sourceDouble!, resultDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void Tensor_Double()
        => Tensor.Negate<double>(sourceDouble!, resultDouble!);
}