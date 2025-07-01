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
        // —— CASOS DE USO ————————————————
        Container.Bind<DamageUseCase>().AsTransient();
        Container.Bind<SaveGameUseCase>().AsTransient();

        // Ahora CollectScoreUseCase es singleton
        Container.Bind<CollectScoreUseCase>().AsSingle();

        // —— EVENT BUS GLOBAL ————————————
        Container.Bind<CharacterEventBus>()
                 .AsSingle()
                 .NonLazy();

        // —— PRESENTERS ———————————————
        Container.Bind<DamagePresenter>().AsTransient();
        // Inyección automática de CollectScoreUseCase y CharacterEventBus
        Container.Bind<ScorePresenter>().AsSingle();

        // —— SESSION y CONTROLADORES —————————
        Container.Bind<Session>()
                 .FromInstance(_session)
                 .AsSingle();
        Container.Bind<LoadGameController>().AsSingle();
        Container.Bind<SaveGameController>().AsSingle();

        // —— INSTANCIAR JUGADOR ————————————
        // Resuelve el Player que viene desde GameInstaller
        var player = Container.Resolve<Player>();
        Container.BindInstance(player)
                 .WhenInjectedInto<GameplayInstaller>();

        // —— VISTAS EN ESCENA ————————————
        // Vincula los MonoBehaviours de la UI para Score y Heal
        //Container.BindInterfacesAndSelfTo<ScoreViewMB>()
        //         .FromComponentInHierarchy()
        //         .AsSingle();

        Container.BindInterfacesAndSelfTo<DiamondPickupView>()
                 .FromComponentInHierarchy()
                 .AsSingle();

        Container.BindInterfacesAndSelfTo<HealView>()
                 .FromComponentInHierarchy()
                 .AsSingle();
    }

    public override void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("[GameplayInstaller] playerPrefab NO está asignado en el Inspector.");
            return;
        }

        // Instancia y DI para todos los MonoBehaviours
        var instance = Container.InstantiatePrefab(playerPrefab);
        Container.Inject(instance);

        foreach (var mb in GameObject.FindObjectsOfType<MonoBehaviour>(true))
            Container.Inject(mb);

        Debug.Log("[GameplayInstaller] Scene injected and player instantiated.");
    }
}
