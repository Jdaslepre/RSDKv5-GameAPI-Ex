using System;

namespace RSDK;

public enum EngineStates : uint8
{
    ENGINESTATE_LOAD,
    ENGINESTATE_REGULAR,
    ENGINESTATE_PAUSED,
    ENGINESTATE_FROZEN,
    ENGINESTATE_STEPOVER = 4,
    ENGINESTATE_DEVMENU  = 8,
    ENGINESTATE_VIDEOPLAYBACK,
    ENGINESTATE_SHOWIMAGE,
#if RETRO_REV02
    ENGINESTATE_ERRORMSG,
    ENGINESTATE_ERRORMSG_FATAL,
#endif
    ENGINESTATE_NONE,
#if RETRO_REV0U
    // Prolly origins-only, called by the ending so I assume this handles playing ending movies and returning to menu
    ENGINESTATE_GAME_FINISHED,
#endif
}

[CRepr] public struct SceneListInfo
{
    public uint32[4] hash;
    public char8[0x20] name;
    public uint16 sceneOffsetStart;
    public uint16 sceneOffsetEnd;
    public uint8 sceneCount;
}

[CRepr] public struct SceneListEntry
{
    public uint32[4] hash;
    public char8[0x20] name;
    public char8[0x10] folder;
    public char8[0x08] id;
#if RETRO_REV02
    public uint8 filter;
#endif
}

[CRepr] public struct SceneInfo
{
    public GameObject.Entity* entity;
    public RSDK.SceneListEntry* listData;
    public RSDK.SceneListInfo* listCategory;
    public int32 timeCounter;
    public int32 currentDrawGroup;
    public int32 currentScreenID;
    public uint16 listPos;
    public uint16 entitySlot;
    public uint16 createSlot;
    public uint16 classCount;
    public bool32 inEditor;
    public bool32 effectGizmo;
    public bool32 debugMode;
    public bool32 useGlobalScripts;
    public bool32 timeEnabled;
    public uint8 activeCategory;
    public uint8 categoryCount;
    public EngineStates state;
#if RETRO_REV02
    public uint8 filter;
#endif
    public uint8 milliseconds;
    public uint8 seconds;
    public uint8 minutes;
}

public static class Stage
{
    public static bool32 CheckSceneFolder(char8* folderName) { return RSDKTable.CheckSceneFolder(folderName); }
    public static bool32 CheckValidScene() { return RSDKTable.CheckValidScene(); }
    public static void SetScene(char8* categoryName, char8* sceneName) => RSDKTable.SetScene(categoryName, sceneName);
    public static void LoadScene() => RSDKTable.LoadScene();
    public static void SetEngineState(RSDK.EngineStates state) => RSDKTable.SetEngineState((.)state);
#if RETRO_REV02
    public static void ForceHardReset(bool32 shouldHardReset) => RSDKTable.ForceHardReset(shouldHardReset);
#endif

    public static RSDK.ScanlineInfo* GetScanlines() { return RSDKTable.GetScanlines(); }
}