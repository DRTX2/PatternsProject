/// <summary>
/// TargetTrackingUseCase is a use case that handles the tracking of targets for an entity.
/// T represents the type of entity that can be tracked, which must implement ITrakerEntity.
/// </summary>
/// <typeparam name="T"></typeparam>

public class TargetTrackingUseCase<T> where T : ITrakerEntity
{
    private readonly ITrackBehaviour<T> _tracker;

    public TargetTrackingUseCase(ITrackBehaviour<T> tracker)
    {
        _tracker = tracker;
    }

    public void RegisterTarget()
    {
        _tracker.RegisterTarget();
    }

    public void ClearTarget()
    {
        _tracker.ClearTargets();
    }
}