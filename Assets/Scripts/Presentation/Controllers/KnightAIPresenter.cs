using UnityEngine;
using Zenject;

public class KnightAIPresenter : MonoBehaviour
{
    [Inject] private KnightMovementService _movementService;
    [Inject] private AttackUseCase _attackUseCase;
    [Inject] private TargetTrackingUseCase _targetTrackingUseCase;
    [SerializeField] private AttackDetectionView attackView;
    [SerializeField] private CliffDetectionView cliffView;

    private void Start()
    {
        cliffView.OnCliffDetected.AddListener(OnCliffDetected);
        attackView.OnTargetDetected.AddListener(OnTargetDetected);
        attackView.NoRemainingTargets.AddListener(OnNoRemainingTargets);
    }

    private void Update() => _movementService.Tick(Time.deltaTime);

    private void OnCliffDetected()
    {
        _movementService.OnCliffDetected();
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
