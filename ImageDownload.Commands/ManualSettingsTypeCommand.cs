using ImageDownload.Domain;

namespace ImageDownload.Commands;

public sealed class ManualSettingsTypeCommand : ICommand<ProcessSettings>
{
    private readonly ProcessSettingsBuilder _processSettingsBuilder;

    public ManualSettingsTypeCommand()
    {
        this._processSettingsBuilder = new ProcessSettingsBuilder();
    }
    
    public ProcessSettings Process()
    {
        Console.Write("Enter the number of images to download : ");

        int count;
        while (!int.TryParse(Console.ReadLine(), out count))
        {
            Console.Write("Only numeric values accepted!\nEnter the number of images to download : ");
        }

        _processSettingsBuilder.WithCount(count);


        Console.Write("Enter the count of parallel downloads : ");
        int parallelism;
        while (!int.TryParse(Console.ReadLine(), out parallelism))
        {
            Console.Write("Only numeric values accepted!\nEnter the count of parallel downloads : ");
        }

        _processSettingsBuilder.WithParallelism(parallelism);


        Console.Write("Enter the path where downloaded images will be saved : ");
        string? savePath = Console.ReadLine();
        while (string.IsNullOrEmpty(savePath))
        {
            Console.Write("Save path cannot be null!\nEnter the path where downloaded images will be saved : ");
            savePath = Console.ReadLine();
        }

        _processSettingsBuilder.WithSavePath(savePath);
        return _processSettingsBuilder.Build();
    }
}