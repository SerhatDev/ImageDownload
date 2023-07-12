namespace ImageDownload.Domain;

public sealed class ProcessSettings
{
    public int Count { get; set; }
    public int Parallelism { get; set; }
    public string SavePath { get; set; }
}