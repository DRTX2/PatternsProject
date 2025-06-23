using UnityEngine;
using Zenject;

public class FlyingEyeInstaller : MonoInstaller
{
    [SerializeField] private float maxHealth = 50f;

    public override void InstallBindings()
    {
        // 🧠 ENTIDAD de dominio
        Container.Bind<FlyingEyeEnemy>()
                 .AsSingle()
                 .WithArguments(maxHealth);

        // 📦 COMPORTAMIENTOS concretos (MBs)
        Container.Bind<IDamageBehaviour>()
                 .To<FlyingEyeDamageMB>()
                 .FromComponentOnRoot()
                 .AsSingle();

        Container.Bind<IMoveBehaviour<FlyingEyeEnemy>>() // Volador implementa IMoveBehaviour<FlyingEyeEnemy>
                 .To<FlyingEyeMovementMB>()
                 .FromComponentOnRoot()
                 .AsSingle();

        Container.BindInterfacesAndSelfTo<FlyingEyeAttackMB>() // Bind for ITracker and IAttacker
             .FromComponentOnRoot()
             .AsSingle();

        // 🧩 CASOS DE USO
        Container.Bind<DamageUseCase>()
                 .AsSingle();

        Container.Bind<AttackUseCase<FlyingEyeEnemy>>()
                 .AsSingle();

        Container.Bind<TargetTrackingUseCase<FlyingEyeEnemy>>()
                 .AsSingle();

        // ❤️ INTERACCIÓN DE SALUD
        Container.Bind<DamagePresenter>()
                 .AsSingle();

        Container.Bind<FlyingEyeMovementService>()
            .AsSingle();
    }
}
