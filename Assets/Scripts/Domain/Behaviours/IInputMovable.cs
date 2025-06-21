using System;
using System.Collections.Generic;
using System.Text;

public interface IInputMovable
{
    void Move(float x);
    void Jump();
    void FaceDirection(float x);
    void SetRunning(bool isRunning);
}
