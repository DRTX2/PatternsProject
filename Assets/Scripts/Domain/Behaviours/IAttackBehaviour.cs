/// <summary>
/// IAttackBehaviour is an interface that defines an attack behavior for entities.
/// Abstracts the attack logic from the entity itself, 
/// allowing for different attack strategies to be implemented without modifying the entity class.
/// T represents the type of entity that can be attacked, which must implement IAttackableEntity.
/// </summary>
/// <typeparam name="T"></typeparam>
/// 
public interface IAttackBehaviour<T> where T : IAttackableEntity
{
    void Attack(IAnimationAttackStrategy strategy);
}
