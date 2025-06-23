/// <summary>
/// ITrackBehaviour is an interface that defines a tracking behavior for entities.
/// Abstracts the tracking logic from the entity itself, allowing for different tracking strategies to be implemented without modifying the entity class.
/// T represents the type of entity that can be tracked, which must implement ITrakerEntity.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ITrackBehaviour<T> where T : ITrakerEntity
{
    void RegisterTarget();
    void ClearTargets();
}
