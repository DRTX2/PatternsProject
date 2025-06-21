using UnityEngine;
using Zenject;

public class FlyingEyeInstaller : MonoInstaller
{
    [SerializeField] private float maxHealth = 50f;

    public override void InstallBindings()
    {
        Container.Bind<FlyingEyeEnemy>().AsSingle().WithArguments(maxHealth);

        Container.Bind<IFlyer>()
            .To<FlyingEyeMovementMB>()
            .FromComponentOnRoot()
            .AsSingle();

        Container.Bind<IDamageable>()
            .To<FlyingEyeHealthMB>()
            .FromComponentOnRoot()
            .AsSingle();

        /*Container.Bind<IAttacker>()
            .To<FlyingEyeAttackMB>()
            .FromComponentOnRoot()
            .AsSingle();

        Container.Bind<ITracker>()
            .To<KnightAttackMB>()
            .FromComponentOnRoot()
            .AsSingle();*/
        Container.BindInterfacesAndSelfTo<FlyingEyeAttackMB>() // Bind for ITracker and IAttacker
             .FromComponentOnRoot()
             .AsSingle();

        Container.Bind<DamageUseCase>().AsSingle();
        Container.Bind<AttackUseCase>().AsSingle();
        Container.Bind<TargetTrackingUseCase>().AsSingle();

        Container.Bind<FlyingEyeMovementService>()
            .AsSingle();
    }
}
