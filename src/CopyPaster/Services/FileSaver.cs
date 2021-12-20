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

    public async Task<bool> Save(
        string path,
        string content,
        bool force = false,
        CancellationToken cancellationToken = default)
    {
        var exists = this.fileSystem.File.Exists(path);

        if (exists && !force)
        {
            return false;
        }
        await this.fileSystem.File.WriteAllTextAsync(path, content, cancellationToken);

        return true;
    }
}
