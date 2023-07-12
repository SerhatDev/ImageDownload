using System.Text.Json;
using System.Text.Json.Serialization;

namespace ImageDownload.Domain;

public class ProcessSettingsBuilder
{
    private readonly ProcessSettings _processSettings;

    public ProcessSettingsBuilder()
    {
        this._processSettings = new ProcessSettings();
    }
    
    public ProcessSettingsBuilder WithCount(int count)
    {
        this._processSettings.Count = count;
        return this;
    }

    public ProcessSettingsBuilder WithParallelism(int count)
    {
        this._processSettings.Parallelism = count;
        return this;
    }

    public ProcessSettingsBuilder WithSavePath(string savePath)
    {
        this._processSettings.SavePath = savePath;
        return this;
    }

    public ProcessSettings BuildFromJson(string jsonFilePath)
    {
        string jsonStr = File.ReadAllText(jsonFilePath);
        var data = JsonSerializer.Deserialize<ProcessSettings>(jsonStr);
        return data;
    }

    public ProcessSettings Build() => this._processSettings;
}