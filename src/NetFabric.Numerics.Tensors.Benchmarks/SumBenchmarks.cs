﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Runtime.InteropServices;
using System.Linq;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class SumBenchmarks
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
        => Baseline.Sum<short>(arrayShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public short Tensor_Short()
        => Tensor.Sum<short>(arrayShort!);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public int Baseline_Int()
        => Baseline.Sum<int>(arrayInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int LINQ_Int()
        => Enumerable.Sum(arrayInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int Tensor_Int()
        => Tensor.Sum<int>(arrayInt!);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public long Baseline_Long()
        => Baseline.Sum<long>(arrayLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public long LINQ_Long()
        => Enumerable.Sum(arrayLong!);    

    [BenchmarkCategory("Long")]
    [Benchmark]
    public long Tensor_Long()
        => Tensor.Sum<long>(arrayLong!);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public Half Baseline_Half()
        => Baseline.Sum<Half>(arrayHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public Half Tensor_Half()
        => Tensor.Sum<Half>(arrayHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public float Baseline_Float()
        => Baseline.Sum<float>(arrayFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public float LINQ_Float()
        => Enumerable.Sum(arrayFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public float Tensor_Float()
        => Tensor.Sum<float>(arrayFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public double Baseline_Double()
        => Baseline.Sum<double>(arrayDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public double LINQ_Double()
        => Enumerable.Sum(arrayDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public double Tensor_Double()
        => Tensor.Sum<double>(arrayDouble!);
}