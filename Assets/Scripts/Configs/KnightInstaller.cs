using UnityEngine;
using Zenject;

public class KnightInstaller : MonoInstaller
{
    [SerializeField] private float maxHealth = 100f;

    public override void InstallBindings()
    {
        Container.Bind<KnightEnemy>()
                 .AsSingle()
                 .WithArguments(maxHealth);

        // 📦 COMPORTAMIENTOS concretos (MBs)
        Container.Bind<IDamageBehaviour>()
                 .To<KnightDamageMB>()
                 .FromComponentOnRoot()
                 .AsSingle();

        Container.Bind<IMoveBehaviour<KnightEnemy>>()
                 .To<KnightMovementMB>()
                 .FromComponentOnRoot()
                 .AsSingle();

        Container.BindInterfacesAndSelfTo<KnightAttackMB>() // Bind for ITracker and IAttacker
                .FromComponentOnRoot()
                .AsSingle();

        // 🧩 CASOS DE USO
        Container.Bind<AttackUseCase<KnightEnemy>>()
                 .AsSingle();

        Container.Bind<DamageUseCase>()
                 .AsSingle();

        Container.Bind<TargetTrackingUseCase<KnightEnemy>>()
                 .AsSingle();

        // 🎯 PRESENTER de interacción (daño/curación)
        Container.Bind<DamagePresenter>()
                 .AsSingle();

        Container.Bind<KnightMovementService>()
            .AsSingle();
    }
}
