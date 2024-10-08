using static RSDK.EngineAPI;

namespace RSDK
{
    public unsafe class Dev
    {
#if RETRO_REV02
        public enum ViewableVarTypes
        {
            VIEWVAR_INVALID,
            VIEWVAR_BOOL,
            VIEWVAR_UINT8,
            VIEWVAR_UINT16,
            VIEWVAR_UINT32,
            VIEWVAR_INT8,
            VIEWVAR_INT16,
            VIEWVAR_INT32,
        }

        public enum PrintModes
        {
            PRINT_NORMAL,
            PRINT_POPUP,
            PRINT_ERROR,
            PRINT_FATAL,
        }
#else
        public enum PrintMessageTypes
        {
            MESSAGE_STRING,
            MESSAGE_INT32,
            MESSAGE_UINT32,
            MESSAGE_FLOAT,
        }
#endif

#if RETRO_REV02
        public static void Print(PrintModes severity, string message, params object[] args) => RSDKTable.PrintLog(severity, string.Format(message, args));
        public static void PrintText(PrintModes severity, string message) => RSDKTable.PrintText(severity, message);
        public static void PrintString(PrintModes severity, RSDK.String* str) => RSDKTable.PrintString(severity, str);
        public static void PrintUInt32(PrintModes severity, string message, uint integer) => RSDKTable.PrintUInt32(severity, message, integer);
        public static void PrintInt32(PrintModes severity, string message, int integer) => RSDKTable.PrintInt32(severity, message, integer);
        public static void PrintFloat(PrintModes severity, string message, float f) => RSDKTable.PrintFloat(severity, message, f);
        public static void PrintVector2(PrintModes severity, string message, int x, int y) => RSDKTable.PrintVector2(severity, message, new RSDK.Vector2(x, y));
        public static void PrintVector2(PrintModes severity, string message, Vector2* vec) => RSDKTable.PrintVector2(severity, message, *vec);
        public static void PrintVector2(PrintModes severity, string message, Vector2 vec) => RSDKTable.PrintVector2(severity, message, vec);
        public static void PrintHitbox(PrintModes severity, string message, Hitbox* hitbox) => RSDKTable.PrintHitbox(severity, message, *hitbox);
        public static void PrintHitbox(PrintModes severity, string message, Hitbox hitbox) => RSDKTable.PrintHitbox(severity, message, hitbox);

        public static void ClearViewableVariables() => RSDKTable.ClearViewableVariables();
        public static void AddViewableVariable(string name, void* value, ViewableVarTypes type, int min, int max) => RSDKTable.AddViewableVariable(name, value, type, min, max);
#else
        public static void Print(PrintModes severity, string message, params object[] args) => RSDKTable.PrintMessage(string.Format(message, args), PrintMessageTypes.MESSAGE_STRING);
#endif
    }
}
