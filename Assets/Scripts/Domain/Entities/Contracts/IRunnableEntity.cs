/// <summary>
/// Contract for entities that can be in a running state.
/// IRunnableEntity is an interface that defines the behavior for entities that can be in a running state.
/// </summary>
public interface IRunnableEntity
{
    bool IsRunning { get; }
    void SetRunning(bool isRunning);
}
