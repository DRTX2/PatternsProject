public class TargetTrackingUseCase
{
    private readonly ITracker _tracker;

    public TargetTrackingUseCase(ITracker tracker)
    {
        _tracker = tracker;
    }

    public void ClearTarget()
    {
        _tracker.ClearTargets();
    }
}
