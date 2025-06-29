using Assets.Scripts.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ScorePresenter
{
    private readonly CollectScoreUseCase _useCase;

    public ScorePresenter(CollectScoreUseCase useCase)
    {
        _useCase = useCase;
    }

    public void ApplyScore(ScoreCollectData data)
    {
        _useCase.Execute(data.Target, data.Amount);
    }
}
