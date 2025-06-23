using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private ProjectileSpawner projectileSpawner;

    public override void InstallBindings()
    {
        // 🧠 ENTIDAD de dominio (jugador)
        Container.Bind<Player>()
                 .FromInstance(new Player(100))
                 .AsSingle();

        // ⚔️ COMPORTAMIENTOS (adaptadores MB)
        Container.BindInterfacesAndSelfTo<PlayerAttackMB>() // IAttackBehaviour<Player>
                 .FromComponentInHierarchy()
                 .AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerDamageMB>() // IDamageBehaviour<Player>, IHealBehaviour<Player>
                 .FromComponentInHierarchy()
                 .AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerMovementMB>() // IMoveBehaviour<Player>, IJumpBehaviour<Player>, IRunBehaviour<Player>
                 .FromComponentInHierarchy()
                 .AsSingle();


        Container.BindInterfacesAndSelfTo<PlayerHealMB>() // IMoveBehaviour<Player>, IJumpBehaviour<Player>, IRunBehaviour<Player>
                 .FromComponentInHierarchy()
                 .AsSingle();

        // 🎮 Entrada del jugador
        Container.Bind<IInputReceiver>()
                 .To<PlayerInputReceiver>()
                 .AsSingle();

        Container.Bind<PlayerInputView>()
                 .FromComponentInHierarchy()
                 .AsSingle();

        // 🧩 CASOS DE USO GENÉRICOS
        Container.Bind<AttackUseCase<Player>>()
                 .AsSingle();

        Container.Bind<DamageUseCase>()
                 .AsSingle();

        Container.Bind<HealUseCase>()
                 .AsSingle();

        // ❤️ INTERACCIÓN DE SALUD
        Container.Bind<HealthPresenter>()
                 .AsSingle();

        Container.Bind<DamagePresenter>()
                 .AsSingle();

        // 🚀 SERVICIO DE MOVIMIENTO DEL JUGADOR
        Container.Bind<PlayerMovementService>()
                 .AsSingle();

        // 💥 PROYECTILES
        Container.Bind<IProjectileSpawner>()
                 .FromInstance(projectileSpawner)
                 .AsSingle();

        // 🛠 FACTORÍAS Y ADAPTADORES BASE
        Container.Bind<IAnimatorAdapter>()
                 .To<AnimatorAdapter>()
                 .AsSingle();

        Container.Bind<IPhysicsAdapter>()
                 .To<RigidbodyAdapter>()
                 .AsSingle();

        Container.Bind<IAttackStrategyFactory>()
                 .To<AttackStrategyFactory>()
                 .AsSingle();

        // 🔊 EVENTOS GLOBALES
        Container.Bind<CharacterEventBus>()
                 .AsSingle();
    }
}
