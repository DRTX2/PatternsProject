public class FlyingEyeMovementService
{
    private readonly IFlyer _flyer;

    public FlyingEyeMovementService(IFlyer flyer)
    {
        _flyer = flyer;
    }

    public void Tick(float deltaTime)
    {
        _flyer.Fly(deltaTime);
    }
}
