using System;

namespace RSDK;

public static class Math
{
    public static T MIN<T>(T a, T b) where T : var, IInteger
    {
        return (a < b) ? a : b;
    }

    public static T MAX<T>(T a, T b) where T : var, IInteger
    {
        return (a) > (b) ? a : b;
    }

    public static T CLAMP<T>(T value, T minimum, T maximum) where T : var, IInteger
    {
        return (((value) < (minimum)) ? (minimum) : (((value) > (maximum)) ? (maximum) : (value)));
    }

    public static T FABS<T>(T a) where T : var, IInteger
    {
        return (a) > default ? a : -a;
    }

    public static int SET_BIT(ref int value, int set, int pos) { return value ^= (-set ^ (value)) & (1 << (pos)); }
    public static int GET_BIT(int b, int pos) { return (b) >> (pos) & 1; }

    public static void* INT_TO_VOID(int x) { return (.)(size_t)(x); }
    public static void* FLOAT_TO_VOID(ref float x) { return INT_TO_VOID(*(int*)&x); }
    public static int32 VOID_TO_INT(void* x) { return (.)(size_t)(x); }
    public static T VOID_TO_INT<T>(void* x) where T : var, IInteger { return (.)(size_t)(x); }
    public static float VOID_TO_FLOAT(ref void* x) { return *(float*)&x; }

    public static int TO_FIXED(int x) { return (x) << 16; }
    public static int32 TO_FIXED(int32 x) { return (x) << 16; }
    public static uint TO_FIXED(uint x) { return (x) << 16; }
    public static uint32 TO_FIXED(uint32 x) { return (x) << 16; }
    public static float TO_FIXED_F(int x) { return (.)(x * 65536.0f); }
    public static float TO_FIXED_F(float x) { return x * 65536.0f; }

    public static int FROM_FIXED(int x) { return (x) >> 16; }
    public static int32 FROM_FIXED(int32 x) { return (x) >> 16; }
    public static uint FROM_FIXED(uint x) { return (x) >> 16; }
    public static uint32 FROM_FIXED(uint32 x) { return (x) >> 16; }
    public static float FROM_FIXED_F(int x) { return (.)(x / 65536.0f); }
    public static float FROM_FIXED_F(float x) { return x / 65536.0f; }

    // Functions
    public static int32 Sin1024(int32 angle) { return RSDKTable.Sin1024(angle); }
    public static int32 Cos1024(int32 angle) { return RSDKTable.Cos1024(angle); }
    public static int32 Tan1024(int32 angle) { return RSDKTable.Tan1024(angle); }
    public static int32 ASin1024(int32 angle) { return RSDKTable.ASin1024(angle); }
    public static int32 ACos1024(int32 angle) { return RSDKTable.ACos1024(angle); }

    public static int32 Sin512(int32 angle) { return RSDKTable.Sin512(angle); }
    public static int32 Cos512(int32 angle) { return RSDKTable.Cos512(angle); }
    public static int32 Tan512(int32 angle) { return RSDKTable.Tan512(angle); }
    public static int32 ASin512(int32 angle) { return RSDKTable.ASin512(angle); }
    public static int32 ACos512(int32 angle) { return RSDKTable.ACos512(angle); }

    public static int32 Sin256(int32 angle) { return RSDKTable.Sin256(angle); }
    public static int32 Cos256(int32 angle) { return RSDKTable.Cos256(angle); }
    public static int32 Tan256(int32 angle) { return RSDKTable.Tan256(angle); }
    public static int32 ASin256(int32 angle) { return RSDKTable.ASin256(angle); }
    public static int32 ACos256(int32 angle) { return RSDKTable.ACos256(angle); }

    public static int32 Rand(int32 min, int32 max) { return RSDKTable.Rand(min, max); }
    public static int32 RandSeeded(int32 min, int32 max, int32* seed) { return RSDKTable.RandSeeded(min, max, seed); }
    public static void SetRandSeed(int32 seed) => RSDKTable.SetRandSeed(seed);

    public static uint8 ATan2(int32 x, int32 y) { return RSDKTable.ATan2(x, y); }
}
