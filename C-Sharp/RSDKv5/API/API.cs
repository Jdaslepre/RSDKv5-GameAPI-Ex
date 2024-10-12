namespace RSDK.API;

public unsafe class API
{
#if !RETRO_REV02
    public static void *GetAPIFunction(string funcName) => RSDKTable->GetAPIFunction(funcName);
#endif

#if RETRO_REV02
    public static int GetUserLanguage() => APITable->GetUserLanguage();
    public static bool32 GetConfirmButtonFlip() => APITable->GetConfirmButtonFlip();
    public static void ExitGame() => APITable->ExitGame();
    public static void LaunchManual() => APITable->LaunchManual();
#if RETRO_REV0U
    public static int GetDefaultGamepadType() => APITable->GetDefaultGamepadType();
#endif
    public static bool32 IsOverlayEnabled(uint inputID) => APITable->IsOverlayEnabled(inputID);
    public static bool32 CheckDLC(int dlc) => APITable->CheckDLC(dlc);
#if RETRO_USE_EGS
    public static bool32 SetupExtensionOverlay() => APITable->SetupExtensionOverlay();
    public static bool32 CanShowExtensionOverlay(int overlay) => APITable->CanShowExtensionOverlay(overlay);
#endif
    public static bool32 ShowExtensionOverlay(int overlay) => APITable->ShowExtensionOverlay(overlay);
#if RETRO_USE_EGS
    public static bool32 CanShowAltExtensionOverlay(int overlay) => APITable->CanShowAltExtensionOverlay(overlay);
    public static bool32 ShowAltExtensionOverlay(int overlay) => APITable->ShowAltExtensionOverlay(overlay);
    public static int GetConnectingStringID() => APITable->GetConnectingStringID();
    public static void ShowLimitedVideoOptions(int id) => APITable->ShowLimitedVideoOptions(id);
#endif

#endif
}