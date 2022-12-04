public static class Routing
{
    public static class Things
    {
        public const string Page = "/things";

        public const string Details = Page + "/{id}/details";

        public static string GetDetails(string id)
            => string.Format(Details.Replace("{id}", "{0}"), id);
    }

    public static class Correspondent
    {
        public const string Page = "/correspondents";

        public const string Add = Page + "/add";
    }

    public static class Connectors
    {
        public const string Page = "/connectors";

        public const string Add = Page + "/add";
    }

    public static class JobDefintions
    {
        public const string Page = "/jobdefinitions";

        public const string Add = Page + "/add";

        public const string Edit = Page + "/{id}/edit";

        public static string GetEdit(string id)
            => string.Format(Edit.Replace("{id}", "{0}"), id);
    }

    public static class Credentials
    {
        public const string Page = "/credentials";

        public const string Add = Page + "/add";

        public const string Edit = Page + "/{id}/edit";

        public static string GetEdit(string id)
            => string.Format(Edit.Replace("{id}", "{0}"), id);
    }
}
