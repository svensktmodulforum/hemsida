namespace Tellurian.Website.Extensions;

public static class StringExtensions
{
    public static bool HasValue(this string? valueOrEmpty) => !string.IsNullOrEmpty(valueOrEmpty);
}
