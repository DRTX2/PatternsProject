using Assets.Scripts.Application.Dtos;
using Zenject;

public class ScorePresenter
{
    private readonly CollectScoreUseCase _useCase;
    private readonly CharacterEventBus _eventBus;

    // Zenject inyecta aquí las dependencias
    public ScorePresenter(CollectScoreUseCase useCase, CharacterEventBus eventBus)
    {
        _useCase = useCase;
        _eventBus = eventBus;
    }

    public void ApplyScore(ScoreCollectData data)
    {
        _useCase.Execute(data.Target, data.Amount);

        _eventBus.ScoreCollected.Notify(new ScoreCollectedEvent
        {
            TotalScore = data.Target.Score,
            CollectedAmount = data.Amount
        });
    }
}
