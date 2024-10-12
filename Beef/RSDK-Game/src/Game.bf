using System;
using RSDK;

namespace GameLogic;

public class Game
{
    // 1 = RETRO_REV01
    // 2 = RETRO_REV02
    // 3 = RETRO_REV0U

    // These are independent from the project's preprocessor macros - and should only be
    // used and configured for the modInfo export.

    private const int RETRO_REVISION = 3;
    private const int GAME_VERSION = 6;
#if RETRO_USE_MOD_LOADER
    #if RETRO_MOD_LOADER_VER_2
    private const int RETRO_MOD_LOADER_VER = 2;
    #else
    private const int RETRO_MOD_LOADER_VER = 1;
    #endif
#endif

    // -------------------------
    // LINK GAME/MOD LOGIC
    // -------------------------

    // Don't touch LinkGameLogicDLL or LinkModLogic, if you want code
    // to be ran after linking, use the LinkEmbeddedLogic function

    [Export, CLink, AlwaysInclude]
#if RETRO_REV02
    public static void LinkGameLogicDLL(RSDK.EngineInfo* info)
    {
        InitEngineInfo(info);
#else
    public static void LinkGameLogicDLL(RSDK.EngineInfo info)
    {
        InitEngineInfo(&info);
#endif

        if (GameVariables.registerGlobals != null)
        {
#if RETRO_REV0U
            RSDKTable.RegisterGlobalVariables(GameVariables.registerGlobals, GameVariables.registerGlobalsSize, GameVariables.registerGlobalsInitCB);
#else
            RSDKTable.RegisterGlobalVariables(GameVariables.registerGlobals, GameVariables.registerGlobalsSize);
#endif
        }

        for (int32 r = 0; r < GameObject.registerObjectListCount; ++r)
        {
            var registration = &GameObject.registerObjectList[r];

            if (registration.name != null)
            {
#if RETRO_USE_MOD_LOADER
                if (registration.isModded) {
                #if RETRO_REV0U
                    modTable.RegisterObject(registration.staticVars, registration.modStaticVars, registration.name, registration.entityClassSize,
                                             registration.staticClassSize, registration.modStaticClassSize, registration.update,
                                             registration.lateUpdate, registration.staticUpdate, registration.draw, registration.create,
                                             registration.stageLoad, registration.editorLoad, registration.editorDraw, registration.serialize,
                                             registration.staticLoad, registration.inherit);
                #else
                    modTable.RegisterObject(registration.staticVars, registration.modStaticVars, registration.name, registration.entityClassSize,
                                             registration.staticClassSize, registration.modStaticClassSize, registration.update,
                                             registration.lateUpdate, registration.staticUpdate, registration.draw, registration.create,
                                             registration.stageLoad, registration.editorLoad, registration.editorDraw, registration.serialize,
                                             registration.inherit);
                #endif

                    continue;
                }
#endif


#if RETRO_REV0U
                RSDKTable.RegisterObject(registration.staticVars, registration.name, registration.entityClassSize, registration.staticClassSize,
                    => registration.update, => registration.lateUpdate, => registration.staticUpdate, => registration.draw,
                    => registration.create, => registration.stageLoad, => registration.editorLoad, => registration.editorDraw,
                    => registration.serialize, => registration.staticLoad);
#else
                RSDKTable.RegisterObject(registration.staticVars, registration.name, registration.entityClassSize, registration.staticClassSize,
                    => registration.update, => registration.lateUpdate, => registration.staticUpdate, => registration.draw,
                    => registration.create, => registration.stageLoad, => registration.editorDraw, => registration.editorLoad,
                    => registration.serialize);
#endif
            }
        }

#if RETRO_REV02
        for (int32 r = 0; r < GameObject.registerStaticListCount; ++r)
        {
            var registration = &GameObject.registerStaticList[r];

            if (registration.name != null)
                RSDKTable.RegisterStaticVariables(registration.staticVars, registration.name, registration.staticClassSize);
        }
#endif

        LinkEmbeddedLogic();
    }

#if RETRO_USE_MOD_LOADER
    [Export]
    public static RSDK.Mod.ModVersionInfo modInfo = .() { engineVer = RETRO_REVISION, gameVer = GAME_VERSION, modLoaderVer = RETRO_MOD_LOADER_VER };

    [Export, CLink, AlwaysInclude]
    public static bool32 LinkModLogic(RSDK.EngineInfo* info, char8* id)
    {
    #if RETRO_REV02
        LinkGameLogicDLL(info);
    #else
        LinkGameLogicDLL(*info);
    #endif

        RSDK.Mod.id = id;

        return true;
    }
#endif

    private static void LinkEmbeddedLogic()
    {

    }
}
