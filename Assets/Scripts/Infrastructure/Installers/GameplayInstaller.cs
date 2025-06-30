using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Presentation.Controllers;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [Inject] private Session _session;
    [Inject] private LoadGameController _loader;

    //[SerializeField] private GameObject diamondPickupPrefab;

    public override void InstallBindings()
    {
        Container.Bind<DamageUseCase>().AsTransient();
        Container.Bind<DamagePresenter>().AsTransient();

        Container.Bind<SaveGameUseCase>().AsTransient();

        // Score UseCase y Presenter
        Container.Bind<CollectScoreUseCase>().AsTransient();
        Container.Bind<ScorePresenter>()
    .AsSingle()
    .WithArguments(
        Container.Resolve<CollectScoreUseCase>(),
        Container.Resolve<CharacterEventBus>()
    );

        //Container.BindFactory<Vector3, DiamondPickup, DiamondPickupFactory>()
        //.FromComponentInNewPrefab(diamondPickupPrefab);
    }

    public override void Start()
    {
        if (_session.CurrentUser == null)
        {
            Debug.LogError("🚫 No hay usuario en sesión.");
            return;
        }

        _loader.Load();
        var player = _loader.LoadedPlayer;

        Container.Bind<Player>().FromInstance(player).AsSingle();

        var saveGameUseCase = Container.Resolve<SaveGameUseCase>();
        var saveGameController = new SaveGameController(saveGameUseCase, _session, player);
        Container.Bind<SaveGameController>().FromInstance(saveGameController).AsSingle();

        var prefab = Resources.Load<GameObject>("Player");
        var instance = Container.InstantiatePrefab(prefab);
        Container.Inject(instance);

        Debug.Log($"[GameplayInstaller] Player instanciado. Hash: {player.GetHashCode()}");

        InjectAllMonoBehavioursInScene();
    }

    private void InjectAllMonoBehavioursInScene()
    {
        var allBehaviours = GameObject.FindObjectsOfType<MonoBehaviour>(true);
        foreach (var mb in allBehaviours)
        {
            Container.Inject(mb);
        }
    }
}