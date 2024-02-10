using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using System.Runtime.Intrinsics;

class VectorizationConfig : ManualConfig
{
    public VectorizationConfig()
    {
        _ = WithSummaryStyle(SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend));
        _ = HideColumns(Column.EnvironmentVariables, Column.RatioSD, Column.Error);
        _ = AddJob(Job.Default.WithId("Scalar")
            .WithEnvironmentVariable("DOTNET_EnableHWIntrinsic", "0")
            .AsBaseline());
        if (Vector128.IsHardwareAccelerated)
        {
            _ = AddJob(Job.Default.WithId("Vector128")
                    .WithEnvironmentVariable("DOTNET_EnableAVX2", "0")
                    .WithEnvironmentVariable("DOTNET_EnableAVX512F", "0"));
        }
        if (Vector256.IsHardwareAccelerated)
        {
            _ = AddJob(Job.Default.WithId("Vector256")
                .WithEnvironmentVariable("DOTNET_EnableAVX512F", "0"));
        }
        if (Vector512.IsHardwareAccelerated)
        {
            _ = AddJob(Job.Default.WithId("Vector512"));
        }
    }
}