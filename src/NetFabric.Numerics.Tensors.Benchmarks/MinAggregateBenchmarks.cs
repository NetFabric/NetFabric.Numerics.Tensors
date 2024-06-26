﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[Config(typeof(VectorizationConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[MemoryDiagnoser]
[MemoryRandomization]
public class MinAggregateBenchmarks
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
            var value = random.Next(100) - 50;
            arrayShort[index] = (short)value;
            arrayInt[index] = value;
            arrayLong[index] = value;
            arrayHalf[index] = (Half)value;
            arrayFloat[index] = value;
            arrayDouble[index] = value;
        }
    }

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public short Baseline_Short()
        => Baseline.Min<short>(arrayShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public short System_Short()
        => TensorPrimitives.Min<short>(arrayShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public short NetFabric_Short()
        => TensorOperations.Min<short>(arrayShort!);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public int Baseline_Int()
        => Baseline.Min<int>(arrayInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int System_Int()
        => TensorPrimitives.Min<int>(arrayInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int NetFabric_Int()
        => TensorOperations.Min<int>(arrayInt!);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public long Baseline_Long()
        => Baseline.Min<long>(arrayLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public long System_Long()
        => TensorPrimitives.Min<long>(arrayLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public long NetFabric_Long()
        => TensorOperations.Min<long>(arrayLong!);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public Half Baseline_Half()
        => Baseline.Min<Half>(arrayHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public Half System_Half()
        => TensorPrimitives.Min<Half>(arrayHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public Half NetFabric_Half()
        => TensorOperations.Min<Half>(arrayHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public float Baseline_Float()
        => Baseline.Min<float>(arrayFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public float System_Float()
        => TensorPrimitives.Min(arrayFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public float NetFabric_Float()
        => TensorOperations.Min<float>(arrayFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public double Baseline_Double()
        => Baseline.Min<double>(arrayDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public double System_Double()
        => TensorPrimitives.Min<double>(arrayDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public double NetFabric_Double()
        => TensorOperations.Min<double>(arrayDouble!);
}