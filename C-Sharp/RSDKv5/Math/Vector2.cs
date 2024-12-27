namespace RSDK;

public struct Vector2
{
    // ---------
    // Variables
    // ---------

    public int x = 0, y = 0;

    // ------------
    // Constructors
    // ------------

    public Vector2() => x = y = 0;

    public Vector2(Vector2 other)
    {
        x = other.x;
        y = other.y;
    }

    public Vector2(ref Vector2 other)
    {
        x = other.x;
        y = other.y;
    }

    public Vector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    // ---------
    // Functions
    // ---------

    public unsafe bool32 CheckOnScreen(ref Vector2 range) => RSDKTable.CheckPosOnScreen(ref this, ref range);
    public unsafe bool32 CheckOnScreen(Vector2 range) => RSDKTable.CheckPosOnScreen(ref this, ref range);

    // ---------
    // Operators
    // ---------

    public static Vector2 operator +(Vector2 lhs, Vector2 rhs) => new(lhs.x + rhs.x, lhs.y + rhs.y);
    public static Vector2 operator -(Vector2 lhs, Vector2 rhs) => new(lhs.x - rhs.x, lhs.y - rhs.y);
    public static Vector2 operator *(Vector2 lhs, Vector2 rhs) => new(lhs.x * rhs.x, lhs.y * rhs.y);
    public static Vector2 operator /(Vector2 lhs, Vector2 rhs) => new(lhs.x / rhs.x, lhs.y / rhs.y);
    public static Vector2 operator &(Vector2 lhs, Vector2 rhs) => new(lhs.x & rhs.x, lhs.y & rhs.y);
    public static Vector2 operator %(Vector2 lhs, Vector2 rhs) => new(lhs.x % rhs.x, lhs.y % rhs.y);
    public static Vector2 operator ^(Vector2 lhs, Vector2 rhs) => new(lhs.x ^ rhs.x, lhs.y ^ rhs.y);
}
