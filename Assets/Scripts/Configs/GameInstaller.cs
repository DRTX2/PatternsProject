using Assets.Scripts.Application.Session;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private ProjectileSpawner projectileSpawner;
    [Inject] private Session _session;
    public override void InstallBindings()
    {
        var user = _session.CurrentUser;
        Player player;

        if (user != null)
        {
            player = new Player(
            maxHealth: 100,
            currentHealth: user.Health,
            positionX: user.PositionX,
            positionY: user.PositionY,
            enemiesEliminated: user.EnemiesEliminated,
            score: user.Score
        );
        }
        else {

            player = new Player(
                maxHealth: 100,
                currentHealth: 100,
                positionX: 11.11f,
                positionY: -2.77f,
                enemiesEliminated: 0,
                score: 0
            );
            Debug.LogError("Player instance is null, creating a default player. [FOR DEVELOP ENVIROMENT]");
        }


        Container.Bind<Player>()
                 .FromInstance(player)
                 .AsSingle();


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

        Container.BindInterfacesAndSelfTo<PlayerScoreMB>() // IMoveBehaviour<Player>, IJumpBehaviour<Player>, IRunBehaviour<Player>
                 .FromComponentInHierarchy()
                 .AsSingle();

        Container.Bind<IInputReceiver>()
                 .To<PlayerInputReceiver>()
                 .AsSingle();

        Container.Bind<PlayerInputView>()
                 .FromComponentInHierarchy()
                 .AsSingle();


        Container.Bind<AttackUseCase<Player>>()
                 .AsSingle();

        Container.Bind<DamageUseCase>()
                 .AsSingle();

        Container.Bind<HealUseCase>()
                 .AsSingle();

        Container.Bind<CollectScoreUseCase>()
                 .AsSingle();


      
        Container.Bind<HealthPresenter>()
                 .AsSingle();

        Container.Bind<DamagePresenter>()
                 .AsSingle();

        Container.Bind<ScorePresenter>()
                 .AsSingle();

    
        Container.Bind<PlayerMovementService>()
                 .AsSingle();

       
        Container.Bind<IProjectileSpawner>()
                 .FromInstance(projectileSpawner)
                 .AsSingle();

       
        Container.Bind<IAnimatorAdapter>()
                 .To<AnimatorAdapter>()
                 .AsSingle();

        Container.Bind<IPhysicsAdapter>()
                 .To<RigidbodyAdapter>()
                 .AsSingle();

        Container.Bind<IAttackStrategyFactory>()
                 .To<AttackStrategyFactory>()
                 .AsSingle();


      

      
        Container.Bind<CharacterEventBus>()
         .AsSingle()
         .NonLazy();
    }
}
