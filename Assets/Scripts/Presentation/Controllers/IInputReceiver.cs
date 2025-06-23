using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// IInputReceiver is an interface that defines the methods for receiving input events in the game.
/// </summary>
public interface IInputReceiver
{
    void OnMoveInput(float x);
    void OnJumpInput();
    void OnMeleeAttackInput();
    void OnRangedAttackInput();
    void OnRunInput(bool isRunning);
}