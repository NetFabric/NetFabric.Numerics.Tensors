using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Runtime.InteropServices;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[Config(typeof(VectorizationConfig))]
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

    [Params(100)]
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
            var x = random.Next(10);
            var y = random.Next(10);
            arrayShort[index] = new((short)x, (short)y);
            arrayInt[index] = new(x, y); 
            arrayLong[index] = new(x, y);
            arrayHalf[index] = new((Half)x, (Half)y);
            arrayFloat[index] = new(x, y);
            arrayDouble[index] = new(x, y);           
        }
    }

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public MyVector2<short> Baseline_Short()
        => Baseline.SumNumber<MyVector2<short>>(arrayShort!);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public MyVector2<short> NetFabric_Short()
    {
        var result = TensorOperations.Sum2D<short>(MemoryMarshal.Cast<MyVector2<short>, short>(arrayShort!));
        return new(result);
    }

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public MyVector2<int> Baseline_Int()
        => Baseline.SumNumber<MyVector2<int>>(arrayInt!);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public MyVector2<int> NetFabric_Int()
    {
        var result = TensorOperations.Sum2D<int>(MemoryMarshal.Cast<MyVector2<int>, int>(arrayInt!));
        return new(result);
    }

    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public MyVector2<long> Baseline_Long()
        => Baseline.SumNumber<MyVector2<long>>(arrayLong!);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public MyVector2<long> NetFabric_Long()
    {
        var result = TensorOperations.Sum2D<long>(MemoryMarshal.Cast<MyVector2<long>, long>(arrayLong!));
        return new(result);
    }

    [BenchmarkCategory("Half")]
    [Benchmark(Baseline = true)]
    public MyVector2<Half> Baseline_Half()
        => Baseline.SumNumber<MyVector2<Half>>(arrayHalf!);

    [BenchmarkCategory("Half")]
    [Benchmark]
    public MyVector2<Half> NetFabric_Half()
    {
        var result = TensorOperations.Sum2D<Half>(MemoryMarshal.Cast<MyVector2<Half>, Half>(arrayHalf!));
        return new(result);
    }

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public MyVector2<float> Baseline_Float()
        => Baseline.SumNumber<MyVector2<float>>(arrayFloat!);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public MyVector2<float> NetFabric_Float()
    {
        var result = TensorOperations.Sum2D<float>(MemoryMarshal.Cast<MyVector2<float>, float>(arrayFloat!));
        return new(result);
    }

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public MyVector2<double> Baseline_Double()
        => Baseline.SumNumber<MyVector2<double>>(arrayDouble!);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public MyVector2<double> NetFabric_Double()
    {
        var result = TensorOperations.Sum2D<double>(MemoryMarshal.Cast<MyVector2<double>, double>(arrayDouble!));
        return new(result);
    }
}