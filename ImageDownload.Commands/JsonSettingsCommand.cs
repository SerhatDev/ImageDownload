using ImageDownload.Domain;

namespace ImageDownload.Commands;

public sealed class JsonSettingsCommand : ICommand<ProcessSettings>
{
    private readonly ProcessSettingsBuilder _processSettingsBuilder;
    private const string DefaultPath = "input.json";

    public JsonSettingsCommand()
    {
        this._processSettingsBuilder = new ProcessSettingsBuilder();
    }
    public ProcessSettings Process()
    {
        Console.Write("Please provide the Json File name : ");
        var fileName = Console.ReadLine();
        if (string.IsNullOrEmpty(fileName))
            fileName = DefaultPath;
        return _processSettingsBuilder
            .BuildFromJson(fileName);
    }
}