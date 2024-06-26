﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Numerics.Tensors;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[Config(typeof(VectorizationConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[MemoryDiagnoser]
[MemoryRandomization]
public class AddValueBenchmarks
{
    short[]? sourceShort, resultShort;
    int[]? sourceInt, resultInt;
    long[]? sourceLong, resultLong;
    Half[]? sourceHalf, resultHalf;
    float[]? sourceFloat, resultFloat;
    double[]? sourceDouble, resultDouble;

    [Params(100)]
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
        => Baseline.Add<short>(sourceShort!, 42, resultShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public void System_Short()
        => TensorPrimitives.Add<short>(sourceShort!, 42, resultShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public void NetFabric_Short()
        => TensorOperations.Add<short>(sourceShort!, 42, resultShort!);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public void Baseline_Int()
        => Baseline.Add<int>(sourceInt!, 42, resultInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public void System_Int()
        => TensorPrimitives.Add<int>(sourceInt!, 42, resultInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public void NetFabric_Int()
        => TensorOperations.Add<int>(sourceInt!, 42, resultInt!);

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public void Baseline_Long()
        => Baseline.Add<long>(sourceLong!, 42, resultLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public void System_Long()
        => TensorPrimitives.Add<long>(sourceLong!, 42, resultLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public void NetFabric_Long()
        => TensorOperations.Add<long>(sourceLong!, 42, resultLong!);

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public void Baseline_Half()
        => Baseline.Add<Half>(sourceHalf!, (Half)42, resultHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void System_Half()
        => TensorPrimitives.Add<Half>(sourceHalf!, (Half)42, resultHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public void NetFabric_Half()
        => TensorOperations.Add<Half>(sourceHalf!, (Half)42, resultHalf!);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public void Baseline_Float()
        => Baseline.Add<float>(sourceFloat!, 42, resultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void System_Float()
        => TensorPrimitives.Add(sourceFloat!, 42, resultFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public void NetFabric_Float()
        => TensorOperations.Add<float>(sourceFloat!, 42, resultFloat!);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public void Baseline_Double()
        => Baseline.Add<double>(sourceDouble!, 42, resultDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void System_Double()
        => TensorPrimitives.Add<double>(sourceDouble!, 42, resultDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public void NetFabric_Double()
        => TensorOperations.Add<double>(sourceDouble!, 42, resultDouble!);
}