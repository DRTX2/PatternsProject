using UnityEngine;
using Zenject;

public class KnightInstaller : MonoInstaller
{
    [SerializeField] private float maxHealth = 100f;

    public override void InstallBindings()
    {
        Container.Bind<KnightEnemy>().AsSingle().WithArguments(maxHealth);

        Container.Bind<IAutonomousMovable>()
            .To<KnightMovementMB>()
            .FromComponentOnRoot()
            .AsSingle();

        Container.Bind<IDamageable>()
            .To<KnightHealthMB>()
            .FromComponentOnRoot()
            .AsSingle();

        /*Container.Bind<IAttacker>()
            .To<KnightAttackMB>()
            .FromComponentOnRoot()
            .AsSingle();

        Container.Bind<ITracker>()
            .To<KnightAttackMB>()
            .FromComponentOnRoot()
            .AsSingle();*/
        Container.BindInterfacesAndSelfTo<KnightAttackMB>() // Bind for ITracker and IAttacker
             .FromComponentOnRoot()
             .AsSingle();


        Container.Bind<AttackUseCase>().AsSingle();
        Container.Bind<TargetTrackingUseCase>().AsSingle();

        Container.Bind<KnightMovementService>()
            .AsSingle();
    }
}
