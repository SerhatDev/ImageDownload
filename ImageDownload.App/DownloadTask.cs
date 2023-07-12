using ImageDownload.Domain;

namespace ImageDownload.App;

public sealed class DownloadTask 
{
    private readonly string _url;
    private readonly string _path;
    private int CurrentCount { get; set; }
    private int TotalCount { get; set; }

    public delegate void DownloadFinishedDelegate(string fileName);
    public event DownloadFinishedDelegate OnDownloadFinished;

    public delegate void BatchDownloadFinishedDelegate();

    public static event BatchDownloadFinishedDelegate OnBatchDownloadFinished;

    public DownloadTask(string url,string path,int currentCount,int totalCount)
    {
        CurrentCount = currentCount;
        TotalCount = totalCount;
        _url = url;
        _path = path;
    }
    
    public void OnProgressUpdate( int currentCount,int count)
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write($"Progress: {currentCount}/{count}");
    }

    private void Download(CancellationToken ct)
    {
        HttpClient httpClient = new HttpClient();
                
        // Get bytes of the image hold it in the memory
        var bytes = httpClient.GetByteArrayAsync(this._url, ct).GetAwaiter().GetResult();
        // Save image on the disk
        var task= File.WriteAllBytesAsync(this._path, bytes, ct);
        Task.WaitAll(task);
        this.OnDownloadFinished.Invoke(this._path);
        OnProgressUpdate(CurrentCount, TotalCount);
    }

    public static void DownloadAll(List<DownloadTask> tasks, CancellationToken token)
    {
        List<Task> batchTasks = new();
        for (int i = 0; i < tasks.Count; i++)
        {
            var dTask = tasks[i];
            var tt= Task.Factory.StartNew(() => dTask.Download(token));
            batchTasks.Add(tt);
        }
        Task.WhenAll(batchTasks).GetAwaiter().GetResult();
        OnBatchDownloadFinished.Invoke();
    }
}