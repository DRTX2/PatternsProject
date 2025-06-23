using UnityEngine;

/// <summary>
/// VectorExtensions provides methods to convert between Unity's Vector2/Vector3 and the domain's Vector2D.
/// </summary>
public static class VectorExtensions
{
    // -------------------------
    // FROM Unity TO Domain
    // -------------------------

    public static Vector2D ToDomain(this Vector2 v)
    {
        return new Vector2D(v.x, v.y);
    }

    public static Vector2D ToDomain(this Vector3 v)
    {
        return new Vector2D(v.x, v.y); // Ignora Z por ser 2D
    }

    // -------------------------
    // FROM Domain TO Unity
    // -------------------------

    public static Vector2 ToUnity(this Vector2D v)
    {
        return new Vector2(v.X, v.Y);
    }

    public static Vector3 ToUnity(this Vector2D v, float z = 0f)
    {
        return new Vector3(v.X, v.Y, z);
    }
}
