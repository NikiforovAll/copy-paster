namespace NikiforovAll.CopyPaster.Commands;

using System.CommandLine;
using System.CommandLine.Invocation;
using Flurl;
using NikiforovAll.CopyPaster.Services;
using Spectre.Console;

public class DownloadFromGithub : RootCommand
{
    public DownloadFromGithub()
        : base("Copies file by url")
    {
        this.AddArgument(new Argument<string>("url", "url"));
        this.AddOption(new Option<string>(
            new string[] { "--output", "-o" }, "Output file path"));
    }

    public new class Handler : ICommandHandler
    {
        private readonly IGithubCodeDownloader codeDownloader;
        private readonly FileSaver fileSaver;

        public Handler(IGithubCodeDownloader codeDownloader, FileSaver fileSaver)
        {
            this.codeDownloader = codeDownloader
                ?? throw new ArgumentNullException(nameof(codeDownloader));
            this.fileSaver = fileSaver
                ?? throw new ArgumentNullException(nameof(fileSaver));
        }

        public string? Url { get; set; }
        public string? Output { get; set; }

        public bool DryRun { get; set; }

        public bool Raw { get; set; }

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            if (this.Url is null)
            {
                return -1;
            }
            try
            {
                await AnsiConsole.Status()
                    .AutoRefresh(true)
                    .Spinner(Spinner.Known.Ascii)
                    .SpinnerStyle(Style.Parse("green bold"))
                    .Start("Thinking...", async _ =>
                    {
                        var content = await this.codeDownloader.Download(this.Url);
                        var u = new Url(this.Url);
                        if (content is null)
                        {
                            return;
                        }
                        if (this.DryRun && !this.Raw)
                        {
                            var panel = new Panel(content.EscapeMarkup())
                            {
                                Border = BoxBorder.Rounded,
                                Header = new PanelHeader($"{u.Path}", Justify.Right),
                            };
                            AnsiConsole.Write(panel);
                        }
                        else if (this.DryRun)
                        {
                            Console.WriteLine(content);
                        }
                        else
                        {
                            var output = this.Output ??
                                u.Path.Split('/', StringSplitOptions.RemoveEmptyEntries)
                                .Last();
                            await this.fileSaver.Save(output, content);
                        }
                    });
            }
            catch (Exception ex)
            {
                const string message =
                    "Please contact source code for more details" +
                    ". Contribution is welcomed." +
                    "https://github.com/NikiforovAll/copy-paster";
                AnsiConsole.WriteLine(message);
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                return -1;
            }

            return 0;
        }
    }
}
