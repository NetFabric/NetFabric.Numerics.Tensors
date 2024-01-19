﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class Sum1DBenchmarks
{
    short[]? arrayShort;
    int[]? arrayInt;
    long[]? arrayLong;
    Half[]? arrayHalf;
    float[]? arrayFloat;
    double[]? arrayDouble;

    [Params(10_000)]
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
            arrayShort[index] = (short)random.Next(100);
            arrayInt[index] = random.Next(100);
            arrayLong[index] = random.Next(100);
            arrayHalf[index] = (Half)random.Next(100);
            arrayFloat[index] = random.Next(100);
            arrayDouble[index] = random.Next(100);
        }
    }

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public short Baseline_Short()
        => arrayShort!.BaselineSum();

    [BenchmarkCategory("Short")]
    [Benchmark]
    public short Tensor_Short()
        => Tensor.Sum1D<short>(arrayShort!);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public int Baseline_Int()
        => arrayInt!.BaselineSum();

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int Tensor_Int()
        => Tensor.Sum1D<int>(arrayInt!);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public long Baseline_Long()
        => arrayLong!.BaselineSum();

    [BenchmarkCategory("Long")]
    [Benchmark]
    public long Tensor_Long()
        => Tensor.Sum1D<long>(arrayLong!);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public Half Baseline_Half()
        => arrayHalf!.BaselineSum();

    [BenchmarkCategory("Half")]
    [Benchmark]
    public Half Tensor_Half()
        => Tensor.Sum1D<Half>(arrayHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public float Baseline_Float()
        => arrayFloat!.BaselineSum();

    [BenchmarkCategory("Float")]
    [Benchmark]
    public float Tensor_Float()
        => Tensor.Sum1D<float>(arrayFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public double Baseline_Double()
        => arrayDouble!.BaselineSum();

    [BenchmarkCategory("Double")]
    [Benchmark]
    public double Tensor_Double()
        => Tensor.Sum1D<double>(arrayDouble!);
}