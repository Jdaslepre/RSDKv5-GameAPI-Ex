using System;
using System.Runtime.CompilerServices;
using static RSDK.EngineAPI;

namespace RSDK
{
    public unsafe class MathRSDK
    {
        // RSDK.Math.Min   -> System.Math.Min
        // RSDK.Math.Max   -> System.Math.Max
        // RSDK.Math.Clamp -> System.Math.Clamp
        // RSDK.Math.Fabs  -> System.Math.Abs

        public static int SET_BIT(int value, int set, int pos) { return value ^= (-set ^ (value)) & (1 << (pos)); }
        public static int GET_BIT(int b, int pos) { return (b) >> (pos) & 1; }

        public static void* INT_TO_VOID(int x) { return (void*)(size_t)x; }
        public static void* FLOAT_TO_VOID(float x) { return INT_TO_VOID(*(int*)&x); }
        public static int VOID_TO_INT(void* x) { return (int)(size_t)x; }
        public static float VOID_TO_FLOAT(void* x) { return *(float*)&x; }

        public static int TO_FIXED(int x) { return (x) << 16; }
        public static uint TO_FIXED(uint x) { return (x) << 16; }
        public static float TO_FIXED_F(int x) { return (float)(x * 65536.0f); }
        public static float TO_FIXED_F(float x) { return x * 65536.0f; }

        public static int FROM_FIXED(int x) { return (x) >> 16; }
        public static uint FROM_FIXED(uint x) { return (x) >> 16; }
        public static float FROM_FIXED_F(int x) { return (float)(x / 65536.0f); }
        public static float FROM_FIXED_F(float x) { return x / 65536.0f; }

        // Functions
        public static int Sin1024(int angle) { return RSDKTable.Sin1024(angle); }
        public static int Cos1024(int angle) { return RSDKTable.Cos1024(angle); }
        public static int Tan1024(int angle) { return RSDKTable.Tan1024(angle); }
        public static int ASin1024(int angle) { return RSDKTable.ASin1024(angle); }
        public static int ACos1024(int angle) { return RSDKTable.ACos1024(angle); }
        public static int Sin512(int angle) { return RSDKTable.Sin512(angle); }
        public static int Cos512(int angle) { return RSDKTable.Cos512(angle); }
        public static int Tan512(int angle) { return RSDKTable.Tan512(angle); }
        public static int ASin512(int angle) { return RSDKTable.ASin512(angle); }
        public static int ACos512(int angle) { return RSDKTable.ACos512(angle); }
        public static int Sin256(int angle) { return RSDKTable.Sin256(angle); }
        public static int Cos256(int angle) { return RSDKTable.Cos256(angle); }
        public static int Tan256(int angle) { return RSDKTable.Tan256(angle); }
        public static int ASin256(int angle) { return RSDKTable.ASin256(angle); }
        public static int ACos256(int angle) { return RSDKTable.ACos256(angle); }

        public static int Rand(int min, int max) { return RSDKTable.Rand(min, max); }
        public static int RandSeeded(int min, int max, int* seed) { return RSDKTable.RandSeeded(min, max, seed); }
        public static void SetRandSeed(int seed) => RSDKTable.SetRandSeed(seed);

        public static byte ATan2(int x, int y) { return RSDKTable.ATan2(x, y); }
    }
}
