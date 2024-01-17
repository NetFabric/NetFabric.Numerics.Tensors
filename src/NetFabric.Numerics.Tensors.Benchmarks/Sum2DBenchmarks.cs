using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class Sum2DBenchmarks
{
    MyVector2<short>[]? arrayShort;
    MyVector2<int>[]? arrayInt;
    MyVector2<long>[]? arrayLong;
    MyVector2<Half>[]? arrayHalf;
    MyVector2<float>[]? arrayFloat;
    MyVector2<double>[]? arrayDouble;

    [Params(10_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        arrayShort = new MyVector2<short>[Count];
        arrayInt = new MyVector2<int>[Count];
        arrayLong = new MyVector2<long>[Count];
        arrayHalf = new MyVector2<Half>[Count];
        arrayFloat = new MyVector2<float>[Count];
        arrayDouble = new MyVector2<double>[Count];

        var random = new Random(42);
        for(var index = 0; index + 1 < Count; index += 2)
        {
            arrayShort[index] = new((short)random.Next(100), (short)random.Next(100));
            arrayInt[index] = new(random.Next(100), random.Next(100)); 
            arrayLong[index] = new((long)random.Next(100), (long)random.Next(100));
            arrayHalf[index] = new((Half)random.Next(100), (Half)random.Next(100));
            arrayFloat[index] = new((float)random.Next(100), (float)random.Next(100));
            arrayDouble[index] = new((double)random.Next(100), (double)random.Next(100));           
        }
    }

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public MyVector2<short> Baseline_Short()
        => arrayShort!.BaselineSum();

    [BenchmarkCategory("Short")]
    [Benchmark]
    public MyVector2<short> Tensor_Short()
    {
        var result = Tensor.Sum<short>(MemoryMarshal.Cast<MyVector2<short>, short>(arrayShort!), 2);
        return new(result[0], result[1]);
    }

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public MyVector2<int> Baseline_Int()
        => arrayInt!.BaselineSum();

    [BenchmarkCategory("Int")]
    [Benchmark]
    public MyVector2<int> Tensor_Int()
    {
        var result = Tensor.Sum<int>(MemoryMarshal.Cast<MyVector2<int>, int>(arrayInt!), 2);
        return new(result[0], result[1]);
    }

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public MyVector2<long> Baseline_Long()
        => arrayLong!.BaselineSum();

    [BenchmarkCategory("Long")]
    [Benchmark]
    public MyVector2<long> Tensor_Long()
    {
        var result = Tensor.Sum<long>(MemoryMarshal.Cast<MyVector2<long>, long>(arrayLong!), 2);
        return new(result[0], result[1]);
    }

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public MyVector2<Half> Baseline_Half()
        => arrayHalf!.BaselineSum();

    [BenchmarkCategory("Half")]
    [Benchmark]
    public MyVector2<Half> Tensor_Half()
    {
        var result = Tensor.Sum<Half>(MemoryMarshal.Cast<MyVector2<Half>, Half>(arrayHalf!), 2);
        return new(result[0], result[1]);
    }

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public MyVector2<float> Baseline_Float()
        => arrayFloat!.BaselineSum();

    [BenchmarkCategory("Float")]
    [Benchmark]
    public MyVector2<float> Tensor_Float()
    {
        var result = Tensor.Sum<float>(MemoryMarshal.Cast<MyVector2<float>, float>(arrayFloat!), 2);
        return new(result[0], result[1]);
    }

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public MyVector2<double> Baseline_Double()
        => arrayDouble!.BaselineSum();
}