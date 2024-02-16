# Benchmarks

This document presents benchmarks conducted to evaluate various operations on a specific system configuration. The system details are as follows:

```
BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3007/23H2/2023Update/SunValley3)
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

- Baseline_*: Simple iteration without explicit optimizations
- LINQ_*: Utilizing LINQ (when available)
- System_*: Utilizing System.Numerics.Tensors
- NetFabric_*: Utilizing NetFabric.Numerics.Tensors

The source code for the benchmarks can be accessed [here](https://github.com/NetFabric/NetFabric.Numerics.Tensors/tree/main/src/NetFabric.Numerics.Tensors.Benchmarks).

## Test Cases and Considerations

Benchmarks were conducted on small source spans (5 items) as well as larger ones (100 items). Various scenarios of operators and their applications were covered. The baseline for each scenario involved equivalent operations using a `for` loop, with no optimizations applied other than those automatically added by the JIT compiler.

It's important to note that while these benchmarks provide insights into the performance characteristics of the library, they are not exhaustive and are not intended to serve as a comprehensive performance analysis. They aim to offer a general understanding of the library's performance under different scenarios.

## Results

### Negate

Applying a vectorizable unary operator on a span.

| Method           | Job       | Categories | Count | Mean       | StdDev    | Median     | Ratio        | 
|----------------- |---------- |----------- |------ |-----------:|----------:|-----------:|-------------:|-
| **Baseline_Double**  | **Scalar**    | **Double**     | **5**     |   **3.146 ns** | **0.0846 ns** |   **3.146 ns** |     **baseline** | 
| System_Double    | Scalar    | Double     | 5     |   3.027 ns | 0.0617 ns |   3.023 ns | 1.04x faster | 
| NetFabric_Double | Scalar    | Double     | 5     |   3.948 ns | 0.0424 ns |   3.950 ns | 1.26x slower | 
| Baseline_Double  | Vector128 | Double     | 5     |   3.086 ns | 0.0682 ns |   3.089 ns | 1.02x faster | 
| System_Double    | Vector128 | Double     | 5     |   3.549 ns | 0.0353 ns |   3.557 ns | 1.13x slower | 
| NetFabric_Double | Vector128 | Double     | 5     |   2.987 ns | 0.0700 ns |   2.978 ns | 1.05x faster | 
| Baseline_Double  | Vector256 | Double     | 5     |   2.656 ns | 0.0389 ns |   2.647 ns | 1.18x faster | 
| System_Double    | Vector256 | Double     | 5     |   3.706 ns | 0.0477 ns |   3.686 ns | 1.18x slower | 
| NetFabric_Double | Vector256 | Double     | 5     |   2.869 ns | 0.0658 ns |   2.870 ns | 1.10x faster | 
| Baseline_Double  | Vector512 | Double     | 5     |   3.059 ns | 0.0762 ns |   3.039 ns | 1.03x faster | 
| System_Double    | Vector512 | Double     | 5     |   2.586 ns | 0.0335 ns |   2.584 ns | 1.21x faster | 
| NetFabric_Double | Vector512 | Double     | 5     |   2.869 ns | 0.0706 ns |   2.878 ns | 1.10x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Double**  | **Scalar**    | **Double**     | **100**   |  **40.659 ns** | **0.4949 ns** |  **40.792 ns** |     **baseline** | 
| System_Double    | Scalar    | Double     | 100   |  26.334 ns | 0.2180 ns |  26.400 ns | 1.54x faster | 
| NetFabric_Double | Scalar    | Double     | 100   |  23.459 ns | 0.2350 ns |  23.489 ns | 1.73x faster | 
| Baseline_Double  | Vector128 | Double     | 100   |  40.678 ns | 1.4729 ns |  40.093 ns | 1.00x slower | 
| System_Double    | Vector128 | Double     | 100   |  11.105 ns | 0.1214 ns |  11.116 ns | 3.66x faster | 
| NetFabric_Double | Vector128 | Double     | 100   |  13.576 ns | 0.3271 ns |  13.577 ns | 3.00x faster | 
| Baseline_Double  | Vector256 | Double     | 100   |  28.790 ns | 0.2768 ns |  28.804 ns | 1.41x faster | 
| System_Double    | Vector256 | Double     | 100   |   9.824 ns | 0.1108 ns |   9.795 ns | 4.14x faster | 
| NetFabric_Double | Vector256 | Double     | 100   |   9.116 ns | 0.1368 ns |   9.136 ns | 4.46x faster | 
| Baseline_Double  | Vector512 | Double     | 100   |  40.155 ns | 0.5440 ns |  40.118 ns | 1.01x faster | 
| System_Double    | Vector512 | Double     | 100   |   9.840 ns | 0.0977 ns |   9.861 ns | 4.13x faster | 
| NetFabric_Double | Vector512 | Double     | 100   |   8.434 ns | 0.1617 ns |   8.418 ns | 4.82x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **5**     |   **3.117 ns** | **0.0611 ns** |   **3.123 ns** |     **baseline** | 
| System_Float     | Scalar    | Float      | 5     |   3.069 ns | 0.0698 ns |   3.049 ns | 1.02x faster | 
| NetFabric_Float  | Scalar    | Float      | 5     |   3.963 ns | 0.0606 ns |   3.972 ns | 1.27x slower | 
| Baseline_Float   | Vector128 | Float      | 5     |   3.028 ns | 0.0718 ns |   3.035 ns | 1.03x faster | 
| System_Float     | Vector128 | Float      | 5     |   3.839 ns | 0.0896 ns |   3.822 ns | 1.23x slower | 
| NetFabric_Float  | Vector128 | Float      | 5     |   2.816 ns | 0.0571 ns |   2.830 ns | 1.11x faster | 
| Baseline_Float   | Vector256 | Float      | 5     |   2.604 ns | 0.0488 ns |   2.606 ns | 1.20x faster | 
| System_Float     | Vector256 | Float      | 5     |   2.845 ns | 0.0515 ns |   2.842 ns | 1.10x faster | 
| NetFabric_Float  | Vector256 | Float      | 5     |   3.414 ns | 0.0530 ns |   3.427 ns | 1.09x slower | 
| Baseline_Float   | Vector512 | Float      | 5     |   3.132 ns | 0.0704 ns |   3.119 ns | 1.00x slower | 
| System_Float     | Vector512 | Float      | 5     |   2.901 ns | 0.0525 ns |   2.894 ns | 1.07x faster | 
| NetFabric_Float  | Vector512 | Float      | 5     |   3.782 ns | 0.0718 ns |   3.789 ns | 1.21x slower | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **100**   |  **29.944 ns** | **0.3510 ns** |  **29.922 ns** |     **baseline** | 
| System_Float     | Scalar    | Float      | 100   |  26.279 ns | 0.2293 ns |  26.292 ns | 1.14x faster | 
| NetFabric_Float  | Scalar    | Float      | 100   |  24.070 ns | 0.2073 ns |  24.097 ns | 1.24x faster | 
| Baseline_Float   | Vector128 | Float      | 100   |  40.984 ns | 1.4868 ns |  40.324 ns | 1.37x slower | 
| System_Float     | Vector128 | Float      | 100   |   8.056 ns | 0.1128 ns |   8.055 ns | 3.72x faster | 
| NetFabric_Float  | Vector128 | Float      | 100   |   8.464 ns | 0.1160 ns |   8.514 ns | 3.54x faster | 
| Baseline_Float   | Vector256 | Float      | 100   |  29.020 ns | 0.3105 ns |  28.963 ns | 1.03x faster | 
| System_Float     | Vector256 | Float      | 100   |   8.175 ns | 0.1047 ns |   8.209 ns | 3.67x faster | 
| NetFabric_Float  | Vector256 | Float      | 100   |   6.021 ns | 0.0621 ns |   6.039 ns | 4.98x faster | 
| Baseline_Float   | Vector512 | Float      | 100   |  41.387 ns | 1.1453 ns |  40.839 ns | 1.39x slower | 
| System_Float     | Vector512 | Float      | 100   |   9.441 ns | 0.0953 ns |   9.459 ns | 3.17x faster | 
| NetFabric_Float  | Vector512 | Float      | 100   |   7.079 ns | 0.0636 ns |   7.082 ns | 4.23x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **5**     |  **32.910 ns** | **0.3695 ns** |  **32.745 ns** |     **baseline** | 
| System_Half      | Scalar    | Half       | 5     |  29.555 ns | 0.3854 ns |  29.444 ns | 1.11x faster | 
| NetFabric_Half   | Scalar    | Half       | 5     |  32.800 ns | 0.4621 ns |  32.740 ns | 1.00x faster | 
| Baseline_Half    | Vector128 | Half       | 5     |  27.571 ns | 0.2267 ns |  27.577 ns | 1.19x faster | 
| System_Half      | Vector128 | Half       | 5     |  23.550 ns | 0.1675 ns |  23.558 ns | 1.40x faster | 
| NetFabric_Half   | Vector128 | Half       | 5     |  27.525 ns | 0.2609 ns |  27.549 ns | 1.20x faster | 
| Baseline_Half    | Vector256 | Half       | 5     |  27.536 ns | 0.2192 ns |  27.592 ns | 1.20x faster | 
| System_Half      | Vector256 | Half       | 5     |  23.368 ns | 0.1775 ns |  23.325 ns | 1.41x faster | 
| NetFabric_Half   | Vector256 | Half       | 5     |  27.527 ns | 0.2541 ns |  27.537 ns | 1.20x faster | 
| Baseline_Half    | Vector512 | Half       | 5     |  27.553 ns | 0.2609 ns |  27.622 ns | 1.19x faster | 
| System_Half      | Vector512 | Half       | 5     |  23.555 ns | 0.3010 ns |  23.593 ns | 1.40x faster | 
| NetFabric_Half   | Vector512 | Half       | 5     |  27.643 ns | 0.1740 ns |  27.671 ns | 1.19x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **100**   | **688.642 ns** | **6.6135 ns** | **690.474 ns** |     **baseline** | 
| System_Half      | Scalar    | Half       | 100   | 679.388 ns | 6.6502 ns | 680.802 ns | 1.01x faster | 
| NetFabric_Half   | Scalar    | Half       | 100   | 678.278 ns | 5.9218 ns | 681.040 ns | 1.02x faster | 
| Baseline_Half    | Vector128 | Half       | 100   | 578.373 ns | 4.8883 ns | 579.442 ns | 1.19x faster | 
| System_Half      | Vector128 | Half       | 100   | 575.354 ns | 6.1605 ns | 576.530 ns | 1.20x faster | 
| NetFabric_Half   | Vector128 | Half       | 100   | 569.706 ns | 5.7373 ns | 567.078 ns | 1.21x faster | 
| Baseline_Half    | Vector256 | Half       | 100   | 575.565 ns | 6.5832 ns | 575.151 ns | 1.20x faster | 
| System_Half      | Vector256 | Half       | 100   | 573.670 ns | 4.5719 ns | 574.510 ns | 1.20x faster | 
| NetFabric_Half   | Vector256 | Half       | 100   | 568.580 ns | 5.3747 ns | 565.508 ns | 1.21x faster | 
| Baseline_Half    | Vector512 | Half       | 100   | 576.439 ns | 5.8218 ns | 578.398 ns | 1.19x faster | 
| System_Half      | Vector512 | Half       | 100   | 574.265 ns | 6.6712 ns | 576.701 ns | 1.20x faster | 
| NetFabric_Half   | Vector512 | Half       | 100   | 572.612 ns | 6.5440 ns | 573.418 ns | 1.20x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **5**     |   **3.438 ns** | **0.0662 ns** |   **3.447 ns** |     **baseline** | 
| System_Int       | Scalar    | Int        | 5     |   3.348 ns | 0.0637 ns |   3.356 ns | 1.03x faster | 
| NetFabric_Int    | Scalar    | Int        | 5     |   3.916 ns | 0.0604 ns |   3.914 ns | 1.14x slower | 
| Baseline_Int     | Vector128 | Int        | 5     |   2.859 ns | 0.0548 ns |   2.864 ns | 1.20x faster | 
| System_Int       | Vector128 | Int        | 5     |   3.602 ns | 0.0601 ns |   3.590 ns | 1.05x slower | 
| NetFabric_Int    | Vector128 | Int        | 5     |   3.350 ns | 0.0402 ns |   3.345 ns | 1.03x faster | 
| Baseline_Int     | Vector256 | Int        | 5     |   2.798 ns | 0.0448 ns |   2.784 ns | 1.23x faster | 
| System_Int       | Vector256 | Int        | 5     |   2.649 ns | 0.0310 ns |   2.654 ns | 1.30x faster | 
| NetFabric_Int    | Vector256 | Int        | 5     |   3.417 ns | 0.0654 ns |   3.392 ns | 1.01x faster | 
| Baseline_Int     | Vector512 | Int        | 5     |   2.817 ns | 0.0485 ns |   2.807 ns | 1.22x faster | 
| System_Int       | Vector512 | Int        | 5     |   2.761 ns | 0.0417 ns |   2.760 ns | 1.25x faster | 
| NetFabric_Int    | Vector512 | Int        | 5     |   3.425 ns | 0.0546 ns |   3.440 ns | 1.00x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **100**   |  **32.303 ns** | **0.4811 ns** |  **32.299 ns** |     **baseline** | 
| System_Int       | Scalar    | Int        | 100   |  25.720 ns | 0.1881 ns |  25.689 ns | 1.26x faster | 
| NetFabric_Int    | Scalar    | Int        | 100   |  20.510 ns | 0.2287 ns |  20.472 ns | 1.58x faster | 
| Baseline_Int     | Vector128 | Int        | 100   |  32.514 ns | 0.4398 ns |  32.561 ns | 1.01x slower | 
| System_Int       | Vector128 | Int        | 100   |   9.209 ns | 0.0856 ns |   9.212 ns | 3.51x faster | 
| NetFabric_Int    | Vector128 | Int        | 100   |  11.979 ns | 0.0724 ns |  11.971 ns | 2.70x faster | 
| Baseline_Int     | Vector256 | Int        | 100   |  32.702 ns | 0.4320 ns |  32.668 ns | 1.01x slower | 
| System_Int       | Vector256 | Int        | 100   |   7.326 ns | 0.0886 ns |   7.341 ns | 4.41x faster | 
| NetFabric_Int    | Vector256 | Int        | 100   |   6.224 ns | 0.0728 ns |   6.244 ns | 5.19x faster | 
| Baseline_Int     | Vector512 | Int        | 100   |  32.636 ns | 0.4763 ns |  32.599 ns | 1.01x slower | 
| System_Int       | Vector512 | Int        | 100   |  11.082 ns | 0.1310 ns |  11.105 ns | 2.92x faster | 
| NetFabric_Int    | Vector512 | Int        | 100   |  10.166 ns | 0.1105 ns |  10.184 ns | 3.18x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **5**     |   **2.985 ns** | **0.0553 ns** |   **2.990 ns** |     **baseline** | 
| System_Long      | Scalar    | Long       | 5     |   3.583 ns | 0.0302 ns |   3.592 ns | 1.20x slower | 
| NetFabric_Long   | Scalar    | Long       | 5     |   4.143 ns | 0.0605 ns |   4.122 ns | 1.39x slower | 
| Baseline_Long    | Vector128 | Long       | 5     |   2.655 ns | 0.0370 ns |   2.646 ns | 1.12x faster | 
| System_Long      | Vector128 | Long       | 5     |   3.528 ns | 0.0689 ns |   3.537 ns | 1.18x slower | 
| NetFabric_Long   | Vector128 | Long       | 5     |   2.960 ns | 0.0482 ns |   2.970 ns | 1.01x faster | 
| Baseline_Long    | Vector256 | Long       | 5     |   2.675 ns | 0.0716 ns |   2.681 ns | 1.12x faster | 
| System_Long      | Vector256 | Long       | 5     |   3.548 ns | 0.0662 ns |   3.557 ns | 1.19x slower | 
| NetFabric_Long   | Vector256 | Long       | 5     |   3.122 ns | 0.0546 ns |   3.118 ns | 1.05x slower | 
| Baseline_Long    | Vector512 | Long       | 5     |   2.733 ns | 0.0394 ns |   2.726 ns | 1.09x faster | 
| System_Long      | Vector512 | Long       | 5     |   2.438 ns | 0.0333 ns |   2.436 ns | 1.22x faster | 
| NetFabric_Long   | Vector512 | Long       | 5     |   3.045 ns | 0.0373 ns |   3.045 ns | 1.02x slower | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **100**   |  **32.089 ns** | **0.5043 ns** |  **32.005 ns** |     **baseline** | 
| System_Long      | Scalar    | Long       | 100   |  25.737 ns | 0.2504 ns |  25.738 ns | 1.25x faster | 
| NetFabric_Long   | Scalar    | Long       | 100   |  21.568 ns | 0.2601 ns |  21.592 ns | 1.49x faster | 
| Baseline_Long    | Vector128 | Long       | 100   |  32.870 ns | 0.4063 ns |  32.786 ns | 1.02x slower | 
| System_Long      | Vector128 | Long       | 100   |  12.828 ns | 0.0977 ns |  12.807 ns | 2.50x faster | 
| NetFabric_Long   | Vector128 | Long       | 100   |  14.888 ns | 0.2155 ns |  14.968 ns | 2.16x faster | 
| Baseline_Long    | Vector256 | Long       | 100   |  32.473 ns | 0.5979 ns |  32.398 ns | 1.01x slower | 
| System_Long      | Vector256 | Long       | 100   |  10.961 ns | 0.2301 ns |  11.025 ns | 2.93x faster | 
| NetFabric_Long   | Vector256 | Long       | 100   |   8.813 ns | 0.1257 ns |   8.813 ns | 3.64x faster | 
| Baseline_Long    | Vector512 | Long       | 100   |  32.484 ns | 0.2965 ns |  32.497 ns | 1.01x slower | 
| System_Long      | Vector512 | Long       | 100   |  14.524 ns | 1.1340 ns |  14.788 ns | 2.33x faster | 
| NetFabric_Long   | Vector512 | Long       | 100   |   9.006 ns | 0.0982 ns |   8.980 ns | 3.56x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **5**     |   **3.552 ns** | **0.0590 ns** |   **3.552 ns** |     **baseline** | 
| System_Short     | Scalar    | Short      | 5     |   3.587 ns | 0.0593 ns |   3.578 ns | 1.01x slower | 
| NetFabric_Short  | Scalar    | Short      | 5     |   3.458 ns | 0.0693 ns |   3.432 ns | 1.03x faster | 
| Baseline_Short   | Vector128 | Short      | 5     |   2.827 ns | 0.0725 ns |   2.818 ns | 1.26x faster | 
| System_Short     | Vector128 | Short      | 5     |   2.917 ns | 0.0789 ns |   2.880 ns | 1.22x faster | 
| NetFabric_Short  | Vector128 | Short      | 5     |   3.539 ns | 0.0564 ns |   3.521 ns | 1.00x faster | 
| Baseline_Short   | Vector256 | Short      | 5     |   3.132 ns | 0.0428 ns |   3.113 ns | 1.13x faster | 
| System_Short     | Vector256 | Short      | 5     |   2.855 ns | 0.0379 ns |   2.846 ns | 1.25x faster | 
| NetFabric_Short  | Vector256 | Short      | 5     |   3.675 ns | 0.0431 ns |   3.688 ns | 1.04x slower | 
| Baseline_Short   | Vector512 | Short      | 5     |   2.782 ns | 0.0715 ns |   2.748 ns | 1.28x faster | 
| System_Short     | Vector512 | Short      | 5     |   2.915 ns | 0.0429 ns |   2.911 ns | 1.22x faster | 
| NetFabric_Short  | Vector512 | Short      | 5     |   3.683 ns | 0.0464 ns |   3.696 ns | 1.04x slower | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **100**   |  **35.949 ns** | **0.4859 ns** |  **35.849 ns** |     **baseline** | 
| System_Short     | Scalar    | Short      | 100   |  36.570 ns | 0.4020 ns |  36.447 ns | 1.02x slower | 
| NetFabric_Short  | Scalar    | Short      | 100   |  27.150 ns | 0.1986 ns |  27.161 ns | 1.33x faster | 
| Baseline_Short   | Vector128 | Short      | 100   |  36.758 ns | 0.4800 ns |  36.974 ns | 1.02x slower | 
| System_Short     | Vector128 | Short      | 100   |  36.177 ns | 0.4681 ns |  36.300 ns | 1.01x slower | 
| NetFabric_Short  | Vector128 | Short      | 100   |   6.214 ns | 0.1379 ns |   6.165 ns | 5.80x faster | 
| Baseline_Short   | Vector256 | Short      | 100   |  36.321 ns | 0.2705 ns |  36.371 ns | 1.01x slower | 
| System_Short     | Vector256 | Short      | 100   |  36.700 ns | 0.3842 ns |  36.683 ns | 1.02x slower | 
| NetFabric_Short  | Vector256 | Short      | 100   |   5.541 ns | 0.0414 ns |   5.540 ns | 6.49x faster | 
| Baseline_Short   | Vector512 | Short      | 100   |  36.980 ns | 0.4604 ns |  36.908 ns | 1.03x slower | 
| System_Short     | Vector512 | Short      | 100   |  36.013 ns | 0.5548 ns |  36.050 ns | 1.00x slower | 
| NetFabric_Short  | Vector512 | Short      | 100   |   5.495 ns | 0.0575 ns |   5.492 ns | 6.55x faster | 

### Add

Applying a vectorizable binary operator on two spans.

| Method           | Job       | Categories | Count | Mean       | StdDev    | Median     | Ratio        | 
|----------------- |---------- |----------- |------ |-----------:|----------:|-----------:|-------------:|-
| **Baseline_Double**  | **Scalar**    | **Double**     | **5**     |   **3.967 ns** | **0.0276 ns** |   **3.959 ns** |     **baseline** | 
| System_Double    | Scalar    | Double     | 5     |   5.508 ns | 0.0487 ns |   5.500 ns | 1.39x slower | 
| NetFabric_Double | Scalar    | Double     | 5     |   5.242 ns | 0.0162 ns |   5.240 ns | 1.32x slower | 
| Baseline_Double  | Vector128 | Double     | 5     |   3.398 ns | 0.1617 ns |   3.329 ns | 1.16x faster | 
| System_Double    | Vector128 | Double     | 5     |   4.422 ns | 0.0096 ns |   4.417 ns | 1.11x slower | 
| NetFabric_Double | Vector128 | Double     | 5     |   4.355 ns | 0.0499 ns |   4.346 ns | 1.10x slower | 
| Baseline_Double  | Vector256 | Double     | 5     |   3.485 ns | 0.1754 ns |   3.476 ns | 1.12x faster | 
| System_Double    | Vector256 | Double     | 5     |   4.453 ns | 0.0291 ns |   4.444 ns | 1.12x slower | 
| NetFabric_Double | Vector256 | Double     | 5     |   4.324 ns | 0.0388 ns |   4.330 ns | 1.09x slower | 
| Baseline_Double  | Vector512 | Double     | 5     |   3.720 ns | 0.2818 ns |   3.658 ns | 1.07x faster | 
| System_Double    | Vector512 | Double     | 5     |   3.848 ns | 0.1016 ns |   3.873 ns | 1.03x faster | 
| NetFabric_Double | Vector512 | Double     | 5     |   4.427 ns | 0.0861 ns |   4.403 ns | 1.12x slower | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Double**  | **Scalar**    | **Double**     | **100**   |  **37.851 ns** | **0.5264 ns** |  **37.748 ns** |     **baseline** | 
| System_Double    | Scalar    | Double     | 100   |  27.903 ns | 0.5548 ns |  27.888 ns | 1.36x faster | 
| NetFabric_Double | Scalar    | Double     | 100   |  24.969 ns | 0.2354 ns |  24.968 ns | 1.52x faster | 
| Baseline_Double  | Vector128 | Double     | 100   |  42.823 ns | 1.3994 ns |  42.659 ns | 1.13x slower | 
| System_Double    | Vector128 | Double     | 100   |  19.715 ns | 0.1989 ns |  19.696 ns | 1.92x faster | 
| NetFabric_Double | Vector128 | Double     | 100   |  19.413 ns | 0.1692 ns |  19.473 ns | 1.95x faster | 
| Baseline_Double  | Vector256 | Double     | 100   |  43.328 ns | 1.4951 ns |  43.172 ns | 1.14x slower | 
| System_Double    | Vector256 | Double     | 100   |  15.099 ns | 0.0893 ns |  15.093 ns | 2.50x faster | 
| NetFabric_Double | Vector256 | Double     | 100   |  12.169 ns | 0.1647 ns |  12.147 ns | 3.11x faster | 
| Baseline_Double  | Vector512 | Double     | 100   |  42.595 ns | 1.4087 ns |  42.439 ns | 1.15x slower | 
| System_Double    | Vector512 | Double     | 100   |  16.205 ns | 0.0928 ns |  16.209 ns | 2.33x faster | 
| NetFabric_Double | Vector512 | Double     | 100   |  11.851 ns | 0.2395 ns |  11.742 ns | 3.19x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **5**     |   **4.026 ns** | **0.0439 ns** |   **4.041 ns** |     **baseline** | 
| System_Float     | Scalar    | Float      | 5     |   4.318 ns | 0.0450 ns |   4.298 ns | 1.07x slower | 
| NetFabric_Float  | Scalar    | Float      | 5     |   5.397 ns | 0.0521 ns |   5.384 ns | 1.34x slower | 
| Baseline_Float   | Vector128 | Float      | 5     |   3.347 ns | 0.0379 ns |   3.345 ns | 1.20x faster | 
| System_Float     | Vector128 | Float      | 5     |   4.395 ns | 0.0972 ns |   4.401 ns | 1.09x slower | 
| NetFabric_Float  | Vector128 | Float      | 5     |   4.298 ns | 0.0776 ns |   4.301 ns | 1.07x slower | 
| Baseline_Float   | Vector256 | Float      | 5     |   3.404 ns | 0.0981 ns |   3.392 ns | 1.18x faster | 
| System_Float     | Vector256 | Float      | 5     |   3.803 ns | 0.0657 ns |   3.801 ns | 1.06x faster | 
| NetFabric_Float  | Vector256 | Float      | 5     |   4.761 ns | 0.0906 ns |   4.749 ns | 1.18x slower | 
| Baseline_Float   | Vector512 | Float      | 5     |   3.337 ns | 0.0576 ns |   3.345 ns | 1.21x faster | 
| System_Float     | Vector512 | Float      | 5     |   3.624 ns | 0.0776 ns |   3.649 ns | 1.11x faster | 
| NetFabric_Float  | Vector512 | Float      | 5     |   4.802 ns | 0.0918 ns |   4.806 ns | 1.19x slower | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **100**   |  **37.027 ns** | **0.2657 ns** |  **37.037 ns** |     **baseline** | 
| System_Float     | Scalar    | Float      | 100   |  27.522 ns | 0.4283 ns |  27.534 ns | 1.35x faster | 
| NetFabric_Float  | Scalar    | Float      | 100   |  25.049 ns | 0.3098 ns |  25.008 ns | 1.48x faster | 
| Baseline_Float   | Vector128 | Float      | 100   |  38.401 ns | 1.0349 ns |  38.215 ns | 1.04x slower | 
| System_Float     | Vector128 | Float      | 100   |  12.850 ns | 0.1740 ns |  12.850 ns | 2.88x faster | 
| NetFabric_Float  | Vector128 | Float      | 100   |  11.008 ns | 0.1987 ns |  10.991 ns | 3.36x faster | 
| Baseline_Float   | Vector256 | Float      | 100   |  38.139 ns | 0.6990 ns |  38.123 ns | 1.03x slower | 
| System_Float     | Vector256 | Float      | 100   |   9.712 ns | 0.1335 ns |   9.707 ns | 3.82x faster | 
| NetFabric_Float  | Vector256 | Float      | 100   |   8.818 ns | 0.2470 ns |   8.796 ns | 4.21x faster | 
| Baseline_Float   | Vector512 | Float      | 100   |  37.968 ns | 0.7198 ns |  38.024 ns | 1.03x slower | 
| System_Float     | Vector512 | Float      | 100   |  10.334 ns | 0.1918 ns |  10.390 ns | 3.58x faster | 
| NetFabric_Float  | Vector512 | Float      | 100   |   9.093 ns | 0.2063 ns |   9.039 ns | 4.06x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **5**     |  **44.582 ns** | **0.8844 ns** |  **44.516 ns** |     **baseline** | 
| System_Half      | Scalar    | Half       | 5     |  45.099 ns | 0.7102 ns |  45.022 ns | 1.01x slower | 
| NetFabric_Half   | Scalar    | Half       | 5     |  46.506 ns | 0.4963 ns |  46.371 ns | 1.04x slower | 
| Baseline_Half    | Vector128 | Half       | 5     |  36.366 ns | 0.6210 ns |  36.311 ns | 1.23x faster | 
| System_Half      | Vector128 | Half       | 5     |  37.124 ns | 0.4591 ns |  37.142 ns | 1.20x faster | 
| NetFabric_Half   | Vector128 | Half       | 5     |  36.517 ns | 0.3784 ns |  36.641 ns | 1.22x faster | 
| Baseline_Half    | Vector256 | Half       | 5     |  35.174 ns | 0.3316 ns |  35.087 ns | 1.27x faster | 
| System_Half      | Vector256 | Half       | 5     |  36.194 ns | 0.3264 ns |  36.329 ns | 1.23x faster | 
| NetFabric_Half   | Vector256 | Half       | 5     |  36.276 ns | 0.4248 ns |  36.316 ns | 1.23x faster | 
| Baseline_Half    | Vector512 | Half       | 5     |  34.775 ns | 0.3279 ns |  34.692 ns | 1.28x faster | 
| System_Half      | Vector512 | Half       | 5     |  35.962 ns | 0.4258 ns |  35.879 ns | 1.24x faster | 
| NetFabric_Half   | Vector512 | Half       | 5     |  36.618 ns | 0.4779 ns |  36.803 ns | 1.22x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **100**   | **886.588 ns** | **5.9504 ns** | **888.971 ns** |     **baseline** | 
| System_Half      | Scalar    | Half       | 100   | 889.122 ns | 7.0567 ns | 889.861 ns | 1.00x slower | 
| NetFabric_Half   | Scalar    | Half       | 100   | 885.165 ns | 5.0181 ns | 886.420 ns | 1.00x faster | 
| Baseline_Half    | Vector128 | Half       | 100   | 813.949 ns | 7.9313 ns | 816.848 ns | 1.09x faster | 
| System_Half      | Vector128 | Half       | 100   | 792.121 ns | 4.8807 ns | 793.019 ns | 1.12x faster | 
| NetFabric_Half   | Vector128 | Half       | 100   | 802.416 ns | 8.2418 ns | 803.598 ns | 1.11x faster | 
| Baseline_Half    | Vector256 | Half       | 100   | 811.453 ns | 6.0246 ns | 814.662 ns | 1.09x faster | 
| System_Half      | Vector256 | Half       | 100   | 790.901 ns | 6.3525 ns | 793.455 ns | 1.12x faster | 
| NetFabric_Half   | Vector256 | Half       | 100   | 799.524 ns | 4.8134 ns | 801.302 ns | 1.11x faster | 
| Baseline_Half    | Vector512 | Half       | 100   | 812.232 ns | 5.6733 ns | 813.976 ns | 1.09x faster | 
| System_Half      | Vector512 | Half       | 100   | 788.912 ns | 7.1208 ns | 791.641 ns | 1.12x faster | 
| NetFabric_Half   | Vector512 | Half       | 100   | 799.680 ns | 4.8350 ns | 801.585 ns | 1.11x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **5**     |   **4.655 ns** | **0.1410 ns** |   **4.630 ns** |     **baseline** | 
| System_Int       | Scalar    | Int        | 5     |   5.197 ns | 0.0347 ns |   5.201 ns | 1.11x slower | 
| NetFabric_Int    | Scalar    | Int        | 5     |   5.733 ns | 0.0643 ns |   5.745 ns | 1.23x slower | 
| Baseline_Int     | Vector128 | Int        | 5     |   5.561 ns | 0.0433 ns |   5.564 ns | 1.19x slower | 
| System_Int       | Vector128 | Int        | 5     |   6.330 ns | 0.0867 ns |   6.285 ns | 1.36x slower | 
| NetFabric_Int    | Vector128 | Int        | 5     |   4.338 ns | 0.0665 ns |   4.371 ns | 1.08x faster | 
| Baseline_Int     | Vector256 | Int        | 5     |   5.559 ns | 0.0462 ns |   5.570 ns | 1.19x slower | 
| System_Int       | Vector256 | Int        | 5     |   4.473 ns | 0.0729 ns |   4.472 ns | 1.04x faster | 
| NetFabric_Int    | Vector256 | Int        | 5     |   6.171 ns | 0.0909 ns |   6.147 ns | 1.32x slower | 
| Baseline_Int     | Vector512 | Int        | 5     |   4.942 ns | 0.0885 ns |   4.954 ns | 1.06x slower | 
| System_Int       | Vector512 | Int        | 5     |   3.913 ns | 0.0440 ns |   3.905 ns | 1.19x faster | 
| NetFabric_Int    | Vector512 | Int        | 5     |   4.920 ns | 0.0512 ns |   4.915 ns | 1.05x slower | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **100**   |  **42.549 ns** | **0.5866 ns** |  **42.462 ns** |     **baseline** | 
| System_Int       | Scalar    | Int        | 100   |  32.751 ns | 0.4079 ns |  32.576 ns | 1.30x faster | 
| NetFabric_Int    | Scalar    | Int        | 100   |  25.492 ns | 0.1765 ns |  25.524 ns | 1.67x faster | 
| Baseline_Int     | Vector128 | Int        | 100   |  45.536 ns | 0.3933 ns |  45.499 ns | 1.07x slower | 
| System_Int       | Vector128 | Int        | 100   |  12.894 ns | 0.0871 ns |  12.885 ns | 3.30x faster | 
| NetFabric_Int    | Vector128 | Int        | 100   |  12.129 ns | 0.1592 ns |  12.104 ns | 3.51x faster | 
| Baseline_Int     | Vector256 | Int        | 100   |  43.369 ns | 0.4463 ns |  43.394 ns | 1.02x slower | 
| System_Int       | Vector256 | Int        | 100   |  11.011 ns | 0.0998 ns |  11.024 ns | 3.86x faster | 
| NetFabric_Int    | Vector256 | Int        | 100   |   8.438 ns | 0.0893 ns |   8.402 ns | 5.04x faster | 
| Baseline_Int     | Vector512 | Int        | 100   |  43.180 ns | 0.4705 ns |  43.216 ns | 1.01x slower | 
| System_Int       | Vector512 | Int        | 100   |  11.741 ns | 0.0923 ns |  11.737 ns | 3.63x faster | 
| NetFabric_Int    | Vector512 | Int        | 100   |   8.629 ns | 0.1104 ns |   8.621 ns | 4.94x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **5**     |   **4.745 ns** | **0.1140 ns** |   **4.741 ns** |     **baseline** | 
| System_Long      | Scalar    | Long       | 5     |   5.141 ns | 0.0482 ns |   5.148 ns | 1.08x slower | 
| NetFabric_Long   | Scalar    | Long       | 5     |   5.674 ns | 0.0520 ns |   5.687 ns | 1.20x slower | 
| Baseline_Long    | Vector128 | Long       | 5     |   3.443 ns | 0.0575 ns |   3.438 ns | 1.38x faster | 
| System_Long      | Vector128 | Long       | 5     |   4.539 ns | 0.0427 ns |   4.541 ns | 1.05x faster | 
| NetFabric_Long   | Vector128 | Long       | 5     |   4.298 ns | 0.0667 ns |   4.299 ns | 1.11x faster | 
| Baseline_Long    | Vector256 | Long       | 5     |   3.407 ns | 0.0595 ns |   3.412 ns | 1.39x faster | 
| System_Long      | Vector256 | Long       | 5     |   4.491 ns | 0.0641 ns |   4.464 ns | 1.06x faster | 
| NetFabric_Long   | Vector256 | Long       | 5     |   4.691 ns | 0.0606 ns |   4.679 ns | 1.01x faster | 
| Baseline_Long    | Vector512 | Long       | 5     |   3.400 ns | 0.0740 ns |   3.382 ns | 1.40x faster | 
| System_Long      | Vector512 | Long       | 5     |   3.745 ns | 0.0519 ns |   3.753 ns | 1.27x faster | 
| NetFabric_Long   | Vector512 | Long       | 5     |   4.681 ns | 0.0594 ns |   4.687 ns | 1.01x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **100**   |  **41.710 ns** | **0.5362 ns** |  **41.756 ns** |     **baseline** | 
| System_Long      | Scalar    | Long       | 100   |  33.493 ns | 0.4887 ns |  33.530 ns | 1.25x faster | 
| NetFabric_Long   | Scalar    | Long       | 100   |  26.899 ns | 0.2824 ns |  26.996 ns | 1.55x faster | 
| Baseline_Long    | Vector128 | Long       | 100   |  43.072 ns | 0.7994 ns |  43.220 ns | 1.03x slower | 
| System_Long      | Vector128 | Long       | 100   |  18.664 ns | 0.1593 ns |  18.710 ns | 2.24x faster | 
| NetFabric_Long   | Vector128 | Long       | 100   |  22.627 ns | 0.2804 ns |  22.674 ns | 1.84x faster | 
| Baseline_Long    | Vector256 | Long       | 100   |  42.589 ns | 0.6813 ns |  42.783 ns | 1.02x slower | 
| System_Long      | Vector256 | Long       | 100   |  15.442 ns | 0.1840 ns |  15.377 ns | 2.70x faster | 
| NetFabric_Long   | Vector256 | Long       | 100   |  13.391 ns | 0.0898 ns |  13.385 ns | 3.12x faster | 
| Baseline_Long    | Vector512 | Long       | 100   |  42.026 ns | 0.6525 ns |  41.786 ns | 1.01x slower | 
| System_Long      | Vector512 | Long       | 100   |  16.571 ns | 0.1907 ns |  16.636 ns | 2.52x faster | 
| NetFabric_Long   | Vector512 | Long       | 100   |  12.595 ns | 0.1187 ns |  12.596 ns | 3.31x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **5**     |   **4.618 ns** | **0.0675 ns** |   **4.608 ns** |     **baseline** | 
| System_Short     | Scalar    | Short      | 5     |   5.230 ns | 0.0400 ns |   5.237 ns | 1.13x slower | 
| NetFabric_Short  | Scalar    | Short      | 5     |   6.131 ns | 0.0476 ns |   6.155 ns | 1.33x slower | 
| Baseline_Short   | Vector128 | Short      | 5     |   3.777 ns | 0.0823 ns |   3.774 ns | 1.22x faster | 
| System_Short     | Vector128 | Short      | 5     |   3.975 ns | 0.0574 ns |   3.987 ns | 1.16x faster | 
| NetFabric_Short  | Vector128 | Short      | 5     |   6.233 ns | 0.0610 ns |   6.254 ns | 1.35x slower | 
| Baseline_Short   | Vector256 | Short      | 5     |   3.851 ns | 0.0453 ns |   3.843 ns | 1.20x faster | 
| System_Short     | Vector256 | Short      | 5     |   3.995 ns | 0.0551 ns |   4.005 ns | 1.16x faster | 
| NetFabric_Short  | Vector256 | Short      | 5     |   5.655 ns | 0.0514 ns |   5.676 ns | 1.22x slower | 
| Baseline_Short   | Vector512 | Short      | 5     |   3.730 ns | 0.0389 ns |   3.725 ns | 1.24x faster | 
| System_Short     | Vector512 | Short      | 5     |   4.020 ns | 0.0501 ns |   4.022 ns | 1.15x faster | 
| NetFabric_Short  | Vector512 | Short      | 5     |   5.701 ns | 0.0400 ns |   5.701 ns | 1.23x slower | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **100**   |  **47.066 ns** | **0.4758 ns** |  **47.152 ns** |     **baseline** | 
| System_Short     | Scalar    | Short      | 100   |  40.629 ns | 0.4205 ns |  40.667 ns | 1.16x faster | 
| NetFabric_Short  | Scalar    | Short      | 100   |  34.388 ns | 0.1970 ns |  34.294 ns | 1.37x faster | 
| Baseline_Short   | Vector128 | Short      | 100   |  46.724 ns | 0.4928 ns |  46.883 ns | 1.01x faster | 
| System_Short     | Vector128 | Short      | 100   |  40.148 ns | 0.4713 ns |  40.230 ns | 1.17x faster | 
| NetFabric_Short  | Vector128 | Short      | 100   |   9.377 ns | 0.0908 ns |   9.424 ns | 5.02x faster | 
| Baseline_Short   | Vector256 | Short      | 100   |  46.926 ns | 0.6054 ns |  47.043 ns | 1.00x faster | 
| System_Short     | Vector256 | Short      | 100   |  39.606 ns | 0.3969 ns |  39.461 ns | 1.19x faster | 
| NetFabric_Short  | Vector256 | Short      | 100   |   7.459 ns | 0.0856 ns |   7.431 ns | 6.31x faster | 
| Baseline_Short   | Vector512 | Short      | 100   |  46.767 ns | 0.4819 ns |  46.875 ns | 1.01x faster | 
| System_Short     | Vector512 | Short      | 100   |  39.276 ns | 0.5019 ns |  39.198 ns | 1.20x faster | 
| NetFabric_Short  | Vector512 | Short      | 100   |   8.117 ns | 0.1219 ns |   8.150 ns | 5.80x faster | 

### Min

Applying a vectorizable binary operator with propagation of `NaN`.

| Method           | Job       | Categories | Count | Mean       | StdDev    | Ratio        | 
|----------------- |---------- |----------- |------ |-----------:|----------:|-------------:|-
| **Baseline_Double**  | **Scalar**    | **Double**     | **5**     |   **5.610 ns** | **0.0237 ns** |     **baseline** | 
| System_Double    | Scalar    | Double     | 5     |   5.414 ns | 0.0552 ns | 1.04x faster | 
| NetFabric_Double | Scalar    | Double     | 5     |   5.642 ns | 0.0124 ns | 1.01x slower | 
| Baseline_Double  | Vector128 | Double     | 5     |   5.647 ns | 0.1093 ns | 1.01x slower | 
| System_Double    | Vector128 | Double     | 5     |   6.626 ns | 0.0310 ns | 1.18x slower | 
| NetFabric_Double | Vector128 | Double     | 5     |   5.608 ns | 0.0338 ns | 1.00x faster | 
| Baseline_Double  | Vector256 | Double     | 5     |   5.731 ns | 0.1198 ns | 1.02x slower | 
| System_Double    | Vector256 | Double     | 5     |   6.072 ns | 0.1152 ns | 1.08x slower | 
| NetFabric_Double | Vector256 | Double     | 5     |   5.058 ns | 0.0672 ns | 1.11x faster | 
| Baseline_Double  | Vector512 | Double     | 5     |   3.997 ns | 0.0638 ns | 1.40x faster | 
| System_Double    | Vector512 | Double     | 5     |   4.370 ns | 0.0611 ns | 1.28x faster | 
| NetFabric_Double | Vector512 | Double     | 5     |   4.784 ns | 0.0490 ns | 1.17x faster | 
|                  |           |            |       |            |           |              | 
| **Baseline_Double**  | **Scalar**    | **Double**     | **100**   |  **70.542 ns** | **0.7986 ns** |     **baseline** | 
| System_Double    | Scalar    | Double     | 100   |  61.936 ns | 1.0089 ns | 1.14x faster | 
| NetFabric_Double | Scalar    | Double     | 100   |  75.267 ns | 0.8168 ns | 1.07x slower | 
| Baseline_Double  | Vector128 | Double     | 100   |  86.448 ns | 1.6840 ns | 1.23x slower | 
| System_Double    | Vector128 | Double     | 100   |  30.763 ns | 0.4378 ns | 2.29x faster | 
| NetFabric_Double | Vector128 | Double     | 100   |  35.470 ns | 0.5647 ns | 1.99x faster | 
| Baseline_Double  | Vector256 | Double     | 100   |  73.714 ns | 0.8682 ns | 1.05x slower | 
| System_Double    | Vector256 | Double     | 100   |  17.687 ns | 0.0780 ns | 3.98x faster | 
| NetFabric_Double | Vector256 | Double     | 100   |  19.301 ns | 0.0581 ns | 3.65x faster | 
| Baseline_Double  | Vector512 | Double     | 100   |  45.976 ns | 0.3133 ns | 1.53x faster | 
| System_Double    | Vector512 | Double     | 100   |  23.029 ns | 0.1408 ns | 3.06x faster | 
| NetFabric_Double | Vector512 | Double     | 100   |  19.420 ns | 0.1483 ns | 3.63x faster | 
|                  |           |            |       |            |           |              | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **5**     |   **5.864 ns** | **0.0091 ns** |     **baseline** | 
| System_Float     | Scalar    | Float      | 5     |   5.297 ns | 0.0335 ns | 1.11x faster | 
| NetFabric_Float  | Scalar    | Float      | 5     |   5.694 ns | 0.0444 ns | 1.03x faster | 
| Baseline_Float   | Vector128 | Float      | 5     |   6.072 ns | 0.0876 ns | 1.04x slower | 
| System_Float     | Vector128 | Float      | 5     |   6.182 ns | 0.0097 ns | 1.05x slower | 
| NetFabric_Float  | Vector128 | Float      | 5     |   4.559 ns | 0.0386 ns | 1.29x faster | 
| Baseline_Float   | Vector256 | Float      | 5     |   6.018 ns | 0.0874 ns | 1.02x slower | 
| System_Float     | Vector256 | Float      | 5     |   4.263 ns | 0.0150 ns | 1.38x faster | 
| NetFabric_Float  | Vector256 | Float      | 5     |   6.024 ns | 0.0415 ns | 1.03x slower | 
| Baseline_Float   | Vector512 | Float      | 5     |   3.946 ns | 0.0604 ns | 1.49x faster | 
| System_Float     | Vector512 | Float      | 5     |   4.263 ns | 0.0144 ns | 1.38x faster | 
| NetFabric_Float  | Vector512 | Float      | 5     |   5.110 ns | 0.0203 ns | 1.15x faster | 
|                  |           |            |       |            |           |              | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **100**   |  **69.403 ns** | **0.3472 ns** |     **baseline** | 
| System_Float     | Scalar    | Float      | 100   |  61.350 ns | 0.3633 ns | 1.13x faster | 
| NetFabric_Float  | Scalar    | Float      | 100   |  72.844 ns | 0.4696 ns | 1.05x slower | 
| Baseline_Float   | Vector128 | Float      | 100   |  90.810 ns | 1.1215 ns | 1.31x slower | 
| System_Float     | Vector128 | Float      | 100   |  17.687 ns | 0.0659 ns | 3.92x faster | 
| NetFabric_Float  | Vector128 | Float      | 100   |  19.101 ns | 0.1123 ns | 3.63x faster | 
| Baseline_Float   | Vector256 | Float      | 100   |  90.562 ns | 0.7776 ns | 1.30x slower | 
| System_Float     | Vector256 | Float      | 100   |  11.461 ns | 0.0141 ns | 6.06x faster | 
| NetFabric_Float  | Vector256 | Float      | 100   |  12.729 ns | 0.1028 ns | 5.45x faster | 
| Baseline_Float   | Vector512 | Float      | 100   |  45.722 ns | 0.1923 ns | 1.52x faster | 
| System_Float     | Vector512 | Float      | 100   |  12.143 ns | 0.0460 ns | 5.72x faster | 
| NetFabric_Float  | Vector512 | Float      | 100   |  12.098 ns | 0.0367 ns | 5.74x faster | 
|                  |           |            |       |            |           |              | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **5**     |  **40.592 ns** | **0.1795 ns** |     **baseline** | 
| System_Half      | Scalar    | Half       | 5     |  39.978 ns | 0.1001 ns | 1.02x faster | 
| NetFabric_Half   | Scalar    | Half       | 5     |  41.933 ns | 0.1676 ns | 1.03x slower | 
| Baseline_Half    | Vector128 | Half       | 5     |  32.984 ns | 0.1084 ns | 1.23x faster | 
| System_Half      | Vector128 | Half       | 5     |  32.678 ns | 0.1246 ns | 1.24x faster | 
| NetFabric_Half   | Vector128 | Half       | 5     |  33.129 ns | 0.0727 ns | 1.23x faster | 
| Baseline_Half    | Vector256 | Half       | 5     |  32.977 ns | 0.0667 ns | 1.23x faster | 
| System_Half      | Vector256 | Half       | 5     |  33.409 ns | 0.3744 ns | 1.22x faster | 
| NetFabric_Half   | Vector256 | Half       | 5     |  33.280 ns | 0.0988 ns | 1.22x faster | 
| Baseline_Half    | Vector512 | Half       | 5     |  34.114 ns | 0.2821 ns | 1.19x faster | 
| System_Half      | Vector512 | Half       | 5     |  34.614 ns | 0.0755 ns | 1.17x faster | 
| NetFabric_Half   | Vector512 | Half       | 5     |  35.103 ns | 0.1036 ns | 1.16x faster | 
|                  |           |            |       |            |           |              | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **100**   | **791.142 ns** | **3.5212 ns** |     **baseline** | 
| System_Half      | Scalar    | Half       | 100   | 785.696 ns | 3.2846 ns | 1.01x faster | 
| NetFabric_Half   | Scalar    | Half       | 100   | 794.225 ns | 4.5387 ns | 1.00x slower | 
| Baseline_Half    | Vector128 | Half       | 100   | 674.985 ns | 2.3091 ns | 1.17x faster | 
| System_Half      | Vector128 | Half       | 100   | 684.489 ns | 7.3033 ns | 1.16x faster | 
| NetFabric_Half   | Vector128 | Half       | 100   | 695.407 ns | 3.9303 ns | 1.14x faster | 
| Baseline_Half    | Vector256 | Half       | 100   | 675.691 ns | 3.8596 ns | 1.17x faster | 
| System_Half      | Vector256 | Half       | 100   | 682.424 ns | 4.3481 ns | 1.16x faster | 
| NetFabric_Half   | Vector256 | Half       | 100   | 694.971 ns | 4.3757 ns | 1.14x faster | 
| Baseline_Half    | Vector512 | Half       | 100   | 773.664 ns | 3.8622 ns | 1.02x faster | 
| System_Half      | Vector512 | Half       | 100   | 771.173 ns | 3.9787 ns | 1.03x faster | 
| NetFabric_Half   | Vector512 | Half       | 100   | 772.787 ns | 3.6129 ns | 1.02x faster | 
|                  |           |            |       |            |           |              | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **5**     |   **5.351 ns** | **0.0257 ns** |     **baseline** | 
| System_Int       | Scalar    | Int        | 5     |   5.490 ns | 0.0179 ns | 1.03x slower | 
| NetFabric_Int    | Scalar    | Int        | 5     |   5.391 ns | 0.0402 ns | 1.01x slower | 
| Baseline_Int     | Vector128 | Int        | 5     |   4.600 ns | 0.0147 ns | 1.16x faster | 
| System_Int       | Vector128 | Int        | 5     |   5.583 ns | 0.0377 ns | 1.04x slower | 
| NetFabric_Int    | Vector128 | Int        | 5     |   4.319 ns | 0.0292 ns | 1.24x faster | 
| Baseline_Int     | Vector256 | Int        | 5     |   4.615 ns | 0.0359 ns | 1.16x faster | 
| System_Int       | Vector256 | Int        | 5     |   4.638 ns | 0.0283 ns | 1.15x faster | 
| NetFabric_Int    | Vector256 | Int        | 5     |   6.134 ns | 0.0615 ns | 1.15x slower | 
| Baseline_Int     | Vector512 | Int        | 5     |   4.687 ns | 0.0447 ns | 1.14x faster | 
| System_Int       | Vector512 | Int        | 5     |   3.897 ns | 0.0525 ns | 1.37x faster | 
| NetFabric_Int    | Vector512 | Int        | 5     |   4.883 ns | 0.0461 ns | 1.10x faster | 
|                  |           |            |       |            |           |              | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **100**   |  **46.668 ns** | **0.5979 ns** |     **baseline** | 
| System_Int       | Scalar    | Int        | 100   |  31.561 ns | 0.3382 ns | 1.48x faster | 
| NetFabric_Int    | Scalar    | Int        | 100   |  24.497 ns | 0.1947 ns | 1.91x faster | 
| Baseline_Int     | Vector128 | Int        | 100   |  46.183 ns | 0.4463 ns | 1.01x faster | 
| System_Int       | Vector128 | Int        | 100   |  13.189 ns | 0.0684 ns | 3.54x faster | 
| NetFabric_Int    | Vector128 | Int        | 100   |  10.777 ns | 0.2038 ns | 4.33x faster | 
| Baseline_Int     | Vector256 | Int        | 100   |  47.262 ns | 0.6239 ns | 1.01x slower | 
| System_Int       | Vector256 | Int        | 100   |  11.428 ns | 0.1558 ns | 4.08x faster | 
| NetFabric_Int    | Vector256 | Int        | 100   |   8.264 ns | 0.1034 ns | 5.65x faster | 
| Baseline_Int     | Vector512 | Int        | 100   |  47.077 ns | 0.5213 ns | 1.01x slower | 
| System_Int       | Vector512 | Int        | 100   |  11.729 ns | 0.1309 ns | 3.98x faster | 
| NetFabric_Int    | Vector512 | Int        | 100   |   8.435 ns | 0.1087 ns | 5.53x faster | 
|                  |           |            |       |            |           |              | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **5**     |   **4.669 ns** | **0.0670 ns** |     **baseline** | 
| System_Long      | Scalar    | Long       | 5     |   5.530 ns | 0.0672 ns | 1.18x slower | 
| NetFabric_Long   | Scalar    | Long       | 5     |   5.461 ns | 0.0610 ns | 1.17x slower | 
| Baseline_Long    | Vector128 | Long       | 5     |   4.624 ns | 0.0802 ns | 1.01x faster | 
| System_Long      | Vector128 | Long       | 5     |   4.919 ns | 0.0650 ns | 1.05x slower | 
| NetFabric_Long   | Vector128 | Long       | 5     |   4.612 ns | 0.0813 ns | 1.01x faster | 
| Baseline_Long    | Vector256 | Long       | 5     |   4.581 ns | 0.0771 ns | 1.02x faster | 
| System_Long      | Vector256 | Long       | 5     |   4.901 ns | 0.0568 ns | 1.05x slower | 
| NetFabric_Long   | Vector256 | Long       | 5     |   4.318 ns | 0.0528 ns | 1.08x faster | 
| Baseline_Long    | Vector512 | Long       | 5     |   4.615 ns | 0.0506 ns | 1.01x faster | 
| System_Long      | Vector512 | Long       | 5     |   3.559 ns | 0.0584 ns | 1.31x faster | 
| NetFabric_Long   | Vector512 | Long       | 5     |   4.607 ns | 0.0725 ns | 1.01x faster | 
|                  |           |            |       |            |           |              | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **100**   |  **46.732 ns** | **0.5266 ns** |     **baseline** | 
| System_Long      | Scalar    | Long       | 100   |  31.294 ns | 0.4396 ns | 1.49x faster | 
| NetFabric_Long   | Scalar    | Long       | 100   |  24.909 ns | 0.2328 ns | 1.88x faster | 
| Baseline_Long    | Vector128 | Long       | 100   |  67.376 ns | 1.5155 ns | 1.44x slower | 
| System_Long      | Vector128 | Long       | 100   |  18.987 ns | 0.1919 ns | 2.46x faster | 
| NetFabric_Long   | Vector128 | Long       | 100   |  22.425 ns | 0.3134 ns | 2.08x faster | 
| Baseline_Long    | Vector256 | Long       | 100   |  67.452 ns | 1.6402 ns | 1.43x slower | 
| System_Long      | Vector256 | Long       | 100   |  16.020 ns | 0.1823 ns | 2.92x faster | 
| NetFabric_Long   | Vector256 | Long       | 100   |  12.584 ns | 0.1483 ns | 3.71x faster | 
| Baseline_Long    | Vector512 | Long       | 100   |  67.389 ns | 1.2560 ns | 1.44x slower | 
| System_Long      | Vector512 | Long       | 100   |  17.008 ns | 0.2148 ns | 2.75x faster | 
| NetFabric_Long   | Vector512 | Long       | 100   |  12.425 ns | 0.1707 ns | 3.76x faster | 
|                  |           |            |       |            |           |              | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **5**     |   **4.907 ns** | **0.0806 ns** |     **baseline** | 
| System_Short     | Scalar    | Short      | 5     |   5.629 ns | 0.0554 ns | 1.15x slower | 
| NetFabric_Short  | Scalar    | Short      | 5     |   5.322 ns | 0.0635 ns | 1.08x slower | 
| Baseline_Short   | Vector128 | Short      | 5     |   3.936 ns | 0.0477 ns | 1.25x faster | 
| System_Short     | Vector128 | Short      | 5     |   4.301 ns | 0.0569 ns | 1.14x faster | 
| NetFabric_Short  | Vector128 | Short      | 5     |   5.479 ns | 0.0415 ns | 1.12x slower | 
| Baseline_Short   | Vector256 | Short      | 5     |   3.929 ns | 0.0705 ns | 1.25x faster | 
| System_Short     | Vector256 | Short      | 5     |   5.280 ns | 0.1012 ns | 1.07x slower | 
| NetFabric_Short  | Vector256 | Short      | 5     |   5.471 ns | 0.0443 ns | 1.11x slower | 
| Baseline_Short   | Vector512 | Short      | 5     |   3.946 ns | 0.0599 ns | 1.24x faster | 
| System_Short     | Vector512 | Short      | 5     |   4.279 ns | 0.0929 ns | 1.15x faster | 
| NetFabric_Short  | Vector512 | Short      | 5     |   5.447 ns | 0.0541 ns | 1.11x slower | 
|                  |           |            |       |            |           |              | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **100**   |  **48.046 ns** | **0.7258 ns** |     **baseline** | 
| System_Short     | Scalar    | Short      | 100   |  41.307 ns | 0.3393 ns | 1.16x faster | 
| NetFabric_Short  | Scalar    | Short      | 100   |  35.951 ns | 0.6949 ns | 1.34x faster | 
| Baseline_Short   | Vector128 | Short      | 100   |  53.677 ns | 0.3867 ns | 1.12x slower | 
| System_Short     | Vector128 | Short      | 100   |  46.767 ns | 0.5974 ns | 1.03x faster | 
| NetFabric_Short  | Vector128 | Short      | 100   |   9.502 ns | 0.0884 ns | 5.06x faster | 
| Baseline_Short   | Vector256 | Short      | 100   |  53.337 ns | 0.5609 ns | 1.11x slower | 
| System_Short     | Vector256 | Short      | 100   |  57.113 ns | 0.5817 ns | 1.19x slower | 
| NetFabric_Short  | Vector256 | Short      | 100   |   7.281 ns | 0.1152 ns | 6.60x faster | 
| Baseline_Short   | Vector512 | Short      | 100   |  53.486 ns | 0.3726 ns | 1.11x slower | 
| System_Short     | Vector512 | Short      | 100   |  46.895 ns | 0.3564 ns | 1.02x faster | 
| NetFabric_Short  | Vector512 | Short      | 100   |   8.173 ns | 0.0908 ns | 5.88x faster | 

### Add Value

Applying a vectorizable binary operator on a span and a fixed scalar value.

| Method           | Job       | Categories | Count | Mean       | StdDev    | Median     | Ratio        | 
|----------------- |---------- |----------- |------ |-----------:|----------:|-----------:|-------------:|-
| **Baseline_Double**  | **Scalar**    | **Double**     | **5**     |   **1.424 ns** | **0.0230 ns** |   **1.420 ns** |     **baseline** | 
| System_Double    | Scalar    | Double     | 5     |   2.929 ns | 0.0392 ns |   2.917 ns | 2.06x slower | 
| NetFabric_Double | Scalar    | Double     | 5     |   3.535 ns | 0.0496 ns |   3.533 ns | 2.48x slower | 
| Baseline_Double  | Vector128 | Double     | 5     |   1.836 ns | 0.0277 ns |   1.834 ns | 1.29x slower | 
| System_Double    | Vector128 | Double     | 5     |   3.889 ns | 0.0507 ns |   3.882 ns | 2.73x slower | 
| NetFabric_Double | Vector128 | Double     | 5     |   3.000 ns | 0.0595 ns |   2.990 ns | 2.11x slower | 
| Baseline_Double  | Vector256 | Double     | 5     |   1.338 ns | 0.0320 ns |   1.320 ns | 1.06x faster | 
| System_Double    | Vector256 | Double     | 5     |   3.558 ns | 0.0631 ns |   3.558 ns | 2.50x slower | 
| NetFabric_Double | Vector256 | Double     | 5     |   2.730 ns | 0.0501 ns |   2.708 ns | 1.92x slower | 
| Baseline_Double  | Vector512 | Double     | 5     |   1.820 ns | 0.0411 ns |   1.830 ns | 1.28x slower | 
| System_Double    | Vector512 | Double     | 5     |   2.678 ns | 0.0389 ns |   2.671 ns | 1.88x slower | 
| NetFabric_Double | Vector512 | Double     | 5     |   3.177 ns | 0.0399 ns |   3.160 ns | 2.23x slower | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Double**  | **Scalar**    | **Double**     | **100**   |  **27.612 ns** | **0.3078 ns** |  **27.634 ns** |     **baseline** | 
| System_Double    | Scalar    | Double     | 100   |  26.640 ns | 0.2570 ns |  26.591 ns | 1.04x faster | 
| NetFabric_Double | Scalar    | Double     | 100   |  19.391 ns | 0.1315 ns |  19.377 ns | 1.42x faster | 
| Baseline_Double  | Vector128 | Double     | 100   |  25.745 ns | 0.1708 ns |  25.739 ns | 1.07x faster | 
| System_Double    | Vector128 | Double     | 100   |  12.792 ns | 0.1027 ns |  12.820 ns | 2.16x faster | 
| NetFabric_Double | Vector128 | Double     | 100   |  21.969 ns | 0.2488 ns |  21.958 ns | 1.26x faster | 
| Baseline_Double  | Vector256 | Double     | 100   |  40.819 ns | 2.4507 ns |  39.717 ns | 1.48x slower | 
| System_Double    | Vector256 | Double     | 100   |  10.585 ns | 0.0747 ns |  10.581 ns | 2.61x faster | 
| NetFabric_Double | Vector256 | Double     | 100   |   7.961 ns | 0.0846 ns |   7.970 ns | 3.47x faster | 
| Baseline_Double  | Vector512 | Double     | 100   |  25.870 ns | 0.2636 ns |  25.951 ns | 1.07x faster | 
| System_Double    | Vector512 | Double     | 100   |   9.873 ns | 0.1006 ns |   9.861 ns | 2.79x faster | 
| NetFabric_Double | Vector512 | Double     | 100   |   7.857 ns | 0.1088 ns |   7.868 ns | 3.52x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **5**     |   **1.387 ns** | **0.0233 ns** |   **1.389 ns** |     **baseline** | 
| System_Float     | Scalar    | Float      | 5     |   2.960 ns | 0.0427 ns |   2.964 ns | 2.13x slower | 
| NetFabric_Float  | Scalar    | Float      | 5     |   3.463 ns | 0.0417 ns |   3.462 ns | 2.50x slower | 
| Baseline_Float   | Vector128 | Float      | 5     |   1.810 ns | 0.0254 ns |   1.815 ns | 1.31x slower | 
| System_Float     | Vector128 | Float      | 5     |   3.530 ns | 0.0628 ns |   3.500 ns | 2.55x slower | 
| NetFabric_Float  | Vector128 | Float      | 5     |   2.597 ns | 0.0205 ns |   2.606 ns | 1.87x slower | 
| Baseline_Float   | Vector256 | Float      | 5     |   1.355 ns | 0.0488 ns |   1.351 ns | 1.03x faster | 
| System_Float     | Vector256 | Float      | 5     |   2.603 ns | 0.0246 ns |   2.612 ns | 1.88x slower | 
| NetFabric_Float  | Vector256 | Float      | 5     |   3.291 ns | 0.0557 ns |   3.286 ns | 2.37x slower | 
| Baseline_Float   | Vector512 | Float      | 5     |   1.836 ns | 0.0390 ns |   1.824 ns | 1.32x slower | 
| System_Float     | Vector512 | Float      | 5     |   2.665 ns | 0.0399 ns |   2.666 ns | 1.92x slower | 
| NetFabric_Float  | Vector512 | Float      | 5     |   3.488 ns | 0.0548 ns |   3.472 ns | 2.52x slower | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **100**   |  **27.844 ns** | **0.3658 ns** |  **27.840 ns** |     **baseline** | 
| System_Float     | Scalar    | Float      | 100   |  27.171 ns | 0.3011 ns |  27.087 ns | 1.02x faster | 
| NetFabric_Float  | Scalar    | Float      | 100   |  19.552 ns | 0.1943 ns |  19.568 ns | 1.42x faster | 
| Baseline_Float   | Vector128 | Float      | 100   |  41.433 ns | 2.4706 ns |  41.204 ns | 1.48x slower | 
| System_Float     | Vector128 | Float      | 100   |   8.083 ns | 0.0979 ns |   8.119 ns | 3.45x faster | 
| NetFabric_Float  | Vector128 | Float      | 100   |  11.299 ns | 0.2448 ns |  11.252 ns | 2.46x faster | 
| Baseline_Float   | Vector256 | Float      | 100   |  25.845 ns | 0.2376 ns |  25.921 ns | 1.08x faster | 
| System_Float     | Vector256 | Float      | 100   |   8.416 ns | 0.0698 ns |   8.386 ns | 3.31x faster | 
| NetFabric_Float  | Vector256 | Float      | 100   |   6.279 ns | 0.0842 ns |   6.256 ns | 4.44x faster | 
| Baseline_Float   | Vector512 | Float      | 100   |  42.117 ns | 2.1837 ns |  43.345 ns | 1.53x slower | 
| System_Float     | Vector512 | Float      | 100   |   7.517 ns | 0.0790 ns |   7.503 ns | 3.71x faster | 
| NetFabric_Float  | Vector512 | Float      | 100   |   7.664 ns | 0.1011 ns |   7.667 ns | 3.63x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **5**     |  **43.723 ns** | **0.4775 ns** |  **43.403 ns** |     **baseline** | 
| System_Half      | Scalar    | Half       | 5     |  44.786 ns | 0.5093 ns |  44.794 ns | 1.02x slower | 
| NetFabric_Half   | Scalar    | Half       | 5     |  44.716 ns | 0.5228 ns |  44.679 ns | 1.02x slower | 
| Baseline_Half    | Vector128 | Half       | 5     |  35.553 ns | 0.3404 ns |  35.565 ns | 1.23x faster | 
| System_Half      | Vector128 | Half       | 5     |  36.536 ns | 0.4028 ns |  36.553 ns | 1.20x faster | 
| NetFabric_Half   | Vector128 | Half       | 5     |  36.450 ns | 0.3577 ns |  36.341 ns | 1.20x faster | 
| Baseline_Half    | Vector256 | Half       | 5     |  35.297 ns | 0.3580 ns |  35.302 ns | 1.24x faster | 
| System_Half      | Vector256 | Half       | 5     |  36.275 ns | 0.4319 ns |  36.071 ns | 1.21x faster | 
| NetFabric_Half   | Vector256 | Half       | 5     |  37.133 ns | 0.4001 ns |  37.281 ns | 1.18x faster | 
| Baseline_Half    | Vector512 | Half       | 5     |  36.961 ns | 0.4245 ns |  36.994 ns | 1.18x faster | 
| System_Half      | Vector512 | Half       | 5     |  36.109 ns | 0.3829 ns |  36.018 ns | 1.21x faster | 
| NetFabric_Half   | Vector512 | Half       | 5     |  36.689 ns | 0.3749 ns |  36.817 ns | 1.19x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **100**   | **883.249 ns** | **4.1610 ns** | **884.625 ns** |     **baseline** | 
| System_Half      | Scalar    | Half       | 100   | 880.913 ns | 7.0118 ns | 881.630 ns | 1.00x faster | 
| NetFabric_Half   | Scalar    | Half       | 100   | 903.758 ns | 4.8716 ns | 905.558 ns | 1.02x slower | 
| Baseline_Half    | Vector128 | Half       | 100   | 797.542 ns | 7.1680 ns | 799.315 ns | 1.11x faster | 
| System_Half      | Vector128 | Half       | 100   | 780.217 ns | 6.4614 ns | 782.250 ns | 1.13x faster | 
| NetFabric_Half   | Vector128 | Half       | 100   | 792.732 ns | 6.1311 ns | 795.781 ns | 1.11x faster | 
| Baseline_Half    | Vector256 | Half       | 100   | 802.835 ns | 7.2582 ns | 802.788 ns | 1.10x faster | 
| System_Half      | Vector256 | Half       | 100   | 785.828 ns | 4.2824 ns | 786.906 ns | 1.12x faster | 
| NetFabric_Half   | Vector256 | Half       | 100   | 794.689 ns | 4.8650 ns | 796.682 ns | 1.11x faster | 
| Baseline_Half    | Vector512 | Half       | 100   | 798.745 ns | 5.5638 ns | 799.541 ns | 1.10x faster | 
| System_Half      | Vector512 | Half       | 100   | 780.524 ns | 6.7716 ns | 783.679 ns | 1.13x faster | 
| NetFabric_Half   | Vector512 | Half       | 100   | 793.789 ns | 6.3665 ns | 795.762 ns | 1.11x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **5**     |   **1.647 ns** | **0.0220 ns** |   **1.644 ns** |     **baseline** | 
| System_Int       | Scalar    | Int        | 5     |   3.462 ns | 0.0518 ns |   3.464 ns | 2.10x slower | 
| NetFabric_Int    | Scalar    | Int        | 5     |   3.737 ns | 0.0499 ns |   3.733 ns | 2.27x slower | 
| Baseline_Int     | Vector128 | Int        | 5     |   1.573 ns | 0.0288 ns |   1.561 ns | 1.05x faster | 
| System_Int       | Vector128 | Int        | 5     |   4.165 ns | 0.0511 ns |   4.150 ns | 2.53x slower | 
| NetFabric_Int    | Vector128 | Int        | 5     |   3.637 ns | 0.0364 ns |   3.638 ns | 2.21x slower | 
| Baseline_Int     | Vector256 | Int        | 5     |   1.575 ns | 0.0297 ns |   1.564 ns | 1.05x faster | 
| System_Int       | Vector256 | Int        | 5     |   2.756 ns | 0.0448 ns |   2.746 ns | 1.67x slower | 
| NetFabric_Int    | Vector256 | Int        | 5     |   3.793 ns | 0.0599 ns |   3.807 ns | 2.30x slower | 
| Baseline_Int     | Vector512 | Int        | 5     |   1.573 ns | 0.0227 ns |   1.564 ns | 1.05x faster | 
| System_Int       | Vector512 | Int        | 5     |   2.693 ns | 0.0494 ns |   2.706 ns | 1.64x slower | 
| NetFabric_Int    | Vector512 | Int        | 5     |   3.763 ns | 0.0497 ns |   3.749 ns | 2.29x slower | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **100**   |  **39.761 ns** | **0.5372 ns** |  **39.661 ns** |     **baseline** | 
| System_Int       | Scalar    | Int        | 100   |  32.120 ns | 0.4257 ns |  32.136 ns | 1.24x faster | 
| NetFabric_Int    | Scalar    | Int        | 100   |  22.922 ns | 0.2160 ns |  22.941 ns | 1.73x faster | 
| Baseline_Int     | Vector128 | Int        | 100   |  31.187 ns | 0.4093 ns |  31.104 ns | 1.28x faster | 
| System_Int       | Vector128 | Int        | 100   |   9.864 ns | 0.0992 ns |   9.830 ns | 4.03x faster | 
| NetFabric_Int    | Vector128 | Int        | 100   |   8.496 ns | 0.1199 ns |   8.439 ns | 4.68x faster | 
| Baseline_Int     | Vector256 | Int        | 100   |  30.712 ns | 0.3255 ns |  30.701 ns | 1.29x faster | 
| System_Int       | Vector256 | Int        | 100   |   7.483 ns | 0.1095 ns |   7.502 ns | 5.31x faster | 
| NetFabric_Int    | Vector256 | Int        | 100   |   6.289 ns | 0.0527 ns |   6.293 ns | 6.32x faster | 
| Baseline_Int     | Vector512 | Int        | 100   |  30.872 ns | 0.3659 ns |  30.814 ns | 1.29x faster | 
| System_Int       | Vector512 | Int        | 100   |  10.835 ns | 0.1100 ns |  10.842 ns | 3.67x faster | 
| NetFabric_Int    | Vector512 | Int        | 100   |   9.636 ns | 0.0611 ns |   9.634 ns | 4.13x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **5**     |   **1.598 ns** | **0.0337 ns** |   **1.591 ns** |     **baseline** | 
| System_Long      | Scalar    | Long       | 5     |   3.596 ns | 0.0542 ns |   3.609 ns | 2.25x slower | 
| NetFabric_Long   | Scalar    | Long       | 5     |   3.771 ns | 0.0360 ns |   3.762 ns | 2.36x slower | 
| Baseline_Long    | Vector128 | Long       | 5     |   1.546 ns | 0.0216 ns |   1.541 ns | 1.03x faster | 
| System_Long      | Vector128 | Long       | 5     |   3.779 ns | 0.0346 ns |   3.785 ns | 2.37x slower | 
| NetFabric_Long   | Vector128 | Long       | 5     |   3.240 ns | 0.0588 ns |   3.231 ns | 2.03x slower | 
| Baseline_Long    | Vector256 | Long       | 5     |   1.561 ns | 0.0341 ns |   1.563 ns | 1.02x faster | 
| System_Long      | Vector256 | Long       | 5     |   3.879 ns | 0.0602 ns |   3.871 ns | 2.43x slower | 
| NetFabric_Long   | Vector256 | Long       | 5     |   3.019 ns | 0.0594 ns |   3.036 ns | 1.89x slower | 
| Baseline_Long    | Vector512 | Long       | 5     |   1.564 ns | 0.0331 ns |   1.572 ns | 1.02x faster | 
| System_Long      | Vector512 | Long       | 5     |   2.702 ns | 0.0387 ns |   2.713 ns | 1.69x slower | 
| NetFabric_Long   | Vector512 | Long       | 5     |   3.259 ns | 0.0551 ns |   3.252 ns | 2.04x slower | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **100**   |  **40.096 ns** | **0.7299 ns** |  **40.306 ns** |     **baseline** | 
| System_Long      | Scalar    | Long       | 100   |  32.536 ns | 0.3968 ns |  32.582 ns | 1.23x faster | 
| NetFabric_Long   | Scalar    | Long       | 100   |  22.680 ns | 0.2073 ns |  22.708 ns | 1.77x faster | 
| Baseline_Long    | Vector128 | Long       | 100   |  30.933 ns | 0.6490 ns |  30.859 ns | 1.29x faster | 
| System_Long      | Vector128 | Long       | 100   |  11.615 ns | 0.1096 ns |  11.640 ns | 3.45x faster | 
| NetFabric_Long   | Vector128 | Long       | 100   |  13.145 ns | 0.1226 ns |  13.141 ns | 3.05x faster | 
| Baseline_Long    | Vector256 | Long       | 100   |  31.063 ns | 0.5024 ns |  30.925 ns | 1.29x faster | 
| System_Long      | Vector256 | Long       | 100   |  10.136 ns | 0.1508 ns |  10.087 ns | 3.96x faster | 
| NetFabric_Long   | Vector256 | Long       | 100   |   9.303 ns | 0.1036 ns |   9.303 ns | 4.31x faster | 
| Baseline_Long    | Vector512 | Long       | 100   |  31.151 ns | 0.5770 ns |  31.291 ns | 1.29x faster | 
| System_Long      | Vector512 | Long       | 100   |  11.954 ns | 0.1366 ns |  11.992 ns | 3.36x faster | 
| NetFabric_Long   | Vector512 | Long       | 100   |  11.395 ns | 0.1597 ns |  11.350 ns | 3.52x faster | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **5**     |   **1.849 ns** | **0.0366 ns** |   **1.844 ns** |     **baseline** | 
| System_Short     | Scalar    | Short      | 5     |   3.749 ns | 0.0553 ns |   3.766 ns | 2.03x slower | 
| NetFabric_Short  | Scalar    | Short      | 5     |   3.482 ns | 0.0565 ns |   3.488 ns | 1.88x slower | 
| Baseline_Short   | Vector128 | Short      | 5     |   1.832 ns | 0.0272 ns |   1.822 ns | 1.01x faster | 
| System_Short     | Vector128 | Short      | 5     |   3.305 ns | 0.0599 ns |   3.309 ns | 1.79x slower | 
| NetFabric_Short  | Vector128 | Short      | 5     |   3.686 ns | 0.0900 ns |   3.659 ns | 2.00x slower | 
| Baseline_Short   | Vector256 | Short      | 5     |   1.919 ns | 0.0190 ns |   1.923 ns | 1.04x slower | 
| System_Short     | Vector256 | Short      | 5     |   3.380 ns | 0.0518 ns |   3.387 ns | 1.83x slower | 
| NetFabric_Short  | Vector256 | Short      | 5     |   3.695 ns | 0.0575 ns |   3.689 ns | 2.00x slower | 
| Baseline_Short   | Vector512 | Short      | 5     |   1.854 ns | 0.0346 ns |   1.854 ns | 1.00x slower | 
| System_Short     | Vector512 | Short      | 5     |   3.211 ns | 0.0586 ns |   3.214 ns | 1.74x slower | 
| NetFabric_Short  | Vector512 | Short      | 5     |   3.677 ns | 0.0442 ns |   3.672 ns | 1.99x slower | 
|                  |           |            |       |            |           |            |              | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **100**   |  **41.027 ns** | **0.7182 ns** |  **40.916 ns** |     **baseline** | 
| System_Short     | Scalar    | Short      | 100   |  37.492 ns | 0.3784 ns |  37.417 ns | 1.09x faster | 
| NetFabric_Short  | Scalar    | Short      | 100   |  26.503 ns | 0.2259 ns |  26.463 ns | 1.55x faster | 
| Baseline_Short   | Vector128 | Short      | 100   |  34.751 ns | 0.4492 ns |  34.878 ns | 1.18x faster | 
| System_Short     | Vector128 | Short      | 100   |  36.836 ns | 0.5159 ns |  36.763 ns | 1.11x faster | 
| NetFabric_Short  | Vector128 | Short      | 100   |   6.285 ns | 0.1069 ns |   6.270 ns | 6.53x faster | 
| Baseline_Short   | Vector256 | Short      | 100   |  35.693 ns | 0.3887 ns |  35.763 ns | 1.15x faster | 
| System_Short     | Vector256 | Short      | 100   |  35.489 ns | 0.2693 ns |  35.460 ns | 1.16x faster | 
| NetFabric_Short  | Vector256 | Short      | 100   |   5.064 ns | 0.0461 ns |   5.083 ns | 8.10x faster | 
| Baseline_Short   | Vector512 | Short      | 100   |  34.974 ns | 0.5009 ns |  35.045 ns | 1.17x faster | 
| System_Short     | Vector512 | Short      | 100   |  36.764 ns | 0.4734 ns |  36.529 ns | 1.12x faster | 
| NetFabric_Short  | Vector512 | Short      | 100   |   5.039 ns | 0.0579 ns |   5.032 ns | 8.14x faster | 

### AddMultiply

Applying a vectorizable ternary operator on three spans.

| Method           | Job       | Categories | Count | Mean         | StdDev     | Ratio         | 
|----------------- |---------- |----------- |------ |-------------:|-----------:|--------------:|-
| **Baseline_Double**  | **Scalar**    | **Double**     | **5**     |     **5.994 ns** |  **0.0571 ns** |      **baseline** | 
| System_Double    | Scalar    | Double     | 5     |     5.944 ns |  0.0623 ns |  1.01x faster | 
| NetFabric_Double | Scalar    | Double     | 5     |     5.735 ns |  0.0447 ns |  1.04x faster | 
| Baseline_Double  | Vector128 | Double     | 5     |     5.363 ns |  0.0364 ns |  1.12x faster | 
| System_Double    | Vector128 | Double     | 5     |     5.918 ns |  0.0927 ns |  1.01x faster | 
| NetFabric_Double | Vector128 | Double     | 5     |     6.598 ns |  0.1909 ns |  1.11x slower | 
| Baseline_Double  | Vector256 | Double     | 5     |     5.423 ns |  0.0691 ns |  1.11x faster | 
| System_Double    | Vector256 | Double     | 5     |     5.746 ns |  0.0508 ns |  1.04x faster | 
| NetFabric_Double | Vector256 | Double     | 5     |     6.212 ns |  0.0509 ns |  1.04x slower | 
| Baseline_Double  | Vector512 | Double     | 5     |     5.397 ns |  0.0927 ns |  1.11x faster | 
| System_Double    | Vector512 | Double     | 5     |     5.040 ns |  0.0349 ns |  1.19x faster | 
| NetFabric_Double | Vector512 | Double     | 5     |     6.161 ns |  0.0684 ns |  1.03x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Double**  | **Scalar**    | **Double**     | **100**   |    **47.376 ns** |  **0.6180 ns** |      **baseline** | 
| System_Double    | Scalar    | Double     | 100   |    46.209 ns |  0.4904 ns |  1.02x faster | 
| NetFabric_Double | Scalar    | Double     | 100   |    34.279 ns |  0.4607 ns |  1.38x faster | 
| Baseline_Double  | Vector128 | Double     | 100   |    46.608 ns |  0.6250 ns |  1.02x faster | 
| System_Double    | Vector128 | Double     | 100   |    25.171 ns |  0.1976 ns |  1.88x faster | 
| NetFabric_Double | Vector128 | Double     | 100   |    23.455 ns |  0.2896 ns |  2.02x faster | 
| Baseline_Double  | Vector256 | Double     | 100   |    47.539 ns |  0.4338 ns |  1.00x slower | 
| System_Double    | Vector256 | Double     | 100   |    17.933 ns |  0.2070 ns |  2.64x faster | 
| NetFabric_Double | Vector256 | Double     | 100   |    15.673 ns |  0.1803 ns |  3.02x faster | 
| Baseline_Double  | Vector512 | Double     | 100   |    47.439 ns |  0.6320 ns |  1.00x slower | 
| System_Double    | Vector512 | Double     | 100   |    20.043 ns |  0.1699 ns |  2.36x faster | 
| NetFabric_Double | Vector512 | Double     | 100   |    15.337 ns |  0.1994 ns |  3.09x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **5**     |     **6.052 ns** |  **0.1316 ns** |      **baseline** | 
| Syatem_Float     | Scalar    | Float      | 5     |     5.877 ns |  0.0682 ns |  1.03x faster | 
| NetFabric_Float  | Scalar    | Float      | 5     |     5.538 ns |  0.0311 ns |  1.09x faster | 
| Baseline_Float   | Vector128 | Float      | 5     |     5.141 ns |  0.0689 ns |  1.18x faster | 
| Syatem_Float     | Vector128 | Float      | 5     |     6.074 ns |  0.0454 ns |  1.00x slower | 
| NetFabric_Float  | Vector128 | Float      | 5     |     6.089 ns |  0.0386 ns |  1.01x slower | 
| Baseline_Float   | Vector256 | Float      | 5     |     5.070 ns |  0.0535 ns |  1.19x faster | 
| Syatem_Float     | Vector256 | Float      | 5     |     4.787 ns |  0.0597 ns |  1.26x faster | 
| NetFabric_Float  | Vector256 | Float      | 5     |     6.198 ns |  0.0500 ns |  1.03x slower | 
| Baseline_Float   | Vector512 | Float      | 5     |     5.327 ns |  0.0425 ns |  1.13x faster | 
| Syatem_Float     | Vector512 | Float      | 5     |     4.746 ns |  0.0617 ns |  1.27x faster | 
| NetFabric_Float  | Vector512 | Float      | 5     |     6.203 ns |  0.0489 ns |  1.03x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **100**   |    **46.306 ns** |  **0.5490 ns** |      **baseline** | 
| Syatem_Float     | Scalar    | Float      | 100   |    36.081 ns |  0.4352 ns |  1.28x faster | 
| NetFabric_Float  | Scalar    | Float      | 100   |    33.434 ns |  0.4299 ns |  1.39x faster | 
| Baseline_Float   | Vector128 | Float      | 100   |    47.243 ns |  0.4788 ns |  1.02x slower | 
| Syatem_Float     | Vector128 | Float      | 100   |    17.863 ns |  0.1757 ns |  2.59x faster | 
| NetFabric_Float  | Vector128 | Float      | 100   |    14.851 ns |  0.1581 ns |  3.12x faster | 
| Baseline_Float   | Vector256 | Float      | 100   |    47.712 ns |  0.6222 ns |  1.03x slower | 
| Syatem_Float     | Vector256 | Float      | 100   |    15.487 ns |  0.1735 ns |  2.99x faster | 
| NetFabric_Float  | Vector256 | Float      | 100   |    11.735 ns |  0.1169 ns |  3.95x faster | 
| Baseline_Float   | Vector512 | Float      | 100   |    48.408 ns |  0.5439 ns |  1.05x slower | 
| Syatem_Float     | Vector512 | Float      | 100   |    13.300 ns |  0.0751 ns |  3.47x faster | 
| NetFabric_Float  | Vector512 | Float      | 100   |    11.210 ns |  0.0676 ns |  4.13x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **5**     |    **77.903 ns** |  **1.0615 ns** |      **baseline** | 
| System_Half      | Scalar    | Half       | 5     |   104.125 ns |  0.7931 ns |  1.34x slower | 
| NetFabric_Half   | Scalar    | Half       | 5     |   104.881 ns |  0.4717 ns |  1.35x slower | 
| Baseline_Half    | Vector128 | Half       | 5     |    68.190 ns |  0.9561 ns |  1.14x faster | 
| System_Half      | Vector128 | Half       | 5     |    96.030 ns |  0.6582 ns |  1.23x slower | 
| NetFabric_Half   | Vector128 | Half       | 5     |    95.680 ns |  0.8181 ns |  1.23x slower | 
| Baseline_Half    | Vector256 | Half       | 5     |    67.759 ns |  0.7219 ns |  1.15x faster | 
| System_Half      | Vector256 | Half       | 5     |    95.425 ns |  0.6339 ns |  1.23x slower | 
| NetFabric_Half   | Vector256 | Half       | 5     |    96.252 ns |  0.4215 ns |  1.24x slower | 
| Baseline_Half    | Vector512 | Half       | 5     |    67.683 ns |  0.6364 ns |  1.15x faster | 
| System_Half      | Vector512 | Half       | 5     |    95.746 ns |  0.6208 ns |  1.23x slower | 
| NetFabric_Half   | Vector512 | Half       | 5     |    96.159 ns |  0.4923 ns |  1.24x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **100**   | **1,456.534 ns** | **14.7659 ns** |      **baseline** | 
| System_Half      | Scalar    | Half       | 100   | 2,140.475 ns | 18.2185 ns |  1.47x slower | 
| NetFabric_Half   | Scalar    | Half       | 100   | 2,134.596 ns | 26.0693 ns |  1.47x slower | 
| Baseline_Half    | Vector128 | Half       | 100   | 1,305.400 ns | 15.6250 ns |  1.12x faster | 
| System_Half      | Vector128 | Half       | 100   | 2,019.821 ns |  3.4068 ns |  1.39x slower | 
| NetFabric_Half   | Vector128 | Half       | 100   | 2,047.616 ns | 23.4868 ns |  1.41x slower | 
| Baseline_Half    | Vector256 | Half       | 100   | 1,300.803 ns | 13.4474 ns |  1.12x faster | 
| System_Half      | Vector256 | Half       | 100   | 2,044.598 ns | 25.5245 ns |  1.40x slower | 
| NetFabric_Half   | Vector256 | Half       | 100   | 2,041.924 ns | 22.9503 ns |  1.40x slower | 
| Baseline_Half    | Vector512 | Half       | 100   | 1,302.894 ns | 14.9229 ns |  1.12x faster | 
| System_Half      | Vector512 | Half       | 100   | 2,046.744 ns | 21.5451 ns |  1.41x slower | 
| NetFabric_Half   | Vector512 | Half       | 100   | 2,044.560 ns | 26.1372 ns |  1.40x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **5**     |     **5.268 ns** |  **0.0650 ns** |      **baseline** | 
| System_Int       | Scalar    | Int        | 5     |     5.980 ns |  0.0643 ns |  1.14x slower | 
| NetFabric_Int    | Scalar    | Int        | 5     |     6.100 ns |  0.0580 ns |  1.16x slower | 
| Baseline_Int     | Vector128 | Int        | 5     |     5.559 ns |  0.0751 ns |  1.06x slower | 
| System_Int       | Vector128 | Int        | 5     |     6.106 ns |  0.0417 ns |  1.16x slower | 
| NetFabric_Int    | Vector128 | Int        | 5     |     6.059 ns |  0.0497 ns |  1.15x slower | 
| Baseline_Int     | Vector256 | Int        | 5     |     5.510 ns |  0.0684 ns |  1.05x slower | 
| System_Int       | Vector256 | Int        | 5     |     4.701 ns |  0.0551 ns |  1.12x faster | 
| NetFabric_Int    | Vector256 | Int        | 5     |     6.515 ns |  0.0947 ns |  1.24x slower | 
| Baseline_Int     | Vector512 | Int        | 5     |     5.479 ns |  0.0609 ns |  1.04x slower | 
| System_Int       | Vector512 | Int        | 5     |     4.782 ns |  0.0389 ns |  1.10x faster | 
| NetFabric_Int    | Vector512 | Int        | 5     |     6.503 ns |  0.0983 ns |  1.23x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **100**   |    **52.647 ns** |  **0.3876 ns** |      **baseline** | 
| System_Int       | Scalar    | Int        | 100   |    46.926 ns |  0.3705 ns |  1.12x faster | 
| NetFabric_Int    | Scalar    | Int        | 100   |    44.936 ns |  0.3041 ns |  1.17x faster | 
| Baseline_Int     | Vector128 | Int        | 100   |    52.428 ns |  0.4680 ns |  1.00x faster | 
| System_Int       | Vector128 | Int        | 100   |    17.222 ns |  0.2266 ns |  3.06x faster | 
| NetFabric_Int    | Vector128 | Int        | 100   |    15.597 ns |  0.2314 ns |  3.38x faster | 
| Baseline_Int     | Vector256 | Int        | 100   |    52.329 ns |  0.5298 ns |  1.01x faster | 
| System_Int       | Vector256 | Int        | 100   |    14.340 ns |  0.1981 ns |  3.67x faster | 
| NetFabric_Int    | Vector256 | Int        | 100   |    12.167 ns |  0.0836 ns |  4.33x faster | 
| Baseline_Int     | Vector512 | Int        | 100   |    53.104 ns |  0.4180 ns |  1.01x slower | 
| System_Int       | Vector512 | Int        | 100   |    12.888 ns |  0.0991 ns |  4.09x faster | 
| NetFabric_Int    | Vector512 | Int        | 100   |    11.825 ns |  0.1225 ns |  4.46x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **5**     |     **5.241 ns** |  **0.0213 ns** |      **baseline** | 
| System_Long      | Scalar    | Long       | 5     |     6.082 ns |  0.0608 ns |  1.16x slower | 
| NetFabric_Long   | Scalar    | Long       | 5     |     6.137 ns |  0.0662 ns |  1.17x slower | 
| Baseline_Long    | Vector128 | Long       | 5     |     5.203 ns |  0.0563 ns |  1.01x faster | 
| System_Long      | Vector128 | Long       | 5     |    59.031 ns |  0.7416 ns | 11.27x slower | 
| NetFabric_Long   | Vector128 | Long       | 5     |    11.683 ns |  0.0569 ns |  2.23x slower | 
| Baseline_Long    | Vector256 | Long       | 5     |     5.182 ns |  0.0760 ns |  1.01x faster | 
| System_Long      | Vector256 | Long       | 5     |    67.184 ns |  0.7865 ns | 12.83x slower | 
| NetFabric_Long   | Vector256 | Long       | 5     |     8.560 ns |  0.1037 ns |  1.63x slower | 
| Baseline_Long    | Vector512 | Long       | 5     |     5.140 ns |  0.0530 ns |  1.02x faster | 
| System_Long      | Vector512 | Long       | 5     |     5.299 ns |  0.0381 ns |  1.01x slower | 
| NetFabric_Long   | Vector512 | Long       | 5     |     8.702 ns |  0.1285 ns |  1.66x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **100**   |    **53.411 ns** |  **0.5068 ns** |      **baseline** | 
| System_Long      | Scalar    | Long       | 100   |    47.753 ns |  0.4281 ns |  1.12x faster | 
| NetFabric_Long   | Scalar    | Long       | 100   |    45.561 ns |  0.4574 ns |  1.17x faster | 
| Baseline_Long    | Vector128 | Long       | 100   |    52.956 ns |  0.5345 ns |  1.01x faster | 
| System_Long      | Vector128 | Long       | 100   |   316.267 ns |  3.8707 ns |  5.92x slower | 
| NetFabric_Long   | Vector128 | Long       | 100   |   234.398 ns |  1.4619 ns |  4.39x slower | 
| Baseline_Long    | Vector256 | Long       | 100   |    53.092 ns |  0.5833 ns |  1.01x faster | 
| System_Long      | Vector256 | Long       | 100   |   340.281 ns |  2.6694 ns |  6.37x slower | 
| NetFabric_Long   | Vector256 | Long       | 100   |   133.660 ns |  1.9327 ns |  2.50x slower | 
| Baseline_Long    | Vector512 | Long       | 100   |    53.232 ns |  0.6180 ns |  1.00x faster | 
| System_Long      | Vector512 | Long       | 100   |    20.161 ns |  0.2680 ns |  2.65x faster | 
| NetFabric_Long   | Vector512 | Long       | 100   |   129.004 ns |  1.7666 ns |  2.42x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **5**     |     **6.703 ns** |  **0.1022 ns** |      **baseline** | 
| System_Short     | Scalar    | Short      | 5     |     7.001 ns |  0.1066 ns |  1.04x slower | 
| NetFabric_Short  | Scalar    | Short      | 5     |     7.405 ns |  0.1112 ns |  1.10x slower | 
| Baseline_Short   | Vector128 | Short      | 5     |     6.982 ns |  0.1260 ns |  1.04x slower | 
| System_Short     | Vector128 | Short      | 5     |     6.434 ns |  0.0825 ns |  1.04x faster | 
| NetFabric_Short  | Vector128 | Short      | 5     |     7.399 ns |  0.1015 ns |  1.10x slower | 
| Baseline_Short   | Vector256 | Short      | 5     |     6.957 ns |  0.1073 ns |  1.04x slower | 
| System_Short     | Vector256 | Short      | 5     |     6.420 ns |  0.1113 ns |  1.04x faster | 
| NetFabric_Short  | Vector256 | Short      | 5     |     7.384 ns |  0.1049 ns |  1.10x slower | 
| Baseline_Short   | Vector512 | Short      | 5     |     7.017 ns |  0.1095 ns |  1.05x slower | 
| System_Short     | Vector512 | Short      | 5     |     6.394 ns |  0.0987 ns |  1.05x faster | 
| NetFabric_Short  | Vector512 | Short      | 5     |     7.771 ns |  0.1204 ns |  1.16x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **100**   |    **76.178 ns** |  **0.6328 ns** |      **baseline** | 
| System_Short     | Scalar    | Short      | 100   |    71.310 ns |  1.0128 ns |  1.07x faster | 
| NetFabric_Short  | Scalar    | Short      | 100   |    54.240 ns |  0.7376 ns |  1.40x faster | 
| Baseline_Short   | Vector128 | Short      | 100   |    76.131 ns |  1.2122 ns |  1.00x faster | 
| System_Short     | Vector128 | Short      | 100   |    71.661 ns |  0.8294 ns |  1.06x faster | 
| NetFabric_Short  | Vector128 | Short      | 100   |    11.031 ns |  0.1057 ns |  6.91x faster | 
| Baseline_Short   | Vector256 | Short      | 100   |    76.039 ns |  0.5724 ns |  1.00x faster | 
| System_Short     | Vector256 | Short      | 100   |    70.956 ns |  0.9030 ns |  1.07x faster | 
| NetFabric_Short  | Vector256 | Short      | 100   |     9.463 ns |  0.0520 ns |  8.05x faster | 
| Baseline_Short   | Vector512 | Short      | 100   |    75.701 ns |  0.6948 ns |  1.01x faster | 
| System_Short     | Vector512 | Short      | 100   |    70.976 ns |  0.6520 ns |  1.07x faster | 
| NetFabric_Short  | Vector512 | Short      | 100   |    11.093 ns |  0.1222 ns |  6.86x faster | 

### DegreesToRadians

Applying a vectorizable ternary operator on a span and two fixed scalar values.

| Method           | Job       | Categories | Count | Mean         | StdDev     | Ratio        | 
|----------------- |---------- |----------- |------ |-------------:|-----------:|-------------:|-
| **Baseline_Double**  | **Scalar**    | **Double**     | **5**     |     **3.874 ns** |  **0.0479 ns** |     **baseline** | 
| System_Double    | Scalar    | Double     | 5     |     4.414 ns |  0.0758 ns | 1.14x slower | 
| NetFabric_Double | Scalar    | Double     | 5     |     4.341 ns |  0.0235 ns | 1.12x slower | 
| Baseline_Double  | Vector128 | Double     | 5     |     3.848 ns |  0.0681 ns | 1.01x faster | 
| System_Double    | Vector128 | Double     | 5     |     9.267 ns |  0.0861 ns | 2.39x slower | 
| NetFabric_Double | Vector128 | Double     | 5     |     4.300 ns |  0.0431 ns | 1.11x slower | 
| Baseline_Double  | Vector256 | Double     | 5     |     3.796 ns |  0.0573 ns | 1.02x faster | 
| System_Double    | Vector256 | Double     | 5     |    14.060 ns |  0.2413 ns | 3.63x slower | 
| NetFabric_Double | Vector256 | Double     | 5     |     4.293 ns |  0.0621 ns | 1.11x slower | 
| Baseline_Double  | Vector512 | Double     | 5     |     3.705 ns |  0.0539 ns | 1.05x faster | 
| System_Double    | Vector512 | Double     | 5     |     5.319 ns |  0.0754 ns | 1.37x slower | 
| NetFabric_Double | Vector512 | Double     | 5     |     4.267 ns |  0.0469 ns | 1.10x slower | 
|                  |           |            |       |              |            |              | 
| **Baseline_Double**  | **Scalar**    | **Double**     | **100**   |    **93.996 ns** |  **0.9547 ns** |     **baseline** | 
| System_Double    | Scalar    | Double     | 100   |    93.369 ns |  0.5741 ns | 1.01x faster | 
| NetFabric_Double | Scalar    | Double     | 100   |    96.682 ns |  0.7775 ns | 1.03x slower | 
| Baseline_Double  | Vector128 | Double     | 100   |    93.516 ns |  0.6591 ns | 1.01x faster | 
| System_Double    | Vector128 | Double     | 100   |    63.615 ns |  0.9717 ns | 1.48x faster | 
| NetFabric_Double | Vector128 | Double     | 100   |    45.175 ns |  0.6568 ns | 2.08x faster | 
| Baseline_Double  | Vector256 | Double     | 100   |    93.520 ns |  0.8818 ns | 1.01x faster | 
| System_Double    | Vector256 | Double     | 100   |    45.665 ns |  0.5480 ns | 2.06x faster | 
| NetFabric_Double | Vector256 | Double     | 100   |    21.109 ns |  0.1810 ns | 4.45x faster | 
| Baseline_Double  | Vector512 | Double     | 100   |    94.654 ns |  0.8733 ns | 1.01x slower | 
| System_Double    | Vector512 | Double     | 100   |   165.902 ns |  1.2992 ns | 1.77x slower | 
| NetFabric_Double | Vector512 | Double     | 100   |    21.114 ns |  0.2206 ns | 4.45x faster | 
|                  |           |            |       |              |            |              | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **5**     |     **3.539 ns** |  **0.0474 ns** |     **baseline** | 
| System_Float     | Scalar    | Float      | 5     |     3.562 ns |  0.0179 ns | 1.01x slower | 
| NetFabric_Float  | Scalar    | Float      | 5     |     3.738 ns |  0.0532 ns | 1.06x slower | 
| Baseline_Float   | Vector128 | Float      | 5     |     2.903 ns |  0.0571 ns | 1.22x faster | 
| System_Float     | Vector128 | Float      | 5     |     8.263 ns |  0.1257 ns | 2.34x slower | 
| NetFabric_Float  | Vector128 | Float      | 5     |     4.160 ns |  0.0691 ns | 1.18x slower | 
| Baseline_Float   | Vector256 | Float      | 5     |     2.773 ns |  0.0656 ns | 1.28x faster | 
| System_Float     | Vector256 | Float      | 5     |     2.962 ns |  0.0424 ns | 1.19x faster | 
| NetFabric_Float  | Vector256 | Float      | 5     |     4.486 ns |  0.0464 ns | 1.27x slower | 
| Baseline_Float   | Vector512 | Float      | 5     |     2.767 ns |  0.0625 ns | 1.28x faster | 
| System_Float     | Vector512 | Float      | 5     |     4.997 ns |  0.0387 ns | 1.41x slower | 
| NetFabric_Float  | Vector512 | Float      | 5     |     4.501 ns |  0.0684 ns | 1.27x slower | 
|                  |           |            |       |              |            |              | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **100**   |    **57.638 ns** |  **0.5762 ns** |     **baseline** | 
| System_Float     | Scalar    | Float      | 100   |    57.938 ns |  0.8505 ns | 1.01x slower | 
| NetFabric_Float  | Scalar    | Float      | 100   |    57.813 ns |  0.4701 ns | 1.00x slower | 
| Baseline_Float   | Vector128 | Float      | 100   |    57.163 ns |  0.4460 ns | 1.01x faster | 
| System_Float     | Vector128 | Float      | 100   |    36.086 ns |  0.6259 ns | 1.60x faster | 
| NetFabric_Float  | Vector128 | Float      | 100   |    13.932 ns |  0.2167 ns | 4.14x faster | 
| Baseline_Float   | Vector256 | Float      | 100   |    57.791 ns |  0.3792 ns | 1.00x slower | 
| System_Float     | Vector256 | Float      | 100   |    25.535 ns |  0.2626 ns | 2.26x faster | 
| NetFabric_Float  | Vector256 | Float      | 100   |     8.437 ns |  0.1019 ns | 6.84x faster | 
| Baseline_Float   | Vector512 | Float      | 100   |    58.285 ns |  0.6503 ns | 1.01x slower | 
| System_Float     | Vector512 | Float      | 100   |    68.610 ns |  0.5772 ns | 1.19x slower | 
| NetFabric_Float  | Vector512 | Float      | 100   |    10.744 ns |  0.0809 ns | 5.36x faster | 
|                  |           |            |       |              |            |              | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **5**     |    **44.175 ns** |  **0.4475 ns** |     **baseline** | 
| System_Half      | Scalar    | Half       | 5     |    38.570 ns |  0.4226 ns | 1.15x faster | 
| NetFabric_Half   | Scalar    | Half       | 5     |   112.452 ns |  1.1506 ns | 2.55x slower | 
| Baseline_Half    | Vector128 | Half       | 5     |    39.767 ns |  0.3630 ns | 1.11x faster | 
| System_Half      | Vector128 | Half       | 5     |    29.718 ns |  0.4081 ns | 1.49x faster | 
| NetFabric_Half   | Vector128 | Half       | 5     |   102.546 ns |  0.5989 ns | 2.32x slower | 
| Baseline_Half    | Vector256 | Half       | 5     |    39.844 ns |  0.3364 ns | 1.11x faster | 
| System_Half      | Vector256 | Half       | 5     |    29.888 ns |  0.3833 ns | 1.48x faster | 
| NetFabric_Half   | Vector256 | Half       | 5     |   102.769 ns |  0.7549 ns | 2.33x slower | 
| Baseline_Half    | Vector512 | Half       | 5     |    39.561 ns |  0.3703 ns | 1.12x faster | 
| System_Half      | Vector512 | Half       | 5     |    29.696 ns |  0.3986 ns | 1.49x faster | 
| NetFabric_Half   | Vector512 | Half       | 5     |   102.666 ns |  0.7215 ns | 2.32x slower | 
|                  |           |            |       |              |            |              | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **100**   |   **886.924 ns** |  **4.9085 ns** |     **baseline** | 
| System_Half      | Scalar    | Half       | 100   |   885.135 ns |  7.4666 ns | 1.00x faster | 
| NetFabric_Half   | Scalar    | Half       | 100   | 2,317.382 ns | 23.3518 ns | 2.61x slower | 
| Baseline_Half    | Vector128 | Half       | 100   |   809.089 ns |  7.0259 ns | 1.10x faster | 
| System_Half      | Vector128 | Half       | 100   |   801.256 ns |  5.0528 ns | 1.11x faster | 
| NetFabric_Half   | Vector128 | Half       | 100   | 2,176.527 ns | 28.7586 ns | 2.45x slower | 
| Baseline_Half    | Vector256 | Half       | 100   |   809.389 ns |  6.0195 ns | 1.10x faster | 
| System_Half      | Vector256 | Half       | 100   |   802.714 ns |  7.1502 ns | 1.10x faster | 
| NetFabric_Half   | Vector256 | Half       | 100   | 2,179.970 ns | 28.3543 ns | 2.46x slower | 
| Baseline_Half    | Vector512 | Half       | 100   |   809.797 ns |  5.9153 ns | 1.10x faster | 
| System_Half      | Vector512 | Half       | 100   |   803.333 ns |  4.7556 ns | 1.10x faster | 
| NetFabric_Half   | Vector512 | Half       | 100   | 2,182.736 ns | 26.3836 ns | 2.46x slower | 

### Sum

Applying a vectorizable aggregation operator on a span.

It additionally compares with the performance of LINQ's `Sum()`. However, it's worth noting that this method lacks support for the types `short` and `Half`. In such instances, LINQ's `Aggregate()` is employed instead.

| Method           | Job       | Categories | Count |         Mean |     StdDev |       Median |         Ratio |
| ---------------- | --------- | ---------- | ----- | -----------: | ---------: | -----------: | ------------: |
| Baseline_Double  | Scalar    | Double     | 1000  |    570.98 ns |   5.629 ns |    573.36 ns |      baseline |
| LINQ_Double      | Scalar    | Double     | 1000  |    571.74 ns |   5.388 ns |    573.79 ns |  1.00x slower |
| NetFabric_Double | Scalar    | Double     | 1000  |    147.88 ns |   2.157 ns |    148.22 ns |  3.86x faster |
| Baseline_Double  | Vector128 | Double     | 1000  |    572.54 ns |   5.996 ns |    573.91 ns |  1.00x slower |
| LINQ_Double      | Vector128 | Double     | 1000  |    570.89 ns |   5.926 ns |    571.20 ns |  1.00x faster |
| NetFabric_Double | Vector128 | Double     | 1000  |    277.94 ns |   2.981 ns |    278.61 ns |  2.05x faster |
| Baseline_Double  | Vector256 | Double     | 1000  |    572.45 ns |   5.287 ns |    573.21 ns |  1.00x slower |
| LINQ_Double      | Vector256 | Double     | 1000  |    573.01 ns |   6.732 ns |    574.56 ns |  1.00x slower |
| NetFabric_Double | Vector256 | Double     | 1000  |    128.70 ns |   1.034 ns |    128.53 ns |  4.44x faster |
| Baseline_Double  | Vector512 | Double     | 1000  |    571.11 ns |   6.915 ns |    572.84 ns |  1.00x slower |
| LINQ_Double      | Vector512 | Double     | 1000  |    570.82 ns |   4.606 ns |    572.51 ns |  1.00x faster |
| NetFabric_Double | Vector512 | Double     | 1000  |    128.17 ns |   1.055 ns |    128.33 ns |  4.46x faster |
|                  |           |            |       |              |            |              |               |
| Baseline_Float   | Scalar    | Float      | 1000  |    570.56 ns |   4.612 ns |    572.98 ns |      baseline |
| LINQ_Float       | Scalar    | Float      | 1000  |  1,014.42 ns |  10.750 ns |  1,018.47 ns |  1.78x slower |
| System_Float     | Scalar    | Float      | 1000  |    574.55 ns |   5.687 ns |    575.36 ns |  1.01x slower |
| NetFabric_Float  | Scalar    | Float      | 1000  |    145.22 ns |   1.107 ns |    145.80 ns |  3.93x faster |
| Baseline_Float   | Vector128 | Float      | 1000  |    569.56 ns |   4.442 ns |    572.11 ns |  1.00x faster |
| LINQ_Float       | Vector128 | Float      | 1000  |  1,207.31 ns |   8.692 ns |  1,209.80 ns |  2.12x slower |
| System_Float     | Vector128 | Float      | 1000  |    115.47 ns |   0.658 ns |    115.61 ns |  4.94x faster |
| NetFabric_Float  | Vector128 | Float      | 1000  |    127.75 ns |   1.536 ns |    126.71 ns |  4.46x faster |
| Baseline_Float   | Vector256 | Float      | 1000  |    568.82 ns |   5.199 ns |    567.58 ns |  1.00x faster |
| LINQ_Float       | Vector256 | Float      | 1000  |  1,210.59 ns |  10.939 ns |  1,213.35 ns |  2.12x slower |
| System_Float     | Vector256 | Float      | 1000  |     43.05 ns |   0.335 ns |     43.16 ns | 13.25x faster |
| NetFabric_Float  | Vector256 | Float      | 1000  |     52.75 ns |   0.238 ns |     52.77 ns | 10.82x faster |
| Baseline_Float   | Vector512 | Float      | 1000  |    570.12 ns |   5.911 ns |    571.47 ns |  1.00x slower |
| LINQ_Float       | Vector512 | Float      | 1000  |  1,401.84 ns |  10.311 ns |  1,405.26 ns |  2.46x slower |
| System_Float     | Vector512 | Float      | 1000  |     20.96 ns |   0.204 ns |     20.99 ns | 27.23x faster |
| NetFabric_Float  | Vector512 | Float      | 1000  |     52.38 ns |   0.445 ns |     52.32 ns | 10.89x faster |
|                  |           |            |       |              |            |              |               |
| Baseline_Half    | Scalar    | Half       | 1000  | 12,303.82 ns |  51.396 ns | 12,320.83 ns |      baseline |
| LINQ_Half        | Scalar    | Half       | 1000  | 12,569.00 ns |  42.232 ns | 12,580.73 ns |  1.02x slower |
| NetFabric_Half   | Scalar    | Half       | 1000  |  9,224.23 ns |  81.418 ns |  9,274.72 ns |  1.33x faster |
| Baseline_Half    | Vector128 | Half       | 1000  | 11,958.97 ns |  44.551 ns | 11,980.43 ns |  1.03x faster |
| LINQ_Half        | Vector128 | Half       | 1000  | 12,195.96 ns |  74.433 ns | 12,202.99 ns |  1.01x faster |
| NetFabric_Half   | Vector128 | Half       | 1000  |  8,146.77 ns |  81.343 ns |  8,164.09 ns |  1.51x faster |
| Baseline_Half    | Vector256 | Half       | 1000  | 11,973.93 ns | 108.398 ns | 11,984.58 ns |  1.03x faster |
| LINQ_Half        | Vector256 | Half       | 1000  | 12,158.34 ns | 126.659 ns | 12,116.17 ns |  1.01x faster |
| NetFabric_Half   | Vector256 | Half       | 1000  |  8,136.64 ns |  71.782 ns |  8,164.75 ns |  1.51x faster |
| Baseline_Half    | Vector512 | Half       | 1000  | 11,966.10 ns |  63.814 ns | 11,992.15 ns |  1.03x faster |
| LINQ_Half        | Vector512 | Half       | 1000  | 12,183.80 ns |  77.386 ns | 12,207.81 ns |  1.01x faster |
| NetFabric_Half   | Vector512 | Half       | 1000  |  8,132.30 ns |  73.452 ns |  8,140.99 ns |  1.51x faster |
|                  |           |            |       |              |            |              |               |
| Baseline_Int     | Scalar    | Int        | 1000  |    209.13 ns |   1.815 ns |    209.57 ns |      baseline |
| LINQ_Int         | Scalar    | Int        | 1000  |    207.11 ns |   1.197 ns |    207.26 ns |  1.01x faster |
| NetFabric_Int    | Scalar    | Int        | 1000  |    106.23 ns |   0.707 ns |    106.22 ns |  1.97x faster |
| Baseline_Int     | Vector128 | Int        | 1000  |    221.71 ns |   1.209 ns |    221.74 ns |  1.06x slower |
| LINQ_Int         | Vector128 | Int        | 1000  |    106.27 ns |   3.544 ns |    105.34 ns |  1.96x faster |
| NetFabric_Int    | Vector128 | Int        | 1000  |     59.76 ns |   0.899 ns |     60.06 ns |  3.50x faster |
| Baseline_Int     | Vector256 | Int        | 1000  |    221.06 ns |   1.133 ns |    220.85 ns |  1.06x slower |
| LINQ_Int         | Vector256 | Int        | 1000  |     50.87 ns |   0.211 ns |     50.88 ns |  4.11x faster |
| NetFabric_Int    | Vector256 | Int        | 1000  |     33.41 ns |   0.293 ns |     33.41 ns |  6.26x faster |
| Baseline_Int     | Vector512 | Int        | 1000  |    219.06 ns |   1.548 ns |    218.67 ns |  1.05x slower |
| LINQ_Int         | Vector512 | Int        | 1000  |     50.72 ns |   0.320 ns |     50.70 ns |  4.12x faster |
| NetFabric_Int    | Vector512 | Int        | 1000  |     33.69 ns |   0.330 ns |     33.72 ns |  6.21x faster |
|                  |           |            |       |              |            |              |               |
| Baseline_Long    | Scalar    | Long       | 1000  |    209.33 ns |   3.041 ns |    208.05 ns |      baseline |
| LINQ_Long        | Scalar    | Long       | 1000  |    208.01 ns |   1.683 ns |    208.05 ns |  1.01x faster |
| NetFabric_Long   | Scalar    | Long       | 1000  |    106.83 ns |   0.630 ns |    106.97 ns |  1.96x faster |
| Baseline_Long    | Vector128 | Long       | 1000  |    220.86 ns |   1.267 ns |    220.79 ns |  1.06x slower |
| LINQ_Long        | Vector128 | Long       | 1000  |    205.47 ns |   1.143 ns |    205.74 ns |  1.02x faster |
| NetFabric_Long   | Vector128 | Long       | 1000  |    110.76 ns |   0.171 ns |    110.71 ns |  1.89x faster |
| Baseline_Long    | Vector256 | Long       | 1000  |    220.38 ns |   1.423 ns |    220.08 ns |  1.05x slower |
| LINQ_Long        | Vector256 | Long       | 1000  |    108.83 ns |   2.826 ns |    108.70 ns |  1.93x faster |
| NetFabric_Long   | Vector256 | Long       | 1000  |     59.73 ns |   0.512 ns |     59.62 ns |  3.51x faster |
| Baseline_Long    | Vector512 | Long       | 1000  |    220.13 ns |   2.014 ns |    220.33 ns |  1.05x slower |
| LINQ_Long        | Vector512 | Long       | 1000  |    109.13 ns |   4.001 ns |    109.02 ns |  1.91x faster |
| NetFabric_Long   | Vector512 | Long       | 1000  |     60.10 ns |   0.533 ns |     60.23 ns |  3.48x faster |
|                  |           |            |       |              |            |              |               |
| Baseline_Short   | Scalar    | Short      | 1000  |    398.96 ns |   4.568 ns |    397.85 ns |      baseline |
| LINQ_Short       | Scalar    | Short      | 1000  |    747.23 ns |   4.973 ns |    746.78 ns |  1.87x slower |
| NetFabric_Short  | Scalar    | Short      | 1000  |    217.72 ns |   1.566 ns |    217.20 ns |  1.83x faster |
| Baseline_Short   | Vector128 | Short      | 1000  |    397.20 ns |   2.326 ns |    398.01 ns |  1.00x faster |
| LINQ_Short       | Vector128 | Short      | 1000  |    743.73 ns |   3.997 ns |    744.50 ns |  1.86x slower |
| NetFabric_Short  | Vector128 | Short      | 1000  |     33.28 ns |   0.324 ns |     33.35 ns | 12.00x faster |
| Baseline_Short   | Vector256 | Short      | 1000  |    398.76 ns |   3.406 ns |    398.69 ns |  1.00x slower |
| LINQ_Short       | Vector256 | Short      | 1000  |    745.48 ns |   6.354 ns |    745.06 ns |  1.87x slower |
| NetFabric_Short  | Vector256 | Short      | 1000  |     17.16 ns |   0.238 ns |     17.16 ns | 23.25x faster |
| Baseline_Short   | Vector512 | Short      | 1000  |    396.99 ns |   3.059 ns |    397.21 ns |  1.00x faster |
| LINQ_Short       | Vector512 | Short      | 1000  |    754.52 ns |  12.566 ns |    752.41 ns |  1.89x slower |
| NetFabric_Short  | Vector512 | Short      | 1000  |     20.55 ns |   1.059 ns |     20.91 ns | 18.86x faster |

### Sum2D

Applying a vectorizable aggregation operator on a span with two contiguos values for each element.

It also compares to the performance of LINQ's `Aggregate()`, as LINQ's `Sum()` does not support non-native numeric types.

| Method           | Job       | Categories | Count | Mean         | StdDev     | Ratio         | 
|----------------- |---------- |----------- |------ |-------------:|-----------:|--------------:|-
| **Baseline_Double**  | **Scalar**    | **Double**     | **5**     |     **2.856 ns** |  **0.0672 ns** |      **baseline** | 
| LINQ_Double      | Scalar    | Double     | 5     |    22.363 ns |  0.1260 ns |  7.80x slower | 
| NetFabric_Double | Scalar    | Double     | 5     |     2.715 ns |  0.0317 ns |  1.05x faster | 
| Baseline_Double  | Vector128 | Double     | 5     |     2.924 ns |  0.0669 ns |  1.02x slower | 
| LINQ_Double      | Vector128 | Double     | 5     |    21.854 ns |  0.1417 ns |  7.66x slower | 
| NetFabric_Double | Vector128 | Double     | 5     |     3.978 ns |  0.0425 ns |  1.39x slower | 
| Baseline_Double  | Vector256 | Double     | 5     |     2.916 ns |  0.0546 ns |  1.02x slower | 
| LINQ_Double      | Vector256 | Double     | 5     |    21.640 ns |  0.2272 ns |  7.57x slower | 
| NetFabric_Double | Vector256 | Double     | 5     |     4.038 ns |  0.0404 ns |  1.41x slower | 
| Baseline_Double  | Vector512 | Double     | 5     |     2.915 ns |  0.0519 ns |  1.02x slower | 
| LINQ_Double      | Vector512 | Double     | 5     |    21.815 ns |  0.2609 ns |  7.64x slower | 
| NetFabric_Double | Vector512 | Double     | 5     |     4.024 ns |  0.0443 ns |  1.41x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Double**  | **Scalar**    | **Double**     | **100**   |    **47.532 ns** |  **0.4812 ns** |      **baseline** | 
| LINQ_Double      | Scalar    | Double     | 100   |   251.786 ns |  2.5022 ns |  5.30x slower | 
| NetFabric_Double | Scalar    | Double     | 100   |    26.235 ns |  0.2072 ns |  1.81x faster | 
| Baseline_Double  | Vector128 | Double     | 100   |    47.782 ns |  0.3649 ns |  1.01x slower | 
| LINQ_Double      | Vector128 | Double     | 100   |   253.530 ns |  4.0536 ns |  5.33x slower | 
| NetFabric_Double | Vector128 | Double     | 100   |    43.869 ns |  0.4604 ns |  1.08x faster | 
| Baseline_Double  | Vector256 | Double     | 100   |    47.532 ns |  0.4819 ns |  1.00x slower | 
| LINQ_Double      | Vector256 | Double     | 100   |   252.565 ns |  3.9375 ns |  5.31x slower | 
| NetFabric_Double | Vector256 | Double     | 100   |    22.509 ns |  0.1589 ns |  2.11x faster | 
| Baseline_Double  | Vector512 | Double     | 100   |    47.827 ns |  0.4383 ns |  1.01x slower | 
| LINQ_Double      | Vector512 | Double     | 100   |   253.378 ns |  2.2868 ns |  5.34x slower | 
| NetFabric_Double | Vector512 | Double     | 100   |    22.754 ns |  0.2851 ns |  2.09x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **5**     |     **5.334 ns** |  **0.0985 ns** |      **baseline** | 
| LINQ_Float       | Scalar    | Float      | 5     |    34.944 ns |  0.3808 ns |  6.55x slower | 
| NetFabric_Float  | Scalar    | Float      | 5     |     9.873 ns |  0.0618 ns |  1.85x slower | 
| Baseline_Float   | Vector128 | Float      | 5     |     5.261 ns |  0.0450 ns |  1.01x faster | 
| LINQ_Float       | Vector128 | Float      | 5     |    35.705 ns |  0.4709 ns |  6.69x slower | 
| NetFabric_Float  | Vector128 | Float      | 5     |    10.744 ns |  0.0939 ns |  2.01x slower | 
| Baseline_Float   | Vector256 | Float      | 5     |     5.258 ns |  0.0527 ns |  1.01x faster | 
| LINQ_Float       | Vector256 | Float      | 5     |    35.696 ns |  0.3284 ns |  6.69x slower | 
| NetFabric_Float  | Vector256 | Float      | 5     |    11.521 ns |  0.0810 ns |  2.16x slower | 
| Baseline_Float   | Vector512 | Float      | 5     |     5.283 ns |  0.0515 ns |  1.01x faster | 
| LINQ_Float       | Vector512 | Float      | 5     |    36.049 ns |  0.3963 ns |  6.77x slower | 
| NetFabric_Float  | Vector512 | Float      | 5     |    11.488 ns |  0.0866 ns |  2.16x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **100**   |   **199.708 ns** |  **1.0896 ns** |      **baseline** | 
| LINQ_Float       | Scalar    | Float      | 100   |   748.563 ns |  6.9195 ns |  3.75x slower | 
| NetFabric_Float  | Scalar    | Float      | 100   |    31.235 ns |  0.4406 ns |  6.38x faster | 
| Baseline_Float   | Vector128 | Float      | 100   |   198.116 ns |  1.6824 ns |  1.01x faster | 
| LINQ_Float       | Vector128 | Float      | 100   |   748.911 ns |  4.5953 ns |  3.75x slower | 
| NetFabric_Float  | Vector128 | Float      | 100   |    25.878 ns |  0.1873 ns |  7.73x faster | 
| Baseline_Float   | Vector256 | Float      | 100   |   198.476 ns |  1.2236 ns |  1.01x faster | 
| LINQ_Float       | Vector256 | Float      | 100   |   748.884 ns |  6.3603 ns |  3.75x slower | 
| NetFabric_Float  | Vector256 | Float      | 100   |    15.339 ns |  0.2260 ns | 13.03x faster | 
| Baseline_Float   | Vector512 | Float      | 100   |   199.057 ns |  1.3559 ns |  1.00x faster | 
| LINQ_Float       | Vector512 | Float      | 100   |   749.709 ns |  3.6144 ns |  3.75x slower | 
| NetFabric_Float  | Vector512 | Float      | 100   |    15.263 ns |  0.1812 ns | 13.11x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **5**     |    **88.125 ns** |  **0.7883 ns** |      **baseline** | 
| LINQ_Half        | Scalar    | Half       | 5     |   138.777 ns |  1.7547 ns |  1.57x slower | 
| NetFabric_Half   | Scalar    | Half       | 5     |   106.188 ns |  0.7468 ns |  1.20x slower | 
| Baseline_Half    | Vector128 | Half       | 5     |    79.350 ns |  0.6893 ns |  1.11x faster | 
| LINQ_Half        | Vector128 | Half       | 5     |   131.888 ns |  1.8551 ns |  1.50x slower | 
| NetFabric_Half   | Vector128 | Half       | 5     |    95.892 ns |  0.6641 ns |  1.09x slower | 
| Baseline_Half    | Vector256 | Half       | 5     |    79.703 ns |  0.9271 ns |  1.11x faster | 
| LINQ_Half        | Vector256 | Half       | 5     |   131.180 ns |  1.4925 ns |  1.49x slower | 
| NetFabric_Half   | Vector256 | Half       | 5     |    95.693 ns |  0.5178 ns |  1.09x slower | 
| Baseline_Half    | Vector512 | Half       | 5     |    79.588 ns |  0.7426 ns |  1.11x faster | 
| LINQ_Half        | Vector512 | Half       | 5     |   131.583 ns |  1.6302 ns |  1.49x slower | 
| NetFabric_Half   | Vector512 | Half       | 5     |    95.545 ns |  0.8052 ns |  1.08x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **100**   | **1,787.518 ns** | **13.9908 ns** |      **baseline** | 
| LINQ_Half        | Scalar    | Half       | 100   | 2,659.217 ns | 31.8466 ns |  1.49x slower | 
| NetFabric_Half   | Scalar    | Half       | 100   | 1,804.273 ns | 10.6682 ns |  1.01x slower | 
| Baseline_Half    | Vector128 | Half       | 100   | 1,604.938 ns | 11.1322 ns |  1.11x faster | 
| LINQ_Half        | Vector128 | Half       | 100   | 2,527.768 ns | 24.4705 ns |  1.41x slower | 
| NetFabric_Half   | Vector128 | Half       | 100   | 1,614.064 ns | 10.1499 ns |  1.11x faster | 
| Baseline_Half    | Vector256 | Half       | 100   | 1,604.241 ns | 13.4777 ns |  1.11x faster | 
| LINQ_Half        | Vector256 | Half       | 100   | 2,544.758 ns | 22.5713 ns |  1.42x slower | 
| NetFabric_Half   | Vector256 | Half       | 100   | 1,620.582 ns |  8.0262 ns |  1.10x faster | 
| Baseline_Half    | Vector512 | Half       | 100   | 1,607.558 ns | 10.4403 ns |  1.11x faster | 
| LINQ_Half        | Vector512 | Half       | 100   | 2,531.149 ns | 25.9190 ns |  1.42x slower | 
| NetFabric_Half   | Vector512 | Half       | 100   | 1,615.188 ns | 13.4999 ns |  1.11x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **5**     |     **5.102 ns** |  **0.0527 ns** |      **baseline** | 
| LINQ_Int         | Scalar    | Int        | 5     |    32.124 ns |  0.4176 ns |  6.31x slower | 
| NetFabric_Int    | Scalar    | Int        | 5     |     9.518 ns |  0.0883 ns |  1.87x slower | 
| Baseline_Int     | Vector128 | Int        | 5     |     5.286 ns |  0.0578 ns |  1.03x slower | 
| LINQ_Int         | Vector128 | Int        | 5     |    31.946 ns |  0.2887 ns |  6.27x slower | 
| NetFabric_Int    | Vector128 | Int        | 5     |    10.307 ns |  0.0957 ns |  2.02x slower | 
| Baseline_Int     | Vector256 | Int        | 5     |     5.305 ns |  0.0505 ns |  1.04x slower | 
| LINQ_Int         | Vector256 | Int        | 5     |    32.062 ns |  0.3753 ns |  6.30x slower | 
| NetFabric_Int    | Vector256 | Int        | 5     |    12.258 ns |  0.1163 ns |  2.40x slower | 
| Baseline_Int     | Vector512 | Int        | 5     |     5.286 ns |  0.0617 ns |  1.04x slower | 
| LINQ_Int         | Vector512 | Int        | 5     |    31.520 ns |  0.5342 ns |  6.17x slower | 
| NetFabric_Int    | Vector512 | Int        | 5     |    10.940 ns |  0.0932 ns |  2.14x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **100**   |   **139.049 ns** |  **1.5118 ns** |      **baseline** | 
| LINQ_Int         | Scalar    | Int        | 100   |   534.049 ns |  7.1846 ns |  3.84x slower | 
| NetFabric_Int    | Scalar    | Int        | 100   |    24.329 ns |  0.1629 ns |  5.72x faster | 
| Baseline_Int     | Vector128 | Int        | 100   |   138.423 ns |  1.2174 ns |  1.00x faster | 
| LINQ_Int         | Vector128 | Int        | 100   |   534.054 ns |  6.1699 ns |  3.84x slower | 
| NetFabric_Int    | Vector128 | Int        | 100   |    16.245 ns |  0.3110 ns |  8.56x faster | 
| Baseline_Int     | Vector256 | Int        | 100   |   138.946 ns |  1.6309 ns |  1.00x faster | 
| LINQ_Int         | Vector256 | Int        | 100   |   533.523 ns |  6.9988 ns |  3.84x slower | 
| NetFabric_Int    | Vector256 | Int        | 100   |    14.274 ns |  0.1336 ns |  9.74x faster | 
| Baseline_Int     | Vector512 | Int        | 100   |   138.906 ns |  1.4827 ns |  1.00x faster | 
| LINQ_Int         | Vector512 | Int        | 100   |   531.327 ns |  7.4129 ns |  3.82x slower | 
| NetFabric_Int    | Vector512 | Int        | 100   |    13.489 ns |  0.2242 ns | 10.31x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **5**     |     **2.562 ns** |  **0.0264 ns** |      **baseline** | 
| LINQ_Long        | Scalar    | Long       | 5     |    16.894 ns |  0.1750 ns |  6.60x slower | 
| NetFabric_Long   | Scalar    | Long       | 5     |     4.065 ns |  0.0343 ns |  1.59x slower | 
| Baseline_Long    | Vector128 | Long       | 5     |     2.549 ns |  0.0311 ns |  1.01x faster | 
| LINQ_Long        | Vector128 | Long       | 5     |    16.967 ns |  0.1337 ns |  6.62x slower | 
| NetFabric_Long   | Vector128 | Long       | 5     |     4.549 ns |  0.0690 ns |  1.78x slower | 
| Baseline_Long    | Vector256 | Long       | 5     |     2.563 ns |  0.0218 ns |  1.00x slower | 
| LINQ_Long        | Vector256 | Long       | 5     |    16.969 ns |  0.2302 ns |  6.62x slower | 
| NetFabric_Long   | Vector256 | Long       | 5     |     4.721 ns |  0.0507 ns |  1.84x slower | 
| Baseline_Long    | Vector512 | Long       | 5     |     2.551 ns |  0.0303 ns |  1.00x faster | 
| LINQ_Long        | Vector512 | Long       | 5     |    16.611 ns |  0.1523 ns |  6.48x slower | 
| NetFabric_Long   | Vector512 | Long       | 5     |     4.699 ns |  0.0654 ns |  1.83x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **100**   |    **41.779 ns** |  **0.4013 ns** |      **baseline** | 
| LINQ_Long        | Scalar    | Long       | 100   |   150.482 ns |  1.4243 ns |  3.60x slower | 
| NetFabric_Long   | Scalar    | Long       | 100   |    22.478 ns |  0.2376 ns |  1.86x faster | 
| Baseline_Long    | Vector128 | Long       | 100   |    42.214 ns |  0.4221 ns |  1.01x slower | 
| LINQ_Long        | Vector128 | Long       | 100   |   148.231 ns |  1.5552 ns |  3.55x slower | 
| NetFabric_Long   | Vector128 | Long       | 100   |    28.935 ns |  0.3830 ns |  1.44x faster | 
| Baseline_Long    | Vector256 | Long       | 100   |    41.873 ns |  0.3778 ns |  1.00x slower | 
| LINQ_Long        | Vector256 | Long       | 100   |   149.398 ns |  1.0473 ns |  3.58x slower | 
| NetFabric_Long   | Vector256 | Long       | 100   |    14.728 ns |  0.2087 ns |  2.84x faster | 
| Baseline_Long    | Vector512 | Long       | 100   |    42.276 ns |  0.3643 ns |  1.01x slower | 
| LINQ_Long        | Vector512 | Long       | 100   |   146.177 ns |  1.8160 ns |  3.50x slower | 
| NetFabric_Long   | Vector512 | Long       | 100   |    14.905 ns |  0.1977 ns |  2.80x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **5**     |     **6.618 ns** |  **0.1123 ns** |      **baseline** | 
| LINQ_Short       | Scalar    | Short      | 5     |    48.195 ns |  0.3987 ns |  7.28x slower | 
| NetFabric_Short  | Scalar    | Short      | 5     |    10.379 ns |  0.0998 ns |  1.57x slower | 
| Baseline_Short   | Vector128 | Short      | 5     |     6.683 ns |  0.1042 ns |  1.01x slower | 
| LINQ_Short       | Vector128 | Short      | 5     |    48.587 ns |  0.4001 ns |  7.34x slower | 
| NetFabric_Short  | Vector128 | Short      | 5     |    11.397 ns |  0.0767 ns |  1.72x slower | 
| Baseline_Short   | Vector256 | Short      | 5     |     6.614 ns |  0.0905 ns |  1.00x faster | 
| LINQ_Short       | Vector256 | Short      | 5     |    48.409 ns |  0.2994 ns |  7.30x slower | 
| NetFabric_Short  | Vector256 | Short      | 5     |    10.601 ns |  0.1128 ns |  1.60x slower | 
| Baseline_Short   | Vector512 | Short      | 5     |     6.737 ns |  0.0943 ns |  1.02x slower | 
| LINQ_Short       | Vector512 | Short      | 5     |    49.052 ns |  0.5798 ns |  7.41x slower | 
| NetFabric_Short  | Vector512 | Short      | 5     |    10.598 ns |  0.0764 ns |  1.60x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **100**   |   **184.199 ns** |  **1.1563 ns** |      **baseline** | 
| LINQ_Short       | Scalar    | Short      | 100   |   836.732 ns |  4.3644 ns |  4.54x slower | 
| NetFabric_Short  | Scalar    | Short      | 100   |    45.562 ns |  0.3983 ns |  4.04x faster | 
| Baseline_Short   | Vector128 | Short      | 100   |   185.459 ns |  0.9637 ns |  1.01x slower | 
| LINQ_Short       | Vector128 | Short      | 100   |   835.004 ns |  5.4555 ns |  4.53x slower | 
| NetFabric_Short  | Vector128 | Short      | 100   |    13.604 ns |  0.1290 ns | 13.53x faster | 
| Baseline_Short   | Vector256 | Short      | 100   |   184.943 ns |  1.5798 ns |  1.00x slower | 
| LINQ_Short       | Vector256 | Short      | 100   |   835.774 ns |  5.3602 ns |  4.54x slower | 
| NetFabric_Short  | Vector256 | Short      | 100   |    16.527 ns |  0.2068 ns | 11.15x faster | 
| Baseline_Short   | Vector512 | Short      | 100   |   184.132 ns |  2.0755 ns |  1.00x faster | 
| LINQ_Short       | Vector512 | Short      | 100   |   837.173 ns |  6.0779 ns |  4.55x slower | 
| NetFabric_Short  | Vector512 | Short      | 100   |    14.271 ns |  0.0589 ns | 12.91x faster | 

### Sum3D

Applying a vectorizable aggregation operator on a span with three contiguos values for each element.

It also compares to the performance of LINQ's `Aggregate()`, as LINQ's `Sum()` does not support non-native numeric types.

| Method           | Job       | Categories | Count | Mean         | StdDev     | Ratio        | 
|----------------- |---------- |----------- |------ |-------------:|-----------:|-------------:|-
| **Baseline_Double**  | **Scalar**    | **Double**     | **5**     |     **2.960 ns** |  **0.0481 ns** |     **baseline** | 
| LINQ_Double      | Scalar    | Double     | 5     |    21.829 ns |  0.2019 ns | 7.38x slower | 
| NetFabric_Double | Scalar    | Double     | 5     |     3.223 ns |  0.0501 ns | 1.09x slower | 
| Baseline_Double  | Vector128 | Double     | 5     |     3.539 ns |  0.0747 ns | 1.20x slower | 
| LINQ_Double      | Vector128 | Double     | 5     |    21.708 ns |  0.2006 ns | 7.34x slower | 
| NetFabric_Double | Vector128 | Double     | 5     |    12.294 ns |  0.0923 ns | 4.16x slower | 
| Baseline_Double  | Vector256 | Double     | 5     |     3.472 ns |  0.1282 ns | 1.18x slower | 
| LINQ_Double      | Vector256 | Double     | 5     |    21.830 ns |  0.2568 ns | 7.38x slower | 
| NetFabric_Double | Vector256 | Double     | 5     |    12.468 ns |  0.1286 ns | 4.21x slower | 
| Baseline_Double  | Vector512 | Double     | 5     |     3.280 ns |  0.0518 ns | 1.11x slower | 
| LINQ_Double      | Vector512 | Double     | 5     |    21.969 ns |  0.1784 ns | 7.43x slower | 
| NetFabric_Double | Vector512 | Double     | 5     |    12.395 ns |  0.2009 ns | 4.19x slower | 
|                  |           |            |       |              |            |              | 
| **Baseline_Double**  | **Scalar**    | **Double**     | **100**   |    **56.636 ns** |  **0.3975 ns** |     **baseline** | 
| LINQ_Double      | Scalar    | Double     | 100   |   252.748 ns |  2.8843 ns | 4.46x slower | 
| NetFabric_Double | Scalar    | Double     | 100   |    51.322 ns |  0.3848 ns | 1.10x faster | 
| Baseline_Double  | Vector128 | Double     | 100   |    55.207 ns |  0.5253 ns | 1.03x faster | 
| LINQ_Double      | Vector128 | Double     | 100   |   251.067 ns |  3.2227 ns | 4.43x slower | 
| NetFabric_Double | Vector128 | Double     | 100   |   116.847 ns |  0.9099 ns | 2.06x slower | 
| Baseline_Double  | Vector256 | Double     | 100   |    54.492 ns |  0.3282 ns | 1.04x faster | 
| LINQ_Double      | Vector256 | Double     | 100   |   252.558 ns |  3.1550 ns | 4.46x slower | 
| NetFabric_Double | Vector256 | Double     | 100   |    65.158 ns |  1.1544 ns | 1.15x slower | 
| Baseline_Double  | Vector512 | Double     | 100   |    55.194 ns |  0.4357 ns | 1.03x faster | 
| LINQ_Double      | Vector512 | Double     | 100   |   252.475 ns |  3.6423 ns | 4.46x slower | 
| NetFabric_Double | Vector512 | Double     | 100   |    66.788 ns |  0.6897 ns | 1.18x slower | 
|                  |           |            |       |              |            |              | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **5**     |     **2.939 ns** |  **0.0386 ns** |     **baseline** | 
| LINQ_Float       | Scalar    | Float      | 5     |    20.884 ns |  0.2785 ns | 7.11x slower | 
| NetFabric_Float  | Scalar    | Float      | 5     |     3.192 ns |  0.0497 ns | 1.08x slower | 
| Baseline_Float   | Vector128 | Float      | 5     |     3.496 ns |  0.1074 ns | 1.19x slower | 
| LINQ_Float       | Vector128 | Float      | 5     |    21.174 ns |  0.1969 ns | 7.20x slower | 
| NetFabric_Float  | Vector128 | Float      | 5     |    11.010 ns |  0.0779 ns | 3.75x slower | 
| Baseline_Float   | Vector256 | Float      | 5     |     3.456 ns |  0.1140 ns | 1.17x slower | 
| LINQ_Float       | Vector256 | Float      | 5     |    21.268 ns |  0.2210 ns | 7.24x slower | 
| NetFabric_Float  | Vector256 | Float      | 5     |    14.962 ns |  0.2333 ns | 5.10x slower | 
| Baseline_Float   | Vector512 | Float      | 5     |     3.532 ns |  0.0791 ns | 1.20x slower | 
| LINQ_Float       | Vector512 | Float      | 5     |    21.206 ns |  0.2426 ns | 7.22x slower | 
| NetFabric_Float  | Vector512 | Float      | 5     |    14.818 ns |  0.1472 ns | 5.04x slower | 
|                  |           |            |       |              |            |              | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **100**   |    **53.845 ns** |  **0.2755 ns** |     **baseline** | 
| LINQ_Float       | Scalar    | Float      | 100   |   250.110 ns |  4.6315 ns | 4.66x slower | 
| NetFabric_Float  | Scalar    | Float      | 100   |    51.884 ns |  0.4586 ns | 1.04x faster | 
| Baseline_Float   | Vector128 | Float      | 100   |    54.995 ns |  0.4232 ns | 1.02x slower | 
| LINQ_Float       | Vector128 | Float      | 100   |   250.124 ns |  3.7722 ns | 4.64x slower | 
| NetFabric_Float  | Vector128 | Float      | 100   |    54.762 ns |  0.5763 ns | 1.02x slower | 
| Baseline_Float   | Vector256 | Float      | 100   |    54.748 ns |  0.3695 ns | 1.02x slower | 
| LINQ_Float       | Vector256 | Float      | 100   |   249.798 ns |  3.4660 ns | 4.63x slower | 
| NetFabric_Float  | Vector256 | Float      | 100   |    38.760 ns |  0.4965 ns | 1.39x faster | 
| Baseline_Float   | Vector512 | Float      | 100   |    55.165 ns |  0.3124 ns | 1.02x slower | 
| LINQ_Float       | Vector512 | Float      | 100   |   250.597 ns |  4.7211 ns | 4.67x slower | 
| NetFabric_Float  | Vector512 | Float      | 100   |    39.318 ns |  0.2752 ns | 1.37x faster | 
|                  |           |            |       |              |            |              | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **5**     |   **132.762 ns** |  **1.9027 ns** |     **baseline** | 
| LINQ_Half        | Scalar    | Half       | 5     |   136.708 ns |  1.5709 ns | 1.03x slower | 
| NetFabric_Half   | Scalar    | Half       | 5     |   134.941 ns |  1.6633 ns | 1.02x slower | 
| Baseline_Half    | Vector128 | Half       | 5     |   119.963 ns |  1.8054 ns | 1.11x faster | 
| LINQ_Half        | Vector128 | Half       | 5     |   113.326 ns |  0.5877 ns | 1.17x faster | 
| NetFabric_Half   | Vector128 | Half       | 5     |   121.935 ns |  1.4467 ns | 1.09x faster | 
| Baseline_Half    | Vector256 | Half       | 5     |   119.663 ns |  1.5163 ns | 1.11x faster | 
| LINQ_Half        | Vector256 | Half       | 5     |   114.053 ns |  1.1180 ns | 1.16x faster | 
| NetFabric_Half   | Vector256 | Half       | 5     |   121.749 ns |  1.4069 ns | 1.09x faster | 
| Baseline_Half    | Vector512 | Half       | 5     |   119.006 ns |  1.5726 ns | 1.12x faster | 
| LINQ_Half        | Vector512 | Half       | 5     |   113.595 ns |  0.3653 ns | 1.17x faster | 
| NetFabric_Half   | Vector512 | Half       | 5     |   121.609 ns |  1.5715 ns | 1.09x faster | 
|                  |           |            |       |              |            |              | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **100**   | **2,692.931 ns** | **26.9663 ns** |     **baseline** | 
| LINQ_Half        | Scalar    | Half       | 100   | 2,332.548 ns | 30.3005 ns | 1.15x faster | 
| NetFabric_Half   | Scalar    | Half       | 100   | 2,714.001 ns | 27.4642 ns | 1.01x slower | 
| Baseline_Half    | Vector128 | Half       | 100   | 2,420.425 ns | 24.9648 ns | 1.11x faster | 
| LINQ_Half        | Vector128 | Half       | 100   | 1,931.986 ns | 22.7982 ns | 1.39x faster | 
| NetFabric_Half   | Vector128 | Half       | 100   | 2,454.254 ns | 31.0076 ns | 1.10x faster | 
| Baseline_Half    | Vector256 | Half       | 100   | 2,401.352 ns | 25.7421 ns | 1.12x faster | 
| LINQ_Half        | Vector256 | Half       | 100   | 1,939.409 ns | 22.8330 ns | 1.39x faster | 
| NetFabric_Half   | Vector256 | Half       | 100   | 2,447.118 ns | 18.7983 ns | 1.10x faster | 
| Baseline_Half    | Vector512 | Half       | 100   | 2,409.692 ns | 31.4590 ns | 1.12x faster | 
| LINQ_Half        | Vector512 | Half       | 100   | 1,944.026 ns | 25.3846 ns | 1.39x faster | 
| NetFabric_Half   | Vector512 | Half       | 100   | 2,443.154 ns | 21.1450 ns | 1.10x faster | 
|                  |           |            |       |              |            |              | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **5**     |     **3.147 ns** |  **0.0572 ns** |     **baseline** | 
| LINQ_Int         | Scalar    | Int        | 5     |    15.559 ns |  0.2210 ns | 4.95x slower | 
| NetFabric_Int    | Scalar    | Int        | 5     |     5.545 ns |  0.0385 ns | 1.76x slower | 
| Baseline_Int     | Vector128 | Int        | 5     |     3.205 ns |  0.0500 ns | 1.02x slower | 
| LINQ_Int         | Vector128 | Int        | 5     |    22.764 ns |  0.2175 ns | 7.23x slower | 
| NetFabric_Int    | Vector128 | Int        | 5     |    10.969 ns |  0.0889 ns | 3.49x slower | 
| Baseline_Int     | Vector256 | Int        | 5     |     3.138 ns |  0.0379 ns | 1.00x faster | 
| LINQ_Int         | Vector256 | Int        | 5     |    15.579 ns |  0.2599 ns | 4.95x slower | 
| NetFabric_Int    | Vector256 | Int        | 5     |    14.233 ns |  0.2525 ns | 4.52x slower | 
| Baseline_Int     | Vector512 | Int        | 5     |     3.212 ns |  0.0393 ns | 1.02x slower | 
| LINQ_Int         | Vector512 | Int        | 5     |    17.351 ns |  0.2223 ns | 5.52x slower | 
| NetFabric_Int    | Vector512 | Int        | 5     |    14.304 ns |  0.1875 ns | 4.55x slower | 
|                  |           |            |       |              |            |              | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **100**   |    **46.790 ns** |  **0.4434 ns** |     **baseline** | 
| LINQ_Int         | Scalar    | Int        | 100   |   168.746 ns |  1.5240 ns | 3.61x slower | 
| NetFabric_Int    | Scalar    | Int        | 100   |    45.121 ns |  0.6139 ns | 1.04x faster | 
| Baseline_Int     | Vector128 | Int        | 100   |    47.357 ns |  0.2387 ns | 1.01x slower | 
| LINQ_Int         | Vector128 | Int        | 100   |   169.378 ns |  1.4947 ns | 3.62x slower | 
| NetFabric_Int    | Vector128 | Int        | 100   |    52.015 ns |  0.6004 ns | 1.11x slower | 
| Baseline_Int     | Vector256 | Int        | 100   |    48.897 ns |  0.4448 ns | 1.05x slower | 
| LINQ_Int         | Vector256 | Int        | 100   |   168.963 ns |  1.5119 ns | 3.61x slower | 
| NetFabric_Int    | Vector256 | Int        | 100   |    36.208 ns |  0.4078 ns | 1.29x faster | 
| Baseline_Int     | Vector512 | Int        | 100   |    46.327 ns |  0.4008 ns | 1.01x faster | 
| LINQ_Int         | Vector512 | Int        | 100   |   170.040 ns |  2.0630 ns | 3.63x slower | 
| NetFabric_Int    | Vector512 | Int        | 100   |    37.620 ns |  0.3776 ns | 1.24x faster | 
|                  |           |            |       |              |            |              | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **5**     |     **3.196 ns** |  **0.0435 ns** |     **baseline** | 
| LINQ_Long        | Scalar    | Long       | 5     |    16.090 ns |  0.2331 ns | 5.03x slower | 
| NetFabric_Long   | Scalar    | Long       | 5     |     4.959 ns |  0.0433 ns | 1.55x slower | 
| Baseline_Long    | Vector128 | Long       | 5     |     3.164 ns |  0.0556 ns | 1.01x faster | 
| LINQ_Long        | Vector128 | Long       | 5     |    16.308 ns |  0.2484 ns | 5.11x slower | 
| NetFabric_Long   | Vector128 | Long       | 5     |    11.034 ns |  0.1004 ns | 3.45x slower | 
| Baseline_Long    | Vector256 | Long       | 5     |     3.155 ns |  0.0492 ns | 1.01x faster | 
| LINQ_Long        | Vector256 | Long       | 5     |    19.818 ns |  0.1861 ns | 6.21x slower | 
| NetFabric_Long   | Vector256 | Long       | 5     |    12.662 ns |  0.1215 ns | 3.96x slower | 
| Baseline_Long    | Vector512 | Long       | 5     |     3.153 ns |  0.0479 ns | 1.01x faster | 
| LINQ_Long        | Vector512 | Long       | 5     |    15.928 ns |  0.2184 ns | 4.98x slower | 
| NetFabric_Long   | Vector512 | Long       | 5     |    12.679 ns |  0.1328 ns | 3.97x slower | 
|                  |           |            |       |              |            |              | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **100**   |    **49.540 ns** |  **0.5214 ns** |     **baseline** | 
| LINQ_Long        | Scalar    | Long       | 100   |   176.245 ns |  1.9846 ns | 3.56x slower | 
| NetFabric_Long   | Scalar    | Long       | 100   |    45.064 ns |  0.4338 ns | 1.10x faster | 
| Baseline_Long    | Vector128 | Long       | 100   |    48.008 ns |  0.4888 ns | 1.03x faster | 
| LINQ_Long        | Vector128 | Long       | 100   |   182.327 ns |  1.9191 ns | 3.68x slower | 
| NetFabric_Long   | Vector128 | Long       | 100   |    99.610 ns |  0.7628 ns | 2.01x slower | 
| Baseline_Long    | Vector256 | Long       | 100   |    48.783 ns |  0.7101 ns | 1.02x faster | 
| LINQ_Long        | Vector256 | Long       | 100   |   175.985 ns |  1.7181 ns | 3.56x slower | 
| NetFabric_Long   | Vector256 | Long       | 100   |    60.374 ns |  0.7067 ns | 1.22x slower | 
| Baseline_Long    | Vector512 | Long       | 100   |    47.827 ns |  0.4379 ns | 1.04x faster | 
| LINQ_Long        | Vector512 | Long       | 100   |   177.156 ns |  1.5095 ns | 3.58x slower | 
| NetFabric_Long   | Vector512 | Long       | 100   |    57.064 ns |  0.4262 ns | 1.15x slower | 
|                  |           |            |       |              |            |              | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **5**     |     **5.271 ns** |  **0.0841 ns** |     **baseline** | 
| LINQ_Short       | Scalar    | Short      | 5     |    19.565 ns |  0.2131 ns | 3.71x slower | 
| NetFabric_Short  | Scalar    | Short      | 5     |     7.915 ns |  0.1193 ns | 1.50x slower | 
| Baseline_Short   | Vector128 | Short      | 5     |     5.342 ns |  0.1161 ns | 1.01x slower | 
| LINQ_Short       | Vector128 | Short      | 5     |    19.708 ns |  0.1657 ns | 3.74x slower | 
| NetFabric_Short  | Vector128 | Short      | 5     |    19.994 ns |  0.1837 ns | 3.79x slower | 
| Baseline_Short   | Vector256 | Short      | 5     |     5.582 ns |  0.1310 ns | 1.06x slower | 
| LINQ_Short       | Vector256 | Short      | 5     |    19.620 ns |  0.3269 ns | 3.72x slower | 
| NetFabric_Short  | Vector256 | Short      | 5     |     7.883 ns |  0.1266 ns | 1.50x slower | 
| Baseline_Short   | Vector512 | Short      | 5     |     5.289 ns |  0.0905 ns | 1.00x slower | 
| LINQ_Short       | Vector512 | Short      | 5     |    19.660 ns |  0.1503 ns | 3.73x slower | 
| NetFabric_Short  | Vector512 | Short      | 5     |     7.890 ns |  0.0985 ns | 1.50x slower | 
|                  |           |            |       |              |            |              | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **100**   |    **91.972 ns** |  **0.8489 ns** |     **baseline** | 
| LINQ_Short       | Scalar    | Short      | 100   |   232.785 ns |  2.8185 ns | 2.53x slower | 
| NetFabric_Short  | Scalar    | Short      | 100   |    74.560 ns |  0.8245 ns | 1.23x faster | 
| Baseline_Short   | Vector128 | Short      | 100   |    91.827 ns |  0.8719 ns | 1.00x faster | 
| LINQ_Short       | Vector128 | Short      | 100   |   230.814 ns |  1.9601 ns | 2.51x slower | 
| NetFabric_Short  | Vector128 | Short      | 100   |    41.231 ns |  0.4107 ns | 2.23x faster | 
| Baseline_Short   | Vector256 | Short      | 100   |    90.880 ns |  1.0636 ns | 1.01x faster | 
| LINQ_Short       | Vector256 | Short      | 100   |   229.896 ns |  2.2885 ns | 2.50x slower | 
| NetFabric_Short  | Vector256 | Short      | 100   |    39.088 ns |  0.2440 ns | 2.35x faster | 
| Baseline_Short   | Vector512 | Short      | 100   |    91.767 ns |  1.2460 ns | 1.00x faster | 
| LINQ_Short       | Vector512 | Short      | 100   |   232.236 ns |  3.1004 ns | 2.53x slower | 
| NetFabric_Short  | Vector512 | Short      | 100   |    39.559 ns |  0.2941 ns | 2.32x faster | 

### Sum4D

Applying a vectorizable aggregation operator on a span with four contiguos values for each element.

It also compares to the performance of LINQ's `Aggregate()`, as LINQ's `Sum()` does not support non-native numeric types.

| Method           | Job       | Categories | Count | Mean         | StdDev     | Ratio         | 
|----------------- |---------- |----------- |------ |-------------:|-----------:|--------------:|-
| **Baseline_Double**  | **Scalar**    | **Double**     | **5**     |     **3.989 ns** |  **0.0843 ns** |      **baseline** | 
| LINQ_Double      | Scalar    | Double     | 5     |    22.398 ns |  0.2316 ns |  5.62x slower | 
| NetFabric_Double | Scalar    | Double     | 5     |     4.043 ns |  0.0499 ns |  1.01x slower | 
| Baseline_Double  | Vector128 | Double     | 5     |     3.948 ns |  0.0619 ns |  1.01x faster | 
| LINQ_Double      | Vector128 | Double     | 5     |    22.895 ns |  0.2247 ns |  5.74x slower | 
| NetFabric_Double | Vector128 | Double     | 5     |     3.731 ns |  0.0646 ns |  1.07x faster | 
| Baseline_Double  | Vector256 | Double     | 5     |     3.955 ns |  0.0392 ns |  1.01x faster | 
| LINQ_Double      | Vector256 | Double     | 5     |    23.072 ns |  0.2567 ns |  5.79x slower | 
| NetFabric_Double | Vector256 | Double     | 5     |     4.882 ns |  0.0549 ns |  1.22x slower | 
| Baseline_Double  | Vector512 | Double     | 5     |     3.730 ns |  0.0382 ns |  1.07x faster | 
| LINQ_Double      | Vector512 | Double     | 5     |    22.870 ns |  0.2571 ns |  5.74x slower | 
| NetFabric_Double | Vector512 | Double     | 5     |     4.961 ns |  0.0703 ns |  1.24x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Double**  | **Scalar**    | **Double**     | **100**   |    **59.333 ns** |  **0.8969 ns** |      **baseline** | 
| LINQ_Double      | Scalar    | Double     | 100   |   257.306 ns |  3.5363 ns |  4.34x slower | 
| NetFabric_Double | Scalar    | Double     | 100   |    55.695 ns |  0.4376 ns |  1.07x faster | 
| Baseline_Double  | Vector128 | Double     | 100   |    59.477 ns |  0.9375 ns |  1.00x slower | 
| LINQ_Double      | Vector128 | Double     | 100   |   256.488 ns |  2.2597 ns |  4.32x slower | 
| NetFabric_Double | Vector128 | Double     | 100   |    55.744 ns |  0.3417 ns |  1.06x faster | 
| Baseline_Double  | Vector256 | Double     | 100   |    59.255 ns |  0.8276 ns |  1.00x faster | 
| LINQ_Double      | Vector256 | Double     | 100   |   256.065 ns |  2.6444 ns |  4.32x slower | 
| NetFabric_Double | Vector256 | Double     | 100   |    43.986 ns |  0.3865 ns |  1.35x faster | 
| Baseline_Double  | Vector512 | Double     | 100   |    60.000 ns |  0.9160 ns |  1.01x slower | 
| LINQ_Double      | Vector512 | Double     | 100   |   255.457 ns |  2.9969 ns |  4.31x slower | 
| NetFabric_Double | Vector512 | Double     | 100   |    44.730 ns |  0.5025 ns |  1.33x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **5**     |     **4.063 ns** |  **0.0996 ns** |      **baseline** | 
| LINQ_Float       | Scalar    | Float      | 5     |    22.475 ns |  0.2193 ns |  5.53x slower | 
| NetFabric_Float  | Scalar    | Float      | 5     |     3.952 ns |  0.0511 ns |  1.03x faster | 
| Baseline_Float   | Vector128 | Float      | 5     |     3.921 ns |  0.0254 ns |  1.03x faster | 
| LINQ_Float       | Vector128 | Float      | 5     |    22.479 ns |  0.2480 ns |  5.54x slower | 
| NetFabric_Float  | Vector128 | Float      | 5     |     4.394 ns |  0.0417 ns |  1.08x slower | 
| Baseline_Float   | Vector256 | Float      | 5     |     3.898 ns |  0.0538 ns |  1.04x faster | 
| LINQ_Float       | Vector256 | Float      | 5     |    22.597 ns |  0.1965 ns |  5.56x slower | 
| NetFabric_Float  | Vector256 | Float      | 5     |     5.534 ns |  0.0387 ns |  1.36x slower | 
| Baseline_Float   | Vector512 | Float      | 5     |     3.731 ns |  0.0438 ns |  1.09x faster | 
| LINQ_Float       | Vector512 | Float      | 5     |    22.379 ns |  0.1031 ns |  5.51x slower | 
| NetFabric_Float  | Vector512 | Float      | 5     |     6.046 ns |  0.0456 ns |  1.49x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **100**   |    **59.274 ns** |  **0.7406 ns** |      **baseline** | 
| LINQ_Float       | Scalar    | Float      | 100   |   254.065 ns |  3.0303 ns |  4.29x slower | 
| NetFabric_Float  | Scalar    | Float      | 100   |    55.889 ns |  0.4679 ns |  1.06x faster | 
| Baseline_Float   | Vector128 | Float      | 100   |    59.794 ns |  0.8374 ns |  1.01x slower | 
| LINQ_Float       | Vector128 | Float      | 100   |   252.669 ns |  2.6128 ns |  4.26x slower | 
| NetFabric_Float  | Vector128 | Float      | 100   |    43.873 ns |  0.4147 ns |  1.35x faster | 
| Baseline_Float   | Vector256 | Float      | 100   |    59.430 ns |  0.7000 ns |  1.00x slower | 
| LINQ_Float       | Vector256 | Float      | 100   |   253.993 ns |  3.7535 ns |  4.29x slower | 
| NetFabric_Float  | Vector256 | Float      | 100   |    23.352 ns |  0.1609 ns |  2.54x faster | 
| Baseline_Float   | Vector512 | Float      | 100   |    59.989 ns |  0.9477 ns |  1.01x slower | 
| LINQ_Float       | Vector512 | Float      | 100   |   252.504 ns |  2.9619 ns |  4.26x slower | 
| NetFabric_Float  | Vector512 | Float      | 100   |    23.534 ns |  0.0939 ns |  2.52x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **5**     |   **159.247 ns** |  **1.6140 ns** |      **baseline** | 
| LINQ_Half        | Scalar    | Half       | 5     |   180.823 ns |  1.9210 ns |  1.14x slower | 
| NetFabric_Half   | Scalar    | Half       | 5     |   185.576 ns |  1.2357 ns |  1.17x slower | 
| Baseline_Half    | Vector128 | Half       | 5     |   127.551 ns |  1.0307 ns |  1.25x faster | 
| LINQ_Half        | Vector128 | Half       | 5     |   152.643 ns |  1.6865 ns |  1.04x faster | 
| NetFabric_Half   | Vector128 | Half       | 5     |   169.626 ns |  0.3886 ns |  1.07x slower | 
| Baseline_Half    | Vector256 | Half       | 5     |   127.024 ns |  1.3688 ns |  1.25x faster | 
| LINQ_Half        | Vector256 | Half       | 5     |   152.803 ns |  1.6964 ns |  1.04x faster | 
| NetFabric_Half   | Vector256 | Half       | 5     |   171.238 ns |  1.3846 ns |  1.08x slower | 
| Baseline_Half    | Vector512 | Half       | 5     |   127.533 ns |  1.5699 ns |  1.25x faster | 
| LINQ_Half        | Vector512 | Half       | 5     |   151.688 ns |  1.6220 ns |  1.05x faster | 
| NetFabric_Half   | Vector512 | Half       | 5     |   170.873 ns |  1.7805 ns |  1.07x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **100**   | **3,147.153 ns** | **30.2891 ns** |      **baseline** | 
| LINQ_Half        | Scalar    | Half       | 100   | 3,235.904 ns | 26.2941 ns |  1.03x slower | 
| NetFabric_Half   | Scalar    | Half       | 100   | 3,617.294 ns | 26.0972 ns |  1.15x slower | 
| Baseline_Half    | Vector128 | Half       | 100   | 2,502.145 ns | 24.9056 ns |  1.26x faster | 
| LINQ_Half        | Vector128 | Half       | 100   | 2,625.870 ns | 38.9347 ns |  1.20x faster | 
| NetFabric_Half   | Vector128 | Half       | 100   | 3,281.982 ns | 24.6957 ns |  1.04x slower | 
| Baseline_Half    | Vector256 | Half       | 100   | 2,503.050 ns | 30.9673 ns |  1.26x faster | 
| LINQ_Half        | Vector256 | Half       | 100   | 2,630.446 ns | 32.7372 ns |  1.20x faster | 
| NetFabric_Half   | Vector256 | Half       | 100   | 3,292.924 ns | 19.1968 ns |  1.05x slower | 
| Baseline_Half    | Vector512 | Half       | 100   | 2,504.199 ns | 31.5805 ns |  1.26x faster | 
| LINQ_Half        | Vector512 | Half       | 100   | 2,627.510 ns | 27.2529 ns |  1.20x faster | 
| NetFabric_Half   | Vector512 | Half       | 100   | 3,289.336 ns | 23.8176 ns |  1.05x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **5**     |     **3.915 ns** |  **0.0502 ns** |      **baseline** | 
| LINQ_Int         | Scalar    | Int        | 5     |    19.641 ns |  0.1702 ns |  5.02x slower | 
| NetFabric_Int    | Scalar    | Int        | 5     |     6.105 ns |  0.0486 ns |  1.56x slower | 
| Baseline_Int     | Vector128 | Int        | 5     |     3.879 ns |  0.0596 ns |  1.01x faster | 
| LINQ_Int         | Vector128 | Int        | 5     |    19.979 ns |  0.1710 ns |  5.11x slower | 
| NetFabric_Int    | Vector128 | Int        | 5     |     5.220 ns |  0.0542 ns |  1.33x slower | 
| Baseline_Int     | Vector256 | Int        | 5     |     3.898 ns |  0.0570 ns |  1.00x faster | 
| LINQ_Int         | Vector256 | Int        | 5     |    19.800 ns |  0.1970 ns |  5.06x slower | 
| NetFabric_Int    | Vector256 | Int        | 5     |     6.089 ns |  0.0502 ns |  1.56x slower | 
| Baseline_Int     | Vector512 | Int        | 5     |     3.892 ns |  0.0358 ns |  1.01x faster | 
| LINQ_Int         | Vector512 | Int        | 5     |    19.895 ns |  0.2119 ns |  5.08x slower | 
| NetFabric_Int    | Vector512 | Int        | 5     |     6.068 ns |  0.0881 ns |  1.55x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **100**   |    **55.098 ns** |  **0.4043 ns** |      **baseline** | 
| LINQ_Int         | Scalar    | Int        | 100   | 1,520.113 ns |  2.7779 ns | 27.61x slower | 
| NetFabric_Int    | Scalar    | Int        | 100   |    45.852 ns |  0.4517 ns |  1.20x faster | 
| Baseline_Int     | Vector128 | Int        | 100   |    55.961 ns |  0.4151 ns |  1.02x slower | 
| LINQ_Int         | Vector128 | Int        | 100   |   212.836 ns |  1.8863 ns |  3.86x slower | 
| NetFabric_Int    | Vector128 | Int        | 100   |    29.887 ns |  0.4425 ns |  1.84x faster | 
| Baseline_Int     | Vector256 | Int        | 100   |    57.904 ns |  0.4287 ns |  1.05x slower | 
| LINQ_Int         | Vector256 | Int        | 100   |   213.580 ns |  1.0298 ns |  3.88x slower | 
| NetFabric_Int    | Vector256 | Int        | 100   |    16.156 ns |  0.1745 ns |  3.41x faster | 
| Baseline_Int     | Vector512 | Int        | 100   |    56.187 ns |  0.4792 ns |  1.02x slower | 
| LINQ_Int         | Vector512 | Int        | 100   |   215.168 ns |  1.6038 ns |  3.91x slower | 
| NetFabric_Int    | Vector512 | Int        | 100   |    16.816 ns |  0.2483 ns |  3.28x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **5**     |     **4.159 ns** |  **0.0907 ns** |      **baseline** | 
| LINQ_Long        | Scalar    | Long       | 5     |    19.455 ns |  0.2083 ns |  4.68x slower | 
| NetFabric_Long   | Scalar    | Long       | 5     |     5.716 ns |  0.0524 ns |  1.38x slower | 
| Baseline_Long    | Vector128 | Long       | 5     |     4.242 ns |  0.1260 ns |  1.02x slower | 
| LINQ_Long        | Vector128 | Long       | 5     |    20.765 ns |  0.1428 ns |  5.00x slower | 
| NetFabric_Long   | Vector128 | Long       | 5     |     5.699 ns |  0.0785 ns |  1.37x slower | 
| Baseline_Long    | Vector256 | Long       | 5     |     4.286 ns |  0.1058 ns |  1.03x slower | 
| LINQ_Long        | Vector256 | Long       | 5     |    20.365 ns |  0.1832 ns |  4.90x slower | 
| NetFabric_Long   | Vector256 | Long       | 5     |     4.862 ns |  0.0533 ns |  1.17x slower | 
| Baseline_Long    | Vector512 | Long       | 5     |     4.211 ns |  0.1423 ns |  1.01x slower | 
| LINQ_Long        | Vector512 | Long       | 5     |    20.478 ns |  0.2161 ns |  4.93x slower | 
| NetFabric_Long   | Vector512 | Long       | 5     |     5.211 ns |  0.0495 ns |  1.25x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **100**   |    **56.028 ns** |  **0.4481 ns** |      **baseline** | 
| LINQ_Long        | Scalar    | Long       | 100   |   215.218 ns |  1.4632 ns |  3.84x slower | 
| NetFabric_Long   | Scalar    | Long       | 100   |    45.994 ns |  0.4188 ns |  1.22x faster | 
| Baseline_Long    | Vector128 | Long       | 100   |    56.371 ns |  0.4895 ns |  1.01x slower | 
| LINQ_Long        | Vector128 | Long       | 100   |   222.084 ns |  2.3440 ns |  3.96x slower | 
| NetFabric_Long   | Vector128 | Long       | 100   |    46.782 ns |  0.3441 ns |  1.20x faster | 
| Baseline_Long    | Vector256 | Long       | 100   |    56.409 ns |  0.5419 ns |  1.01x slower | 
| LINQ_Long        | Vector256 | Long       | 100   |   219.398 ns |  1.9919 ns |  3.92x slower | 
| NetFabric_Long   | Vector256 | Long       | 100   |    30.488 ns |  0.3631 ns |  1.84x faster | 
| Baseline_Long    | Vector512 | Long       | 100   |    56.151 ns |  0.5253 ns |  1.00x slower | 
| LINQ_Long        | Vector512 | Long       | 100   |   216.762 ns |  1.9311 ns |  3.87x slower | 
| NetFabric_Long   | Vector512 | Long       | 100   |    30.267 ns |  0.7674 ns |  1.84x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **5**     |     **6.684 ns** |  **0.0712 ns** |      **baseline** | 
| LINQ_Short       | Scalar    | Short      | 5     |    16.098 ns |  0.1998 ns |  2.41x slower | 
| NetFabric_Short  | Scalar    | Short      | 5     |    11.451 ns |  0.1148 ns |  1.71x slower | 
| Baseline_Short   | Vector128 | Short      | 5     |     6.688 ns |  0.1155 ns |  1.00x faster | 
| LINQ_Short       | Vector128 | Short      | 5     |    16.711 ns |  0.1527 ns |  2.50x slower | 
| NetFabric_Short  | Vector128 | Short      | 5     |    11.630 ns |  0.1160 ns |  1.74x slower | 
| Baseline_Short   | Vector256 | Short      | 5     |     6.735 ns |  0.1610 ns |  1.01x slower | 
| LINQ_Short       | Vector256 | Short      | 5     |    16.097 ns |  0.2203 ns |  2.41x slower | 
| NetFabric_Short  | Vector256 | Short      | 5     |    16.306 ns |  0.1432 ns |  2.44x slower | 
| Baseline_Short   | Vector512 | Short      | 5     |     6.785 ns |  0.1216 ns |  1.02x slower | 
| LINQ_Short       | Vector512 | Short      | 5     |    16.193 ns |  0.1960 ns |  2.42x slower | 
| NetFabric_Short  | Vector512 | Short      | 5     |    13.428 ns |  0.1841 ns |  2.01x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **100**   |    **83.946 ns** |  **0.1653 ns** |      **baseline** | 
| LINQ_Short       | Scalar    | Short      | 100   |   152.619 ns |  0.7118 ns |  1.82x slower | 
| NetFabric_Short  | Scalar    | Short      | 100   |    92.683 ns |  0.9002 ns |  1.11x slower | 
| Baseline_Short   | Vector128 | Short      | 100   |    84.631 ns |  0.5212 ns |  1.01x slower | 
| LINQ_Short       | Vector128 | Short      | 100   |   153.149 ns |  2.1305 ns |  1.82x slower | 
| NetFabric_Short  | Vector128 | Short      | 100   |    18.404 ns |  0.2247 ns |  4.57x faster | 
| Baseline_Short   | Vector256 | Short      | 100   |    84.276 ns |  0.7965 ns |  1.00x slower | 
| LINQ_Short       | Vector256 | Short      | 100   |   153.505 ns |  1.6243 ns |  1.83x slower | 
| NetFabric_Short  | Vector256 | Short      | 100   |    15.865 ns |  0.1760 ns |  5.28x faster | 
| Baseline_Short   | Vector512 | Short      | 100   |    84.162 ns |  0.6864 ns |  1.00x slower | 
| LINQ_Short       | Vector512 | Short      | 100   |   151.946 ns |  1.7003 ns |  1.81x slower | 
| NetFabric_Short  | Vector512 | Short      | 100   |    16.156 ns |  0.1573 ns |  5.19x faster | 

### Min aggregation

Applying a vectorizable aggregation operator with propagation os `NaN`.

| Method           | Job       | Categories | Count | Mean         | StdDev     | Ratio         | 
|----------------- |---------- |----------- |------ |-------------:|-----------:|--------------:|-
| **Baseline_Double**  | **Scalar**    | **Double**     | **5**     |     **2.714 ns** |  **0.0571 ns** |      **baseline** | 
| LINQ_Double      | Scalar    | Double     | 5     |     3.683 ns |  0.0531 ns |  1.36x slower | 
| System_Double    | Scalar    | Double     | 5     |     3.331 ns |  0.0564 ns |  1.23x slower | 
| NetFabric_Double | Scalar    | Double     | 5     |     6.321 ns |  0.0589 ns |  2.33x slower | 
| Baseline_Double  | Vector128 | Double     | 5     |     2.707 ns |  0.0549 ns |  1.00x faster | 
| LINQ_Double      | Vector128 | Double     | 5     |     4.111 ns |  0.0475 ns |  1.52x slower | 
| System_Double    | Vector128 | Double     | 5     |     3.096 ns |  0.0422 ns |  1.14x slower | 
| NetFabric_Double | Vector128 | Double     | 5     |     5.460 ns |  0.0729 ns |  2.01x slower | 
| Baseline_Double  | Vector256 | Double     | 5     |     2.674 ns |  0.0507 ns |  1.02x faster | 
| LINQ_Double      | Vector256 | Double     | 5     |     4.134 ns |  0.0575 ns |  1.52x slower | 
| System_Double    | Vector256 | Double     | 5     |     3.095 ns |  0.0652 ns |  1.14x slower | 
| NetFabric_Double | Vector256 | Double     | 5     |     7.594 ns |  0.1172 ns |  2.80x slower | 
| Baseline_Double  | Vector512 | Double     | 5     |     2.670 ns |  0.0479 ns |  1.02x faster | 
| LINQ_Double      | Vector512 | Double     | 5     |     4.172 ns |  0.0434 ns |  1.54x slower | 
| System_Double    | Vector512 | Double     | 5     |     3.263 ns |  0.0403 ns |  1.20x slower | 
| NetFabric_Double | Vector512 | Double     | 5     |     3.091 ns |  0.0520 ns |  1.14x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Double**  | **Scalar**    | **Double**     | **100**   |    **45.734 ns** |  **0.5004 ns** |      **baseline** | 
| LINQ_Double      | Scalar    | Double     | 100   |    56.100 ns |  0.8296 ns |  1.23x slower | 
| System_Double    | Scalar    | Double     | 100   |    67.678 ns |  0.8804 ns |  1.48x slower | 
| NetFabric_Double | Scalar    | Double     | 100   |   124.766 ns |  1.7506 ns |  2.73x slower | 
| Baseline_Double  | Vector128 | Double     | 100   |    45.540 ns |  0.5628 ns |  1.00x faster | 
| LINQ_Double      | Vector128 | Double     | 100   |    55.704 ns |  0.6003 ns |  1.22x slower | 
| System_Double    | Vector128 | Double     | 100   |    32.960 ns |  0.2493 ns |  1.39x faster | 
| NetFabric_Double | Vector128 | Double     | 100   |    19.390 ns |  0.8415 ns |  2.39x faster | 
| Baseline_Double  | Vector256 | Double     | 100   |    45.621 ns |  0.4190 ns |  1.00x faster | 
| LINQ_Double      | Vector256 | Double     | 100   |    55.737 ns |  0.3995 ns |  1.22x slower | 
| System_Double    | Vector256 | Double     | 100   |    17.207 ns |  0.2165 ns |  2.66x faster | 
| NetFabric_Double | Vector256 | Double     | 100   |    13.864 ns |  0.1846 ns |  3.30x faster | 
| Baseline_Double  | Vector512 | Double     | 100   |    45.684 ns |  0.5813 ns |  1.00x faster | 
| LINQ_Double      | Vector512 | Double     | 100   |    55.369 ns |  0.3870 ns |  1.21x slower | 
| System_Double    | Vector512 | Double     | 100   |    25.392 ns |  0.2662 ns |  1.80x faster | 
| NetFabric_Double | Vector512 | Double     | 100   |    11.660 ns |  0.0952 ns |  3.92x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **5**     |     **2.721 ns** |  **0.0558 ns** |      **baseline** | 
| LINQ_Float       | Scalar    | Float      | 5     |     3.631 ns |  0.0431 ns |  1.33x slower | 
| System_Float     | Scalar    | Float      | 5     |     3.288 ns |  0.0524 ns |  1.21x slower | 
| NetFabric_Float  | Scalar    | Float      | 5     |     6.769 ns |  0.1446 ns |  2.49x slower | 
| Baseline_Float   | Vector128 | Float      | 5     |     2.682 ns |  0.0555 ns |  1.01x faster | 
| LINQ_Float       | Vector128 | Float      | 5     |     3.974 ns |  0.0622 ns |  1.46x slower | 
| System_Float     | Vector128 | Float      | 5     |     3.083 ns |  0.0459 ns |  1.13x slower | 
| NetFabric_Float  | Vector128 | Float      | 5     |     7.013 ns |  0.1032 ns |  2.58x slower | 
| Baseline_Float   | Vector256 | Float      | 5     |     2.691 ns |  0.0574 ns |  1.01x faster | 
| LINQ_Float       | Vector256 | Float      | 5     |     4.014 ns |  0.0589 ns |  1.48x slower | 
| System_Float     | Vector256 | Float      | 5     |     3.088 ns |  0.0658 ns |  1.14x slower | 
| NetFabric_Float  | Vector256 | Float      | 5     |     4.469 ns |  0.0519 ns |  1.64x slower | 
| Baseline_Float   | Vector512 | Float      | 5     |     2.668 ns |  0.0516 ns |  1.02x faster | 
| LINQ_Float       | Vector512 | Float      | 5     |     4.008 ns |  0.0478 ns |  1.47x slower | 
| System_Float     | Vector512 | Float      | 5     |     3.277 ns |  0.0718 ns |  1.20x slower | 
| NetFabric_Float  | Vector512 | Float      | 5     |     3.507 ns |  0.0569 ns |  1.29x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **100**   |    **45.358 ns** |  **0.3314 ns** |      **baseline** | 
| LINQ_Float       | Scalar    | Float      | 100   |    54.798 ns |  0.4113 ns |  1.21x slower | 
| System_Float     | Scalar    | Float      | 100   |    67.184 ns |  0.8467 ns |  1.48x slower | 
| NetFabric_Float  | Scalar    | Float      | 100   |   124.937 ns |  1.9066 ns |  2.75x slower | 
| Baseline_Float   | Vector128 | Float      | 100   |    45.140 ns |  0.4192 ns |  1.00x faster | 
| LINQ_Float       | Vector128 | Float      | 100   |    55.245 ns |  0.3858 ns |  1.22x slower | 
| System_Float     | Vector128 | Float      | 100   |    16.125 ns |  0.2141 ns |  2.81x faster | 
| NetFabric_Float  | Vector128 | Float      | 100   |    15.315 ns |  0.2792 ns |  2.96x faster | 
| Baseline_Float   | Vector256 | Float      | 100   |    45.368 ns |  0.3134 ns |  1.00x slower | 
| LINQ_Float       | Vector256 | Float      | 100   |    55.506 ns |  0.4596 ns |  1.22x slower | 
| System_Float     | Vector256 | Float      | 100   |     9.398 ns |  0.1077 ns |  4.83x faster | 
| NetFabric_Float  | Vector256 | Float      | 100   |    19.820 ns |  0.2481 ns |  2.29x faster | 
| Baseline_Float   | Vector512 | Float      | 100   |    45.518 ns |  0.3767 ns |  1.00x slower | 
| LINQ_Float       | Vector512 | Float      | 100   |    55.228 ns |  0.3628 ns |  1.22x slower | 
| System_Float     | Vector512 | Float      | 100   |    13.452 ns |  0.0940 ns |  3.37x faster | 
| NetFabric_Float  | Vector512 | Float      | 100   |    10.422 ns |  0.0967 ns |  4.35x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **5**     |     **6.659 ns** |  **0.1153 ns** |      **baseline** | 
| LINQ_Half        | Scalar    | Half       | 5     |    60.419 ns |  0.8637 ns |  9.08x slower | 
| System_Half      | Scalar    | Half       | 5     |     8.703 ns |  0.1039 ns |  1.31x slower | 
| NetFabric_Half   | Scalar    | Half       | 5     |    11.407 ns |  0.1123 ns |  1.71x slower | 
| Baseline_Half    | Vector128 | Half       | 5     |     6.699 ns |  0.1107 ns |  1.01x slower | 
| LINQ_Half        | Vector128 | Half       | 5     |    54.129 ns |  0.4949 ns |  8.13x slower | 
| System_Half      | Vector128 | Half       | 5     |     8.308 ns |  0.1052 ns |  1.25x slower | 
| NetFabric_Half   | Vector128 | Half       | 5     |    10.839 ns |  0.1191 ns |  1.63x slower | 
| Baseline_Half    | Vector256 | Half       | 5     |     6.635 ns |  0.0809 ns |  1.00x faster | 
| LINQ_Half        | Vector256 | Half       | 5     |    54.327 ns |  0.3909 ns |  8.16x slower | 
| System_Half      | Vector256 | Half       | 5     |     8.670 ns |  0.0883 ns |  1.30x slower | 
| NetFabric_Half   | Vector256 | Half       | 5     |    10.658 ns |  0.0985 ns |  1.60x slower | 
| Baseline_Half    | Vector512 | Half       | 5     |     6.638 ns |  0.0852 ns |  1.00x faster | 
| LINQ_Half        | Vector512 | Half       | 5     |    54.324 ns |  0.3308 ns |  8.18x slower | 
| System_Half      | Vector512 | Half       | 5     |     8.238 ns |  0.1002 ns |  1.24x slower | 
| NetFabric_Half   | Vector512 | Half       | 5     |    10.873 ns |  0.1131 ns |  1.63x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **100**   |   **115.248 ns** |  **0.7373 ns** |      **baseline** | 
| LINQ_Half        | Scalar    | Half       | 100   | 1,249.269 ns | 15.4456 ns | 10.84x slower | 
| System_Half      | Scalar    | Half       | 100   |   160.668 ns |  1.0151 ns |  1.39x slower | 
| NetFabric_Half   | Scalar    | Half       | 100   |   215.217 ns |  3.1507 ns |  1.87x slower | 
| Baseline_Half    | Vector128 | Half       | 100   |   115.154 ns |  0.8110 ns |  1.00x faster | 
| LINQ_Half        | Vector128 | Half       | 100   | 1,204.076 ns |  9.8682 ns | 10.45x slower | 
| System_Half      | Vector128 | Half       | 100   |   160.727 ns |  1.8351 ns |  1.39x slower | 
| NetFabric_Half   | Vector128 | Half       | 100   |   232.907 ns |  2.3087 ns |  2.02x slower | 
| Baseline_Half    | Vector256 | Half       | 100   |   114.830 ns |  0.4893 ns |  1.00x faster | 
| LINQ_Half        | Vector256 | Half       | 100   | 1,213.700 ns | 14.7370 ns | 10.53x slower | 
| System_Half      | Vector256 | Half       | 100   |   161.999 ns |  1.6736 ns |  1.41x slower | 
| NetFabric_Half   | Vector256 | Half       | 100   |   221.540 ns |  2.5442 ns |  1.92x slower | 
| Baseline_Half    | Vector512 | Half       | 100   |   114.789 ns |  0.9889 ns |  1.00x faster | 
| LINQ_Half        | Vector512 | Half       | 100   | 1,212.176 ns | 11.9613 ns | 10.52x slower | 
| System_Half      | Vector512 | Half       | 100   |   160.739 ns |  1.4989 ns |  1.39x slower | 
| NetFabric_Half   | Vector512 | Half       | 100   |   223.126 ns |  2.6790 ns |  1.94x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **5**     |     **2.184 ns** |  **0.0183 ns** |      **baseline** | 
| LINQ_Int         | Scalar    | Int        | 5     |     4.669 ns |  0.0843 ns |  2.14x slower | 
| System_Int       | Scalar    | Int        | 5     |     3.019 ns |  0.1531 ns |  1.41x slower | 
| NetFabric_Int    | Scalar    | Int        | 5     |     2.830 ns |  0.0801 ns |  1.30x slower | 
| Baseline_Int     | Vector128 | Int        | 5     |     2.200 ns |  0.0450 ns |  1.01x slower | 
| LINQ_Int         | Vector128 | Int        | 5     |     3.177 ns |  0.0543 ns |  1.46x slower | 
| System_Int       | Vector128 | Int        | 5     |     1.509 ns |  0.0267 ns |  1.45x faster | 
| NetFabric_Int    | Vector128 | Int        | 5     |     2.297 ns |  0.0275 ns |  1.05x slower | 
| Baseline_Int     | Vector256 | Int        | 5     |     2.372 ns |  0.0230 ns |  1.09x slower | 
| LINQ_Int         | Vector256 | Int        | 5     |     3.294 ns |  0.0717 ns |  1.50x slower | 
| System_Int       | Vector256 | Int        | 5     |     1.722 ns |  0.0249 ns |  1.27x faster | 
| NetFabric_Int    | Vector256 | Int        | 5     |     3.358 ns |  0.1025 ns |  1.55x slower | 
| Baseline_Int     | Vector512 | Int        | 5     |     2.373 ns |  0.0342 ns |  1.09x slower | 
| LINQ_Int         | Vector512 | Int        | 5     |     3.258 ns |  0.0632 ns |  1.49x slower | 
| System_Int       | Vector512 | Int        | 5     |     1.738 ns |  0.0386 ns |  1.26x faster | 
| NetFabric_Int    | Vector512 | Int        | 5     |     3.326 ns |  0.0872 ns |  1.53x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **100**   |    **30.486 ns** |  **0.5180 ns** |      **baseline** | 
| LINQ_Int         | Scalar    | Int        | 100   |    47.103 ns |  0.9046 ns |  1.54x slower | 
| System_Int       | Scalar    | Int        | 100   |    31.460 ns |  0.6028 ns |  1.03x slower | 
| NetFabric_Int    | Scalar    | Int        | 100   |    30.507 ns |  0.3771 ns |  1.00x slower | 
| Baseline_Int     | Vector128 | Int        | 100   |    30.172 ns |  0.5997 ns |  1.01x faster | 
| LINQ_Int         | Vector128 | Int        | 100   |     7.026 ns |  0.0289 ns |  4.35x faster | 
| System_Int       | Vector128 | Int        | 100   |     5.892 ns |  0.0582 ns |  5.17x faster | 
| NetFabric_Int    | Vector128 | Int        | 100   |    11.354 ns |  0.1354 ns |  2.69x faster | 
| Baseline_Int     | Vector256 | Int        | 100   |    30.435 ns |  0.3395 ns |  1.00x faster | 
| LINQ_Int         | Vector256 | Int        | 100   |     8.000 ns |  0.0922 ns |  3.81x faster | 
| System_Int       | Vector256 | Int        | 100   |     3.517 ns |  0.0532 ns |  8.67x faster | 
| NetFabric_Int    | Vector256 | Int        | 100   |     8.298 ns |  0.2166 ns |  3.67x faster | 
| Baseline_Int     | Vector512 | Int        | 100   |    30.650 ns |  0.6316 ns |  1.01x slower | 
| LINQ_Int         | Vector512 | Int        | 100   |     8.051 ns |  0.1120 ns |  3.79x faster | 
| System_Int       | Vector512 | Int        | 100   |     2.527 ns |  0.0342 ns | 12.07x faster | 
| NetFabric_Int    | Vector512 | Int        | 100   |     9.299 ns |  0.1374 ns |  3.28x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **5**     |     **2.374 ns** |  **0.0230 ns** |      **baseline** | 
| LINQ_Long        | Scalar    | Long       | 5     |     4.644 ns |  0.0535 ns |  1.96x slower | 
| System_Long      | Scalar    | Long       | 5     |     3.208 ns |  0.0791 ns |  1.35x slower | 
| NetFabric_Long   | Scalar    | Long       | 5     |     2.864 ns |  0.0735 ns |  1.21x slower | 
| Baseline_Long    | Vector128 | Long       | 5     |     2.172 ns |  0.0307 ns |  1.09x faster | 
| LINQ_Long        | Vector128 | Long       | 5     |     2.267 ns |  0.0437 ns |  1.05x faster | 
| System_Long      | Vector128 | Long       | 5     |     1.729 ns |  0.0218 ns |  1.37x faster | 
| NetFabric_Long   | Vector128 | Long       | 5     |     2.514 ns |  0.0301 ns |  1.06x slower | 
| Baseline_Long    | Vector256 | Long       | 5     |     2.368 ns |  0.0240 ns |  1.00x faster | 
| LINQ_Long        | Vector256 | Long       | 5     |     3.387 ns |  0.0522 ns |  1.43x slower | 
| System_Long      | Vector256 | Long       | 5     |     1.714 ns |  0.0328 ns |  1.39x faster | 
| NetFabric_Long   | Vector256 | Long       | 5     |     2.472 ns |  0.0283 ns |  1.04x slower | 
| Baseline_Long    | Vector512 | Long       | 5     |     2.369 ns |  0.0276 ns |  1.00x faster | 
| LINQ_Long        | Vector512 | Long       | 5     |     3.394 ns |  0.0582 ns |  1.43x slower | 
| System_Long      | Vector512 | Long       | 5     |     1.709 ns |  0.0291 ns |  1.39x faster | 
| NetFabric_Long   | Vector512 | Long       | 5     |     2.513 ns |  0.0250 ns |  1.06x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **100**   |    **30.224 ns** |  **0.3970 ns** |      **baseline** | 
| LINQ_Long        | Scalar    | Long       | 100   |    54.290 ns |  0.4733 ns |  1.80x slower | 
| System_Long      | Scalar    | Long       | 100   |    31.327 ns |  0.5587 ns |  1.04x slower | 
| NetFabric_Long   | Scalar    | Long       | 100   |    30.396 ns |  0.4123 ns |  1.01x slower | 
| Baseline_Long    | Vector128 | Long       | 100   |    30.175 ns |  0.4289 ns |  1.00x faster | 
| LINQ_Long        | Vector128 | Long       | 100   |    22.631 ns |  0.1840 ns |  1.33x faster | 
| System_Long      | Vector128 | Long       | 100   |    17.896 ns |  0.2661 ns |  1.69x faster | 
| NetFabric_Long   | Vector128 | Long       | 100   |    23.509 ns |  0.5163 ns |  1.29x faster | 
| Baseline_Long    | Vector256 | Long       | 100   |    30.535 ns |  0.5294 ns |  1.01x slower | 
| LINQ_Long        | Vector256 | Long       | 100   |    10.115 ns |  0.0867 ns |  2.99x faster | 
| System_Long      | Vector256 | Long       | 100   |     7.862 ns |  0.1121 ns |  3.84x faster | 
| NetFabric_Long   | Vector256 | Long       | 100   |    11.655 ns |  0.1151 ns |  2.59x faster | 
| Baseline_Long    | Vector512 | Long       | 100   |    30.019 ns |  0.3931 ns |  1.01x faster | 
| LINQ_Long        | Vector512 | Long       | 100   |     7.814 ns |  0.0946 ns |  3.87x faster | 
| System_Long      | Vector512 | Long       | 100   |     5.512 ns |  0.0551 ns |  5.48x faster | 
| NetFabric_Long   | Vector512 | Long       | 100   |    11.473 ns |  0.1245 ns |  2.63x faster | 
|                  |           |            |       |              |            |               | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **5**     |     **2.394 ns** |  **0.0268 ns** |      **baseline** | 
| LINQ_Short       | Scalar    | Short      | 5     |    10.229 ns |  0.0927 ns |  4.27x slower | 
| System_Short     | Scalar    | Short      | 5     |     3.214 ns |  0.0498 ns |  1.34x slower | 
| NetFabric_Short  | Scalar    | Short      | 5     |     2.633 ns |  0.0301 ns |  1.10x slower | 
| Baseline_Short   | Vector128 | Short      | 5     |     2.376 ns |  0.0259 ns |  1.01x faster | 
| LINQ_Short       | Vector128 | Short      | 5     |    10.210 ns |  0.0962 ns |  4.27x slower | 
| System_Short     | Vector128 | Short      | 5     |     2.677 ns |  0.0587 ns |  1.12x slower | 
| NetFabric_Short  | Vector128 | Short      | 5     |     3.039 ns |  0.1403 ns |  1.28x slower | 
| Baseline_Short   | Vector256 | Short      | 5     |     1.989 ns |  0.0418 ns |  1.20x faster | 
| LINQ_Short       | Vector256 | Short      | 5     |    10.264 ns |  0.1746 ns |  4.29x slower | 
| System_Short     | Vector256 | Short      | 5     |     2.769 ns |  0.0432 ns |  1.16x slower | 
| NetFabric_Short  | Vector256 | Short      | 5     |     3.242 ns |  0.0872 ns |  1.36x slower | 
| Baseline_Short   | Vector512 | Short      | 5     |     1.988 ns |  0.0206 ns |  1.20x faster | 
| LINQ_Short       | Vector512 | Short      | 5     |    10.222 ns |  0.0815 ns |  4.27x slower | 
| System_Short     | Vector512 | Short      | 5     |     3.110 ns |  0.0542 ns |  1.30x slower | 
| NetFabric_Short  | Vector512 | Short      | 5     |     3.202 ns |  0.0376 ns |  1.34x slower | 
|                  |           |            |       |              |            |               | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **100**   |    **29.735 ns** |  **0.1886 ns** |      **baseline** | 
| LINQ_Short       | Scalar    | Short      | 100   |    85.664 ns |  0.6717 ns |  2.88x slower | 
| System_Short     | Scalar    | Short      | 100   |    35.702 ns |  0.2059 ns |  1.20x slower | 
| NetFabric_Short  | Scalar    | Short      | 100   |    34.066 ns |  0.3936 ns |  1.15x slower | 
| Baseline_Short   | Vector128 | Short      | 100   |    30.261 ns |  0.3249 ns |  1.02x slower | 
| LINQ_Short       | Vector128 | Short      | 100   |    83.408 ns |  0.6449 ns |  2.81x slower | 
| System_Short     | Vector128 | Short      | 100   |     3.183 ns |  0.0327 ns |  9.34x faster | 
| NetFabric_Short  | Vector128 | Short      | 100   |     8.321 ns |  0.0219 ns |  3.57x faster | 
| Baseline_Short   | Vector256 | Short      | 100   |    30.061 ns |  0.3098 ns |  1.01x slower | 
| LINQ_Short       | Vector256 | Short      | 100   |    85.039 ns |  0.1828 ns |  2.86x slower | 
| System_Short     | Vector256 | Short      | 100   |     2.482 ns |  0.0084 ns | 11.98x faster | 
| NetFabric_Short  | Vector256 | Short      | 100   |    11.898 ns |  0.2177 ns |  2.50x faster | 
| Baseline_Short   | Vector512 | Short      | 100   |    30.795 ns |  0.4463 ns |  1.04x slower | 
| LINQ_Short       | Vector512 | Short      | 100   |    85.403 ns |  0.1864 ns |  2.87x slower | 
| System_Short     | Vector512 | Short      | 100   |     2.423 ns |  0.0192 ns | 12.27x faster | 
| NetFabric_Short  | Vector512 | Short      | 100   |    11.605 ns |  0.0874 ns |  2.56x faster | 

### MinMax

Applying two vectorizable aggregation operators on a single iteration of a span.

| Method           | Job       | Categories | Count | Mean       | StdDev    | Ratio        | 
|----------------- |---------- |----------- |------ |-----------:|----------:|-------------:|-
| **Baseline_Double**  | **Scalar**    | **Double**     | **5**     |   **3.820 ns** | **0.0581 ns** |     **baseline** | 
| NetFabric_Double | Scalar    | Double     | 5     |  12.503 ns | 0.2343 ns | 3.27x slower | 
| Baseline_Double  | Vector128 | Double     | 5     |   4.037 ns | 0.1729 ns | 1.05x slower | 
| NetFabric_Double | Vector128 | Double     | 5     |   9.554 ns | 0.1084 ns | 2.50x slower | 
| Baseline_Double  | Vector256 | Double     | 5     |   4.050 ns | 0.1762 ns | 1.06x slower | 
| NetFabric_Double | Vector256 | Double     | 5     |  15.131 ns | 0.2355 ns | 3.96x slower | 
| Baseline_Double  | Vector512 | Double     | 5     |   3.847 ns | 0.1344 ns | 1.01x slower | 
| NetFabric_Double | Vector512 | Double     | 5     |   5.761 ns | 0.0547 ns | 1.51x slower | 
|                  |           |            |       |            |           |              | 
| **Baseline_Double**  | **Scalar**    | **Double**     | **100**   |  **69.785 ns** | **0.8833 ns** |     **baseline** | 
| NetFabric_Double | Scalar    | Double     | 100   | 251.982 ns | 2.9541 ns | 3.61x slower | 
| Baseline_Double  | Vector128 | Double     | 100   |  69.322 ns | 0.9702 ns | 1.01x faster | 
| NetFabric_Double | Vector128 | Double     | 100   |  24.379 ns | 0.2482 ns | 2.87x faster | 
| Baseline_Double  | Vector256 | Double     | 100   |  69.105 ns | 0.9018 ns | 1.01x faster | 
| NetFabric_Double | Vector256 | Double     | 100   |  20.444 ns | 0.1999 ns | 3.41x faster | 
| Baseline_Double  | Vector512 | Double     | 100   |  69.594 ns | 1.0375 ns | 1.00x faster | 
| NetFabric_Double | Vector512 | Double     | 100   |  16.744 ns | 0.1764 ns | 4.17x faster | 
|                  |           |            |       |            |           |              | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **5**     |   **6.973 ns** | **0.0567 ns** |     **baseline** | 
| NetFabric_Float  | Scalar    | Float      | 5     |  13.106 ns | 0.1060 ns | 1.88x slower | 
| Baseline_Float   | Vector128 | Float      | 5     |   7.041 ns | 0.0761 ns | 1.01x slower | 
| NetFabric_Float  | Vector128 | Float      | 5     |  13.256 ns | 0.0907 ns | 1.90x slower | 
| Baseline_Float   | Vector256 | Float      | 5     |   7.058 ns | 0.0592 ns | 1.01x slower | 
| NetFabric_Float  | Vector256 | Float      | 5     |   8.632 ns | 0.1108 ns | 1.24x slower | 
| Baseline_Float   | Vector512 | Float      | 5     |   7.101 ns | 0.1055 ns | 1.02x slower | 
| NetFabric_Float  | Vector512 | Float      | 5     |   7.734 ns | 0.0974 ns | 1.11x slower | 
|                  |           |            |       |            |           |              | 
| **Baseline_Float**   | **Scalar**    | **Float**      | **100**   |  **71.261 ns** | **0.8181 ns** |     **baseline** | 
| NetFabric_Float  | Scalar    | Float      | 100   | 229.300 ns | 1.8454 ns | 3.22x slower | 
| Baseline_Float   | Vector128 | Float      | 100   |  71.156 ns | 1.0325 ns | 1.00x faster | 
| NetFabric_Float  | Vector128 | Float      | 100   |  18.872 ns | 0.1965 ns | 3.78x faster | 
| Baseline_Float   | Vector256 | Float      | 100   |  71.525 ns | 1.8220 ns | 1.01x slower | 
| NetFabric_Float  | Vector256 | Float      | 100   |  37.166 ns | 0.3463 ns | 1.92x faster | 
| Baseline_Float   | Vector512 | Float      | 100   |  72.193 ns | 1.8333 ns | 1.01x slower | 
| NetFabric_Float  | Vector512 | Float      | 100   |  15.992 ns | 0.2247 ns | 4.46x faster | 
|                  |           |            |       |            |           |              | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **5**     |  **13.013 ns** | **0.1057 ns** |     **baseline** | 
| NetFabric_Half   | Scalar    | Half       | 5     |  20.594 ns | 0.2817 ns | 1.58x slower | 
| Baseline_Half    | Vector128 | Half       | 5     |  10.250 ns | 0.0655 ns | 1.27x faster | 
| NetFabric_Half   | Vector128 | Half       | 5     |  21.024 ns | 0.2037 ns | 1.61x slower | 
| Baseline_Half    | Vector256 | Half       | 5     |  10.207 ns | 0.1460 ns | 1.28x faster | 
| NetFabric_Half   | Vector256 | Half       | 5     |  20.954 ns | 0.2077 ns | 1.61x slower | 
| Baseline_Half    | Vector512 | Half       | 5     |  10.207 ns | 0.1152 ns | 1.27x faster | 
| NetFabric_Half   | Vector512 | Half       | 5     |  20.915 ns | 0.2388 ns | 1.61x slower | 
|                  |           |            |       |            |           |              | 
| **Baseline_Half**    | **Scalar**    | **Half**       | **100**   | **180.443 ns** | **1.8815 ns** |     **baseline** | 
| NetFabric_Half   | Scalar    | Half       | 100   | 410.504 ns | 4.9683 ns | 2.28x slower | 
| Baseline_Half    | Vector128 | Half       | 100   | 178.944 ns | 1.5151 ns | 1.01x faster | 
| NetFabric_Half   | Vector128 | Half       | 100   | 398.002 ns | 7.4502 ns | 2.21x slower | 
| Baseline_Half    | Vector256 | Half       | 100   | 177.041 ns | 1.6286 ns | 1.02x faster | 
| NetFabric_Half   | Vector256 | Half       | 100   | 416.582 ns | 2.7951 ns | 2.31x slower | 
| Baseline_Half    | Vector512 | Half       | 100   | 178.148 ns | 2.1589 ns | 1.01x faster | 
| NetFabric_Half   | Vector512 | Half       | 100   | 417.356 ns | 4.2927 ns | 2.31x slower | 
|                  |           |            |       |            |           |              | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **5**     |   **4.621 ns** | **0.0468 ns** |     **baseline** | 
| NetFabric_Int    | Scalar    | Int        | 5     |   5.056 ns | 0.0478 ns | 1.09x slower | 
| Baseline_Int     | Vector128 | Int        | 5     |   4.614 ns | 0.0500 ns | 1.00x faster | 
| NetFabric_Int    | Vector128 | Int        | 5     |   5.432 ns | 0.0599 ns | 1.18x slower | 
| Baseline_Int     | Vector256 | Int        | 5     |   4.637 ns | 0.0512 ns | 1.00x slower | 
| NetFabric_Int    | Vector256 | Int        | 5     |   5.624 ns | 0.0415 ns | 1.22x slower | 
| Baseline_Int     | Vector512 | Int        | 5     |   4.652 ns | 0.0569 ns | 1.01x slower | 
| NetFabric_Int    | Vector512 | Int        | 5     |   5.805 ns | 0.0759 ns | 1.26x slower | 
|                  |           |            |       |            |           |              | 
| **Baseline_Int**     | **Scalar**    | **Int**        | **100**   |  **52.382 ns** | **0.4315 ns** |     **baseline** | 
| NetFabric_Int    | Scalar    | Int        | 100   |  49.749 ns | 0.5626 ns | 1.05x faster | 
| Baseline_Int     | Vector128 | Int        | 100   |  50.583 ns | 0.3878 ns | 1.04x faster | 
| NetFabric_Int    | Vector128 | Int        | 100   |  12.661 ns | 0.0886 ns | 4.14x faster | 
| Baseline_Int     | Vector256 | Int        | 100   |  52.205 ns | 0.3619 ns | 1.00x faster | 
| NetFabric_Int    | Vector256 | Int        | 100   |  12.110 ns | 0.0817 ns | 4.33x faster | 
| Baseline_Int     | Vector512 | Int        | 100   |  51.903 ns | 0.5573 ns | 1.01x faster | 
| NetFabric_Int    | Vector512 | Int        | 100   |  12.959 ns | 0.0623 ns | 4.04x faster | 
|                  |           |            |       |            |           |              | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **5**     |   **2.729 ns** | **0.0511 ns** |     **baseline** | 
| NetFabric_Long   | Scalar    | Long       | 5     |   3.599 ns | 0.0585 ns | 1.32x slower | 
| Baseline_Long    | Vector128 | Long       | 5     |   2.788 ns | 0.0896 ns | 1.02x slower | 
| NetFabric_Long   | Vector128 | Long       | 5     |   3.491 ns | 0.0464 ns | 1.28x slower | 
| Baseline_Long    | Vector256 | Long       | 5     |   2.670 ns | 0.0464 ns | 1.02x faster | 
| NetFabric_Long   | Vector256 | Long       | 5     |   3.900 ns | 0.0554 ns | 1.43x slower | 
| Baseline_Long    | Vector512 | Long       | 5     |   2.673 ns | 0.0501 ns | 1.02x faster | 
| NetFabric_Long   | Vector512 | Long       | 5     |   3.901 ns | 0.0587 ns | 1.43x slower | 
|                  |           |            |       |            |           |              | 
| **Baseline_Long**    | **Scalar**    | **Long**       | **100**   |  **51.029 ns** | **0.6483 ns** |     **baseline** | 
| NetFabric_Long   | Scalar    | Long       | 100   |  53.578 ns | 2.1685 ns | 1.07x slower | 
| Baseline_Long    | Vector128 | Long       | 100   |  51.774 ns | 0.3891 ns | 1.02x slower | 
| NetFabric_Long   | Vector128 | Long       | 100   |  28.310 ns | 0.4426 ns | 1.80x faster | 
| Baseline_Long    | Vector256 | Long       | 100   |  50.755 ns | 0.3600 ns | 1.01x faster | 
| NetFabric_Long   | Vector256 | Long       | 100   |  15.001 ns | 0.1627 ns | 3.40x faster | 
| Baseline_Long    | Vector512 | Long       | 100   |  50.433 ns | 0.3332 ns | 1.01x faster | 
| NetFabric_Long   | Vector512 | Long       | 100   |  13.658 ns | 0.0695 ns | 3.73x faster | 
|                  |           |            |       |            |           |              | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **5**     |   **4.661 ns** | **0.0549 ns** |     **baseline** | 
| NetFabric_Short  | Scalar    | Short      | 5     |   5.053 ns | 0.0514 ns | 1.08x slower | 
| Baseline_Short   | Vector128 | Short      | 5     |   4.667 ns | 0.0594 ns | 1.00x slower | 
| NetFabric_Short  | Vector128 | Short      | 5     |   5.693 ns | 0.0453 ns | 1.22x slower | 
| Baseline_Short   | Vector256 | Short      | 5     |   4.619 ns | 0.0484 ns | 1.01x faster | 
| NetFabric_Short  | Vector256 | Short      | 5     |   5.873 ns | 0.0616 ns | 1.26x slower | 
| Baseline_Short   | Vector512 | Short      | 5     |   4.610 ns | 0.0380 ns | 1.01x faster | 
| NetFabric_Short  | Vector512 | Short      | 5     |   6.064 ns | 0.0659 ns | 1.30x slower | 
|                  |           |            |       |            |           |              | 
| **Baseline_Short**   | **Scalar**    | **Short**      | **100**   |  **51.788 ns** | **0.3288 ns** |     **baseline** | 
| NetFabric_Short  | Scalar    | Short      | 100   |  50.966 ns | 0.4569 ns | 1.02x faster | 
| Baseline_Short   | Vector128 | Short      | 100   |  52.112 ns | 0.2561 ns | 1.01x slower | 
| NetFabric_Short  | Vector128 | Short      | 100   |  13.847 ns | 0.2160 ns | 3.74x faster | 
| Baseline_Short   | Vector256 | Short      | 100   |  50.209 ns | 0.4874 ns | 1.03x faster | 
| NetFabric_Short  | Vector256 | Short      | 100   |  18.806 ns | 0.1857 ns | 2.75x faster | 
| Baseline_Short   | Vector512 | Short      | 100   |  50.148 ns | 0.4757 ns | 1.03x faster | 
| NetFabric_Short  | Vector512 | Short      | 100   |  18.758 ns | 0.2627 ns | 2.76x faster | 

