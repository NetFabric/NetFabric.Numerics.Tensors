﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class AddBenchmarks
{
    short[]? sourceShort, otherShort, resultShort;
    int[]? sourceInt, otherInt, resultInt;
    long[]? sourceLong, otherLong, resultLong;
    Half[]? sourceHalf, otherHalf, resultHalf;
    float[]? sourceFloat, otherFloat, resultFloat;
    double[]? sourceDouble, otherDouble, resultDouble;

    [Params(10_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        sourceShort = new short[Count];
        otherShort = new short[Count];
        resultShort = new short[Count];
        sourceInt = new int[Count];
        otherInt = new int[Count];
        resultInt = new int[Count];
        sourceLong = new long[Count];
        otherLong = new long[Count];
        resultLong = new long[Count];
        sourceHalf = new Half[Count];
        otherHalf = new Half[Count];
        resultHalf = new Half[Count];
        sourceFloat = new float[Count];
        otherFloat = new float[Count];
        resultFloat = new float[Count];
        sourceDouble = new double[Count];
        otherDouble = new double[Count];
        resultDouble = new double[Count];

        var random = new Random(42);
        for(var index = 0; index < Count; index++)
        {
            var value = random.Next(10);
            sourceShort[index] = (short)value;
            otherShort[index] = (short)value;
            sourceInt[index] = value;
            otherInt[index] = value;
            sourceLong[index] = value;
            otherLong[index] = value;
            sourceHalf[index] = (Half)value;
            otherHalf[index] = (Half)value;
            sourceFloat[index] = value;
            otherFloat[index] = value;
            sourceDouble[index] = value;
            otherDouble[index] = value;
        }
    }

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public void Baseline_Short()
        => Baseline.Add<short>(sourceShort!, otherShort!, resultShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public void Tensor_Short()
        => Tensor.Add<short>(sourceShort!, otherShort!, resultShort!);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public void Baseline_Int()
        => Baseline.Add<int>(sourceInt!, otherInt!, resultInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public void Tensor_Int()
        => Tensor.Add<int>(sourceInt!, otherInt!, resultInt!);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public void Baseline_Long()
        => Baseline.Add<long>(sourceLong!, otherLong!, resultLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public void Tensor_Long()
        => Tensor.Add<long>(sourceLong!, otherLong!, resultLong!);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public void Baseline_Half()
        => Baseline.Add<Half>(sourceHalf!, otherHalf!, resultHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void Tensor_Half()
        => Tensor.Add<Half>(sourceHalf!, otherHalf!, resultHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public void Baseline_Float()
        => Baseline.Add<float>(sourceFloat!, otherFloat!, resultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void Tensor_Float()
        => Tensor.Add<float>(sourceFloat!, otherFloat!, resultFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public void Baseline_Double()
        => Baseline.Add<double>(sourceDouble!, otherDouble!, resultDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void Tensor_Double()
        => Tensor.Add<double>(sourceDouble!, otherDouble!, resultDouble!);
}