﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[Config(typeof(VectorizationConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[MemoryDiagnoser]
[MemoryRandomization]
public class SumNumberBenchmarks
{
    short[]? arrayShort;
    int[]? arrayInt;
    long[]? arrayLong;
    Half[]? arrayHalf;
    float[]? arrayFloat;
    double[]? arrayDouble;

    [Params(100)]
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
        => Baseline.SumNumber<short>(arrayShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public short NetFabric_Short()
        => TensorOperations.SumNumber<short>(arrayShort!);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public int Baseline_Int()
        => Baseline.SumNumber<int>(arrayInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int NetFabric_Int()
        => TensorOperations.SumNumber<int>(arrayInt!);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public long Baseline_Long()
        => Baseline.SumNumber<long>(arrayLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public long NetFabric_Long()
        => TensorOperations.SumNumber<long>(arrayLong!);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public Half Baseline_Half()
        => Baseline.SumNumber<Half>(arrayHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public Half NetFabric_Half()
        => TensorOperations.SumNumber<Half>(arrayHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public float Baseline_Float()
        => Baseline.SumNumber<float>(arrayFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public float NetFabric_Float()
        => TensorOperations.SumNumber<float>(arrayFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public double Baseline_Double()
        => Baseline.SumNumber<double>(arrayDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public double NetFabric_Double()
        => TensorOperations.SumNumber<double>(arrayDouble!);
}