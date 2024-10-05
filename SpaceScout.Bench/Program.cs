using Utils;

namespace SpaceScout.Bench;

internal class Program {
    private static void Main() {
        Logger.SetDefault();
        Logger.WriteToConsoleAsWell = false;
        Logger.IsDebugEnabled = true;
        Console.WriteLine($"Logger is set to {Logger.LogPath}");
        var path = "/Users/aikoradlingmayr"; // Change this to the directory you want to analyze
        var analyzer = new Analyzer.Analyzer(path);
        analyzer.Analyze();
        Console.WriteLine($"File count: {analyzer.FileCount}");
        Console.WriteLine($"Duration: {analyzer.Duration}");
        File.WriteAllText("/Users/aikoradlingmayr/temp/output.txt", analyzer.ToString());
    }
}