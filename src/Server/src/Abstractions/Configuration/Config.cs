namespace TagIt;

public class Config
{
    public static string Section = "TagIt";

    public static class Storage
    {
        public static string Database =
            $"{Section}:{nameof(Storage)}:{nameof(Database)}";
    }

    public static string Pdf = $"{Section}:{nameof(Pdf)}";

    public static string Messaging = $"{Section}:{nameof(Messaging)}";
}
