using UnityEngine;
using Zenject;
using System.Data;
using Assets.Scripts.Infrastructure.Data.sqlite;
using Assets.Config;
using Assets.Scripts.Domain.Interfaces;
using Assets.Scripts.Infrastructure.Repositories;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Presentation.Views;
using Assets.Scripts.Presentation.Controllers;
using Assets.Scripts.Presentation.Interfaces;
using Assets.Scripts.Application.Session;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Envs.Load();
        var dbPath = Envs.SQLITE_PATH;
        var options = new SqliteOptions(dbPath);
        try
        {
            SqliteDatabase.GetInstance().Initialize(options);

            using (var connection = SqliteDatabase.GetInstance().GetConnection())
            {
                connection.Open();
                Debug.Log("✅ Conexión a SQLite exitosa: " + dbPath);
                connection.Close();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("❌ Error al conectar con SQLite: " + ex.Message);
        }
        Container.Bind<IUserRepository>().To<UserRepositorySqlite>().AsSingle();
        Container.Bind<Session>().AsSingle().NonLazy();

    }
}
