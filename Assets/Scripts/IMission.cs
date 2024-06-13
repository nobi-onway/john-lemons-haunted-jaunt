public interface IMission
{
    public string Title { get; }
    public bool IsCompleted { get; }
    public void DoTask();
}