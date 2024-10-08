using static RSDK.EngineAPI;

namespace RSDK
{
    public enum EngineStates
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
        public uint[] hash = new uint[4];
        public char[] name = new char[0x20];
        public ushort sceneOffsetStart;
        public ushort sceneOffsetEnd;
        public byte sceneCount;

        public SceneListInfo() { }
    }

    public struct SceneListEntry
    {
        public uint[] hash = new uint[4];
        public char[] name = new char[0x20];
        public char[] folder = new char[0x10];
        public char[] id = new char[0x04];
#if RETRO_REV02
        public byte filter;
        public byte[] padding = new byte[3];
#endif
        public SceneListEntry() { }
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

    public unsafe class Stage
    {
        public uint CheckSceneFolder(string folderName) { return RSDKTable.CheckSceneFolder(folderName); }
        public unsafe RSDK.ScanlineInfo* GetScanlines() { return RSDKTable.GetScanlines(); }
    }
}
