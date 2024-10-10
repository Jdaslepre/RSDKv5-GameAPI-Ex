namespace RSDK.API;

static
{
#if !RETRO_REV02
    public static void* GetAPIFunction(char8* funcName) => RSDKTable.GetAPIFunction(funcName);
#endif

#if RETRO_REV02
    public static int32 GetUserLanguage() => APITable.GetUserLanguage();
    public static bool32 GetConfirmButtonFlip() => APITable.GetConfirmButtonFlip();
    public static void ExitGame() => APITable.ExitGame();
    public static void LaunchManual() => APITable.LaunchManual();
#if RETRO_REV0U
    public static int32 GetDefaultGamepadType() => APITable.GetDefaultGamepadType();
#endif
    public static bool32 IsOverlayEnabled(uint32 inputID) => APITable.IsOverlayEnabled(inputID);
    public static bool32 CheckDLC(int32 dlc) => APITable.CheckDLC(dlc);
#if RETRO_USE_EGS
    public static bool32 SetupExtensionOverlay() => APITable.SetupExtensionOverlay();
    public static bool32 CanShowExtensionOverlay(int32 overlay) => APITable.CanShowExtensionOverlay(overlay);
#endif
    public static bool32 ShowExtensionOverlay(int32 overlay) => APITable.ShowExtensionOverlay(overlay);
#if RETRO_USE_EGS
    public static bool32 CanShowAltExtensionOverlay(int32 overlay) => APITable.CanShowAltExtensionOverlay(overlay);
    public static bool32 ShowAltExtensionOverlay(int32 overlay) => APITable.ShowAltExtensionOverlay(overlay);
    public static int32 GetConnectingStringID() => APITable.GetConnectingStringID();
    public static bool32 ShowLimitedVideoOptions(int32 id) => APITable.ShowLimitedVideoOptions(id);
#endif
#endif
}