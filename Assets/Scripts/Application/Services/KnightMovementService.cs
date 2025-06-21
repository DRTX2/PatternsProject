using System.Diagnostics;

public class KnightMovementService
{
    private readonly IAutonomousMovable _movable;

    public KnightMovementService(IAutonomousMovable movable)
    {
        _movable = movable;
    }

    public void Tick(float deltaTime)
    {
        _movable.Move();
    }

    public void OnCliffDetected()
    {
        _movable.FlipDirection();
    }
}
    