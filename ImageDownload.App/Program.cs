// See https://aka.ms/new-console-template for more information

using ImageDownload.App;
using ImageDownload.Commands;
using ImageDownload.Domain;

// Process settings.

// Command to get user choice for method which will be used to get settings. 
var settingsTypeCommand = new SettingsTypeCommand();

// Command to get settings to download images from manual interaction.
var manualSettingsReceiver = new ManualSettingsTypeCommand();

// Command to get settings to download images from json file.
var jsonSettingsReceiver = new JsonSettingsCommand();


// Cancelation token to control tasks.
CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
CancellationToken cancellationToken = cancellationTokenSource.Token;

// Ask user how they want to provide the settings for download process
var settingsType = settingsTypeCommand.Process();

// Get settings for process depends on the method user choose.
var settings = settingsType == SettingsType.Json 
    ? jsonSettingsReceiver.Process() 
    : manualSettingsReceiver.Process();

// Image downloader instance.
var imageDownloader = new ImageDownloader(settings);

// Register cancel action to stop tasks
Console.CancelKeyPress += delegate
{   
    cancellationTokenSource.Cancel();
    imageDownloader.ClearDownloadedFiles(settings);
    Console.WriteLine("Process has been stopped! Press any key to close the application...");
    Console.ReadKey();
};

// Start the process.
imageDownloader.DownloadImages(cancellationToken);
Console.WriteLine("Process has been stopped! Press any key to close the application...");
Console.ReadKey();