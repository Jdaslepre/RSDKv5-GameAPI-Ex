using RSDK;

namespace GameLogic;

public unsafe static class Game
{
    [UnmanagedCallersOnly(EntryPoint = "LinkGameLogicDLL")]
#if RETRO_REV02
    private static void LinkGameLogicDLL_Unmanaged(EngineInfo* info) => LinkGameLogicDLL(info);
#else
    private static void LinkGameLogicDLL_Unmanaged(EngineInfo info) => LinkGameLogicDLL(&info);
#endif

    [UnmanagedCallersOnly(EntryPoint = "LinkModLogic")]
    private static void LinkModLogic_Unmanaged(EngineInfo* info, char* id) => LinkModLogic(info, id);

    // 1 = RETRO_REV01
    // 2 = RETRO_REV02
    // 3 = RETRO_REV0U

    // These are independent from the project's preprocessor macros - and should only be
    // used and configured for the modInfo export.

    private const byte RETRO_REVISION = 3;
    private const byte GAME_VERSION = 6;
#if RETRO_USE_MOD_LOADER
#if RETRO_MOD_LOADER_VER_2
    private const byte RETRO_MOD_LOADER_VER = 2;
#else
    private const byte RETRO_MOD_LOADER_VER = 1;
#endif
#endif

    // -------------------------
    // LINK GAME/MOD LOGIC
    // -------------------------

    // Don't touch LinkGameLogicDLL or LinkModLogic, if you want code
    // to be ran after linking, use the LinkEmbeddedLogic function

#if RETRO_REV02
    private static void LinkGameLogicDLL(EngineInfo* info)
    {
        InitEngineInfo(info);
#else
    private static void LinkGameLogicDLL(EngineInfo info)
    {
        InitEngineInfo(&info);
#endif


        LinkEmbeddedLogic();
    }

#if RETRO_USE_MOD_LOADER
    [UnmanagedCallersOnly(EntryPoint = "modInfo")]
    public static ModVersionInfo modInfo()
    {
        return new ModVersionInfo { engineVer = RETRO_REVISION, gameVer = GAME_VERSION, modLoaderVer = RETRO_MOD_LOADER_VER };
    }

    public static bool32 LinkModLogic(EngineInfo* info, char* ModID)
    {
#if RETRO_REV02
        LinkGameLogicDLL(info);
#else
        LinkGameLogicDLL(*info);
#endif

        string? id = Marshal.PtrToStringUni((IntPtr)ModID);
        if (ModID != null && id != null) { Mod.id = id; }

        return true;
    }
#endif

    private static void LinkEmbeddedLogic()
    {

    }
}