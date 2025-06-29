/// <summary>
/// IScoreCollectibleBehaviour defines the behavior for entities that can collect score items.
/// This allows separation of the collectible logic from the entity and supports different score collection strategies.
/// </summary>
public interface IScoreCollectibleBehaviour
{
    int Score { get; }
    void CollectScore(int amount);
}