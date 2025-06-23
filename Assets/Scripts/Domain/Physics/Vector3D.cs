using System;

/// <summary>
/// Represents a 3D vector with float components.
/// Abstracts 3D physics calculations, movement, and other vector operations.
/// </summary>

public struct Vector3D
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public Vector3D(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Vector3D Zero => new Vector3D(0, 0, 0);
    public static Vector3D One => new Vector3D(1, 1, 1);

    public float Magnitude => (float)Math.Sqrt(X * X + Y * Y + Z * Z);

    public Vector3D Normalized
    {
        get
        {
            float mag = Magnitude;
            return mag > 0 ? new Vector3D(X / mag, Y / mag, Z / mag) : Zero;
        }
    }

    public static Vector3D operator +(Vector3D a, Vector3D b) => new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vector3D operator -(Vector3D a, Vector3D b) => new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    public static Vector3D operator *(Vector3D a, float scalar) => new Vector3D(a.X * scalar, a.Y * scalar, a.Z * scalar);

    public static float Distance(Vector3D a, Vector3D b)
    {
        float dx = b.X - a.X;
        float dy = b.Y - a.Y;
        float dz = b.Z - a.Z;
        return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }

    public override string ToString() => $"({X}, {Y}, {Z})";
}
