using System;
using System.Collections.Generic;
using System.Text;

public interface IInputReceiver
{
    void OnMoveInput(float x);
    void OnJumpInput();
    void OnMeleeAttackInput();
    void OnRangedAttackInput();
    void OnRunInput(bool isRunning);
}