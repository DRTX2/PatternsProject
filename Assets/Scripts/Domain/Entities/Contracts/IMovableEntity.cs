/// <summary>
/// Contract for entities that can move.
/// IMovableEntity defines the basic properties and methods required for an entity that can move within the game world.
/// </summary>
/// 
public interface IMovableEntity
{
    bool IsAlive { get; }
    bool LockVelocityActive { get; }
    bool IsAttacking { get; }
    
    bool CanMove(bool isGrounded, bool isTouchingWall);
}