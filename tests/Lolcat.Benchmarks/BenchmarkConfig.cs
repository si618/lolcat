namespace Lolcat.Benchmarks;

using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;

public sealed class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddDiagnoser(MemoryDiagnoser.Default);

        AddJob(Job.Default.WithRuntime(CoreRuntime.Core60));

        AddColumn(StatisticColumn.OperationsPerSecond);
    }
}
