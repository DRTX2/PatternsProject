using UnityEngine;
using Zenject;
using Assets.Config;
using Assets.Scripts.Infrastructure.Data.sqlite;
using Assets.Scripts.Domain.Interfaces;
using Assets.Scripts.Infrastructure.Repositories;
using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Presentation.Controllers;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private ProjectileSpawner projectileSpawner;

    public override void InstallBindings()
    {
        DontDestroyOnLoad(gameObject);

        //Envs.Load();
        //var dbPath = Envs.SQLITE_PATH;
        //var options = new SqliteOptions(dbPath);

        //var db = SqliteDatabase.GetInstance();
        //db.Initialize(options);


        //Container.Bind<IUserRepository>()
        //         .To<UserRepositorySqlite>()
        //         .AsSingle();


        Container.Bind<IUserRepository>()
         .FromInstance(UserRepositoryJson.GetInstance())
         .AsSingle();


        Container.Bind<Session>()
                 .AsSingle()
                 .NonLazy();






 
        Container.Bind<PlayerFactory>().AsSingle();
        Container.Bind<LoadGameController>().AsSingle();

   
        {
            var session = Container.Resolve<Session>();
            var loader = Container.Resolve<LoadGameController>();
            loader.Load();

            var player = loader.LoadedPlayer
                         ?? new Player(100, 100, 0f, 0f, 0, 0);

            Container.Bind<Player>()
                     .FromInstance(player)
                     .AsSingle();
        }

  
        Container.Bind<SaveGameUseCase>().AsTransient();
        Container.Bind<RestartGameUseCase>().AsTransient();

        Container.Bind<SaveGameController>()
                 .AsSingle()
                 .WithArguments(
                     Container.Resolve<SaveGameUseCase>(),
                     Container.Resolve<Session>(),
                     Container.Resolve<Player>()
                 );
        Container.Bind<RestartGameController>()
                 .AsSingle()
                 .WithArguments(
                     Container.Resolve<RestartGameUseCase>(),
                     Container.Resolve<Session>(),
                     Container.Resolve<Player>()
                 );


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
    }
}