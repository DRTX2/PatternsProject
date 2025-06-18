using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public class PlayerMovementService
{
    private readonly IMovable _movable;

    public PlayerMovementService(IMovable movable)
    {
        _movable = movable;
    }

    public void HandleMoveInput(float directionX)
    {
        
        _movable.Move(directionX);
    }

    public void HandleJumpRequest()
    {
        _movable.Jump();
    }

    public void HandleRunInput(bool isRunning)
    {
        _movable.SetRunning(isRunning);
    }
}
