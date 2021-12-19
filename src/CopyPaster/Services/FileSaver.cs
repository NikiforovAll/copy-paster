namespace NikiforovAll.CopyPaster.Services;

using System.IO.Abstractions;

public class FileSaver
{
    private readonly IFileSystem fileSystem;

    public FileSaver(IFileSystem fileSystem) =>
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));

    public FileSaver() : this(fileSystem: new FileSystem())
    {
    }

    public async Task Save(
        string path,
        string content,
        CancellationToken cancellationToken = default) =>
            await this.fileSystem.File.WriteAllTextAsync(path, content, cancellationToken);
}
