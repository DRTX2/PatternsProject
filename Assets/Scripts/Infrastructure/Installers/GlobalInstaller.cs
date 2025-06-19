using Assets.Scripts.Infrastructure.Data.mysql;
using UnityEngine;
using Zenject;
using System.Data;
using Assets.Scripts.Infrastructure.Data.sqlite; // Necesario si estás usando IDbConnection

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
       
        var dbPath = Application.dataPath + "/Database/unityGame.db";
        var options = new SqliteOptions(dbPath);

        try
        {
            SqliteDatabase.GetInstance().Initialize(options);

            using (var connection = SqliteDatabase.GetInstance().GetConnection())
            {
                connection.Open(); // 🔥 ¡esto es importante!
                Debug.Log("✅ Conexión a SQLite exitosa.");
                connection.Close();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("❌ Error al conectar con SQLite: " + ex.Message);
        }
    }
}
