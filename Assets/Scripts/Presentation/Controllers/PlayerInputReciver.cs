using Zenject;

/// <summary>
/// PlayerInputReceiver is responsible for handling player input and forwarding it to the appropriate use cases and services.
/// </summary>
public class PlayerInputReceiver : IInputReceiver
{
    [Inject] private readonly AttackUseCase<Player> _attackUseCase;
    [Inject] private PlayerMovementService _movement;

    public void OnMoveInput(float x) => _movement.Move(x);
    public void OnJumpInput() => _movement.Jump();
    public void OnRunInput(bool isRunning) => _movement.SetRunning(isRunning);

    public void OnMeleeAttackInput() => _attackUseCase.HandleAttack(AttackAnimationType.Melee);
    public void OnRangedAttackInput() => _attackUseCase.HandleAttack(AttackAnimationType.Ranged);
}
