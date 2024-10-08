using static RSDK.EngineAPI;

namespace RSDK.API
{
    public unsafe class API
    {
#if !RETRO_REV02
        public static void *GetAPIFunction(string funcName) { return RSDKTable.GetAPIFunction(funcName); }
#endif

#if RETRO_REV02
        public static int GetUserLanguage() { return APITable.GetUserLanguage(); }
        public static bool32 GetConfirmButtonFlip() { return APITable.GetConfirmButtonFlip(); }
        public static void ExitGame() => APITable.ExitGame();
        public static void LaunchManual() => APITable.LaunchManual();
#if RETRO_REV0U
        public static int GetDefaultGamepadType() { return APITable.GetDefaultGamepadType(); }
#endif
        public static bool32 IsOverlayEnabled(uint inputID) { return APITable.IsOverlayEnabled(inputID); }
        public static bool32 CheckDLC(int dlc) { return APITable.CheckDLC(dlc); }
#if RETRO_USE_EGS
        public static bool32 SetupExtensionOverlay() { return APITable.SetupExtensionOverlay(); }
        public static bool32 CanShowExtensionOverlay(int overlay) { return APITable.CanShowExtensionOverlay(overlay); }
#endif
        public static bool32 ShowExtensionOverlay(int overlay) { return APITable.ShowExtensionOverlay(overlay); }
#if RETRO_USE_EGS
        public static bool32 CanShowAltExtensionOverlay(int overlay) { return APITable.CanShowAltExtensionOverlay(overlay); }
        public static bool32 ShowAltExtensionOverlay(int overlay) { return APITable.ShowAltExtensionOverlay(overlay); }
        public static int GetConnectingStringID() { return APITable.GetConnectingStringID(); }
        public static void ShowLimitedVideoOptions(int id) => APITable.ShowLimitedVideoOptions(id);
#endif

#endif
    }
}
