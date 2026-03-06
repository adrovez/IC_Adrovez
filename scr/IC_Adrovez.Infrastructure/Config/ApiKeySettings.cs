namespace IC_Adrovez.Infrastructure.Config
{
    public class ApiKeySettings
    {
        public const string SectionName = "Security";
        public string ApiKey { get; set; } = string.Empty;
        public string HeaderName { get; set; } = "X-API-KEY";
    }
}
