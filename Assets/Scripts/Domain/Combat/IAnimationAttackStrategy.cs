/// <summary>
/// IAnimationAttackStrategy is an interface that defines a strategy for executing attacks using animations.
/// Specific implementations of this interface will handle the logic for executing attacks based on the provided animator.
/// Divides the attack execution logic from the entity itself, allowing for different animation strategies to be implemented.
/// </summary>

public interface IAnimationAttackStrategy
{
    void ExecuteAttack(IAnimatorAdapter animator);
}