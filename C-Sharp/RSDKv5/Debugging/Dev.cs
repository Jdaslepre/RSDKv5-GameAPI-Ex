namespace RSDK;

#if RETRO_REV02
public static class ViewableVarTypes
{
    public const int VIEWVAR_INVALID = 0;
    public const int VIEWVAR_BOOL = 1;
    public const int VIEWVAR_UINT8 = 2;
    public const int VIEWVAR_UINT16 = 3;
    public const int VIEWVAR_UINT32 = 4;
    public const int VIEWVAR_INT8 = 5;
    public const int VIEWVAR_INT16 = 6;
    public const int VIEWVAR_INT32 = 7;
}

public static class PrintModes
{
    public const int PRINT_NORMAL = 0;
    public const int PRINT_POPUP = 1;
    public const int PRINT_ERROR = 2;
    public const int PRINT_FATAL = 3;
}
#else
public static class PrintMessageTypes
{
    public const int MESSAGE_STRING = 0;
    public const int MESSAGE_INT32 = 1;
    public const int MESSAGE_UINT32 = 2;
    public const int MESSAGE_FLOAT = 3;
}
#endif

public class Dev
{
#if RETRO_REV02
    public static unsafe void Print(int severity, string message, params object[] args) => RSDKTable.PrintLog(severity, string.Format(message, args));
    public static unsafe void PrintText(int severity, string message) => RSDKTable.PrintText(severity, message);
    public static unsafe void PrintString(int severity, ref RSDK.String str) => RSDKTable.PrintString(severity, ref str);
    public static unsafe void PrintUInt32(int severity, string message, UInt32 integer) => RSDKTable.PrintUInt32(severity, message, integer);
    public static unsafe void PrintInt32(int severity, string message, Int32 integer) => RSDKTable.PrintInt32(severity, message, integer);
    public static unsafe void PrintFloat(int severity, string message, float f) => RSDKTable.PrintFloat(severity, message, f);
    public static unsafe void PrintVector2(int severity, string message, Int32 x, Int32 y) => RSDKTable.PrintVector2(severity, message, new Vector2(x, y));
    public static unsafe void PrintVector2(int severity, string message, ref Vector2 vec) => RSDKTable.PrintVector2(severity, message, vec);
    public static unsafe void PrintVector2(int severity, string message, Vector2 vec) => RSDKTable.PrintVector2(severity, message, vec);
    public static unsafe void PrintHitbox(int severity, string message, ref Hitbox hitbox) => RSDKTable.PrintHitbox(severity, message, hitbox);
    public static unsafe void PrintHitbox(int severity, string message, Hitbox hitbox) => RSDKTable.PrintHitbox(severity, message, hitbox);

    public static unsafe void ClearViewableVariables() => RSDKTable.ClearViewableVariables();
    public static unsafe void AddViewableVariable(string name, void* value, int type, Int32 min, Int32 max) => RSDKTable.AddViewableVariable(name, value, type, min, max);
#else
    public static void Print(string message, Byte type) => RSDKTable.PrintMessage(message, type);
#endif
}