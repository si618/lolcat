try
{
    BenchmarkRunner.Run<Benchmarks>();

    Report.Cleanup();

    return 0;
}
catch (Exception ex)
{
    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);

    return -99;
}