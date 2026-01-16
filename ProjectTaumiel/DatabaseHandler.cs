using System;
using System.Data.SQLite;
using System.IO;
using Exiled.API.Features;

namespace ProjectTaumiel
{
    public static class DatabaseHandler
    {
        private static readonly object DBLock = new();
        private static SQLiteConnection _connection;

        internal static void InitializeDatabase()
        {
            lock (DBLock)
            {
                if (_connection != null)
                    return;

                var pluginFolder = Path.Combine(Paths.Plugins, Plugin.Instance.Name);
                Directory.CreateDirectory(pluginFolder);

                var dbPath = Path.Combine(pluginFolder, "ProjectTaumiel.db");
                var connectionString = $"Data Source={dbPath};Version=3;";

                _connection = new SQLiteConnection(connectionString);
                _connection.Open();

                using var command = _connection.CreateCommand();
                command.CommandText =
                    @"CREATE TABLE IF NOT EXISTS Players (
                        SteamId TEXT NOT NULL UNIQUE
                    );";
                command.ExecuteNonQuery();
            }
        }

        internal static void Shutdown()
        {
            lock (DBLock)
            {
                _connection?.Close();
                _connection?.Dispose();
                _connection = null;
            }
        }

        internal static bool IsPlayerExists(string steamId)
        {
            lock (DBLock)
            {
                using var cmd = _connection.CreateCommand();
                cmd.CommandText = "SELECT 1 FROM Players WHERE SteamId = @steamId LIMIT 1;";
                cmd.Parameters.AddWithValue("@steamId", steamId);

                return cmd.ExecuteScalar() != null;
            }
        }

        internal static bool AddPlayer(string steamId)
        {
            lock (DBLock)
            {
                try
                {
                    using var cmd = _connection.CreateCommand();
                    cmd.CommandText = "INSERT INTO Players (SteamId) VALUES (@steamId);";
                    cmd.Parameters.AddWithValue("@steamId", steamId);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SQLiteException)
                {
                    return false;
                }
            }
        }
        
        internal static bool RemovePlayer(string steamId)
        {
            lock (DBLock)
            {
                try
                {
                    using var cmd = _connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Players WHERE SteamId = @steamId;";
                    cmd.Parameters.AddWithValue("@steamId", steamId);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (SQLiteException)
                {
                    return false;
                }
            }
        }
    }
}
