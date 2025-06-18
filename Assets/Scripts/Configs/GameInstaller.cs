using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private ProjectileSpawner projectileSpawner;

    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(new Player(100)).AsSingle();
        Container.Bind<IProjectileSpawner>().FromInstance(projectileSpawner).AsSingle();

        Container.Bind<IAnimatorAdapter>().To<AnimatorAdapter>().AsSingle();
        Container.Bind<IPhysicsAdapter>().To<RigidbodyAdapter>().AsSingle();
        Container.Bind<IAttackStrategyFactory>().To<AttackStrategyFactory>().AsSingle();
        Container.Bind<IInputReceiver>().To<InputReciver>().AsSingle();

        Container.Bind<IAttacker>().To<PlayerAttackMB>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IMovable>().To<PlayerMovementMB>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerHealthMB>() // Bind for IDamageable and IHealable interfaces
                 .FromComponentInHierarchy()
                 .AsSingle();

        Container.Bind<DamageUseCase>().AsSingle();
        Container.Bind<HealUseCase>().AsSingle();
        Container.BindInterfacesAndSelfTo<AttackUseCase>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerMovementService>().AsSingle();
        
        Container.Bind<HealthInteractionController>().AsSingle();
        Container.Bind<PlayerInputView>().FromComponentInHierarchy().AsSingle();
    }
}
