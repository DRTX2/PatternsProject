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

        // 1) Configuración de SQLite (igual que antes)…
        Envs.Load();
        var dbPath = Envs.SQLITE_PATH;
        var options = new SqliteOptions(dbPath);
        try
        {
            var db = SqliteDatabase.GetInstance();
            db.Initialize(options);
            using var conn = db.GetConnection();
            conn.Open();
            Debug.Log($"✅ Conexión SQLite exitosa: {dbPath}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"❌ Error conectando con SQLite: {ex.Message}");
        }

        // 2) Repositorios y sesión
        Container.Bind<IUserRepository>()
                 .To<UserRepositorySqlite>()
                 .AsSingle();
        Container.Bind<Session>()
                 .AsSingle()
                 .NonLazy();

        // 3) PlayerFactory y LoadGameController
        Container.Bind<PlayerFactory>().AsSingle();
        Container.Bind<LoadGameController>().AsSingle();

        // 4) **IMPORTANTE: bindea aquí tu Player** antes de cualquier SaveGameController
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

        // 5) Casos de uso y controladores de guardado/reinicio
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

        // 6) Servicios y adaptadores
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