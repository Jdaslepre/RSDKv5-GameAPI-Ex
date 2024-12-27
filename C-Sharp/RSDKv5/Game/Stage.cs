namespace RSDK;

public enum EngineStates : byte
{
    ENGINESTATE_LOAD,
    ENGINESTATE_REGULAR,
    ENGINESTATE_PAUSED,
    ENGINESTATE_FROZEN,
    ENGINESTATE_STEPOVER = 4,
    ENGINESTATE_DEVMENU = 8,
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

public struct SceneListInfo
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public uint[] hash;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x20)]
    public char[] name;

    public ushort sceneOffsetStart;
    public ushort sceneOffsetEnd;
    public byte sceneCount;
}

public struct SceneListEntry
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public uint[] hash;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x20)]
    public char[] name;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x10)]
    public char[] folder;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x04)]
    public char[] id;

#if RETRO_REV02
    public byte filter;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public byte[] padding;
#endif
}

public unsafe struct SceneInfo
{
    public GameObject.Entity* entity;
    public SceneListEntry* listData;
    public SceneListInfo* listCategory;
    public int timeCounter;
    public int currentDrawGroup;
    public int currentScreenID;
    public ushort listPos;
    public ushort entitySlot;
    public ushort createSlot;
    public ushort classCount;
    public bool32 inEditor;
    public bool32 effectGizmo;
    public bool32 debugMode;
    public bool32 useGlobalScripts;
    public bool32 timeEnabled;
    public byte activeCategory;
    public byte categoryCount;
    public byte state;
#if RETRO_REV02
    public byte filter;
#endif
    public byte milliseconds;
    public byte seconds;
    public byte minutes;
}

public unsafe static class Stage
{
    public static bool32 CheckSceneFolder(string folderName) => RSDKTable.CheckSceneFolder(folderName);
    public static bool32 CheckValidScene() => RSDKTable.CheckValidScene();
    public static void SetScene(string categoryName, string sceneName) => RSDKTable.SetScene(categoryName, sceneName);
    public static void LoadScene() => RSDKTable.LoadScene();
    public static void SetEngineState(EngineStates state) => RSDKTable.SetEngineState((byte)state);
#if RETRO_REV02
    public static void ForceHardReset(bool32 shouldHardReset) => RSDKTable.ForceHardReset(shouldHardReset);
#endif

    public static ScanlineInfo* GetScanlines() => RSDKTable.GetScanlines();
}