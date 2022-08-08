using Markdig;

namespace Tellurian.Website.Extensions;

public static class MarkdownExtensions
{
    public static string HtmlFromMarkdown(this string? markdown)
    {
        if (string.IsNullOrWhiteSpace(markdown)) return string.Empty;
        return Markdown.ToHtml(markdown, Pipeline());

        static MarkdownPipeline Pipeline() =>
            new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Build();
    }
}
