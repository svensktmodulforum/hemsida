namespace Tellurian.Website.Extensions;

public class MarkdownLoader
{
    private readonly string MarkdownPath = "content/markdown";
    private readonly IHttpClientFactory ClientFactory;
    public MarkdownLoader(IHttpClientFactory clientFactory, string? markdownPath = null)
    {
        ClientFactory = clientFactory;
        MarkdownPath = markdownPath ?? MarkdownPath;
    }
    public async Task<MarkdownContent> GetLocalMarkdownContentAsync(string contentName, string? twoLetterISOLanguageName = null) =>
        await GetLocalMarkdownAsync(MarkdownPath, contentName, twoLetterISOLanguageName).ConfigureAwait(false);


    public async Task<MarkdownContent> GetRemoteMarkdownContentAsync(string href)
    {
        using var client = ClientFactory.CreateClient();
        string markdown = await client.GetStringAsync(href).ConfigureAwait(false) ?? string.Empty;
        return new MarkdownContent(markdown, "MD", DateTimeOffset.Now);
    }

    private async static Task<MarkdownContent> GetLocalMarkdownAsync(string path, string content, string? twoLetterISOLanguageName = null)
    {
        if (!string.IsNullOrWhiteSpace(twoLetterISOLanguageName))
        {
            var specificCultureFile = new FileInfo($"{path}/{content}.{twoLetterISOLanguageName}.md");
            if (specificCultureFile.Exists) return new MarkdownContent(await File.ReadAllTextAsync(specificCultureFile.FullName), "MD", specificCultureFile.LastWriteTimeUtc);
        }
        var defaultCultureFile = new FileInfo($"{path}/{content}.md");
        if (defaultCultureFile.Exists) return new MarkdownContent(await File.ReadAllTextAsync(defaultCultureFile.FullName), "MD", defaultCultureFile.LastWriteTimeUtc);
        return new MarkdownContent(string.Empty, "MD", DateTimeOffset.Now);
    }
}

