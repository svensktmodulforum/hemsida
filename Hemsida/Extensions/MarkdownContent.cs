namespace Tellurian.Website.Extensions;

public record MarkdownContent(string? Text, string Type, DateTimeOffset LastModified)
{
    public string AsHtml => Text.HtmlFromMarkdown();
    public static MarkdownContent Empty => new(string.Empty, string.Empty, DateTimeOffset.MinValue);

}
