using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Presentation.Controllers;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [Inject] private Session _session;
    [Inject] private LoadGameController _loader; // ✅ Zenject lo resuelve al final del binding

    public override void InstallBindings()
    {
        // No repitas los bindings ya definidos en GlobalInstaller
        Container.Bind<DamageUseCase>().AsTransient();
        Container.Bind<DamagePresenter>().AsTransient();
        Container.Bind<SaveGameUseCase>().AsTransient();
    }

    public override void Start()
    {
        if (_session.CurrentUser == null)
        {
            Debug.LogError("🚫 No hay usuario en sesión.");
            return;
        }

        // 1. Cargar datos del jugador desde sesión
        _loader.Load();
        var player = _loader.LoadedPlayer;

        // 2. Bindear el Player como instancia única compartida
        Container.Bind<Player>()
                 .FromInstance(player)
                 .AsSingle();

        // 3. Instanciar SaveGameController manualmente ahora que Player existe
        var saveGameUseCase = Container.Resolve<SaveGameUseCase>();
        var saveGameController = new SaveGameController(saveGameUseCase, _session, player);
        Container.Bind<SaveGameController>().FromInstance(saveGameController).AsSingle();

        // 4. Instanciar el prefab visual y asegurarse que recibe la instancia inyectada
        var prefab = Resources.Load<GameObject>("Player");
        var instance = Container.InstantiatePrefab(prefab);
        Container.Inject(instance); // Esto asegura que los MB dependientes reciban el modelo correcto

        Debug.Log($"[GameplayInstaller] Player instanciado. Hash: {player.GetHashCode()}");

        // 5. Inyectar también el resto de la escena (NextLevelHandler, pickups, etc.)
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