using System;
using System.Data;
using Assets.Config;
using Assets.Scripts.Domain.Entities;
using Mono.Data.Sqlite;

namespace Assets.Scripts.Infrastructure.Data.sqlite
{
    public class UserModel
    {
        public UserModel()
        {
            using var conn = SqliteDatabase.GetInstance().GetConnection();
            conn.Open();
            CreateTable(conn);
        }

        private void CreateTable(IDbConnection conn)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS users (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    username TEXT NOT NULL,
                    password TEXT NOT NULL,
                    current_level TEXT,
                    score INTEGER DEFAULT 0,
                    position_x REAL DEFAULT 0,
                    position_y REAL DEFAULT 0,
                    enemies_eliminated INTEGER DEFAULT 0,
                    health INTEGER DEFAULT 100
                );";
            cmd.ExecuteNonQuery();
        }

        public UserData Register(UserData entity)
        {
            if (UserExists(entity.UserName))
                throw new Exception("Ya existe ese usuario.");

            using var conn = SqliteDatabase.GetInstance().GetConnection();
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO users (username, password)
                VALUES (@username, @password);";

            AddParameter(cmd, "@username", entity.UserName);
            AddParameter(cmd, "@password", Bcrypt.Encrypt(entity.Password));
            cmd.ExecuteNonQuery();

            return entity;
        }

        public UserData Login(UserData entity)
        {
            using var conn = SqliteDatabase.GetInstance().GetConnection();
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM users WHERE username = @username LIMIT 1";
            AddParameter(cmd, "@username", entity.UserName);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) throw new Exception("Usuario no encontrado");

            var hash = reader["password"].ToString();
            if (!Bcrypt.Compare(entity.Password, hash))
                throw new Exception("Contraseña incorrecta");

            return MapToUserData(reader);
        }

        public UserData LoadGame(int id)
        {
            using var conn = SqliteDatabase.GetInstance().GetConnection();
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM users WHERE id = @id";
            AddParameter(cmd, "@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return MapToUserData(reader);

            throw new Exception("Partida no encontrada");
        }

        public bool SaveGame(UserData entity)
        {
            using var conn = SqliteDatabase.GetInstance().GetConnection();
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE users SET
                    current_level = @level,
                    score = @score,
                    position_x = @x,
                    position_y = @y,
                    enemies_eliminated = @enemies,
                    health = @health
                WHERE username = @username";

            AddParameter(cmd, "@level", entity.CurrentLevel);
            AddParameter(cmd, "@score", entity.Score);
            AddParameter(cmd, "@x", entity.PositionX);
            AddParameter(cmd, "@y", entity.PositionY);
            AddParameter(cmd, "@enemies", entity.EnemiesEliminated);
            AddParameter(cmd, "@health", entity.Health);
            AddParameter(cmd, "@username", entity.UserName);

            return cmd.ExecuteNonQuery() > 0;
        }

        public void RestartGame(int id)
        {
            using var conn = SqliteDatabase.GetInstance().GetConnection();
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE users SET
                    current_level = NULL,
                    score = 0,
                    position_x = 0,
                    position_y = 0,
                    enemies_eliminated = 0,
                    health = 100
                WHERE id = @id";

            AddParameter(cmd, "@id", id);
            cmd.ExecuteNonQuery();
        }

        private bool UserExists(string username)
        {
            using var conn = SqliteDatabase.GetInstance().GetConnection();
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM users WHERE username = @username";
            AddParameter(cmd, "@username", username);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
        private UserData MapToUserData(IDataReader reader)
        {
            return new UserData
            {
                UserName = reader["username"].ToString(),
                Password = reader["password"].ToString(),
                CurrentLevel = reader["current_level"]?.ToString(),
                Score = Convert.ToInt32(reader["score"]),
                PositionX = Convert.ToSingle(reader["position_x"]),
                PositionY = Convert.ToSingle(reader["position_y"]),
                EnemiesEliminated = Convert.ToInt32(reader["enemies_eliminated"]),
                Health = Convert.ToInt32(reader["health"])
            };
        }

        private void AddParameter(IDbCommand cmd, string name, object value)
        {
            var param = cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value;
            cmd.Parameters.Add(param);
        }
    }
}
