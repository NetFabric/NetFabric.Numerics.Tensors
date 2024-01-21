using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class Sum4DBenchmarks
{
    MyVector4<short>[]? arrayShort;
    MyVector4<int>[]? arrayInt;
    MyVector4<long>[]? arrayLong;
    MyVector4<Half>[]? arrayHalf;
    MyVector4<float>[]? arrayFloat;
    MyVector4<double>[]? arrayDouble;

    [Params(10_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        arrayShort = new MyVector4<short>[Count];
        arrayInt = new MyVector4<int>[Count];
        arrayLong = new MyVector4<long>[Count];
        arrayHalf = new MyVector4<Half>[Count];
        arrayFloat = new MyVector4<float>[Count];
        arrayDouble = new MyVector4<double>[Count];

        var random = new Random(42);
        for(var index = 0; index + 1 < Count; index += 2)
        {
            arrayShort[index] = new((short)random.Next(10), (short)random.Next(10), (short)random.Next(10), (short)random.Next(10));
            arrayInt[index] = new(random.Next(10), random.Next(10), random.Next(10), random.Next(10));
            arrayLong[index] = new(random.Next(10), random.Next(10), random.Next(10), random.Next(10));
            arrayHalf[index] = new((Half)random.Next(10), (Half)random.Next(10), (Half)random.Next(10), (Half)random.Next(10));
            arrayFloat[index] = new(random.Next(10), random.Next(10), random.Next(10), random.Next(10));
            arrayDouble[index] = new(random.Next(10), random.Next(10), random.Next(10), random.Next(10));
        }
    }

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public MyVector4<short> Baseline_Short()
        => Baseline.Sum<MyVector4<short>>(arrayShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public MyVector4<short> Tensor_Short()
    {
        var result = Tensor.Sum4D<short>(MemoryMarshal.Cast<MyVector4<short>, short>(arrayShort!));
        return new(result[0], result[1], result[2], result[3]);
    }

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public MyVector4<int> Baseline_Int()
        => Baseline.Sum<MyVector4<int>>(arrayInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public MyVector4<int> LINQ_Int()
        => Enumerable.Aggregate(arrayInt!, MyVector4<int>.AdditiveIdentity, (sum, item) => sum + item);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public MyVector4<int> Tensor_Int()
    {
        var result = Tensor.Sum4D<int>(MemoryMarshal.Cast<MyVector4<int>, int>(arrayInt!));
        return new(result[0], result[1], result[2], result[3]);
    }

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public MyVector4<long> Baseline_Long()
        => Baseline.Sum<MyVector4<long>>(arrayLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public MyVector4<long> LINQ_Long()
        => Enumerable.Aggregate(arrayLong!, MyVector4<long>.AdditiveIdentity, (sum, item) => sum + item);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public MyVector4<long> Tensor_Long()
    {
        var result = Tensor.Sum4D<long>(MemoryMarshal.Cast<MyVector4<long>, long>(arrayLong!));
        return new(result[0], result[1], result[2], result[3]);
    }

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public MyVector4<Half> Baseline_Half()
        => Baseline.Sum<MyVector4<Half>>(arrayHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public MyVector4<Half> Tensor_Half()
    {
        var result = Tensor.Sum4D<Half>(MemoryMarshal.Cast<MyVector4<Half>, Half>(arrayHalf!));
        return new(result[0], result[1], result[2], result[3]);
    }

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public MyVector4<float> Baseline_Float()
        => Baseline.Sum<MyVector4<float>>(arrayFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public MyVector4<float> LINQ_Float()
        => Enumerable.Aggregate(arrayFloat!, MyVector4<float>.AdditiveIdentity, (sum, item) => sum + item);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public MyVector4<float> Tensor_Float()
    {
        var result = Tensor.Sum4D<float>(MemoryMarshal.Cast<MyVector4<float>, float>(arrayFloat!));
        return new(result[0], result[1], result[2], result[3]);
    }

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public MyVector4<double> Baseline_Double()
        => Baseline.Sum<MyVector4<double>>(arrayDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public MyVector4<double> LINQ_Double()
        => Enumerable.Aggregate(arrayDouble!, MyVector4<double>.AdditiveIdentity, (sum, item) => sum + item);
    
    [BenchmarkCategory("Double")]
    [Benchmark]
    public MyVector4<double> Tensor_Double()
    {
        var result = Tensor.Sum4D<double>(MemoryMarshal.Cast<MyVector4<double>, double>(arrayDouble!));
        return new(result[0], result[1], result[2], result[3]);
    }
}