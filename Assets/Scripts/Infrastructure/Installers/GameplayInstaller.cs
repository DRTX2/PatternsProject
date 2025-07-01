using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Presentation.Controllers;
using Zenject;
using UnityEngine;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private GameObject playerPrefab;

    [Inject] private Session _session;

    public override void InstallBindings()
    {
     
        Container.Bind<DamageUseCase>().AsTransient();
        Container.Bind<DamagePresenter>().AsTransient();

        Container.Bind<SaveGameUseCase>().AsTransient();
        Container.Bind<CollectScoreUseCase>().AsTransient();

      
        Container.Bind<CharacterEventBus>()
                 .AsSingle()
                 .NonLazy();
        Container.Bind<ScorePresenter>()
                 .AsSingle()
                 .WithArguments(
                     Container.Resolve<CollectScoreUseCase>(),
                     Container.Resolve<CharacterEventBus>()
                 );

       
        Container.Bind<Session>()
                 .FromInstance(_session)
                 .AsSingle();
        Container.Bind<LoadGameController>()
                 .AsSingle();

        
        var player = Container.Resolve<Player>();
        Container.BindInstance(player)
                 .WhenInjectedInto<GameplayInstaller>();

     
        Container.Bind<SaveGameController>()
                 .AsSingle()
                 .WithArguments(
                     Container.Resolve<SaveGameUseCase>(),
                     _session,
                     player
                 );
    }

    public override void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("[GameplayInstaller] playerPrefab NO está asignado en el Inspector.");
            return;
        }

        var instance = Container.InstantiatePrefab(playerPrefab);
        Container.Inject(instance);

        foreach (var mb in GameObject.FindObjectsOfType<MonoBehaviour>(true))
            Container.Inject(mb);

        Debug.Log("[GameplayInstaller] Scene injected and player instantiated.");
    }
}
