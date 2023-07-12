using ImageDownload.Domain;

namespace ImageDownload.App;

public sealed class ImageDownloader
{
    private readonly ProcessSettings _settings;
    private const string Root = "https://picsum.photos/200/300";

    private int _currentFile = 1;

    public ImageDownloader(ProcessSettings settings)
    {
        _settings = settings;
    }

    public void DownloadImages(CancellationToken tk)
    {
        // Create list of tasks for current batch.
        List<DownloadTask> tasks = new List<DownloadTask>();

        // Loop through required amount to add tasks into current batch.
        for (int i = _currentFile; i < _currentFile + _settings.Parallelism; i++)
        {
            DownloadTask task = new DownloadTask(Root, $"{_settings.SavePath}/{i}.png", i, _settings.Count);
            task.OnDownloadFinished += delegate(string name) { task.OnProgressUpdate(_settings.Count, i); };
            tasks.Add(task);
        }

        // Re-start batching process when the current batch completes.
        DownloadTask
            .OnBatchDownloadFinished += delegate
        {
            _currentFile += _settings.Parallelism;
            if (_currentFile < _settings.Count)
                DownloadImages(tk);
        };

        // If there are any tasks in the batch, process tasks.
        if (tasks.Count > 0)
            DownloadTask.DownloadAll(tasks, tk);
    }


    public void ClearDownloadedFiles(ProcessSettings settings)
    {
        var files = Directory.GetFiles(settings.SavePath);
        for (var i = 0; i < files.Length; i++)
            File.Delete(files[i]);
    }
}