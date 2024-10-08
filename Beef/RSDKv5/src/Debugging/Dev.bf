using System;

namespace RSDK;

#if RETRO_REV02
public enum ViewableVarTypes : uint8 {
    INVALID,
    BOOL,
    UINT8,
    UINT16,
    UINT32,
    INT8,
    INT16,
    INT32,
}

public enum PrintModes : uint8 {
    NORMAL,
    POPUP,
    ERROR,
    FATAL,
#if RETRO_REV0U
    SCRIPTERR,
#endif
}
#else
public enum PrintMessageTypes : uint8
{
    STRING,
    INT32,
    UINT32,
    FLOAT,
}
#endif

public static class Dev
{
#if RETRO_REV02
    public static void Print(PrintModes severity, char8 *message, ...)
    {
        VarArgs vArgs = .();
        vArgs.Start!();
        RSDKTable.PrintLog((.)severity, message, vArgs.ToVAList());
        vArgs.End!();
    }

    public static void PrintText(PrintModes severity, char8 *message) => RSDKTable.PrintText((.)severity, message); 
    public static void PrintString(PrintModes severity, RSDK.String *string, ...) => RSDKTable.PrintString((.)severity, string); 
    public static void PrintUInt32(PrintModes severity, char8 *message, uint32 integer) => RSDKTable.PrintUInt32((.)severity, message, integer); 
    public static void PrintInt32(PrintModes severity, char8 *message, int32 integer) => RSDKTable.PrintInt32((.)severity, message, integer);
    public static void PrintFloat(PrintModes severity, char8 *message, float f) => RSDKTable.PrintFloat((.)severity, message, f);
    public static void PrintVector2(PrintModes severity, char8 *message, int32 x, int32 y) => RSDKTable.PrintVector2((.)severity, message, RSDK.Vector2(x, y));
    public static void PrintVector2(PrintModes severity, char8 *message, RSDK.Vector2 *vec) => RSDKTable.PrintVector2((.)severity, message, *vec);
    public static void PrintVector2(PrintModes severity, char8 *message, RSDK.Vector2 vec) => RSDKTable.PrintVector2((.)severity, message, vec); 
    public static void PrintHitbox(PrintModes severity, char8 *message, RSDK.Hitbox *hitbox) => RSDKTable.PrintHitbox((.)severity, message, *hitbox);
    public static void PrintHitbox(PrintModes severity, char8 *message, RSDK.Hitbox hitbox) => RSDKTable.PrintHitbox((.)severity, message, hitbox);

    public static void ClearViewableVariables() => RSDKTable.ClearViewableVariables();
    public static void AddViewableVariable(char8 *name, int *value, ViewableVarTypes type, int32 min, int32 max) => RSDKTable.AddViewableVariable(name, value, (.)type, min, max);
#else
    public static void Print(char8* message, uint8 severity, ...)
    {
        VarArgs vArgs = VarArgs();
        vArgs.Start!();
        // RSDKTable.PrintMessage(message, vArgs.ToVAList(), (.)PrintMessageTypes.MESSAGE_STRING);
        RSDKTable.PrintMessage(message, (.)PrintMessageTypes.MESSAGE_STRING);
        vArgs.End!();
    }
#endif
}
