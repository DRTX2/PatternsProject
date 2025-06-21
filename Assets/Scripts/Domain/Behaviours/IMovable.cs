using System;
using System.Collections.Generic;
using System.Text;

public interface IMovable
{
    void Move(float x);
    void Jump();
    void FaceDirection(float x);
    void SetRunning(bool isRunning);
}
