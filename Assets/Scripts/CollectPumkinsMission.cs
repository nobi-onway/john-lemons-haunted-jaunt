
public class CollectPumkinsMission : IMission
{
    private int _pumkinsTarget;
    private int _currentPumskin;

    public CollectPumkinsMission(int target, int current)
    {
        _pumkinsTarget = target;
        _currentPumskin = current;
    }
    public bool IsCompleted => _currentPumskin == _pumkinsTarget;

    public string Title => $"Collect Pumskin: {_currentPumskin}/{_pumkinsTarget}";

    public void DoTask()
    {
        _currentPumskin++;
    }
}
