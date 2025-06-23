using System.IO;

namespace Assets.Config
{
    public static class Envs
    {
        public static string SQLITE_PATH;

        public static void Load()
        {
            
            var path = Path.Combine(Directory.GetCurrentDirectory(), ".env");

            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;

                    var parts = line.Split('=', 2);
                    if (parts.Length == 2 && parts[0].Trim() == "SQLITE_PATH")
                    {
                        SQLITE_PATH = parts[1].Trim();
                        return;
                    }
                }
            }

          
            SQLITE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "GameData.db");
        }
    }
}
