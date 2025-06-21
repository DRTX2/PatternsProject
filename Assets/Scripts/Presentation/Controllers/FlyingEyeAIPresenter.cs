using UnityEngine;
using Zenject;

public class FlyingEyeAIPresenter : MonoBehaviour
{
    [Inject] private AttackUseCase _attackUseCase;
    [Inject] private TargetTrackingUseCase _targetTrackingUseCase;
    [Inject] private FlyingEyeMovementService _movementService;
    [SerializeField] private AttackDetectionView attackView;

    private void Start()
    {
        attackView.OnTargetDetected.AddListener(OnTargetDetected);
        attackView.NoRemainingTargets.AddListener(OnNoRemainingTargets);
    }
    private void Update()
    {
        _movementService.Tick(Time.deltaTime);
    }
    private void OnTargetDetected()
    {
        _attackUseCase.HandleAttack(AttackAnimationType.Target);
    }

    private void OnNoRemainingTargets()
    {
        _targetTrackingUseCase.ClearTarget();
    }
}
