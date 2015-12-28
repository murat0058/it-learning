namespace ITLearning.Shared.Formatters
{
    public static class UrlFormatter
    {
        public static string FormatSourceControlUrl(string repositoryName)
        {
            return "http://localhost:2214/" + repositoryName + ".git";
        }
    }
}