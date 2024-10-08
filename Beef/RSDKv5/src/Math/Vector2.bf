using System;

namespace RSDK;

[CRepr] public struct Vector2
{
    public int32 x, y = .();

    public this()
    {
        x = 0;
        y = 0;
    }

    public this(RSDK.Vector2 other)
    {
        x = other.x;
        y = other.y;
    }

    public this(RSDK.Vector2* other)
    {
        x = other.x;
        y = other.y;
    }

    public this(int32 X, int32 Y)
    {
        x = X;
        y = Y;
    }

    public bool32 CheckOnScreen(RSDK.Vector2* range) mut { return RSDKTable.CheckPosOnScreen(&this, range); }
}

[CRepr] public struct Vector2<T> where T : var, IInteger
{
    public T x, y = .();

    public this()
    {
        x = 0;
        y = 0;
    }

    public this(RSDK.Vector2 other)
    {
        x = other.x;
        y = other.y;
    }

    public this(RSDK.Vector2* other)
    {
        x = other.x;
        y = other.y;
    }

    public this(T X, T Y)
    {
        x = X;
        y = Y;
    }

    public bool32 CheckOnScreen(RSDK.Vector2* range) mut { return RSDKTable.CheckPosOnScreen((.)&this, range); }
}