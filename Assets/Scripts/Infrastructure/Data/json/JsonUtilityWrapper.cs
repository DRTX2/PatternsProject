using System;
using System.Collections.Generic;
using UnityEngine;

public static class JsonUtilityWrapper
{
    [Serializable]
    private class Wrapper<T>
    {
        public List<T> Items;
    }

    public static string ToJsonList<T>(List<T> list)
    {
        var wrapper = new Wrapper<T> { Items = list };
        return JsonUtility.ToJson(wrapper, true);
    }

    public static List<T> FromJsonList<T>(string json)
    {
        var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper?.Items ?? new List<T>();
    }
}
