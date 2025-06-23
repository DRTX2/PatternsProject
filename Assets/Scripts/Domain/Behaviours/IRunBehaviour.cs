/// <summary>
/// IRunBehaviour is an interface that defines a running behavior for entities.
/// Abstracts the running logic from the entity itself, allowing for different running strategies to be implemented without modifying the entity class.
/// T represents the type of entity that can run, which must implement IRunnableEntity.
/// </summary>
/// <typeparam name="T"></typeparam>
/// 
public interface IRunBehaviour<T> where T : IRunnableEntity
{
    void SetRunning(bool isRunning);
}
