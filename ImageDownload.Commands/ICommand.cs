namespace ImageDownload.Commands;

public interface ICommand<T>
{
    public T Process();
}