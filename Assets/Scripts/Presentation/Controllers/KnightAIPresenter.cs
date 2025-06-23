using UnityEngine;
using Zenject;

/// <summary>
/// KnightAIPresenter is responsible for managing the AI behavior of the Knight enemy.
/// </summary>
public class KnightAIPresenter : MonoBehaviour
{
    [Inject] private AttackUseCase<KnightEnemy> _attackUseCase;
    [Inject] private TargetTrackingUseCase<KnightEnemy> _targetTrackingUseCase;
    [Inject] private IMoveBehaviour<KnightEnemy> _movement;
    [Inject] private KnightEnemy _knight;

    [SerializeField] private AttackDetectionView attackView;
    [SerializeField] private CliffDetectionView cliffView;

    private void Start()
    {
        cliffView.OnCliffDetected.AddListener(OnCliffDetected);
        attackView.OnTargetDetected.AddListener(OnTargetDetected);
        attackView.NoRemainingTargets.AddListener(OnNoRemainingTargets);
    }

    private void Update()
    {
        _movement.Move(1f);
    }

    private void OnCliffDetected()
    {
        _knight.FlipDirection();
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