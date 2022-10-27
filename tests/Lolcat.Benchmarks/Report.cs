namespace Lolcat.Benchmarks;

public static class Report
{
    internal static void Cleanup()
    {
        var report = Path.Combine(
            "./BenchmarkDotNet.Artifacts/results",
            "Lolcat.Benchmarks.Benchmarks-report.json");

        if (!File.Exists(report))
        {
            throw new FileNotFoundException($"Report not found '{report}'");
        }

        var node = JsonNode.Parse(File.ReadAllText(report))!;

        foreach (var benchmark in node["Benchmarks"]!.AsArray())
        {
            benchmark!["FullName"] = $"{benchmark["Method"]}";
        }

        File.WriteAllText(report, node.ToString());
    }
}
