/// <summary>
/// Service for handling the movement of FlyingEye enemies.
/// </summary>
public class FlyingEyeMovementService : IMoveBehaviour<FlyingEyeEnemy>
{
    private readonly IMoveBehaviour<FlyingEyeEnemy> _mover;

    public FlyingEyeMovementService(IMoveBehaviour<FlyingEyeEnemy> mover)
    {
        _mover = mover;
    }

    public void Move(float directionX)
    {
        _mover.Move(directionX);
    }

    public void FaceDirection(float x)
    {
        _mover.FaceDirection(x);
    }
}