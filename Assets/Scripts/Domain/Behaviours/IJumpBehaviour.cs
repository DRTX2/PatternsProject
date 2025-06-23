/// <summary>
/// Uses the IJumpBehaviour interface to define a jump behavior for entities.
/// T represents the type of entity that can jump, which must implement IJumpableEntity.
/// </summary>
/// <typeparam name="T"></typeparam>

public interface IJumpBehaviour<T> where T : IJumpableEntity
{
    void Jump();
}
