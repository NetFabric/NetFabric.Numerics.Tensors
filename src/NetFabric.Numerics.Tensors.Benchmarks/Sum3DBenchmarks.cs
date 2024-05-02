using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[Config(typeof(VectorizationConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
[MemoryDiagnoser]
[MemoryRandomization]
public class Sum3DBenchmarks
{
    MyVector3<short>[]? arrayShort;
    MyVector3<int>[]? arrayInt;
    MyVector3<long>[]? arrayLong;
    MyVector3<Half>[]? arrayHalf;
    MyVector3<float>[]? arrayFloat;
    MyVector3<double>[]? arrayDouble;

    [Params(100)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        arrayShort = new MyVector3<short>[Count];
        arrayInt = new MyVector3<int>[Count];
        arrayLong = new MyVector3<long>[Count];
        arrayHalf = new MyVector3<Half>[Count];
        arrayFloat = new MyVector3<float>[Count];
        arrayDouble = new MyVector3<double>[Count];

        var random = new Random(42);
        for(var index = 0; index + 1 < Count; index += 2)
        {
            arrayShort[index] = new((short)random.Next(10), (short)random.Next(10), (short)random.Next(10));
            arrayInt[index] = new(random.Next(10), random.Next(10), random.Next(10));
            arrayLong[index] = new(random.Next(10), random.Next(10), random.Next(10));
            arrayHalf[index] = new((Half)random.Next(10), (Half)random.Next(10), (Half)random.Next(10));
            arrayFloat[index] = new(random.Next(10), random.Next(10), random.Next(10));
            arrayDouble[index] = new(random.Next(10), random.Next(10), random.Next(10));
        }
    }

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public MyVector3<short> Baseline_Short()
        => Baseline.SumNumber<MyVector3<short>>(arrayShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public MyVector3<short> NetFabric_Short()
    {
        var result = TensorOperations.Sum3D<short>(MemoryMarshal.Cast<MyVector3<short>, short>(arrayShort!));
        return new(result);
    }

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public MyVector3<int> Baseline_Int()
        => Baseline.SumNumber<MyVector3<int>>(arrayInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public MyVector3<int> NetFabric_Int()
    {
        var result = TensorOperations.Sum3D<int>(MemoryMarshal.Cast<MyVector3<int>, int>(arrayInt!));
        return new(result);
    }

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public MyVector3<long> Baseline_Long()
        => Baseline.SumNumber<MyVector3<long>>(arrayLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public MyVector3<long> NetFabric_Long()
    {
        var result = TensorOperations.Sum3D<long>(MemoryMarshal.Cast<MyVector3<long>, long>(arrayLong!));
        return new(result);
    }

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public MyVector3<Half> Baseline_Half()
        => Baseline.SumNumber<MyVector3<Half>>(arrayHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public MyVector3<Half> NetFabric_Half()
    {
        var result = TensorOperations.Sum3D<Half>(MemoryMarshal.Cast<MyVector3<Half>, Half>(arrayHalf!));
        return new(result);
    }

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public MyVector3<float> Baseline_Float()
        => Baseline.SumNumber<MyVector3<float>>(arrayFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public MyVector3<float> NetFabric_Float()
    {
        var result = TensorOperations.Sum3D<float>(MemoryMarshal.Cast<MyVector3<float>, float>(arrayFloat!));
        return new(result);
    }

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public MyVector3<double> Baseline_Double()
        => Baseline.SumNumber<MyVector3<double>>(arrayDouble!);
    
    [BenchmarkCategory("Double")]
    [Benchmark]
    public MyVector3<double> NetFabric_Double()
    {
        var result = TensorOperations.Sum3D<double>(MemoryMarshal.Cast<MyVector3<double>, double>(arrayDouble!));
        return new(result);
    }
}