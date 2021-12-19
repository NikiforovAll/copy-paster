using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NikiforovAll.CopyPaster.Commands;
using NikiforovAll.CopyPaster.Services;
using Serilog;

var runner = BuildCommandLine()
    .UseHost(_ => CreateHostBuilder(args), (builder) => builder
        .UseSerilog()
        .ConfigureServices((hostContext, services) =>
        {
            services.AddHttpClient<IGithubCodeDownloader, GitHubCodeDownloader>();
            services.AddSingleton(new FileSaver());
        })
        .UseDefaultServiceProvider((context, options) =>
        {
            options.ValidateScopes = true;
        })
        .UseCommandHandler<DownloadFromGithub, DownloadFromGithub.Handler>())
        .UseDefaults().Build();

return await runner.InvokeAsync(args);

static CommandLineBuilder BuildCommandLine()
{
    var root = new DownloadFromGithub();
    root.AddGlobalOption(new Option<bool>("--dry-run", "Dry run"));
    root.AddGlobalOption(new Option<bool>("--raw", "Raw. No panel"));

    return new CommandLineBuilder(root);
}

static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args);

