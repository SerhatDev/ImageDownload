using System.ComponentModel;
using ImageDownload.Domain;

namespace ImageDownload.Commands;

public sealed class SettingsTypeCommand : ICommand<SettingsType>
{
    public SettingsType Process()
    {
        Console.WriteLine("Choose how you want to set the Process Settings:\n[1] - Manual\n[2] - Json File");
        var userChoice = Console.ReadKey(true);
        while (!(userChoice.Key == ConsoleKey.NumPad2 || userChoice.Key ==ConsoleKey.D2 || userChoice.Key == ConsoleKey.NumPad1 || userChoice.Key == ConsoleKey.D1))
        {
            Console.WriteLine("Please enter valid value!\nHow you want to set the Process Settings:\n[1] - Manual\n[2] - Json File");
            userChoice = Console.ReadKey(true);
        }

        SettingsType returnVale = userChoice.Key switch
        {
            var key when key == ConsoleKey.NumPad1 || key == ConsoleKey.D1 => SettingsType.Manual,
            var key when key == ConsoleKey.NumPad2 || key == ConsoleKey.D2 => SettingsType.Json,
            _=> throw new InvalidEnumArgumentException("User choice was not valid")
        };
        return returnVale;
    }
}