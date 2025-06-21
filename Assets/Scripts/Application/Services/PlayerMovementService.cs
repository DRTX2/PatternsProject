using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public class PlayerMovementService
{
    private readonly IInputMovable _movable;

    public PlayerMovementService(IInputMovable movable)
    {
        _movable = movable;
    }

    public void HandleMoveInput(float directionX)
    {
        _movable.Move(directionX);
    }
    public void HandleJumpRequest() => _movable.Jump();
    public void HandleRunInput(bool running) => _movable.SetRunning(running);
}
