using static RSDK.EngineAPI;

namespace RSDK
{
    public struct Vector2
    {
        public int x, y;

        public Vector2()
        {
            x = 0;
            y = 0;
        }

        public Vector2(RSDK.Vector2 other)
        {
            x = other.x;
            y = other.y;
        }

        public unsafe Vector2(RSDK.Vector2* other)
        {
            x = other->x;
            y = other->y;
        }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public unsafe bool32 CheckOnScreen(Vector2 range)
        {
            fixed (RSDK.Vector2* v = &this) { return RSDKTable.CheckPosOnScreen(v, &range); }
        }
    }
}
