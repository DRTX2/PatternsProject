/// <summary>
/// Represents an event bus for character-related events.
/// </summary>
public class CharacterEventBus
{
    public Observable<HealthChangedEvent> HealthChanged { get; } = new();
    public Observable<DamageEvent> DamageReceived { get; } = new();
    public Observable<HealEvent> Healed { get; } = new();
    public Observable<ScoreCollectedEvent> ScoreCollected { get; } = new();
}
