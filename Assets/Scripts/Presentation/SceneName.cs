using System;
using System.Collections.Generic;


namespace Assets.Scripts.Presentation
{
    public static class SceneNames
    {

        public static readonly Dictionary<SceneName, string> Map = new()
{
    { SceneName.Login_RegisterScene, "Login_RegisterScene" },
    { SceneName.Initial_MenuScene, "Initial_MenuScene" },
    { SceneName.Level1Scene, "Level1Scene" },
    { SceneName.Level2Scene, "Level2Scene" },
    { SceneName.Level3Scene, "Level3Scene" }
};

        public static string Get(SceneName key)
        {
            return Map.TryGetValue(key, out var value) ? value : null;
        }
    }
    public enum SceneName
    {
        Login_RegisterScene,
        Initial_MenuScene,
        Level1Scene,
        Level2Scene,
        Level3Scene,

    }
}
