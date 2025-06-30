using Assets.Scripts.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

public class ScorePresenter
{
    [Inject] private readonly CollectScoreUseCase _useCase;
    [Inject] private readonly CharacterEventBus _eventBus;

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