namespace Lolcat.Benchmarks;

public static class Report
{
    internal static void Cleanup()
    {
        string report = Path.Combine(
            "./BenchmarkDotNet.Artifacts/results",
            "Lolcat.Benchmarks.MarkupBenchmarks-report.json");

        if (!File.Exists(report))
        {
            throw new FileNotFoundException($"Report not found '{report}'");
        }

        JsonNode node = JsonNode.Parse(File.ReadAllText(report))!;

        foreach (JsonNode? benchmark in node["Benchmarks"]!.AsArray())
        {
            benchmark!["FullName"] = $"{benchmark["Method"]}";
        }

        File.WriteAllText(report, node.ToString());
    }
}
