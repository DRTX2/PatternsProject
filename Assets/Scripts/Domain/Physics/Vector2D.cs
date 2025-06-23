using System;
/// <summary>
/// Represents a 2D vector with X and Y components.
/// Use this struct for 2D physics calculations, movement, and other vector operations.
/// </summary>
public struct Vector2D
{
    public float X { get; set; }
    public float Y { get; set; }

    public Vector2D(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static Vector2D Zero => new Vector2D(0, 0);
    public static Vector2D One => new Vector2D(1, 1);
    public static Vector2D Up => new Vector2D(0, 1);
    public static Vector2D Down => new Vector2D(0, -1);
    public static Vector2D Left => new Vector2D(-1, 0);
    public static Vector2D Right => new Vector2D(1, 0);

    public float Magnitude => (float)Math.Sqrt(X * X + Y * Y);

    public Vector2D Normalized
    {
        get
        {
            float mag = Magnitude;
            return mag > 0 ? new Vector2D(X / mag, Y / mag) : Zero;
        }
    }

    public static Vector2D operator +(Vector2D a, Vector2D b) => new Vector2D(a.X + b.X, a.Y + b.Y);
    public static Vector2D operator -(Vector2D a, Vector2D b) => new Vector2D(a.X - b.X, a.Y - b.Y);
    public static Vector2D operator *(Vector2D a, float scalar) => new Vector2D(a.X * scalar, a.Y * scalar);
    public static Vector2D operator /(Vector2D a, float scalar) => new Vector2D(a.X / scalar, a.Y / scalar);

    public static float Dot(Vector2D a, Vector2D b) => a.X * b.X + a.Y * b.Y;

    public static float Distance(Vector2D a, Vector2D b)
    {
        float dx = b.X - a.X;
        float dy = b.Y - a.Y;
        return (float)Math.Sqrt(dx * dx + dy * dy);
    }

    public override string ToString() => $"({X}, {Y})";
}
