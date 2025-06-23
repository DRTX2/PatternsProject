public interface ISurfaceContactSensor
{
    bool IsGrounded { get; }
    bool IsOnWall { get; }
    bool IsOnCeiling { get; }
}
