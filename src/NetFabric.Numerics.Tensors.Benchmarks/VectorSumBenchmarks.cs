using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Numerics.Tensors.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class VectorSumBenchmarks
{
    readonly Vector<short> vectorShort = new(1);
    readonly Vector<int> vectorInt = new(1);
    readonly Vector<long> vectorLong = new(1L);
    // readonly Vector<Half> vectorHalf = new((Half)1);
    readonly Vector<float> vectorFloat = new(1.0f);
    readonly Vector<double> vectorDouble = new(1.0);

    [BenchmarkCategory("Short")]
    [Benchmark(Baseline = true)]
    public short Sum_Short()
        => Vector.Sum(vectorShort);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public short Element_Short()
        => SumElement(vectorShort);

    [BenchmarkCategory("Short")]
    [Benchmark]
    public short Unsafe_Short()
        => SumUnsafe(vectorShort);

    [BenchmarkCategory("Int")]
    [Benchmark(Baseline = true)]
    public int Sum_Int()
        => Vector.Sum(vectorInt);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int Element_Int()
        => SumElement(vectorInt);

    [BenchmarkCategory("Int")]
    [Benchmark]
    public int Unsafe_Int()
        => SumUnsafe(vectorInt);
    
    [BenchmarkCategory("Long")]
    [Benchmark(Baseline = true)]
    public long Sum_Long()
        => Vector.Sum(vectorLong);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public long Element_Long()
        => SumElement(vectorLong);

    [BenchmarkCategory("Long")]
    [Benchmark]
    public long Unsafe_Long()
        => SumUnsafe(vectorLong);

    // [BenchmarkCategory("Half")]
    // [Benchmark(Baseline = true)]
    // public Half Sum_Half()
    //     => Vector.Sum(vectorHalf);

    // [BenchmarkCategory("Half")]
    // [Benchmark]
    // public Half Element_Half()
    //     => SumElement(vectorHalf);

    // [BenchmarkCategory("Half")]
    // [Benchmark]
    // public Half Unsafe_Half()
    //     => SumUnsafe(vectorHalf);

    [BenchmarkCategory("Float")]
    [Benchmark(Baseline = true)]
    public float Sum_Float()
        => Vector.Sum(vectorFloat);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public float Element_Float()
        => SumElement(vectorFloat);

    [BenchmarkCategory("Float")]
    [Benchmark]
    public float Unsafe_Float()
        => SumUnsafe(vectorFloat);

    [BenchmarkCategory("Double")]
    [Benchmark(Baseline = true)]
    public double Sum_Double()
        => Vector.Sum(vectorDouble);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public double Element_Double()
        => SumElement(vectorDouble);

    [BenchmarkCategory("Double")]
    [Benchmark]
    public double Unsafe_Double()
        => SumUnsafe(vectorDouble);


    static T Sum<T>(Vector<T> vector)
        where T : struct
        => Vector.Sum<T>(vector);
        
    static T SumElement<T>(Vector<T> vector)
        where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
    {
        var sum = T.AdditiveIdentity;
        for (var index = 0; index < Vector<T>.Count; index++)
            sum += vector[index];
        return sum;
    }

    static T SumUnsafe<T>(Vector<T> vector)
        where T : struct, IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
    {
        var sum = T.AdditiveIdentity;
        ref var address = ref Unsafe.As<Vector<T>, T>(ref Unsafe.AsRef(in vector));
        for (var index = 0; index < Vector<T>.Count; index++)
            sum += Unsafe.Add(ref address, index);
        return sum;
    }
}