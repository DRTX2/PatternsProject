using Zenject;

/// <summary>
/// Service for handling player movement, including moving, jumping, and running.
/// </summary>
public class PlayerMovementService : IMoveBehaviour<Player>, IJumpBehaviour<Player>, IRunBehaviour<Player>
{
    private readonly IMoveBehaviour<Player> _mover;
    private readonly IJumpBehaviour<Player> _jumper;
    private readonly IRunBehaviour<Player> _runner;

    [Inject]
    public PlayerMovementService(
        IMoveBehaviour<Player> mover,
        IJumpBehaviour<Player> jumper,
        IRunBehaviour<Player> runner)
    {
        _mover = mover;
        _jumper = jumper;
        _runner = runner;
    }

    public void Move(float directionX)
    {
        _mover.Move(directionX);
    }
    public void FaceDirection(float directionX) => _mover.FaceDirection(directionX);
    public void Jump() => _jumper.Jump();
    public void SetRunning(bool isRunning) => _runner.SetRunning(isRunning);
}
