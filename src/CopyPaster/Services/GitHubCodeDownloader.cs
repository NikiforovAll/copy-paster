namespace NikiforovAll.CopyPaster.Services;

using System.Net;
using Flurl;
using Spectre.Console;

public class GitHubCodeDownloader : IGithubCodeDownloader
{
    private readonly HttpClient httpClient;

    public GitHubCodeDownloader(HttpClient httpClient) =>
        this.httpClient = httpClient
            ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<string?> Download(string url)
    {
        var u = new Url(url.Replace("/blob", string.Empty));

        if (u.Host.Equals(GitHubConstants.GITHUB_BASE_URL, StringComparison.OrdinalIgnoreCase))
        {
            u = Url.Combine(GitHubConstants.Https + GitHubConstants.GITHUB_RAWCONTENT_BASE_URL, u.Path);
        }

        try
        {
            var content = await this.httpClient.GetStringAsync(u);
            return content;
        }
        catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            AnsiConsole.WriteLine(e.Message);
        }
        return default;
    }
}

public interface IGithubCodeDownloader
{
    Task<string?> Download(string url);
}
