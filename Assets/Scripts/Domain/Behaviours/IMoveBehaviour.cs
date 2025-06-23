/// <summary>
/// IMoveBehaviour is an interface that defines a movement behavior for entities.
/// Abstracts the movement logic from the entity itself, allowing for different movement strategies to be implemented without modifying the entity class.
/// T represents the type of entity that can move, which must implement IMovableEntity.
/// </summary>
/// <typeparam name="T"></typeparam>

public interface IMoveBehaviour<T> where T : IMovableEntity
{
    void Move(float x);
    void FaceDirection(float x);
}
