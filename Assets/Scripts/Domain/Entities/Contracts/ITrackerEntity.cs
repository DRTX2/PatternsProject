/// <summary>
/// Contract for entities that can have a tracker.
/// ITrakerEntity is an interface that defines the behavior for entities that can have a tracker.
/// </summary>
public interface ITrakerEntity
{
    bool HasTracker { get; }
    void SetTracker(bool value);
}