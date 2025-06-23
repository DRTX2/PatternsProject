/// <summary>
/// Service for handling the movement of Knight enemies.
/// </summary>
public class KnightMovementService : IMoveBehaviour<KnightEnemy>
{
    private readonly IMoveBehaviour<KnightEnemy> _mover;

    public KnightMovementService(IMoveBehaviour<KnightEnemy> mover)
    {
        _mover = mover;
    }

    public void Move(float directionMultiplier)
    {
        _mover.Move(directionMultiplier);
    }

    public void FaceDirection(float x)
    {
        _mover.FaceDirection(x);
    }
}