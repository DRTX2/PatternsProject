using UnityEngine;
using Zenject;

/// <summary>
/// FlyingEyeAIPresenter is responsible for managing the AI behavior of the Flying Eye enemy.
/// </summary>
public class FlyingEyeAIPresenter : MonoBehaviour
{
    [Inject] private AttackUseCase<FlyingEyeEnemy> _attackUseCase;
    [Inject] private TargetTrackingUseCase<FlyingEyeEnemy> _targetTrackingUseCase;
    [Inject] private IMoveBehaviour<FlyingEyeEnemy> _movement;

    [SerializeField] private AttackDetectionView attackView;

    private void Start()
    {
        attackView.OnTargetDetected.AddListener(OnTargetDetected);
        attackView.NoRemainingTargets.AddListener(OnNoRemainingTargets);
    }

    private void Update()
    {
        _movement.Move(1f);
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
