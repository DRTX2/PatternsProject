/// <summary>
/// Contract for entities that can jump.
/// Defines properties and methods related to jumping behavior.
/// </summary>
public interface IJumpableEntity
{
    bool IsJumping { get; }
    void SetJumping(bool value);
}
