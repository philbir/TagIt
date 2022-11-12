namespace TagIt.Tests;

public static class TestSources
{
    public static class Pdf
    {
        public static Stream Simple2Page => GetData("001_Simple_2_Page.pdf");
    }

    public static class Image
    {
        public static Stream SmallPng => GetData("010_Small.png");
    }

    private static Stream GetData(string name)
    {
        FileStream stream = new FileStream($"{GetTestSourcePath()}/{name}",
            FileMode.Open);

        return stream;
    }

    private static string GetTestSourcePath()
    {
        string[] segments = typeof(TestSources).Assembly.Location.Split(Path.DirectorySeparatorChar);
        var idx = Array.IndexOf(segments, "Core.Tests");
        var root = string.Join(Path.DirectorySeparatorChar, segments.Take(idx + 1));

        return Path.Join(root, "__test_sources__");
    }
}
