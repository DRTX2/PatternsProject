using System.Collections.Generic;
using System.Text;

public class PlayerState
{
    public bool IsMoving;
    public bool IsRunning;
    public bool IsGrounded;
    public bool IsOnWall;
    public bool IsOnCeiling;
    public bool IsAlive;
    public bool CanMove;
    public bool IsAttacking;
    public bool IsJumping;
    public float VerticalVelocity;
}
