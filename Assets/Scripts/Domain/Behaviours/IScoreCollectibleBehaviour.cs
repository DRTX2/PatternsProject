/// <summary>
/// IScoreCollectibleBehaviour defines the behavior for entities that can collect score items.
/// This allows separation of the collectible logic from the entity and supports different score collection strategies.
/// </summary>
public interface IScoreCollectibleBehaviour
{
    /// <summary>
    /// Increases the score of the entity.
    /// </summary>
    /// <param name="amount">The amount of score to be added.</param>
    void CollectScore(int amount);

    /// <summary>
    /// Current total score of the entity.
    /// </summary>
    int Score { get; }
}
