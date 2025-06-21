public class PlayerInputReciver : IInputReceiver
{
    private readonly PlayerMovementService _movementService;
    private readonly AttackUseCase _attackUserCase;

    public PlayerInputReciver(PlayerMovementService movementService, AttackUseCase attackService)
    {
        _movementService = movementService;
        _attackUserCase = attackService;
    }

    public void OnMoveInput(float x) => _movementService.HandleMoveInput(x);

    public void OnRunInput(bool isRunning) => _movementService.HandleRunInput(isRunning);

    public void OnJumpInput() => _movementService.HandleJumpRequest();

    public void OnMeleeAttackInput() => _attackUserCase.HandleAttack(AttackAnimationType.Melee);

    public void OnRangedAttackInput() => _attackUserCase.HandleAttack(AttackAnimationType.Ranged);

}
