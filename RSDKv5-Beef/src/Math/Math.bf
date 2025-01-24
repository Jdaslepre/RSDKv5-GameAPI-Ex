using System;

namespace RSDK;

static
{
    public static T MIN<T>(T a, T b) where T : var, IInteger => (a < b) ? a : b;
    public static T MAX<T>(T a, T b) where T : var, IInteger => (a) > (b) ? a : b;

    public static T CLAMP<T>(T value, T minimum, T maximum) where T : var, IInteger => (((value) < (minimum)) ? (minimum) : (((value) > (maximum)) ? (maximum) : (value)));

    public static T FABS<T>(T a) where T : var, IInteger => (a) > default ? a : -a;


    public static int SET_BIT(ref int value, int set, int pos)     => value ^= (-set ^ (value)) & (1 << (pos));
    public static int GET_BIT(int b, int pos)                      => (b) >> (pos) & 1;

    public static T GET_BIT<T>(T b, T pos) where T : var, IInteger                => (b) >> (pos) & 1;
    public static T SET_BIT<T>(ref T value, T set, T pos) where T : var, IInteger => value ^= (-set ^ (value)) & (1 << (pos));

    public static void* INT_TO_VOID(int x)         => (.)(size_t)(x);
    public static void* FLOAT_TO_VOID(ref float x) => INT_TO_VOID(*(int*)&x);
    public static int32 VOID_TO_INT(void* x)       => (.)(size_t)(x);
    public static float VOID_TO_FLOAT(ref void* x) => *(float*)&x;

    public static void* INT_TO_VOID<T>(T x) where T : var, IInteger => (.)(size_t)(x);
    public static T VOID_TO_INT<T>(void* x) where T : var, IInteger => (.)(size_t)(x);

    public static int TO_FIXED(int x)       => (x) << 16;
    public static int32 TO_FIXED(int32 x)   => (x) << 16;
    public static uint32 TO_FIXED(uint32 x) => (x) << 16;
    public static float TO_FIXED_F(int x)   => (x * 65536.0f);
    public static float TO_FIXED_F(float x) => x * 65536.0f;

    public static int FROM_FIXED(int x)       => (x) >> 16;
    public static int32 FROM_FIXED(int32 x)   => (x) >> 16;
    public static uint32 FROM_FIXED(uint32 x) => (x) >> 16;
    public static float FROM_FIXED_F(int x)   => (x / 65536.0f);
    public static float FROM_FIXED_F(float x) => x / 65536.0f;

    public static T TO_FIXED<T>(T x) where T : var, IInteger   => (x) << 16;
    public static T FROM_FIXED<T>(T x) where T : var, IInteger => (x) >> 16;
}

public static class Math
{
    public static int32 Sin1024(int32 angle)  => RSDKTable.Sin1024(angle);
    public static int32 Cos1024(int32 angle)  => RSDKTable.Cos1024(angle);
    public static int32 Tan1024(int32 angle)  => RSDKTable.Tan1024(angle);
    public static int32 ASin1024(int32 angle) => RSDKTable.ASin1024(angle);
    public static int32 ACos1024(int32 angle) => RSDKTable.ACos1024(angle);

    public static int32 Sin512(int32 angle)  => RSDKTable.Sin512(angle);
    public static int32 Cos512(int32 angle)  => RSDKTable.Cos512(angle);
    public static int32 Tan512(int32 angle)  => RSDKTable.Tan512(angle);
    public static int32 ASin512(int32 angle) => RSDKTable.ASin512(angle);
    public static int32 ACos512(int32 angle) => RSDKTable.ACos512(angle);

    public static int32 Sin256(int32 angle)  => RSDKTable.Sin256(angle);
    public static int32 Cos256(int32 angle)  => RSDKTable.Cos256(angle);
    public static int32 Tan256(int32 angle)  => RSDKTable.Tan256(angle);
    public static int32 ASin256(int32 angle) => RSDKTable.ASin256(angle);
    public static int32 ACos256(int32 angle) => RSDKTable.ACos256(angle);

    public static int32 Rand(int32 min, int32 max)                    => RSDKTable.Rand(min, max);
    public static int32 RandSeeded(int32 min, int32 max, int32* seed) => RSDKTable.RandSeeded(min, max, seed);
    public static void SetRandSeed(int32 seed)                        => RSDKTable.SetRandSeed(seed);

    public static uint8 ATan2(int32 x, int32 y) => RSDKTable.ATan2(x, y);

    public static T Sin1024<T>(T angle) where T : var, IInteger  => RSDKTable.Sin1024(angle);
    public static T Cos1024<T>(T angle) where T : var, IInteger  => RSDKTable.Cos1024(angle);
    public static T Tan1024<T>(T angle) where T : var, IInteger  => RSDKTable.Tan1024(angle);
    public static T ASin1024<T>(T angle) where T : var, IInteger => RSDKTable.ASin1024(angle);
    public static T ACos1024<T>(T angle) where T : var, IInteger => RSDKTable.ACos1024(angle);

    public static T Sin512<T>(T angle) where T : var, IInteger  => RSDKTable.Sin512(angle);
    public static T Cos512<T>(T angle) where T : var, IInteger  => RSDKTable.Cos512(angle);
    public static T Tan512<T>(T angle) where T : var, IInteger  => RSDKTable.Tan512(angle);
    public static T ASin512<T>(T angle) where T : var, IInteger => RSDKTable.ASin512(angle);
    public static T ACos512<T>(T angle) where T : var, IInteger => RSDKTable.ACos512(angle);

    public static T Sin256<T>(T angle) where T : var, IInteger  => RSDKTable.Sin256(angle);
    public static T Cos256<T>(T angle) where T : var, IInteger  => RSDKTable.Cos256(angle);
    public static T Tan256<T>(T angle) where T : var, IInteger  => RSDKTable.Tan256(angle);
    public static T ASin256<T>(T angle) where T : var, IInteger => RSDKTable.ASin256(angle);
    public static T ACos256<T>(T angle) where T : var, IInteger => RSDKTable.ACos256(angle);

    public static T Rand<T>(T min, T max) where T : var, IInteger                => RSDKTable.Rand(min, max);
    public static T RandSeeded<T>(T min, T max, T* seed) where T : var, IInteger => RSDKTable.RandSeeded(min, max, seed);
    public static T SetRandSeed<T>(T seed) where T : var, IInteger               => RSDKTable.SetRandSeed(seed);
}
