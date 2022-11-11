namespace TagIt.UI;

public class UIOptions
{
    public ApiOptions Api { get; set; }
}


public class ApiOptions
{
    public Uri Url { get; set; }

    public Uri WebSocketUrl { get; set; }
}
