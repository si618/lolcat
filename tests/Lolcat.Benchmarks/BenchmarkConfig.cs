namespace Lolcat.Benchmarks;

public sealed class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddColumn(StatisticColumn.OperationsPerSecond);

        AddDiagnoser(MemoryDiagnoser.Default);

        AddExporter(new JsonExporter());

        AddJob(Job.Default.WithRuntime(CoreRuntime.Core10_0));
    }
}
