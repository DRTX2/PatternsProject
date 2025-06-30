using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Presentation.Controllers;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject playerPrefab;  // Arrastra aquí tu prefab Player desde el Inspector

    [Inject]
    private Session _session;         // Inyectado por GlobalInstaller

    public override void InstallBindings()
    {
        // 1) Use Cases y Presenters
        Container.Bind<DamageUseCase>().AsTransient();
        Container.Bind<DamagePresenter>().AsTransient();

        Container.Bind<SaveGameUseCase>().AsTransient();
        Container.Bind<CollectScoreUseCase>().AsTransient();

        // 2) Event bus y ScorePresenter
        Container.Bind<CharacterEventBus>()
                 .AsSingle()
                 .NonLazy();
        Container.Bind<ScorePresenter>()
                 .AsSingle()
                 .WithArguments(
                     Container.Resolve<CollectScoreUseCase>(),
                     Container.Resolve<CharacterEventBus>()
                 );

        // 3) Session y LoadGameController
        Container.Bind<Session>()
                 .FromInstance(_session)
                 .AsSingle();
        Container.Bind<LoadGameController>()
                 .AsSingle();

        // 4) Carga de datos y bind de Player
        var loader = Container.Resolve<LoadGameController>();
        loader.Load();
        var loadedPlayer = loader.LoadedPlayer
                           ?? new Player(
                                maxHealth: 100,
                                currentHealth: 100,
                                positionX: 0f,
                                positionY: 0f,
                                enemiesEliminated: 0,
                                score: 0
                              );
        Container.Bind<Player>()
                 .FromInstance(loadedPlayer)
                 .AsSingle();

        // 5) SaveGameController con Player inyectado
        Container.Bind<SaveGameController>()
                 .AsSingle()
                 .WithArguments(
                     Container.Resolve<SaveGameUseCase>(),
                     Container.Resolve<Session>(),
                     loadedPlayer
                 );
    }

    public override void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("[GameplayInstaller] playerPrefab NO está asignado en el Inspector.");
            return;
        }

        // Instancia el jugador y realiza la inyección
        var playerInstance = Container.InstantiatePrefab(playerPrefab);
        Container.Inject(playerInstance);

        // Inyecta el resto de los MonoBehaviours de la escena
        foreach (var mb in GameObject.FindObjectsOfType<MonoBehaviour>(true))
        {
            Container.Inject(mb);
        }

        Debug.Log("[GameplayInstaller] Scene injected and player instantiated.");
    }
}
