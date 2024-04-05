# Benchmarks

This document presents benchmarks conducted to evaluate various operations on a specific system configuration. The system details are as follows:

```
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3296/23H2/2023Update/SunValley3)
AMD Ryzen 9 7940HS w/ Radeon 780M Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.101
  [Host]    : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Scalar    : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT
  Vector128 : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX
  Vector256 : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  Vector512 : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
```

The system supports vectorization up to 512 bits. Benchmarks are conducted both with and without vectorization for each supported vectorization size.

## Benchmark Jobs

Four distinct jobs are included in the benchmarks:

- Scalar: No SIMD support
- Vector128: Utilizing 128-bit SIMD support
- Vector256: Utilizing 256-bit SIMD support
- Vector512: Utilizing 512-bit SIMD support

## Benchmark Scenarios

The benchmarks encompass the following scenarios:

- Baseline\_\*: Simple iteration without explicit optimizations
- LINQ\_\*: Utilizing LINQ (when available)
- System\_\*: Utilizing `System.Numerics.Tensors`
- NetFabric\_\*: Utilizing `NetFabric.Numerics.Tensors`

The source code for the benchmarks can be accessed [here](https://github.com/NetFabric/NetFabric.Numerics.Tensors/tree/main/src/NetFabric.Numerics.Tensors.Benchmarks).

## Test Cases and Considerations

Benchmarks were conducted on small source spans (5 items) as well as larger ones (100 items). Various scenarios of operators and their applications were covered. The baseline for each scenario involved equivalent operations using a `for` loop, with no optimizations applied other than those automatically added by the JIT compiler.

It's important to note that while these benchmarks provide insights into the performance characteristics of the library, they are not exhaustive and are not intended to serve as a comprehensive performance analysis. They aim to offer a general understanding of the library's performance under different scenarios.

## Results

### Negate

Applying a vectorizable unary operator on a span.

| Method           | Job       | Categories | Count |       Mean |    StdDev |        Ratio |
| ---------------- | --------- | ---------- | ----- | ---------: | --------: | -----------: |
| Baseline_Double  | Scalar    | Double     | 100   |  45.573 ns | 0.2966 ns |     baseline |
| System_Double    | Scalar    | Double     | 100   |  29.756 ns | 0.1381 ns | 1.53x faster |
| NetFabric_Double | Scalar    | Double     | 100   |  26.658 ns | 0.1550 ns | 1.71x faster |
| Baseline_Double  | Vector128 | Double     | 100   |  47.152 ns | 2.1215 ns | 1.06x slower |
| System_Double    | Vector128 | Double     | 100   |  12.626 ns | 0.1176 ns | 3.61x faster |
| NetFabric_Double | Vector128 | Double     | 100   |  14.744 ns | 0.2733 ns | 3.09x faster |
| Baseline_Double  | Vector256 | Double     | 100   |  32.666 ns | 0.3011 ns | 1.40x faster |
| System_Double    | Vector256 | Double     | 100   |  10.927 ns | 0.0521 ns | 4.17x faster |
| NetFabric_Double | Vector256 | Double     | 100   |   9.304 ns | 0.0298 ns | 4.90x faster |
| Baseline_Double  | Vector512 | Double     | 100   |  47.302 ns | 1.9834 ns | 1.05x slower |
| System_Double    | Vector512 | Double     | 100   |  11.154 ns | 0.0398 ns | 4.09x faster |
| NetFabric_Double | Vector512 | Double     | 100   |   9.813 ns | 0.0464 ns | 4.64x faster |
|                  |           |            |       |            |           |              |
| Baseline_Float   | Scalar    | Float      | 100   |  45.061 ns | 0.2763 ns |     baseline |
| System_Float     | Scalar    | Float      | 100   |  29.691 ns | 0.6771 ns | 1.52x faster |
| NetFabric_Float  | Scalar    | Float      | 100   |  27.657 ns | 0.4055 ns | 1.63x faster |
| Baseline_Float   | Vector128 | Float      | 100   |  47.998 ns | 2.1608 ns | 1.05x slower |
| System_Float     | Vector128 | Float      | 100   |   9.232 ns | 0.0872 ns | 4.88x faster |
| NetFabric_Float  | Vector128 | Float      | 100   |   9.161 ns | 0.0973 ns | 4.92x faster |
| Baseline_Float   | Vector256 | Float      | 100   |  32.591 ns | 0.2715 ns | 1.38x faster |
| System_Float     | Vector256 | Float      | 100   |   8.053 ns | 0.0349 ns | 5.59x faster |
| NetFabric_Float  | Vector256 | Float      | 100   |   7.088 ns | 0.0503 ns | 6.35x faster |
| Baseline_Float   | Vector512 | Float      | 100   |  47.134 ns | 2.0154 ns | 1.05x slower |
| System_Float     | Vector512 | Float      | 100   |   8.638 ns | 0.0700 ns | 5.22x faster |
| NetFabric_Float  | Vector512 | Float      | 100   |   8.214 ns | 0.0763 ns | 5.48x faster |
|                  |           |            |       |            |           |              |
| Baseline_Half    | Scalar    | Half       | 100   | 767.767 ns | 2.5521 ns |     baseline |
| System_Half      | Scalar    | Half       | 100   | 760.971 ns | 1.9508 ns | 1.01x faster |
| NetFabric_Half   | Scalar    | Half       | 100   | 758.768 ns | 2.0240 ns | 1.01x faster |
| Baseline_Half    | Vector128 | Half       | 100   | 642.664 ns | 0.7075 ns | 1.19x faster |
| System_Half      | Vector128 | Half       | 100   | 639.995 ns | 2.0392 ns | 1.20x faster |
| NetFabric_Half   | Vector128 | Half       | 100   | 644.216 ns | 2.7896 ns | 1.19x faster |
| Baseline_Half    | Vector256 | Half       | 100   | 647.412 ns | 4.6062 ns | 1.19x faster |
| System_Half      | Vector256 | Half       | 100   | 639.948 ns | 4.1512 ns | 1.20x faster |
| NetFabric_Half   | Vector256 | Half       | 100   | 642.387 ns | 3.0127 ns | 1.20x faster |
| Baseline_Half    | Vector512 | Half       | 100   | 644.374 ns | 2.6637 ns | 1.19x faster |
| System_Half      | Vector512 | Half       | 100   | 644.819 ns | 6.5954 ns | 1.19x faster |
| NetFabric_Half   | Vector512 | Half       | 100   | 649.468 ns | 3.9853 ns | 1.18x faster |
|                  |           |            |       |            |           |              |
| Baseline_Int     | Scalar    | Int        | 100   |  35.906 ns | 0.4704 ns |     baseline |
| System_Int       | Scalar    | Int        | 100   |  29.608 ns | 0.7775 ns | 1.21x faster |
| NetFabric_Int    | Scalar    | Int        | 100   |  30.740 ns | 0.2080 ns | 1.17x faster |
| Baseline_Int     | Vector128 | Int        | 100   |  36.527 ns | 0.3254 ns | 1.02x slower |
| System_Int       | Vector128 | Int        | 100   |   9.831 ns | 0.0608 ns | 3.65x faster |
| NetFabric_Int    | Vector128 | Int        | 100   |  13.548 ns | 0.0619 ns | 2.65x faster |
| Baseline_Int     | Vector256 | Int        | 100   |  36.767 ns | 0.4214 ns | 1.03x slower |
| System_Int       | Vector256 | Int        | 100   |   7.482 ns | 0.0695 ns | 4.80x faster |
| NetFabric_Int    | Vector256 | Int        | 100   |  12.252 ns | 0.0617 ns | 2.93x faster |
| Baseline_Int     | Vector512 | Int        | 100   |  36.467 ns | 0.1628 ns | 1.02x slower |
| System_Int       | Vector512 | Int        | 100   |  12.493 ns | 0.0274 ns | 2.87x faster |
| NetFabric_Int    | Vector512 | Int        | 100   |  11.466 ns | 0.0437 ns | 3.13x faster |
|                  |           |            |       |            |           |              |
| Baseline_Long    | Scalar    | Long       | 100   |  36.301 ns | 0.2350 ns |     baseline |
| System_Long      | Scalar    | Long       | 100   |  29.256 ns | 0.1246 ns | 1.24x faster |
| NetFabric_Long   | Scalar    | Long       | 100   |  31.201 ns | 0.1614 ns | 1.16x faster |
| Baseline_Long    | Vector128 | Long       | 100   |  36.455 ns | 0.2352 ns | 1.00x slower |
| System_Long      | Vector128 | Long       | 100   |  14.563 ns | 0.1236 ns | 2.49x faster |
| NetFabric_Long   | Vector128 | Long       | 100   |  18.382 ns | 0.1982 ns | 1.98x faster |
| Baseline_Long    | Vector256 | Long       | 100   |  36.484 ns | 0.3588 ns | 1.00x slower |
| System_Long      | Vector256 | Long       | 100   |  11.685 ns | 0.0431 ns | 3.11x faster |
| NetFabric_Long   | Vector256 | Long       | 100   |  10.807 ns | 0.0716 ns | 3.36x faster |
| Baseline_Long    | Vector512 | Long       | 100   |  36.663 ns | 0.2058 ns | 1.01x slower |
| System_Long      | Vector512 | Long       | 100   |  10.474 ns | 0.0600 ns | 3.47x faster |
| NetFabric_Long   | Vector512 | Long       | 100   |  10.635 ns | 0.1332 ns | 3.41x faster |
|                  |           |            |       |            |           |              |
| Baseline_Short   | Scalar    | Short      | 100   |  40.951 ns | 0.3544 ns |     baseline |
| System_Short     | Scalar    | Short      | 100   |  40.886 ns | 0.4036 ns | 1.00x faster |
| NetFabric_Short  | Scalar    | Short      | 100   |  36.349 ns | 0.2416 ns | 1.13x faster |
| Baseline_Short   | Vector128 | Short      | 100   |  41.558 ns | 0.2594 ns | 1.01x slower |
| System_Short     | Vector128 | Short      | 100   |   7.253 ns | 0.0736 ns | 5.65x faster |
| NetFabric_Short  | Vector128 | Short      | 100   |   8.133 ns | 0.0460 ns | 5.04x faster |
| Baseline_Short   | Vector256 | Short      | 100   |  41.490 ns | 0.3011 ns | 1.01x slower |
| System_Short     | Vector256 | Short      | 100   |   5.168 ns | 0.0518 ns | 7.93x faster |
| NetFabric_Short  | Vector256 | Short      | 100   |   6.172 ns | 0.0951 ns | 6.64x faster |
| Baseline_Short   | Vector512 | Short      | 100   |  41.288 ns | 0.1954 ns | 1.01x slower |
| System_Short     | Vector512 | Short      | 100   |   5.517 ns | 0.0320 ns | 7.43x faster |
| NetFabric_Short  | Vector512 | Short      | 100   |   6.571 ns | 0.0211 ns | 6.24x faster |

### Add

Applying a vectorizable binary operator on two spans.

| Method           | Job       | Categories | Count |       Mean |    StdDev |        Ratio |
| ---------------- | --------- | ---------- | ----- | ---------: | --------: | -----------: |
| Baseline_Double  | Scalar    | Double     | 100   |  42.838 ns | 0.5474 ns |     baseline |
| System_Double    | Scalar    | Double     | 100   |  30.240 ns | 0.1343 ns | 1.42x faster |
| NetFabric_Double | Scalar    | Double     | 100   |  27.300 ns | 0.1176 ns | 1.57x faster |
| Baseline_Double  | Vector128 | Double     | 100   |  47.762 ns | 0.6866 ns | 1.12x slower |
| System_Double    | Vector128 | Double     | 100   |  20.282 ns | 0.0841 ns | 2.11x faster |
| NetFabric_Double | Vector128 | Double     | 100   |  20.364 ns | 0.1156 ns | 2.10x faster |
| Baseline_Double  | Vector256 | Double     | 100   |  47.177 ns | 0.3005 ns | 1.10x slower |
| System_Double    | Vector256 | Double     | 100   |  16.653 ns | 0.0518 ns | 2.57x faster |
| NetFabric_Double | Vector256 | Double     | 100   |  13.296 ns | 0.0712 ns | 3.22x faster |
| Baseline_Double  | Vector512 | Double     | 100   |  47.769 ns | 0.4299 ns | 1.12x slower |
| System_Double    | Vector512 | Double     | 100   |  17.823 ns | 0.0964 ns | 2.40x faster |
| NetFabric_Double | Vector512 | Double     | 100   |  13.988 ns | 0.0918 ns | 3.06x faster |
|                  |           |            |       |            |           |              |
| Baseline_Float   | Scalar    | Float      | 100   |  46.910 ns | 0.2536 ns |     baseline |
| System_Float     | Scalar    | Float      | 100   |  30.420 ns | 0.1175 ns | 1.54x faster |
| NetFabric_Float  | Scalar    | Float      | 100   |  27.097 ns | 0.1320 ns | 1.73x faster |
| Baseline_Float   | Vector128 | Float      | 100   |  40.948 ns | 0.6031 ns | 1.14x faster |
| System_Float     | Vector128 | Float      | 100   |  14.220 ns | 0.0738 ns | 3.30x faster |
| NetFabric_Float  | Vector128 | Float      | 100   |  12.781 ns | 0.0379 ns | 3.67x faster |
| Baseline_Float   | Vector256 | Float      | 100   |  40.751 ns | 0.3792 ns | 1.15x faster |
| System_Float     | Vector256 | Float      | 100   |  10.541 ns | 0.0632 ns | 4.45x faster |
| NetFabric_Float  | Vector256 | Float      | 100   |  10.306 ns | 0.0569 ns | 4.55x faster |
| Baseline_Float   | Vector512 | Float      | 100   |  40.859 ns | 0.5954 ns | 1.15x faster |
| System_Float     | Vector512 | Float      | 100   |  11.316 ns | 0.0517 ns | 4.15x faster |
| NetFabric_Float  | Vector512 | Float      | 100   |  10.274 ns | 0.0545 ns | 4.57x faster |
|                  |           |            |       |            |           |              |
| Baseline_Half    | Scalar    | Half       | 100   | 999.391 ns | 0.9589 ns |     baseline |
| System_Half      | Scalar    | Half       | 100   | 997.099 ns | 3.8572 ns | 1.00x faster |
| NetFabric_Half   | Scalar    | Half       | 100   | 995.374 ns | 3.8658 ns | 1.00x faster |
| Baseline_Half    | Vector128 | Half       | 100   | 911.213 ns | 3.2180 ns | 1.10x faster |
| System_Half      | Vector128 | Half       | 100   | 885.748 ns | 2.5029 ns | 1.13x faster |
| NetFabric_Half   | Vector128 | Half       | 100   | 887.482 ns | 3.0970 ns | 1.13x faster |
| Baseline_Half    | Vector256 | Half       | 100   | 915.089 ns | 6.6291 ns | 1.09x faster |
| System_Half      | Vector256 | Half       | 100   | 887.685 ns | 3.2944 ns | 1.13x faster |
| NetFabric_Half   | Vector256 | Half       | 100   | 887.629 ns | 3.2086 ns | 1.13x faster |
| Baseline_Half    | Vector512 | Half       | 100   | 912.210 ns | 2.0226 ns | 1.10x faster |
| System_Half      | Vector512 | Half       | 100   | 886.560 ns | 2.3231 ns | 1.13x faster |
| NetFabric_Half   | Vector512 | Half       | 100   | 885.757 ns | 1.8336 ns | 1.13x faster |
|                  |           |            |       |            |           |              |
| Baseline_Int     | Scalar    | Int        | 100   |  47.679 ns | 0.3331 ns |     baseline |
| System_Int       | Scalar    | Int        | 100   |  37.232 ns | 0.1545 ns | 1.28x faster |
| NetFabric_Int    | Scalar    | Int        | 100   |  33.680 ns | 0.1329 ns | 1.42x faster |
| Baseline_Int     | Vector128 | Int        | 100   |  50.704 ns | 0.1286 ns | 1.06x slower |
| System_Int       | Vector128 | Int        | 100   |  15.799 ns | 0.0674 ns | 3.02x faster |
| NetFabric_Int    | Vector128 | Int        | 100   |  13.488 ns | 0.1433 ns | 3.54x faster |
| Baseline_Int     | Vector256 | Int        | 100   |  48.321 ns | 0.3998 ns | 1.01x slower |
| System_Int       | Vector256 | Int        | 100   |  12.430 ns | 0.0301 ns | 3.84x faster |
| NetFabric_Int    | Vector256 | Int        | 100   |  10.519 ns | 0.1974 ns | 4.53x faster |
| Baseline_Int     | Vector512 | Int        | 100   |  48.420 ns | 0.3729 ns | 1.02x slower |
| System_Int       | Vector512 | Int        | 100   |  13.326 ns | 0.0623 ns | 3.58x faster |
| NetFabric_Int    | Vector512 | Int        | 100   |  10.261 ns | 0.0552 ns | 4.65x faster |
|                  |           |            |       |            |           |              |
| Baseline_Long    | Scalar    | Long       | 100   |  47.667 ns | 0.3977 ns |     baseline |
| System_Long      | Scalar    | Long       | 100   |  36.912 ns | 0.1346 ns | 1.29x faster |
| NetFabric_Long   | Scalar    | Long       | 100   |  35.274 ns | 0.1377 ns | 1.35x faster |
| Baseline_Long    | Vector128 | Long       | 100   |  47.490 ns | 0.6803 ns | 1.00x faster |
| System_Long      | Vector128 | Long       | 100   |  20.943 ns | 0.0900 ns | 2.28x faster |
| NetFabric_Long   | Vector128 | Long       | 100   |  25.661 ns | 0.1937 ns | 1.86x faster |
| Baseline_Long    | Vector256 | Long       | 100   |  48.330 ns | 0.6507 ns | 1.01x slower |
| System_Long      | Vector256 | Long       | 100   |  17.558 ns | 0.0552 ns | 2.71x faster |
| NetFabric_Long   | Vector256 | Long       | 100   |  14.723 ns | 0.1270 ns | 3.24x faster |
| Baseline_Long    | Vector512 | Long       | 100   |  47.401 ns | 0.5282 ns | 1.01x faster |
| System_Long      | Vector512 | Long       | 100   |  17.561 ns | 0.0920 ns | 2.71x faster |
| NetFabric_Long   | Vector512 | Long       | 100   |  13.943 ns | 0.0869 ns | 3.42x faster |
|                  |           |            |       |            |           |              |
| Baseline_Short   | Scalar    | Short      | 100   |  52.775 ns | 0.2422 ns |     baseline |
| System_Short     | Scalar    | Short      | 100   |  45.858 ns | 0.1945 ns | 1.15x faster |
| NetFabric_Short  | Scalar    | Short      | 100   |  43.392 ns | 0.1510 ns | 1.22x faster |
| Baseline_Short   | Vector128 | Short      | 100   |  54.653 ns | 1.5272 ns | 1.04x slower |
| System_Short     | Vector128 | Short      | 100   |   9.671 ns | 0.0582 ns | 5.45x faster |
| NetFabric_Short  | Vector128 | Short      | 100   |  11.470 ns | 0.1263 ns | 4.60x faster |
| Baseline_Short   | Vector256 | Short      | 100   |  52.536 ns | 0.3891 ns | 1.00x faster |
| System_Short     | Vector256 | Short      | 100   |   6.239 ns | 0.0472 ns | 8.46x faster |
| NetFabric_Short  | Vector256 | Short      | 100   |   9.148 ns | 0.0540 ns | 5.77x faster |
| Baseline_Short   | Vector512 | Short      | 100   |  52.830 ns | 0.3333 ns | 1.00x slower |
| System_Short     | Vector512 | Short      | 100   |   7.545 ns | 0.0464 ns | 7.00x faster |
| NetFabric_Short  | Vector512 | Short      | 100   |   9.329 ns | 0.0400 ns | 5.66x faster |

### Min

Applying a vectorizable binary operator with propagation of `NaN`.

| Method           | Job       | Categories | Count |       Mean |    StdDev |        Ratio |
| ---------------- | --------- | ---------- | ----- | ---------: | --------: | -----------: |
| Baseline_Double  | Scalar    | Double     | 100   |  77.822 ns | 0.3262 ns |     baseline |
| System_Double    | Scalar    | Double     | 100   |  68.871 ns | 0.4840 ns | 1.13x faster |
| NetFabric_Double | Scalar    | Double     | 100   |  86.981 ns | 0.6853 ns | 1.12x slower |
| Baseline_Double  | Vector128 | Double     | 100   |  96.596 ns | 1.7143 ns | 1.24x slower |
| System_Double    | Vector128 | Double     | 100   |  34.331 ns | 0.2659 ns | 2.27x faster |
| NetFabric_Double | Vector128 | Double     | 100   |  39.739 ns | 0.2777 ns | 1.96x faster |
| Baseline_Double  | Vector256 | Double     | 100   |  82.876 ns | 1.2040 ns | 1.06x slower |
| System_Double    | Vector256 | Double     | 100   |  20.034 ns | 0.1042 ns | 3.89x faster |
| NetFabric_Double | Vector256 | Double     | 100   |  21.825 ns | 0.1232 ns | 3.57x faster |
| Baseline_Double  | Vector512 | Double     | 100   |  51.904 ns | 0.2927 ns | 1.50x faster |
| System_Double    | Vector512 | Double     | 100   |  27.181 ns | 0.1602 ns | 2.86x faster |
| NetFabric_Double | Vector512 | Double     | 100   |  21.818 ns | 0.0917 ns | 3.57x faster |
|                  |           |            |       |            |           |              |
| Baseline_Float   | Scalar    | Float      | 100   |  78.342 ns | 0.8300 ns |     baseline |
| System_Float     | Scalar    | Float      | 100   |  68.697 ns | 0.4501 ns | 1.14x faster |
| NetFabric_Float  | Scalar    | Float      | 100   |  85.124 ns | 0.6446 ns | 1.09x slower |
| Baseline_Float   | Vector128 | Float      | 100   | 101.167 ns | 0.5612 ns | 1.29x slower |
| System_Float     | Vector128 | Float      | 100   |  19.894 ns | 0.1286 ns | 3.94x faster |
| NetFabric_Float  | Vector128 | Float      | 100   |  21.724 ns | 0.1460 ns | 3.61x faster |
| Baseline_Float   | Vector256 | Float      | 100   | 102.121 ns | 1.1186 ns | 1.30x slower |
| System_Float     | Vector256 | Float      | 100   |  13.085 ns | 0.1873 ns | 5.99x faster |
| NetFabric_Float  | Vector256 | Float      | 100   |  14.734 ns | 0.0741 ns | 5.32x faster |
| Baseline_Float   | Vector512 | Float      | 100   |  51.630 ns | 0.2217 ns | 1.52x faster |
| System_Float     | Vector512 | Float      | 100   |  13.855 ns | 0.0906 ns | 5.65x faster |
| NetFabric_Float  | Vector512 | Float      | 100   |  13.948 ns | 0.0720 ns | 5.61x faster |
|                  |           |            |       |            |           |              |
| Baseline_Half    | Scalar    | Half       | 100   | 883.888 ns | 2.8508 ns |     baseline |
| System_Half      | Scalar    | Half       | 100   | 880.562 ns | 1.4729 ns | 1.00x faster |
| NetFabric_Half   | Scalar    | Half       | 100   | 893.825 ns | 2.5456 ns | 1.01x slower |
| Baseline_Half    | Vector128 | Half       | 100   | 757.449 ns | 4.0445 ns | 1.17x faster |
| System_Half      | Vector128 | Half       | 100   | 763.809 ns | 2.6164 ns | 1.16x faster |
| NetFabric_Half   | Vector128 | Half       | 100   | 760.726 ns | 4.4007 ns | 1.16x faster |
| Baseline_Half    | Vector256 | Half       | 100   | 756.132 ns | 3.4296 ns | 1.17x faster |
| System_Half      | Vector256 | Half       | 100   | 764.713 ns | 4.1691 ns | 1.16x faster |
| NetFabric_Half   | Vector256 | Half       | 100   | 759.633 ns | 2.6422 ns | 1.16x faster |
| Baseline_Half    | Vector512 | Half       | 100   | 865.753 ns | 2.2094 ns | 1.02x faster |
| System_Half      | Vector512 | Half       | 100   | 863.612 ns | 2.5026 ns | 1.02x faster |
| NetFabric_Half   | Vector512 | Half       | 100   | 867.104 ns | 1.8447 ns | 1.02x faster |
|                  |           |            |       |            |           |              |
| Baseline_Int     | Scalar    | Int        | 100   |  51.976 ns | 0.3824 ns |     baseline |
| System_Int       | Scalar    | Int        | 100   |  35.131 ns | 0.2025 ns | 1.48x faster |
| NetFabric_Int    | Scalar    | Int        | 100   |  33.484 ns | 0.1669 ns | 1.55x faster |
| Baseline_Int     | Vector128 | Int        | 100   |  51.267 ns | 0.2203 ns | 1.01x faster |
| System_Int       | Vector128 | Int        | 100   |  14.950 ns | 0.1110 ns | 3.48x faster |
| NetFabric_Int    | Vector128 | Int        | 100   |  12.695 ns | 0.0619 ns | 4.09x faster |
| Baseline_Int     | Vector256 | Int        | 100   |  52.505 ns | 0.4324 ns | 1.01x slower |
| System_Int       | Vector256 | Int        | 100   |  12.779 ns | 0.0542 ns | 4.07x faster |
| NetFabric_Int    | Vector256 | Int        | 100   |  10.157 ns | 0.0634 ns | 5.12x faster |
| Baseline_Int     | Vector512 | Int        | 100   |  52.323 ns | 0.1651 ns | 1.01x slower |
| System_Int       | Vector512 | Int        | 100   |  13.103 ns | 0.0907 ns | 3.97x faster |
| NetFabric_Int    | Vector512 | Int        | 100   |  10.275 ns | 0.0453 ns | 5.06x faster |
|                  |           |            |       |            |           |              |
| Baseline_Long    | Scalar    | Long       | 100   |  52.713 ns | 0.4538 ns |     baseline |
| System_Long      | Scalar    | Long       | 100   |  34.924 ns | 0.3090 ns | 1.51x faster |
| NetFabric_Long   | Scalar    | Long       | 100   |  33.423 ns | 0.0927 ns | 1.58x faster |
| Baseline_Long    | Vector128 | Long       | 100   |  75.910 ns | 2.1183 ns | 1.45x slower |
| System_Long      | Vector128 | Long       | 100   |  21.505 ns | 0.0783 ns | 2.45x faster |
| NetFabric_Long   | Vector128 | Long       | 100   |  25.719 ns | 0.2887 ns | 2.05x faster |
| Baseline_Long    | Vector256 | Long       | 100   |  75.608 ns | 1.5240 ns | 1.43x slower |
| System_Long      | Vector256 | Long       | 100   |  17.885 ns | 0.0486 ns | 2.95x faster |
| NetFabric_Long   | Vector256 | Long       | 100   |  15.290 ns | 0.0587 ns | 3.45x faster |
| Baseline_Long    | Vector512 | Long       | 100   |  74.174 ns | 0.3423 ns | 1.41x slower |
| System_Long      | Vector512 | Long       | 100   |  18.898 ns | 0.1029 ns | 2.79x faster |
| NetFabric_Long   | Vector512 | Long       | 100   |  14.279 ns | 0.1114 ns | 3.69x faster |
|                  |           |            |       |            |           |              |
| Baseline_Short   | Scalar    | Short      | 100   |  53.586 ns | 0.3659 ns |     baseline |
| System_Short     | Scalar    | Short      | 100   |  46.125 ns | 0.3830 ns | 1.16x faster |
| NetFabric_Short  | Scalar    | Short      | 100   |  41.945 ns | 0.5680 ns | 1.28x faster |
| Baseline_Short   | Vector128 | Short      | 100   |  59.540 ns | 0.5334 ns | 1.11x slower |
| System_Short     | Vector128 | Short      | 100   |  10.624 ns | 0.0495 ns | 5.04x faster |
| NetFabric_Short  | Vector128 | Short      | 100   |  12.153 ns | 0.0755 ns | 4.41x faster |
| Baseline_Short   | Vector256 | Short      | 100   |  59.430 ns | 0.4117 ns | 1.11x slower |
| System_Short     | Vector256 | Short      | 100   |   6.224 ns | 0.0330 ns | 8.62x faster |
| NetFabric_Short  | Vector256 | Short      | 100   |   9.478 ns | 0.0505 ns | 5.65x faster |
| Baseline_Short   | Vector512 | Short      | 100   |  59.545 ns | 0.4133 ns | 1.11x slower |
| System_Short     | Vector512 | Short      | 100   |   7.552 ns | 0.0554 ns | 7.10x faster |
| NetFabric_Short  | Vector512 | Short      | 100   |   8.903 ns | 0.0664 ns | 6.02x faster |

### Add Value

Applying a vectorizable binary operator on a span and a fixed scalar value.

| Method           | Job       | Categories | Count |         Mean |    StdDev |        Ratio |
| ---------------- | --------- | ---------- | ----- | -----------: | --------: | -----------: |
| Baseline_Double  | Scalar    | Double     | 100   |    30.491 ns | 0.2496 ns |     baseline |
| System_Double    | Scalar    | Double     | 100   |    30.656 ns | 0.2597 ns | 1.01x slower |
| NetFabric_Double | Scalar    | Double     | 100   |    23.163 ns | 0.2204 ns | 1.32x faster |
| Baseline_Double  | Vector128 | Double     | 100   |    28.773 ns | 0.2517 ns | 1.06x faster |
| System_Double    | Vector128 | Double     | 100   |    14.443 ns | 0.0950 ns | 2.11x faster |
| NetFabric_Double | Vector128 | Double     | 100   |    15.381 ns | 0.1444 ns | 1.99x faster |
| Baseline_Double  | Vector256 | Double     | 100   |    28.832 ns | 0.2141 ns | 1.06x faster |
| System_Double    | Vector256 | Double     | 100   |    10.916 ns | 0.0433 ns | 2.79x faster |
| NetFabric_Double | Vector256 | Double     | 100   |     9.413 ns | 0.1037 ns | 3.24x faster |
| Baseline_Double  | Vector512 | Double     | 100   |    28.806 ns | 0.3041 ns | 1.06x faster |
| System_Double    | Vector512 | Double     | 100   |    11.155 ns | 0.0419 ns | 2.73x faster |
| NetFabric_Double | Vector512 | Double     | 100   |    10.368 ns | 0.0690 ns | 2.94x faster |
|                  |           |            |       |              |           |              |
| Baseline_Float   | Scalar    | Float      | 100   |    31.094 ns | 0.3901 ns |     baseline |
| System_Float     | Scalar    | Float      | 100   |    29.603 ns | 0.3048 ns | 1.05x faster |
| NetFabric_Float  | Scalar    | Float      | 100   |    23.029 ns | 0.1304 ns | 1.35x faster |
| Baseline_Float   | Vector128 | Float      | 100   |    48.725 ns | 1.8406 ns | 1.56x slower |
| System_Float     | Vector128 | Float      | 100   |     9.178 ns | 0.0628 ns | 3.39x faster |
| NetFabric_Float  | Vector128 | Float      | 100   |     9.732 ns | 0.1437 ns | 3.20x faster |
| Baseline_Float   | Vector256 | Float      | 100   |    28.264 ns | 0.2363 ns | 1.10x faster |
| System_Float     | Vector256 | Float      | 100   |     8.336 ns | 0.0831 ns | 3.73x faster |
| NetFabric_Float  | Vector256 | Float      | 100   |     6.873 ns | 0.0598 ns | 4.52x faster |
| Baseline_Float   | Vector512 | Float      | 100   |    48.878 ns | 1.4262 ns | 1.57x slower |
| System_Float     | Vector512 | Float      | 100   |     8.364 ns | 0.0216 ns | 3.71x faster |
| NetFabric_Float  | Vector512 | Float      | 100   |     8.289 ns | 0.0368 ns | 3.75x faster |
|                  |           |            |       |              |           |              |
| Baseline_Half    | Scalar    | Half       | 100   |   992.662 ns | 4.3637 ns |     baseline |
| System_Half      | Scalar    | Half       | 100   |   983.645 ns | 3.8075 ns | 1.01x faster |
| NetFabric_Half   | Scalar    | Half       | 100   | 1,000.728 ns | 3.2709 ns | 1.01x slower |
| Baseline_Half    | Vector128 | Half       | 100   |   894.462 ns | 2.2043 ns | 1.11x faster |
| System_Half      | Vector128 | Half       | 100   |   875.850 ns | 2.5469 ns | 1.13x faster |
| NetFabric_Half   | Vector128 | Half       | 100   |   877.771 ns | 2.2985 ns | 1.13x faster |
| Baseline_Half    | Vector256 | Half       | 100   |   895.050 ns | 1.3509 ns | 1.11x faster |
| System_Half      | Vector256 | Half       | 100   |   875.080 ns | 2.0255 ns | 1.13x faster |
| NetFabric_Half   | Vector256 | Half       | 100   |   879.065 ns | 1.8564 ns | 1.13x faster |
| Baseline_Half    | Vector512 | Half       | 100   |   895.426 ns | 1.7073 ns | 1.11x faster |
| System_Half      | Vector512 | Half       | 100   |   877.551 ns | 3.3208 ns | 1.13x faster |
| NetFabric_Half   | Vector512 | Half       | 100   |   876.934 ns | 2.6832 ns | 1.13x faster |
|                  |           |            |       |              |           |              |
| Baseline_Int     | Scalar    | Int        | 100   |    44.760 ns | 0.6570 ns |     baseline |
| System_Int       | Scalar    | Int        | 100   |    36.937 ns | 0.1208 ns | 1.21x faster |
| NetFabric_Int    | Scalar    | Int        | 100   |    33.508 ns | 0.1485 ns | 1.34x faster |
| Baseline_Int     | Vector128 | Int        | 100   |    35.064 ns | 0.3169 ns | 1.28x faster |
| System_Int       | Vector128 | Int        | 100   |    11.550 ns | 0.0417 ns | 3.88x faster |
| NetFabric_Int    | Vector128 | Int        | 100   |    10.248 ns | 0.0604 ns | 4.37x faster |
| Baseline_Int     | Vector256 | Int        | 100   |    34.246 ns | 0.1734 ns | 1.31x faster |
| System_Int       | Vector256 | Int        | 100   |     8.310 ns | 0.0370 ns | 5.39x faster |
| NetFabric_Int    | Vector256 | Int        | 100   |     7.636 ns | 0.0398 ns | 5.86x faster |
| Baseline_Int     | Vector512 | Int        | 100   |    34.726 ns | 0.1851 ns | 1.29x faster |
| System_Int       | Vector512 | Int        | 100   |    12.269 ns | 0.0535 ns | 3.65x faster |
| NetFabric_Int    | Vector512 | Int        | 100   |    10.999 ns | 0.0325 ns | 4.07x faster |
|                  |           |            |       |              |           |              |
| Baseline_Long    | Scalar    | Long       | 100   |    45.567 ns | 1.5187 ns |     baseline |
| System_Long      | Scalar    | Long       | 100   |    36.564 ns | 0.2023 ns | 1.24x faster |
| NetFabric_Long   | Scalar    | Long       | 100   |    33.437 ns | 0.1774 ns | 1.36x faster |
| Baseline_Long    | Vector128 | Long       | 100   |    34.554 ns | 0.3545 ns | 1.31x faster |
| System_Long      | Vector128 | Long       | 100   |    13.107 ns | 0.0771 ns | 3.46x faster |
| NetFabric_Long   | Vector128 | Long       | 100   |    24.331 ns | 0.2522 ns | 1.86x faster |
| Baseline_Long    | Vector256 | Long       | 100   |    34.498 ns | 0.4009 ns | 1.32x faster |
| System_Long      | Vector256 | Long       | 100   |    11.127 ns | 0.0865 ns | 4.07x faster |
| NetFabric_Long   | Vector256 | Long       | 100   |     9.689 ns | 0.0644 ns | 4.67x faster |
| Baseline_Long    | Vector512 | Long       | 100   |    34.803 ns | 0.1882 ns | 1.30x faster |
| System_Long      | Vector512 | Long       | 100   |    10.955 ns | 0.0720 ns | 4.14x faster |
| NetFabric_Long   | Vector512 | Long       | 100   |    10.202 ns | 0.0799 ns | 4.44x faster |
|                  |           |            |       |              |           |              |
| Baseline_Short   | Scalar    | Short      | 100   |    39.386 ns | 0.4163 ns |     baseline |
| System_Short     | Scalar    | Short      | 100   |    40.853 ns | 0.1484 ns | 1.04x slower |
| NetFabric_Short  | Scalar    | Short      | 100   |    36.261 ns | 0.1594 ns | 1.09x faster |
| Baseline_Short   | Vector128 | Short      | 100   |    39.172 ns | 0.2904 ns | 1.01x faster |
| System_Short     | Vector128 | Short      | 100   |     6.859 ns | 0.0512 ns | 5.74x faster |
| NetFabric_Short  | Vector128 | Short      | 100   |     7.834 ns | 0.1299 ns | 5.03x faster |
| Baseline_Short   | Vector256 | Short      | 100   |    39.163 ns | 0.5241 ns | 1.01x faster |
| System_Short     | Vector256 | Short      | 100   |     5.024 ns | 0.0321 ns | 7.84x faster |
| NetFabric_Short  | Vector256 | Short      | 100   |     6.008 ns | 0.0401 ns | 6.55x faster |
| Baseline_Short   | Vector512 | Short      | 100   |    38.894 ns | 0.5157 ns | 1.01x faster |
| System_Short     | Vector512 | Short      | 100   |     5.725 ns | 0.0396 ns | 6.88x faster |
| NetFabric_Short  | Vector512 | Short      | 100   |     5.886 ns | 0.0306 ns | 6.69x faster |

### AddMultiply

Applying a vectorizable ternary operator on three spans.

| Method           | Job       | Categories | Count |         Mean |     StdDev |        Ratio |
| ---------------- | --------- | ---------- | ----- | -----------: | ---------: | -----------: |
| Baseline_Double  | Scalar    | Double     | 100   |    53.194 ns |  0.5459 ns |     baseline |
| System_Double    | Scalar    | Double     | 100   |    40.778 ns |  0.1742 ns | 1.30x faster |
| NetFabric_Double | Scalar    | Double     | 100   |    38.343 ns |  0.1443 ns | 1.39x faster |
| Baseline_Double  | Vector128 | Double     | 100   |    53.476 ns |  0.4960 ns | 1.01x slower |
| System_Double    | Vector128 | Double     | 100   |    28.606 ns |  0.1557 ns | 1.86x faster |
| NetFabric_Double | Vector128 | Double     | 100   |    28.967 ns |  0.1387 ns | 1.84x faster |
| Baseline_Double  | Vector256 | Double     | 100   |    53.079 ns |  0.2441 ns | 1.00x faster |
| System_Double    | Vector256 | Double     | 100   |    19.916 ns |  0.0737 ns | 2.67x faster |
| NetFabric_Double | Vector256 | Double     | 100   |    18.981 ns |  0.0716 ns | 2.80x faster |
| Baseline_Double  | Vector512 | Double     | 100   |    52.691 ns |  0.4285 ns | 1.01x faster |
| System_Double    | Vector512 | Double     | 100   |    22.735 ns |  0.1027 ns | 2.34x faster |
| NetFabric_Double | Vector512 | Double     | 100   |    18.626 ns |  0.0902 ns | 2.86x faster |
|                  |           |            |       |              |            |              |
| Baseline_Float   | Scalar    | Float      | 100   |    51.615 ns |  0.2995 ns |     baseline |
| System_Float     | Scalar    | Float      | 100   |    40.463 ns |  0.1519 ns | 1.28x faster |
| NetFabric_Float  | Scalar    | Float      | 100   |    38.171 ns |  0.0796 ns | 1.35x faster |
| Baseline_Float   | Vector128 | Float      | 100   |    52.654 ns |  0.1693 ns | 1.02x slower |
| System_Float     | Vector128 | Float      | 100   |    19.467 ns |  0.0949 ns | 2.65x faster |
| NetFabric_Float  | Vector128 | Float      | 100   |    18.488 ns |  0.0830 ns | 2.79x faster |
| Baseline_Float   | Vector256 | Float      | 100   |    52.849 ns |  0.3430 ns | 1.02x slower |
| System_Float     | Vector256 | Float      | 100   |    16.036 ns |  0.0519 ns | 3.22x faster |
| NetFabric_Float  | Vector256 | Float      | 100   |    14.800 ns |  0.0487 ns | 3.49x faster |
| Baseline_Float   | Vector512 | Float      | 100   |    53.026 ns |  0.2116 ns | 1.03x slower |
| System_Float     | Vector512 | Float      | 100   |    16.049 ns |  0.1000 ns | 3.22x faster |
| NetFabric_Float  | Vector512 | Float      | 100   |    15.217 ns |  0.0473 ns | 3.39x faster |
|                  |           |            |       |              |            |              |
| Baseline_Half    | Scalar    | Half       | 100   | 1,618.204 ns |  3.5398 ns |     baseline |
| System_Half      | Scalar    | Half       | 100   | 2,381.759 ns |  2.3965 ns | 1.47x slower |
| NetFabric_Half   | Scalar    | Half       | 100   | 2,375.177 ns |  2.8007 ns | 1.47x slower |
| Baseline_Half    | Vector128 | Half       | 100   | 1,455.613 ns |  6.4194 ns | 1.11x faster |
| System_Half      | Vector128 | Half       | 100   | 2,278.200 ns | 14.9670 ns | 1.41x slower |
| NetFabric_Half   | Vector128 | Half       | 100   | 2,272.810 ns |  9.7295 ns | 1.40x slower |
| Baseline_Half    | Vector256 | Half       | 100   | 1,459.872 ns |  9.3435 ns | 1.11x faster |
| System_Half      | Vector256 | Half       | 100   | 2,272.492 ns | 14.8960 ns | 1.40x slower |
| NetFabric_Half   | Vector256 | Half       | 100   | 2,270.518 ns |  5.6785 ns | 1.40x slower |
| Baseline_Half    | Vector512 | Half       | 100   | 1,454.237 ns |  6.5861 ns | 1.11x faster |
| System_Half      | Vector512 | Half       | 100   | 2,275.960 ns | 12.6515 ns | 1.41x slower |
| NetFabric_Half   | Vector512 | Half       | 100   | 2,277.877 ns | 13.1558 ns | 1.41x slower |
|                  |           |            |       |              |            |              |
| Baseline_Int     | Scalar    | Int        | 100   |    59.095 ns |  0.4008 ns |     baseline |
| System_Int       | Scalar    | Int        | 100   |    52.993 ns |  0.2280 ns | 1.12x faster |
| NetFabric_Int    | Scalar    | Int        | 100   |    45.341 ns |  0.1997 ns | 1.30x faster |
| Baseline_Int     | Vector128 | Int        | 100   |    59.250 ns |  0.5185 ns | 1.00x slower |
| System_Int       | Vector128 | Int        | 100   |    19.522 ns |  0.2302 ns | 3.03x faster |
| NetFabric_Int    | Vector128 | Int        | 100   |    18.133 ns |  0.0982 ns | 3.26x faster |
| Baseline_Int     | Vector256 | Int        | 100   |    59.131 ns |  0.4722 ns | 1.00x slower |
| System_Int       | Vector256 | Int        | 100   |    15.924 ns |  0.0793 ns | 3.71x faster |
| NetFabric_Int    | Vector256 | Int        | 100   |    13.997 ns |  0.0616 ns | 4.22x faster |
| Baseline_Int     | Vector512 | Int        | 100   |    58.863 ns |  0.3824 ns | 1.00x faster |
| System_Int       | Vector512 | Int        | 100   |    14.588 ns |  0.1058 ns | 4.05x faster |
| NetFabric_Int    | Vector512 | Int        | 100   |    14.360 ns |  0.1070 ns | 4.11x faster |
|                  |           |            |       |              |            |              |
| Baseline_Long    | Scalar    | Long       | 100   |    59.422 ns |  0.7016 ns |     baseline |
| System_Long      | Scalar    | Long       | 100   |    52.869 ns |  0.4472 ns | 1.12x faster |
| NetFabric_Long   | Scalar    | Long       | 100   |    45.050 ns |  0.1657 ns | 1.32x faster |
| Baseline_Long    | Vector128 | Long       | 100   |    59.398 ns |  0.5420 ns | 1.00x faster |
| System_Long      | Vector128 | Long       | 100   |   353.783 ns |  1.3794 ns | 5.95x slower |
| NetFabric_Long   | Vector128 | Long       | 100   |   258.917 ns |  1.1508 ns | 4.36x slower |
| Baseline_Long    | Vector256 | Long       | 100   |    59.045 ns |  0.2597 ns | 1.01x faster |
| System_Long      | Vector256 | Long       | 100   |   363.682 ns |  1.2358 ns | 6.12x slower |
| NetFabric_Long   | Vector256 | Long       | 100   |   147.048 ns |  0.6580 ns | 2.48x slower |
| Baseline_Long    | Vector512 | Long       | 100   |    59.265 ns |  0.5245 ns | 1.00x faster |
| System_Long      | Vector512 | Long       | 100   |    22.449 ns |  0.1203 ns | 2.65x faster |
| NetFabric_Long   | Vector512 | Long       | 100   |   144.334 ns |  1.3654 ns | 2.43x slower |
|                  |           |            |       |              |            |              |
| Baseline_Short   | Scalar    | Short      | 100   |    85.215 ns |  0.5693 ns |     baseline |
| System_Short     | Scalar    | Short      | 100   |    79.092 ns |  0.4696 ns | 1.08x faster |
| NetFabric_Short  | Scalar    | Short      | 100   |    80.144 ns |  0.5933 ns | 1.06x faster |
| Baseline_Short   | Vector128 | Short      | 100   |    83.962 ns |  0.3168 ns | 1.02x faster |
| System_Short     | Vector128 | Short      | 100   |    12.624 ns |  0.0586 ns | 6.75x faster |
| NetFabric_Short  | Vector128 | Short      | 100   |    13.848 ns |  0.1233 ns | 6.15x faster |
| Baseline_Short   | Vector256 | Short      | 100   |    84.149 ns |  0.3873 ns | 1.01x faster |
| System_Short     | Vector256 | Short      | 100   |     9.702 ns |  0.0640 ns | 8.78x faster |
| NetFabric_Short  | Vector256 | Short      | 100   |    11.736 ns |  0.0789 ns | 7.26x faster |
| Baseline_Short   | Vector512 | Short      | 100   |    84.477 ns |  0.7474 ns | 1.01x faster |
| System_Short     | Vector512 | Short      | 100   |    19.894 ns |  0.1089 ns | 4.28x faster |
| NetFabric_Short  | Vector512 | Short      | 100   |    12.510 ns |  0.0460 ns | 6.81x faster |

### DegreesToRadians

Applying a vectorizable ternary operator on a span and two fixed scalar values.

| Method           | Job       | Categories | Count |         Mean |     StdDev |        Ratio |
| ---------------- | --------- | ---------- | ----- | -----------: | ---------: | -----------: |
| Baseline_Double  | Scalar    | Double     | 100   |   105.363 ns |  0.6721 ns |     baseline |
| System_Double    | Scalar    | Double     | 100   |   104.955 ns |  0.9296 ns | 1.00x faster |
| NetFabric_Double | Scalar    | Double     | 100   |   108.631 ns |  0.4020 ns | 1.03x slower |
| Baseline_Double  | Vector128 | Double     | 100   |   104.213 ns |  0.4783 ns | 1.01x faster |
| System_Double    | Vector128 | Double     | 100   |    70.537 ns |  0.5717 ns | 1.49x faster |
| NetFabric_Double | Vector128 | Double     | 100   |    50.372 ns |  0.3891 ns | 2.09x faster |
| Baseline_Double  | Vector256 | Double     | 100   |   104.494 ns |  0.6641 ns | 1.01x faster |
| System_Double    | Vector256 | Double     | 100   |    50.886 ns |  0.3590 ns | 2.07x faster |
| NetFabric_Double | Vector256 | Double     | 100   |    23.781 ns |  0.1212 ns | 4.43x faster |
| Baseline_Double  | Vector512 | Double     | 100   |   105.602 ns |  0.7539 ns | 1.00x slower |
| System_Double    | Vector512 | Double     | 100   |   183.516 ns |  0.8624 ns | 1.74x slower |
| NetFabric_Double | Vector512 | Double     | 100   |    23.738 ns |  0.0542 ns | 4.44x faster |
|                  |           |            |       |              |            |              |
| Baseline_Float   | Scalar    | Float      | 100   |    64.749 ns |  0.2191 ns |     baseline |
| System_Float     | Scalar    | Float      | 100   |    65.010 ns |  0.2194 ns | 1.00x slower |
| NetFabric_Float  | Scalar    | Float      | 100   |    65.193 ns |  0.3212 ns | 1.01x slower |
| Baseline_Float   | Vector128 | Float      | 100   |    64.669 ns |  0.2393 ns | 1.00x faster |
| System_Float     | Vector128 | Float      | 100   |    39.316 ns |  0.1564 ns | 1.65x faster |
| NetFabric_Float  | Vector128 | Float      | 100   |    15.905 ns |  0.0988 ns | 4.07x faster |
| Baseline_Float   | Vector256 | Float      | 100   |    64.664 ns |  0.3021 ns | 1.00x faster |
| System_Float     | Vector256 | Float      | 100   |    29.404 ns |  0.3665 ns | 2.20x faster |
| NetFabric_Float  | Vector256 | Float      | 100   |     9.523 ns |  0.0493 ns | 6.80x faster |
| Baseline_Float   | Vector512 | Float      | 100   |    64.599 ns |  0.2632 ns | 1.00x faster |
| System_Float     | Vector512 | Float      | 100   |    77.022 ns |  0.7295 ns | 1.19x slower |
| NetFabric_Float  | Vector512 | Float      | 100   |    12.352 ns |  0.0653 ns | 5.24x faster |
|                  |           |            |       |              |            |              |
| Baseline_Half    | Scalar    | Half       | 100   |   999.253 ns |  6.6407 ns |     baseline |
| System_Half      | Scalar    | Half       | 100   | 1,009.173 ns |  4.2703 ns | 1.01x slower |
| NetFabric_Half   | Scalar    | Half       | 100   | 2,561.898 ns |  7.8650 ns | 2.56x slower |
| Baseline_Half    | Vector128 | Half       | 100   |   905.334 ns |  3.8491 ns | 1.10x faster |
| System_Half      | Vector128 | Half       | 100   |   892.921 ns |  1.9791 ns | 1.12x faster |
| NetFabric_Half   | Vector128 | Half       | 100   | 2,420.872 ns |  9.8031 ns | 2.42x slower |
| Baseline_Half    | Vector256 | Half       | 100   |   903.875 ns |  2.1924 ns | 1.11x faster |
| System_Half      | Vector256 | Half       | 100   |   892.802 ns |  1.8886 ns | 1.12x faster |
| NetFabric_Half   | Vector256 | Half       | 100   | 2,418.932 ns |  8.0544 ns | 2.42x slower |
| Baseline_Half    | Vector512 | Half       | 100   |   906.720 ns |  4.1213 ns | 1.10x faster |
| System_Half      | Vector512 | Half       | 100   |   894.048 ns |  3.8254 ns | 1.12x faster |
| NetFabric_Half   | Vector512 | Half       | 100   | 2,424.812 ns | 13.1376 ns | 2.43x slower |

### Sum

Applying a vectorizable aggregation operator on a span.

It additionally compares with the performance of LINQ's `Sum()`. However, it's worth noting that this method lacks support for the types `short` and `Half`. In such instances, LINQ's `Aggregate()` is employed instead.

| Method           | Job       | Categories | Count |         Mean |     StdDev |         Ratio |
| ---------------- | --------- | ---------- | ----- | -----------: | ---------: | ------------: |
| Baseline_Double  | Scalar    | Double     | 100   |    47.326 ns |  0.5851 ns |      baseline |
| System_Double    | Scalar    | Double     | 100   |    39.147 ns |  0.7875 ns |  1.21x faster |
| NetFabric_Double | Scalar    | Double     | 100   |    61.619 ns |  0.2195 ns |  1.30x slower |
| Baseline_Double  | Vector128 | Double     | 100   |    42.618 ns |  0.3416 ns |  1.11x faster |
| System_Double    | Vector128 | Double     | 100   |    11.973 ns |  0.1163 ns |  3.95x faster |
| NetFabric_Double | Vector128 | Double     | 100   |    29.314 ns |  0.5716 ns |  1.62x faster |
| Baseline_Double  | Vector256 | Double     | 100   |    42.449 ns |  0.2493 ns |  1.11x faster |
| System_Double    | Vector256 | Double     | 100   |     6.136 ns |  0.0649 ns |  7.71x faster |
| NetFabric_Double | Vector256 | Double     | 100   |    11.261 ns |  0.1015 ns |  4.20x faster |
| Baseline_Double  | Vector512 | Double     | 100   |    42.334 ns |  0.3927 ns |  1.12x faster |
| System_Double    | Vector512 | Double     | 100   |     5.625 ns |  0.0293 ns |  8.42x faster |
| NetFabric_Double | Vector512 | Double     | 100   |    13.771 ns |  0.1643 ns |  3.44x faster |
|                  |           |            |       |              |            |               |
| Baseline_Float   | Scalar    | Float      | 100   |    42.699 ns |  0.2814 ns |      baseline |
| System_Float     | Scalar    | Float      | 100   |    38.877 ns |  0.1420 ns |  1.10x faster |
| NetFabric_Float  | Scalar    | Float      | 100   |    61.195 ns |  1.1528 ns |  1.43x slower |
| Baseline_Float   | Vector128 | Float      | 100   |    42.135 ns |  0.1684 ns |  1.01x faster |
| System_Float     | Vector128 | Float      | 100   |     6.284 ns |  0.0545 ns |  6.79x faster |
| NetFabric_Float  | Vector128 | Float      | 100   |    10.830 ns |  0.0863 ns |  3.94x faster |
| Baseline_Float   | Vector256 | Float      | 100   |    42.783 ns |  0.2612 ns |  1.00x slower |
| System_Float     | Vector256 | Float      | 100   |     4.567 ns |  0.0313 ns |  9.35x faster |
| NetFabric_Float  | Vector256 | Float      | 100   |     7.253 ns |  0.0512 ns |  5.88x faster |
| Baseline_Float   | Vector512 | Float      | 100   |    42.215 ns |  0.2125 ns |  1.01x faster |
| System_Float     | Vector512 | Float      | 100   |     4.829 ns |  0.0459 ns |  8.84x faster |
| NetFabric_Float  | Vector512 | Float      | 100   |     8.068 ns |  0.0579 ns |  5.29x faster |
|                  |           |            |       |              |            |               |
| Baseline_Half    | Scalar    | Half       | 100   | 1,248.404 ns |  3.8695 ns |      baseline |
| System_Half      | Scalar    | Half       | 100   | 1,261.046 ns |  3.3137 ns |  1.01x slower |
| NetFabric_Half   | Scalar    | Half       | 100   | 1,246.705 ns |  5.5448 ns |  1.00x faster |
| Baseline_Half    | Vector128 | Half       | 100   | 1,209.489 ns |  3.9070 ns |  1.03x faster |
| System_Half      | Vector128 | Half       | 100   | 1,226.410 ns |  1.3758 ns |  1.02x faster |
| NetFabric_Half   | Vector128 | Half       | 100   | 1,213.822 ns |  8.9948 ns |  1.03x faster |
| Baseline_Half    | Vector256 | Half       | 100   | 1,208.729 ns |  2.2773 ns |  1.03x faster |
| System_Half      | Vector256 | Half       | 100   | 1,228.855 ns |  3.7491 ns |  1.02x faster |
| NetFabric_Half   | Vector256 | Half       | 100   | 1,208.195 ns |  1.4436 ns |  1.03x faster |
| Baseline_Half    | Vector512 | Half       | 100   | 1,236.813 ns | 12.7384 ns |  1.01x faster |
| System_Half      | Vector512 | Half       | 100   | 1,249.052 ns | 11.0453 ns |  1.00x slower |
| NetFabric_Half   | Vector512 | Half       | 100   | 1,251.404 ns | 26.3882 ns |  1.01x slower |
|                  |           |            |       |              |            |               |
| Baseline_Int     | Scalar    | Int        | 100   |    27.755 ns |  0.1998 ns |      baseline |
| LINQ_Int         | Scalar    | Int        | 100   |    26.892 ns |  0.1653 ns |  1.03x faster |
| System_Int       | Scalar    | Int        | 100   |    29.587 ns |  0.1430 ns |  1.07x slower |
| NetFabric_Int    | Scalar    | Int        | 100   |    26.959 ns |  0.1393 ns |  1.03x faster |
| Baseline_Int     | Vector128 | Int        | 100   |    27.303 ns |  0.2011 ns |  1.02x faster |
| LINQ_Int         | Vector128 | Int        | 100   |     8.549 ns |  0.0409 ns |  3.25x faster |
| System_Int       | Vector128 | Int        | 100   |     5.547 ns |  0.0283 ns |  5.00x faster |
| NetFabric_Int    | Vector128 | Int        | 100   |    11.656 ns |  0.0453 ns |  2.38x faster |
| Baseline_Int     | Vector256 | Int        | 100   |    27.162 ns |  0.1590 ns |  1.02x faster |
| LINQ_Int         | Vector256 | Int        | 100   |     6.058 ns |  0.0549 ns |  4.58x faster |
| System_Int       | Vector256 | Int        | 100   |     4.551 ns |  0.0410 ns |  6.10x faster |
| NetFabric_Int    | Vector256 | Int        | 100   |     6.314 ns |  0.0908 ns |  4.40x faster |
| Baseline_Int     | Vector512 | Int        | 100   |    45.141 ns |  0.3433 ns |  1.63x slower |
| LINQ_Int         | Vector512 | Int        | 100   |     5.990 ns |  0.0493 ns |  4.64x faster |
| System_Int       | Vector512 | Int        | 100   |     4.382 ns |  0.0712 ns |  6.34x faster |
| NetFabric_Int    | Vector512 | Int        | 100   |     6.208 ns |  0.0640 ns |  4.47x faster |
|                  |           |            |       |              |            |               |
| Baseline_Long    | Scalar    | Long       | 100   |    45.088 ns |  0.3162 ns |      baseline |
| LINQ_Long        | Scalar    | Long       | 100   |    26.957 ns |  0.1937 ns |  1.67x faster |
| System_Long      | Scalar    | Long       | 100   |    30.365 ns |  0.2084 ns |  1.48x faster |
| NetFabric_Long   | Scalar    | Long       | 100   |    27.270 ns |  0.2998 ns |  1.65x faster |
| Baseline_Long    | Vector128 | Long       | 100   |    45.053 ns |  0.1710 ns |  1.00x faster |
| LINQ_Long        | Vector128 | Long       | 100   |    28.189 ns |  0.6928 ns |  1.62x faster |
| System_Long      | Vector128 | Long       | 100   |     8.667 ns |  0.0958 ns |  5.20x faster |
| NetFabric_Long   | Vector128 | Long       | 100   |    23.970 ns |  0.2298 ns |  1.88x faster |
| Baseline_Long    | Vector256 | Long       | 100   |    44.878 ns |  0.2091 ns |  1.00x faster |
| LINQ_Long        | Vector256 | Long       | 100   |     8.948 ns |  0.0359 ns |  5.04x faster |
| System_Long      | Vector256 | Long       | 100   |     6.441 ns |  0.0544 ns |  7.00x faster |
| NetFabric_Long   | Vector256 | Long       | 100   |    11.967 ns |  0.0714 ns |  3.77x faster |
| Baseline_Long    | Vector512 | Long       | 100   |    27.126 ns |  0.1424 ns |  1.66x faster |
| LINQ_Long        | Vector512 | Long       | 100   |     9.445 ns |  0.0874 ns |  4.77x faster |
| System_Long      | Vector512 | Long       | 100   |     9.783 ns |  0.2136 ns |  4.62x faster |
| NetFabric_Long   | Vector512 | Long       | 100   |    11.440 ns |  0.1473 ns |  3.94x faster |
|                  |           |            |       |              |            |               |
| Baseline_Short   | Scalar    | Short      | 100   |    39.626 ns |  0.1431 ns |      baseline |
| System_Short     | Scalar    | Short      | 100   |    41.646 ns |  0.1554 ns |  1.05x slower |
| NetFabric_Short  | Scalar    | Short      | 100   |    45.522 ns |  0.4657 ns |  1.15x slower |
| Baseline_Short   | Vector128 | Short      | 100   |    45.966 ns |  0.2654 ns |  1.16x slower |
| System_Short     | Vector128 | Short      | 100   |     4.729 ns |  0.0337 ns |  8.38x faster |
| NetFabric_Short  | Vector128 | Short      | 100   |     7.206 ns |  0.0413 ns |  5.50x faster |
| Baseline_Short   | Vector256 | Short      | 100   |    45.998 ns |  0.2975 ns |  1.16x slower |
| System_Short     | Vector256 | Short      | 100   |     3.947 ns |  0.0294 ns | 10.04x faster |
| NetFabric_Short  | Vector256 | Short      | 100   |     4.827 ns |  0.0566 ns |  8.21x faster |
| Baseline_Short   | Vector512 | Short      | 100   |    38.637 ns |  0.2469 ns |  1.03x faster |
| System_Short     | Vector512 | Short      | 100   |     4.740 ns |  0.0400 ns |  8.36x faster |
| NetFabric_Short  | Vector512 | Short      | 100   |     4.960 ns |  0.0394 ns |  7.99x faster |

### Sum2D

Applying a vectorizable aggregation operator on a span with two contiguos values for each element.

It also compares to the performance of LINQ's `Aggregate()`, as LINQ's `Sum()` does not support non-native numeric types.

| Method           | Job       | Categories | Count |        Mean |   StdDev |      Median |         Ratio |
| ---------------- | --------- | ---------- | ----- | ----------: | -------: | ----------: | ------------: |
| Baseline_Double  | Scalar    | Double     | 100   |    52.96 ns | 0.183 ns |    52.99 ns |      baseline |
| NetFabric_Double | Scalar    | Double     | 100   |    35.17 ns | 0.153 ns |    35.18 ns |  1.51x faster |
| Baseline_Double  | Vector128 | Double     | 100   |    53.88 ns | 0.522 ns |    53.94 ns |  1.02x slower |
| NetFabric_Double | Vector128 | Double     | 100   |    48.78 ns | 0.257 ns |    48.70 ns |  1.09x faster |
| Baseline_Double  | Vector256 | Double     | 100   |    54.02 ns | 0.499 ns |    53.96 ns |  1.02x slower |
| NetFabric_Double | Vector256 | Double     | 100   |    24.94 ns | 0.093 ns |    24.97 ns |  2.12x faster |
| Baseline_Double  | Vector512 | Double     | 100   |    53.78 ns | 0.319 ns |    53.67 ns |  1.01x slower |
| NetFabric_Double | Vector512 | Double     | 100   |    25.16 ns | 0.064 ns |    25.19 ns |  2.10x faster |
|                  |           |            |       |             |          |             |               |
| Baseline_Float   | Scalar    | Float      | 100   |   222.75 ns | 0.541 ns |   222.69 ns |      baseline |
| NetFabric_Float  | Scalar    | Float      | 100   |    36.00 ns | 0.063 ns |    36.00 ns |  6.18x faster |
| Baseline_Float   | Vector128 | Float      | 100   |   222.88 ns | 0.807 ns |   222.87 ns |  1.00x slower |
| NetFabric_Float  | Vector128 | Float      | 100   |    28.59 ns | 0.117 ns |    28.60 ns |  7.79x faster |
| Baseline_Float   | Vector256 | Float      | 100   |   222.69 ns | 0.407 ns |   222.74 ns |  1.00x slower |
| NetFabric_Float  | Vector256 | Float      | 100   |    16.06 ns | 0.072 ns |    16.05 ns | 13.87x faster |
| Baseline_Float   | Vector512 | Float      | 100   |   222.47 ns | 0.646 ns |   222.54 ns |  1.00x faster |
| NetFabric_Float  | Vector512 | Float      | 100   |    16.09 ns | 0.047 ns |    16.08 ns | 13.84x faster |
|                  |           |            |       |             |          |             |               |
| Baseline_Half    | Scalar    | Half       | 100   | 2,009.62 ns | 1.739 ns | 2,009.29 ns |      baseline |
| NetFabric_Half   | Scalar    | Half       | 100   | 2,031.01 ns | 2.899 ns | 2,029.84 ns |  1.01x slower |
| Baseline_Half    | Vector128 | Half       | 100   | 1,791.11 ns | 3.574 ns | 1,791.90 ns |  1.12x faster |
| NetFabric_Half   | Vector128 | Half       | 100   | 1,789.25 ns | 6.415 ns | 1,789.96 ns |  1.12x faster |
| Baseline_Half    | Vector256 | Half       | 100   | 1,790.95 ns | 2.931 ns | 1,791.40 ns |  1.12x faster |
| NetFabric_Half   | Vector256 | Half       | 100   | 1,786.29 ns | 8.328 ns | 1,785.79 ns |  1.13x faster |
| Baseline_Half    | Vector512 | Half       | 100   | 1,792.03 ns | 5.916 ns | 1,792.21 ns |  1.12x faster |
| NetFabric_Half   | Vector512 | Half       | 100   | 1,784.97 ns | 2.908 ns | 1,785.13 ns |  1.13x faster |
|                  |           |            |       |             |          |             |               |
| Baseline_Int     | Scalar    | Int        | 100   |   155.52 ns | 1.035 ns |   155.73 ns |      baseline |
| NetFabric_Int    | Scalar    | Int        | 100   |    46.21 ns | 0.312 ns |    46.16 ns |  3.37x faster |
| Baseline_Int     | Vector128 | Int        | 100   |   155.49 ns | 0.525 ns |   155.47 ns |  1.00x faster |
| NetFabric_Int    | Vector128 | Int        | 100   |    18.09 ns | 0.191 ns |    18.02 ns |  8.60x faster |
| Baseline_Int     | Vector256 | Int        | 100   |   155.41 ns | 0.830 ns |   155.68 ns |  1.00x faster |
| NetFabric_Int    | Vector256 | Int        | 100   |    16.31 ns | 0.083 ns |    16.30 ns |  9.54x faster |
| Baseline_Int     | Vector512 | Int        | 100   |   155.47 ns | 0.662 ns |   155.39 ns |  1.00x faster |
| NetFabric_Int    | Vector512 | Int        | 100   |    15.23 ns | 0.163 ns |    15.22 ns | 10.21x faster |
|                  |           |            |       |             |          |             |               |
| Baseline_Long    | Scalar    | Long       | 100   |    46.74 ns | 0.214 ns |    46.76 ns |      baseline |
| NetFabric_Long   | Scalar    | Long       | 100   |    44.23 ns | 0.180 ns |    44.27 ns |  1.06x faster |
| Baseline_Long    | Vector128 | Long       | 100   |    47.42 ns | 0.634 ns |    47.11 ns |  1.01x slower |
| NetFabric_Long   | Vector128 | Long       | 100   |    33.77 ns | 0.260 ns |    33.78 ns |  1.38x faster |
| Baseline_Long    | Vector256 | Long       | 100   |    46.83 ns | 0.193 ns |    46.80 ns |  1.00x slower |
| NetFabric_Long   | Vector256 | Long       | 100   |    24.60 ns | 0.166 ns |    24.55 ns |  1.90x faster |
| Baseline_Long    | Vector512 | Long       | 100   |    46.75 ns | 0.240 ns |    46.73 ns |  1.00x slower |
| NetFabric_Long   | Vector512 | Long       | 100   |    16.93 ns | 0.201 ns |    16.87 ns |  2.76x faster |
|                  |           |            |       |             |          |             |               |
| Baseline_Short   | Scalar    | Short      | 100   |   206.23 ns | 0.565 ns |   206.34 ns |      baseline |
| NetFabric_Short  | Scalar    | Short      | 100   |    70.70 ns | 3.636 ns |    69.14 ns |  2.81x faster |
| Baseline_Short   | Vector128 | Short      | 100   |   206.65 ns | 0.640 ns |   206.62 ns |  1.00x slower |
| NetFabric_Short  | Vector128 | Short      | 100   |    15.54 ns | 0.122 ns |    15.52 ns | 13.27x faster |
| Baseline_Short   | Vector256 | Short      | 100   |   206.70 ns | 0.756 ns |   206.53 ns |  1.00x slower |
| NetFabric_Short  | Vector256 | Short      | 100   |    19.11 ns | 0.077 ns |    19.12 ns | 10.79x faster |
| Baseline_Short   | Vector512 | Short      | 100   |   206.66 ns | 1.016 ns |   206.27 ns |  1.00x slower |
| NetFabric_Short  | Vector512 | Short      | 100   |    17.90 ns | 0.213 ns |    17.78 ns | 11.52x faster |

### Sum3D

Applying a vectorizable aggregation operator on a span with three contiguos values for each element.

It also compares to the performance of LINQ's `Aggregate()`, as LINQ's `Sum()` does not support non-native numeric types.

| Method           | Job       | Categories | Count |        Mean |    StdDev |      Median |        Ratio |
| ---------------- | --------- | ---------- | ----- | ----------: | --------: | ----------: | -----------: |
| Baseline_Double  | Scalar    | Double     | 100   |    63.13 ns |  1.191 ns |    62.88 ns |     baseline |
| NetFabric_Double | Scalar    | Double     | 100   |    64.18 ns |  0.660 ns |    64.02 ns | 1.02x slower |
| Baseline_Double  | Vector128 | Double     | 100   |    64.47 ns |  0.678 ns |    64.20 ns | 1.02x slower |
| NetFabric_Double | Vector128 | Double     | 100   |   136.92 ns |  2.452 ns |   137.40 ns | 2.17x slower |
| Baseline_Double  | Vector256 | Double     | 100   |    65.29 ns |  1.422 ns |    65.45 ns | 1.04x slower |
| NetFabric_Double | Vector256 | Double     | 100   |    77.85 ns |  3.706 ns |    77.32 ns | 1.20x slower |
| Baseline_Double  | Vector512 | Double     | 100   |    65.87 ns |  1.049 ns |    66.15 ns | 1.04x slower |
| NetFabric_Double | Vector512 | Double     | 100   |    78.31 ns |  1.251 ns |    78.17 ns | 1.24x slower |
|                  |           |            |       |             |           |             |              |
| Baseline_Float   | Scalar    | Float      | 100   |    64.87 ns |  1.669 ns |    65.03 ns |     baseline |
| NetFabric_Float  | Scalar    | Float      | 100   |    66.72 ns |  0.964 ns |    67.09 ns | 1.02x slower |
| Baseline_Float   | Vector128 | Float      | 100   |    66.07 ns |  1.175 ns |    66.50 ns | 1.01x slower |
| NetFabric_Float  | Vector128 | Float      | 100   |    70.49 ns |  1.354 ns |    70.22 ns | 1.08x slower |
| Baseline_Float   | Vector256 | Float      | 100   |    64.62 ns |  0.888 ns |    64.97 ns | 1.01x faster |
| NetFabric_Float  | Vector256 | Float      | 100   |    47.88 ns |  0.768 ns |    47.76 ns | 1.37x faster |
| Baseline_Float   | Vector512 | Float      | 100   |    65.29 ns |  1.584 ns |    65.19 ns | 1.01x slower |
| NetFabric_Float  | Vector512 | Float      | 100   |    47.94 ns |  0.577 ns |    48.03 ns | 1.36x faster |
|                  |           |            |       |             |           |             |              |
| Baseline_Half    | Scalar    | Half       | 100   | 3,027.82 ns | 11.188 ns | 3,028.59 ns |     baseline |
| NetFabric_Half   | Scalar    | Half       | 100   | 3,011.36 ns | 27.446 ns | 3,002.45 ns | 1.01x faster |
| Baseline_Half    | Vector128 | Half       | 100   | 2,697.99 ns | 23.967 ns | 2,692.15 ns | 1.12x faster |
| NetFabric_Half   | Vector128 | Half       | 100   | 2,648.99 ns | 16.402 ns | 2,648.01 ns | 1.14x faster |
| Baseline_Half    | Vector256 | Half       | 100   | 2,674.79 ns | 14.089 ns | 2,675.71 ns | 1.13x faster |
| NetFabric_Half   | Vector256 | Half       | 100   | 2,657.25 ns | 22.551 ns | 2,652.22 ns | 1.14x faster |
| Baseline_Half    | Vector512 | Half       | 100   | 2,678.09 ns | 13.898 ns | 2,678.40 ns | 1.13x faster |
| NetFabric_Half   | Vector512 | Half       | 100   | 2,652.20 ns | 17.853 ns | 2,650.41 ns | 1.14x faster |
|                  |           |            |       |             |           |             |              |
| Baseline_Int     | Scalar    | Int        | 100   |    55.13 ns |  0.827 ns |    54.86 ns |     baseline |
| NetFabric_Int    | Scalar    | Int        | 100   |    77.42 ns |  2.504 ns |    77.02 ns | 1.41x slower |
| Baseline_Int     | Vector128 | Int        | 100   |    56.04 ns |  0.775 ns |    55.79 ns | 1.02x slower |
| NetFabric_Int    | Vector128 | Int        | 100   |    57.54 ns |  0.452 ns |    57.52 ns | 1.04x slower |
| Baseline_Int     | Vector256 | Int        | 100   |    56.18 ns |  0.528 ns |    56.14 ns | 1.02x slower |
| NetFabric_Int    | Vector256 | Int        | 100   |    46.41 ns |  1.023 ns |    46.17 ns | 1.18x faster |
| Baseline_Int     | Vector512 | Int        | 100   |    56.17 ns |  0.538 ns |    55.81 ns | 1.02x slower |
| NetFabric_Int    | Vector512 | Int        | 100   |    47.44 ns |  1.304 ns |    47.39 ns | 1.17x faster |
|                  |           |            |       |             |           |             |              |
| Baseline_Long    | Scalar    | Long       | 100   |    55.58 ns |  0.647 ns |    55.60 ns |     baseline |
| NetFabric_Long   | Scalar    | Long       | 100   |    79.05 ns |  3.349 ns |    78.05 ns | 1.45x slower |
| Baseline_Long    | Vector128 | Long       | 100   |    53.84 ns |  0.598 ns |    53.97 ns | 1.03x faster |
| NetFabric_Long   | Vector128 | Long       | 100   |   115.99 ns |  4.999 ns |   114.15 ns | 2.20x slower |
| Baseline_Long    | Vector256 | Long       | 100   |    54.18 ns |  1.042 ns |    53.96 ns | 1.03x faster |
| NetFabric_Long   | Vector256 | Long       | 100   |    65.29 ns |  0.788 ns |    64.94 ns | 1.17x slower |
| Baseline_Long    | Vector512 | Long       | 100   |    53.62 ns |  0.473 ns |    53.47 ns | 1.04x faster |
| NetFabric_Long   | Vector512 | Long       | 100   |    64.74 ns |  0.961 ns |    64.99 ns | 1.17x slower |
|                  |           |            |       |             |           |             |              |
| Baseline_Short   | Scalar    | Short      | 100   |   104.98 ns |  2.734 ns |   103.79 ns |     baseline |
| NetFabric_Short  | Scalar    | Short      | 100   |   116.80 ns |  0.871 ns |   116.76 ns | 1.11x slower |
| Baseline_Short   | Vector128 | Short      | 100   |   105.19 ns |  1.461 ns |   104.58 ns | 1.00x slower |
| NetFabric_Short  | Vector128 | Short      | 100   |    54.18 ns |  2.162 ns |    53.45 ns | 1.93x faster |
| Baseline_Short   | Vector256 | Short      | 100   |   104.76 ns |  0.832 ns |   104.62 ns | 1.00x faster |
| NetFabric_Short  | Vector256 | Short      | 100   |    51.95 ns |  0.549 ns |    51.96 ns | 2.02x faster |
| Baseline_Short   | Vector512 | Short      | 100   |   104.62 ns |  0.647 ns |   104.67 ns | 1.00x faster |
| NetFabric_Short  | Vector512 | Short      | 100   |    52.01 ns |  0.386 ns |    51.97 ns | 2.02x faster |

### Sum4D

Applying a vectorizable aggregation operator on a span with four contiguos values for each element.

It also compares to the performance of LINQ's `Aggregate()`, as LINQ's `Sum()` does not support non-native numeric types.

| Method           | Job       | Categories | Count |        Mean |    StdDev |      Median |        Ratio |
| ---------------- | --------- | ---------- | ----- | ----------: | --------: | ----------: | -----------: |
| Baseline_Double  | Scalar    | Double     | 100   |    70.27 ns |  1.159 ns |    70.54 ns |     baseline |
| NetFabric_Double | Scalar    | Double     | 100   |    74.65 ns |  1.107 ns |    74.23 ns | 1.06x slower |
| Baseline_Double  | Vector128 | Double     | 100   |    66.23 ns |  0.673 ns |    66.16 ns | 1.06x faster |
| NetFabric_Double | Vector128 | Double     | 100   |   266.95 ns |  3.067 ns |   267.10 ns | 3.80x slower |
| Baseline_Double  | Vector256 | Double     | 100   |    70.21 ns |  2.883 ns |    70.53 ns | 1.05x faster |
| NetFabric_Double | Vector256 | Double     | 100   |    51.89 ns |  0.490 ns |    51.98 ns | 1.35x faster |
| Baseline_Double  | Vector512 | Double     | 100   |    71.16 ns |  1.484 ns |    71.40 ns | 1.01x slower |
| NetFabric_Double | Vector512 | Double     | 100   |    53.24 ns |  1.668 ns |    52.35 ns | 1.32x faster |
|                  |           |            |       |             |           |             |              |
| Baseline_Float   | Scalar    | Float      | 100   |    70.08 ns |  1.180 ns |    70.32 ns |     baseline |
| NetFabric_Float  | Scalar    | Float      | 100   |    78.04 ns |  0.886 ns |    78.15 ns | 1.11x slower |
| Baseline_Float   | Vector128 | Float      | 100   |    70.11 ns |  1.266 ns |    70.35 ns | 1.00x slower |
| NetFabric_Float  | Vector128 | Float      | 100   |    51.27 ns |  0.503 ns |    51.20 ns | 1.37x faster |
| Baseline_Float   | Vector256 | Float      | 100   |    70.51 ns |  0.976 ns |    70.50 ns | 1.01x slower |
| NetFabric_Float  | Vector256 | Float      | 100   |    28.12 ns |  0.812 ns |    27.82 ns | 2.50x faster |
| Baseline_Float   | Vector512 | Float      | 100   |    71.39 ns |  1.198 ns |    71.67 ns | 1.02x slower |
| NetFabric_Float  | Vector512 | Float      | 100   |    28.20 ns |  0.806 ns |    27.82 ns | 2.47x faster |
|                  |           |            |       |             |           |             |              |
| Baseline_Half    | Scalar    | Half       | 100   | 3,619.66 ns | 24.005 ns | 3,625.74 ns |     baseline |
| NetFabric_Half   | Scalar    | Half       | 100   | 4,107.22 ns | 39.515 ns | 4,099.78 ns | 1.14x slower |
| Baseline_Half    | Vector128 | Half       | 100   | 2,901.28 ns | 36.231 ns | 2,907.40 ns | 1.25x faster |
| NetFabric_Half   | Vector128 | Half       | 100   | 3,621.06 ns | 50.761 ns | 3,612.47 ns | 1.00x slower |
| Baseline_Half    | Vector256 | Half       | 100   | 2,926.03 ns | 25.950 ns | 2,928.69 ns | 1.24x faster |
| NetFabric_Half   | Vector256 | Half       | 100   | 3,641.74 ns | 34.245 ns | 3,637.19 ns | 1.01x slower |
| Baseline_Half    | Vector512 | Half       | 100   | 2,904.28 ns | 25.974 ns | 2,903.98 ns | 1.25x faster |
| NetFabric_Half   | Vector512 | Half       | 100   | 3,620.77 ns | 30.743 ns | 3,612.92 ns | 1.00x slower |
|                  |           |            |       |             |           |             |              |
| Baseline_Int     | Scalar    | Int        | 100   |    64.53 ns |  1.218 ns |    64.64 ns |     baseline |
| NetFabric_Int    | Scalar    | Int        | 100   |    91.91 ns |  0.867 ns |    92.21 ns | 1.42x slower |
| Baseline_Int     | Vector128 | Int        | 100   |    66.54 ns |  1.323 ns |    66.69 ns | 1.03x slower |
| NetFabric_Int    | Vector128 | Int        | 100   |    35.50 ns |  0.474 ns |    35.67 ns | 1.82x faster |
| Baseline_Int     | Vector256 | Int        | 100   |    66.49 ns |  0.943 ns |    66.77 ns | 1.03x slower |
| NetFabric_Int    | Vector256 | Int        | 100   |    19.73 ns |  0.320 ns |    19.69 ns | 3.27x faster |
| Baseline_Int     | Vector512 | Int        | 100   |    67.16 ns |  2.221 ns |    66.95 ns | 1.04x slower |
| NetFabric_Int    | Vector512 | Int        | 100   |    19.66 ns |  0.254 ns |    19.65 ns | 3.29x faster |
|                  |           |            |       |             |           |             |              |
| Baseline_Long    | Scalar    | Long       | 100   |    68.33 ns |  1.170 ns |    68.47 ns |     baseline |
| NetFabric_Long   | Scalar    | Long       | 100   |    92.38 ns |  3.018 ns |    91.48 ns | 1.36x slower |
| Baseline_Long    | Vector128 | Long       | 100   |    65.33 ns |  0.962 ns |    65.22 ns | 1.05x faster |
| NetFabric_Long   | Vector128 | Long       | 100   |   224.12 ns |  2.034 ns |   223.69 ns | 3.28x slower |
| Baseline_Long    | Vector256 | Long       | 100   |    66.62 ns |  1.365 ns |    66.62 ns | 1.03x faster |
| NetFabric_Long   | Vector256 | Long       | 100   |    36.37 ns |  0.685 ns |    36.52 ns | 1.88x faster |
| Baseline_Long    | Vector512 | Long       | 100   |    66.71 ns |  1.552 ns |    66.69 ns | 1.02x faster |
| NetFabric_Long   | Vector512 | Long       | 100   |    36.98 ns |  0.629 ns |    37.20 ns | 1.85x faster |
|                  |           |            |       |             |           |             |              |
| Baseline_Short   | Scalar    | Short      | 100   |    96.96 ns |  1.010 ns |    97.01 ns |     baseline |
| NetFabric_Short  | Scalar    | Short      | 100   |   137.71 ns |  2.069 ns |   138.09 ns | 1.42x slower |
| Baseline_Short   | Vector128 | Short      | 100   |    96.97 ns |  0.634 ns |    96.82 ns | 1.00x slower |
| NetFabric_Short  | Vector128 | Short      | 100   |    22.32 ns |  0.225 ns |    22.30 ns | 4.35x faster |
| Baseline_Short   | Vector256 | Short      | 100   |    96.67 ns |  2.173 ns |    96.08 ns | 1.00x slower |
| NetFabric_Short  | Vector256 | Short      | 100   |    18.00 ns |  0.273 ns |    17.98 ns | 5.38x faster |
| Baseline_Short   | Vector512 | Short      | 100   |    95.26 ns |  1.179 ns |    94.80 ns | 1.02x faster |
| NetFabric_Short  | Vector512 | Short      | 100   |    21.14 ns |  0.326 ns |    21.10 ns | 4.58x faster |

### Min aggregation

Applying a vectorizable aggregation operator with propagation os `NaN`.

| Method           | Job       | Categories | Count |         Mean |    StdDev |         Ratio |
| ---------------- | --------- | ---------- | ----- | -----------: | --------: | ------------: |
| Baseline_Double  | Scalar    | Double     | 100   |    88.695 ns | 0.4477 ns |      baseline |
| System_Double    | Scalar    | Double     | 100   |    77.290 ns | 0.5142 ns |  1.15x faster |
| NetFabric_Double | Scalar    | Double     | 100   |   101.372 ns | 0.8989 ns |  1.14x slower |
| Baseline_Double  | Vector128 | Double     | 100   |    99.235 ns | 0.8462 ns |  1.12x slower |
| System_Double    | Vector128 | Double     | 100   |    37.595 ns | 0.3627 ns |  2.36x faster |
| NetFabric_Double | Vector128 | Double     | 100   |    74.457 ns | 0.2241 ns |  1.19x faster |
| Baseline_Double  | Vector256 | Double     | 100   |    99.448 ns | 1.1002 ns |  1.12x slower |
| System_Double    | Vector256 | Double     | 100   |    19.433 ns | 0.1401 ns |  4.57x faster |
| NetFabric_Double | Vector256 | Double     | 100   |    37.875 ns | 0.2884 ns |  2.34x faster |
| Baseline_Double  | Vector512 | Double     | 100   |    72.462 ns | 0.2434 ns |  1.22x faster |
| System_Double    | Vector512 | Double     | 100   |    28.316 ns | 0.1643 ns |  3.13x faster |
| NetFabric_Double | Vector512 | Double     | 100   |    38.506 ns | 0.2427 ns |  2.30x faster |
|                  |           |            |       |              |           |               |
| Baseline_Float   | Scalar    | Float      | 100   |   100.707 ns | 0.3968 ns |      baseline |
| System_Float     | Scalar    | Float      | 100   |    74.501 ns | 0.4437 ns |  1.35x faster |
| NetFabric_Float  | Scalar    | Float      | 100   |    88.893 ns | 0.3883 ns |  1.13x faster |
| Baseline_Float   | Vector128 | Float      | 100   |    93.727 ns | 0.3726 ns |  1.07x faster |
| System_Float     | Vector128 | Float      | 100   |    17.961 ns | 0.1359 ns |  5.61x faster |
| NetFabric_Float  | Vector128 | Float      | 100   |    36.605 ns | 0.1388 ns |  2.75x faster |
| Baseline_Float   | Vector256 | Float      | 100   |    96.138 ns | 0.7183 ns |  1.05x faster |
| System_Float     | Vector256 | Float      | 100   |    10.558 ns | 0.0520 ns |  9.54x faster |
| NetFabric_Float  | Vector256 | Float      | 100   |    30.799 ns | 0.1294 ns |  3.27x faster |
| Baseline_Float   | Vector512 | Float      | 100   |    72.845 ns | 0.2197 ns |  1.38x faster |
| System_Float     | Vector512 | Float      | 100   |    14.931 ns | 0.2321 ns |  6.75x faster |
| NetFabric_Float  | Vector512 | Float      | 100   |    23.989 ns | 0.0737 ns |  4.20x faster |
|                  |           |            |       |              |           |               |
| Baseline_Half    | Scalar    | Half       | 100   | 1,152.279 ns | 5.4053 ns |      baseline |
| System_Half      | Scalar    | Half       | 100   |   180.660 ns | 0.6765 ns |  6.38x faster |
| NetFabric_Half   | Scalar    | Half       | 100   | 1,128.877 ns | 4.8094 ns |  1.02x faster |
| Baseline_Half    | Vector128 | Half       | 100   | 1,096.543 ns | 5.6732 ns |  1.05x faster |
| System_Half      | Vector128 | Half       | 100   |   180.565 ns | 1.6048 ns |  6.38x faster |
| NetFabric_Half   | Vector128 | Half       | 100   | 1,089.019 ns | 5.2743 ns |  1.06x faster |
| Baseline_Half    | Vector256 | Half       | 100   | 1,096.248 ns | 6.2655 ns |  1.05x faster |
| System_Half      | Vector256 | Half       | 100   |   179.587 ns | 0.4680 ns |  6.42x faster |
| NetFabric_Half   | Vector256 | Half       | 100   | 1,083.846 ns | 5.0029 ns |  1.06x faster |
| Baseline_Half    | Vector512 | Half       | 100   | 1,264.763 ns | 3.0543 ns |  1.10x slower |
| System_Half      | Vector512 | Half       | 100   |   179.811 ns | 0.7011 ns |  6.41x faster |
| NetFabric_Half   | Vector512 | Half       | 100   | 1,263.840 ns | 3.9612 ns |  1.10x slower |
|                  |           |            |       |              |           |               |
| Baseline_Int     | Scalar    | Int        | 100   |    34.827 ns | 0.1450 ns |      baseline |
| System_Int       | Scalar    | Int        | 100   |    34.970 ns | 0.4225 ns |  1.00x slower |
| NetFabric_Int    | Scalar    | Int        | 100   |    35.142 ns | 0.2962 ns |  1.01x slower |
| Baseline_Int     | Vector128 | Int        | 100   |    35.000 ns | 0.2081 ns |  1.00x slower |
| System_Int       | Vector128 | Int        | 100   |     6.652 ns | 0.0848 ns |  5.24x faster |
| NetFabric_Int    | Vector128 | Int        | 100   |    14.140 ns | 0.0707 ns |  2.46x faster |
| Baseline_Int     | Vector256 | Int        | 100   |    34.743 ns | 0.2514 ns |  1.00x faster |
| System_Int       | Vector256 | Int        | 100   |     3.935 ns | 0.0226 ns |  8.85x faster |
| NetFabric_Int    | Vector256 | Int        | 100   |     9.600 ns | 0.0822 ns |  3.63x faster |
| Baseline_Int     | Vector512 | Int        | 100   |    35.223 ns | 0.3104 ns |  1.01x slower |
| System_Int       | Vector512 | Int        | 100   |     2.844 ns | 0.0248 ns | 12.25x faster |
| NetFabric_Int    | Vector512 | Int        | 100   |     9.713 ns | 0.0722 ns |  3.59x faster |
|                  |           |            |       |              |           |               |
| Baseline_Long    | Scalar    | Long       | 100   |    34.862 ns | 0.2250 ns |      baseline |
| System_Long      | Scalar    | Long       | 100   |    35.469 ns | 0.5156 ns |  1.02x slower |
| NetFabric_Long   | Scalar    | Long       | 100   |    35.329 ns | 0.2556 ns |  1.01x slower |
| Baseline_Long    | Vector128 | Long       | 100   |    71.426 ns | 0.3344 ns |  2.05x slower |
| System_Long      | Vector128 | Long       | 100   |    20.049 ns | 0.1214 ns |  1.74x faster |
| NetFabric_Long   | Vector128 | Long       | 100   |    27.073 ns | 0.1602 ns |  1.29x faster |
| Baseline_Long    | Vector256 | Long       | 100   |    71.732 ns | 0.5353 ns |  2.06x slower |
| System_Long      | Vector256 | Long       | 100   |     8.965 ns | 0.0369 ns |  3.89x faster |
| NetFabric_Long   | Vector256 | Long       | 100   |    14.426 ns | 0.1072 ns |  2.42x faster |
| Baseline_Long    | Vector512 | Long       | 100   |    71.470 ns | 0.3791 ns |  2.05x slower |
| System_Long      | Vector512 | Long       | 100   |     6.234 ns | 0.0301 ns |  5.59x faster |
| NetFabric_Long   | Vector512 | Long       | 100   |    13.922 ns | 0.0954 ns |  2.50x faster |
|                  |           |            |       |              |           |               |
| Baseline_Short   | Scalar    | Short      | 100   |    39.547 ns | 0.1179 ns |      baseline |
| System_Short     | Scalar    | Short      | 100   |    39.819 ns | 0.2629 ns |  1.01x slower |
| NetFabric_Short  | Scalar    | Short      | 100   |    39.546 ns | 0.3363 ns |  1.00x slower |
| Baseline_Short   | Vector128 | Short      | 100   |    40.087 ns | 0.1960 ns |  1.01x slower |
| System_Short     | Vector128 | Short      | 100   |     3.567 ns | 0.0207 ns | 11.09x faster |
| NetFabric_Short  | Vector128 | Short      | 100   |    10.459 ns | 0.0921 ns |  3.78x faster |
| Baseline_Short   | Vector256 | Short      | 100   |    40.558 ns | 0.4019 ns |  1.02x slower |
| System_Short     | Vector256 | Short      | 100   |     2.834 ns | 0.0335 ns | 13.94x faster |
| NetFabric_Short  | Vector256 | Short      | 100   |    12.507 ns | 0.1092 ns |  3.16x faster |
| Baseline_Short   | Vector512 | Short      | 100   |    40.248 ns | 0.3349 ns |  1.02x slower |
| System_Short     | Vector512 | Short      | 100   |     2.710 ns | 0.0450 ns | 14.63x faster |
| NetFabric_Short  | Vector512 | Short      | 100   |    13.168 ns | 0.1512 ns |  3.00x faster |

### MinMax

Applying two vectorizable aggregation operators on a single iteration of a span.

| Method           | Job       | Categories | Count |        Mean |   StdDev |      Median |        Ratio |
| ---------------- | --------- | ---------- | ----- | ----------: | -------: | ----------: | -----------: |
| Baseline_Double  | Scalar    | Double     | 100   |   179.35 ns | 0.721 ns |   179.23 ns |     baseline |
| NetFabric_Double | Scalar    | Double     | 100   |   280.46 ns | 1.142 ns |   280.48 ns | 1.56x slower |
| Baseline_Double  | Vector128 | Double     | 100   |   162.75 ns | 0.844 ns |   162.67 ns | 1.10x faster |
| NetFabric_Double | Vector128 | Double     | 100   |    29.13 ns | 0.211 ns |    29.10 ns | 6.16x faster |
| Baseline_Double  | Vector256 | Double     | 100   |   163.13 ns | 1.237 ns |   162.72 ns | 1.10x faster |
| NetFabric_Double | Vector256 | Double     | 100   |    23.43 ns | 0.106 ns |    23.45 ns | 7.65x faster |
| Baseline_Double  | Vector512 | Double     | 100   |    85.79 ns | 0.456 ns |    85.89 ns | 2.09x faster |
| NetFabric_Double | Vector512 | Double     | 100   |    21.53 ns | 0.147 ns |    21.57 ns | 8.33x faster |
|                  |           |            |       |             |          |             |              |
| Baseline_Float   | Scalar    | Float      | 100   |   170.32 ns | 4.771 ns |   168.32 ns |     baseline |
| NetFabric_Float  | Scalar    | Float      | 100   |   278.95 ns | 0.886 ns |   278.88 ns | 1.64x slower |
| Baseline_Float   | Vector128 | Float      | 100   |   159.66 ns | 0.784 ns |   159.44 ns | 1.06x faster |
| NetFabric_Float  | Vector128 | Float      | 100   |    23.01 ns | 0.243 ns |    23.11 ns | 7.38x faster |
| Baseline_Float   | Vector256 | Float      | 100   |   160.10 ns | 1.203 ns |   160.16 ns | 1.07x faster |
| NetFabric_Float  | Vector256 | Float      | 100   |    40.18 ns | 0.178 ns |    40.20 ns | 4.25x faster |
| Baseline_Float   | Vector512 | Float      | 100   |    85.27 ns | 0.305 ns |    85.35 ns | 2.00x faster |
| NetFabric_Float  | Vector512 | Float      | 100   |    17.24 ns | 0.068 ns |    17.24 ns | 9.91x faster |
|                  |           |            |       |             |          |             |              |
| Baseline_Half    | Scalar    | Half       | 100   | 1,556.85 ns | 8.238 ns | 1,555.36 ns |     baseline |
| NetFabric_Half   | Scalar    | Half       | 100   |   424.22 ns | 2.930 ns |   424.24 ns | 3.67x faster |
| Baseline_Half    | Vector128 | Half       | 100   | 1,326.75 ns | 6.993 ns | 1,326.94 ns | 1.17x faster |
| NetFabric_Half   | Vector128 | Half       | 100   |   443.36 ns | 3.124 ns |   442.03 ns | 3.51x faster |
| Baseline_Half    | Vector256 | Half       | 100   | 1,329.31 ns | 8.359 ns | 1,328.91 ns | 1.17x faster |
| NetFabric_Half   | Vector256 | Half       | 100   |   445.48 ns | 2.852 ns |   445.42 ns | 3.49x faster |
| Baseline_Half    | Vector512 | Half       | 100   | 1,683.51 ns | 5.413 ns | 1,684.22 ns | 1.08x slower |
| NetFabric_Half   | Vector512 | Half       | 100   |   443.25 ns | 3.760 ns |   443.83 ns | 3.52x faster |
|                  |           |            |       |             |          |             |              |
| Baseline_Int     | Scalar    | Int        | 100   |    56.99 ns | 0.471 ns |    56.94 ns |     baseline |
| NetFabric_Int    | Scalar    | Int        | 100   |    56.65 ns | 0.396 ns |    56.69 ns | 1.01x faster |
| Baseline_Int     | Vector128 | Int        | 100   |    74.98 ns | 2.101 ns |    74.10 ns | 1.32x slower |
| NetFabric_Int    | Vector128 | Int        | 100   |    15.97 ns | 0.068 ns |    15.97 ns | 3.57x faster |
| Baseline_Int     | Vector256 | Int        | 100   |    58.38 ns | 0.607 ns |    58.35 ns | 1.02x slower |
| NetFabric_Int    | Vector256 | Int        | 100   |    13.69 ns | 0.158 ns |    13.66 ns | 4.16x faster |
| Baseline_Int     | Vector512 | Int        | 100   |    58.12 ns | 0.684 ns |    57.98 ns | 1.02x slower |
| NetFabric_Int    | Vector512 | Int        | 100   |    13.79 ns | 0.087 ns |    13.78 ns | 4.14x faster |
|                  |           |            |       |             |          |             |              |
| Baseline_Long    | Scalar    | Long       | 100   |    65.65 ns | 6.839 ns |    62.71 ns |     baseline |
| NetFabric_Long   | Scalar    | Long       | 100   |    59.87 ns | 0.458 ns |    59.96 ns | 1.11x faster |
| Baseline_Long    | Vector128 | Long       | 100   |   116.27 ns | 0.414 ns |   116.27 ns | 1.77x slower |
| NetFabric_Long   | Vector128 | Long       | 100   |    33.53 ns | 0.188 ns |    33.51 ns | 1.97x faster |
| Baseline_Long    | Vector256 | Long       | 100   |   133.37 ns | 0.618 ns |   133.28 ns | 2.02x slower |
| NetFabric_Long   | Vector256 | Long       | 100   |    19.29 ns | 0.081 ns |    19.32 ns | 3.43x faster |
| Baseline_Long    | Vector512 | Long       | 100   |   133.90 ns | 0.909 ns |   133.86 ns | 2.03x slower |
| NetFabric_Long   | Vector512 | Long       | 100   |    18.21 ns | 0.248 ns |    18.15 ns | 3.63x faster |
|                  |           |            |       |             |          |             |              |
| Baseline_Short   | Scalar    | Short      | 100   |    57.21 ns | 0.209 ns |    57.17 ns |     baseline |
| NetFabric_Short  | Scalar    | Short      | 100   |    77.70 ns | 1.397 ns |    77.86 ns | 1.36x slower |
| Baseline_Short   | Vector128 | Short      | 100   |    99.30 ns | 0.467 ns |    99.44 ns | 1.74x slower |
| NetFabric_Short  | Vector128 | Short      | 100   |    15.76 ns | 0.133 ns |    15.73 ns | 3.63x faster |
| Baseline_Short   | Vector256 | Short      | 100   |    73.83 ns | 0.381 ns |    73.89 ns | 1.29x slower |
| NetFabric_Short  | Vector256 | Short      | 100   |    23.80 ns | 0.355 ns |    23.95 ns | 2.40x faster |
| Baseline_Short   | Vector512 | Short      | 100   |    74.44 ns | 0.724 ns |    74.45 ns | 1.30x slower |
| NetFabric_Short  | Vector512 | Short      | 100   |    23.79 ns | 0.333 ns |    23.66 ns | 2.41x faster |
