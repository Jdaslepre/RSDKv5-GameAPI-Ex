namespace RSDK;

[System.CRepr] public struct Vector2
{
    // ---------
    // Variables
    // ---------

    public int32 x = 0, y = 0;

    // ------------
    // Constructors
    // ------------

    public this()
    {
        x = 0;
        y = 0;
    }

    public this(Self other)
    {
        x = other.x;
        y = other.y;
    }

    public this(Self* other)
    {
        x = other.x;
        y = other.y;
    }

    public this(int32 X, int32 Y)
    {
        x = X;
        y = Y;
    }

    // ---------
    // Functions
    // ---------

    public bool32 CheckOnScreen(Self* range) mut => RSDKTable.CheckPosOnScreen(&this, range);

    // ---------
    // Operators
    // ---------

    public void operator +=(Self other) mut
    {
        x += other.x;
        y += other.y;
    }
    public void operator -=(Self other) mut
    {
        x -= other.x;
        y -= other.y;
    }
    public void operator *=(Self other) mut
    {
        x *= other.x;
        y *= other.y;
    }
    public void operator /=(Self other) mut
    {
        x /= other.x;
        y /= other.y;
    }
    public void operator &=(Self other) mut
    {
        x &= other.x;
        y &= other.y;
    }
    public void operator %=(Self other) mut
    {
        x %= other.x;
        y %= other.y;
    }
    public void operator ^=(Self other) mut
    {
        x ^= other.x;
        y ^= other.y;
    }

    public static Self operator +(ref Self lhs, ref Self rhs)
    {
        lhs += rhs;
        return lhs;
    }
    public static Self operator -(ref Self lhs, ref Self rhs)
    {
        lhs -= rhs;
        return lhs;
    }
    public static Self operator *(ref Self lhs, ref Self rhs)
    {
        lhs *= rhs;
        return lhs;
    }
    public static Self operator /(ref Self lhs, ref Self rhs)
    {
        lhs /= rhs;
        return lhs;
    }
    public static Self operator &(ref Self lhs, ref Self rhs)
    {
        lhs &= rhs;
        return lhs;
    }
    public static Self operator %(ref Self lhs, ref Self rhs)
    {
        lhs %= rhs;
        return lhs;
    }
    public static Self operator ^(ref Self lhs, ref Self rhs)
    {
        lhs ^= rhs;
        return lhs;
    }
}