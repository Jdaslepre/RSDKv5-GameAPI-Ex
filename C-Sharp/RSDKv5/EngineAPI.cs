using System.Runtime.InteropServices;
using System.Text;

namespace RSDK
{
    public enum Scopes
    {
        SCOPE_NONE,
        SCOPE_GLOBAL,
        SCOPE_STAGE,
    }

    public enum GameRegions
    {
        REGION_US,
        REGION_JP,
        REGION_EU,
    }

    public enum GameLanguages
    {
        LANGUAGE_EN,
        LANGUAGE_FR,
        LANGUAGE_IT,
        LANGUAGE_GE,
        LANGUAGE_SP,
        LANGUAGE_JP,
        LANGUAGE_KO,
        LANGUAGE_SC,
        LANGUAGE_TC,
    }

#if RETRO_USE_MOD_LOADER
    // Mod Table
    public unsafe struct ModFunctionTable
    {
        // Registration & Core
        IntPtr RegisterGlobals;
        IntPtr RegisterObject;
        public void* RegisterObject_STD;
        public delegate* unmanaged<void**, string, void> RegisterObjectHook;
        public delegate* unmanaged<string, void*> FindObject;
        public delegate* unmanaged<void*> GetGlobals;
        public delegate* unmanaged<int, int, void*, void> Super;

        // Mod Info
        public delegate* unmanaged<string, RSDK.String*, RSDK.String*, RSDK.String*, uint*, uint> LoadModInfo;
        public delegate* unmanaged<string, RSDK.String*, void> GetModPath;
        public delegate* unmanaged<uint, int> GetModCount;
        public delegate* unmanaged<uint, string> GetModIDByIndex;
        public delegate* unmanaged<RSDK.String*, uint> ForeachModID;

        // Mod Callbacks & Public Functions
        public delegate* unmanaged<int, delegate* unmanaged<void*, void>, void> AddModCallback;
        public void* AddModCallback_STD;
        public delegate* unmanaged<string, void*, void> AddPublicFunction;
        public delegate* unmanaged<string, string, void*> GetPublicFunction;

        // Mod Settings
        public delegate* unmanaged<string, string, uint, uint> GetSettingsBool;
        public delegate* unmanaged<string, string, int, int> GetSettingsInteger;
        public delegate* unmanaged<string, string, float, float> GetSettingsFloat;
        public delegate* unmanaged<string, string, RSDK.String*, string, void> GetSettingsString;
        public delegate* unmanaged<string, uint, void> SetSettingsBool;
        public delegate* unmanaged<string, int, void> SetSettingsInteger;
        public delegate* unmanaged<string, float, void> SetSettingsFloat;
        public delegate* unmanaged<string, RSDK.String*, void> SetSettingsString;
        public delegate* unmanaged<void> SaveSettings;

        // Config
        public delegate* unmanaged<string, uint, uint> GetConfigBool;
        public delegate* unmanaged<string, int, int> GetConfigInteger;
        public delegate* unmanaged<string, float, float> GetConfigFloat;
        public delegate* unmanaged<string, RSDK.String*, string, void> GetConfigString;
        public delegate* unmanaged<RSDK.String*, uint> ForeachConfig;
        public delegate* unmanaged<RSDK.String*, uint> ForeachConfigCategory;

        // Achievements
        public delegate* unmanaged<string, string, string, void> RegisterAchievement;
        public delegate* unmanaged<uint, RSDK.String*, RSDK.String*, RSDK.String*, uint*, void> GetAchievementInfo;
        public delegate* unmanaged<string, int> GetAchievementIndexByID;
        public delegate* unmanaged<int> GetAchievementCount;

        // Shaders
        public delegate* unmanaged<string, uint, void> LoadShader;

        // StateMachine
        public delegate* unmanaged<delegate* unmanaged<void>, void> StateMachineRun;
        public delegate* unmanaged<delegate* unmanaged<void>, delegate* unmanaged<uint, uint>, uint, void> RegisterStateHook;
        // runs all high priority state hooks hooked to the address of 'state', returns if the main state should be skipped or not
        public delegate* unmanaged<void*, uint> HandleRunState_HighPriority;
        // runs all low priority state hooks hooked to the address of 'state'
        public delegate* unmanaged<void*, uint, void> HandleRunState_LowPriority;

#if RETRO_MOD_LOADER_VER_2
        // Mod Settings (Part 2)
        public delegate* unmanaged<string, RSDK.String*, uint> ForeachSetting;
        public delegate* unmanaged<string, RSDK.String*, uint> ForeachSettingCategory;

        // Files
        public delegate* unmanaged<string, string, uint> ExcludeFile;
        public delegate* unmanaged<string, uint> ExcludeAllFiles;
        public delegate* unmanaged<string, string, uint> ReloadFile;
        public delegate* unmanaged<string, uint> ReloadAllFiles;

        // Graphics
        public delegate* unmanaged<ushort, void*> GetSpriteAnimation;
        public delegate* unmanaged<ushort, void*> GetSpriteSurface;
        public delegate* unmanaged<byte, ushort*> GetPaletteBank;
        public delegate* unmanaged<byte*> GetActivePaletteBuffer;
        public delegate* unmanaged<ushort**, ushort**, ushort**, void> GetRGB32To16Buffer;
        public delegate* unmanaged<ushort*> GetBlendLookupTable;
        public delegate* unmanaged<ushort*> GetSubtractLookupTable;
        public delegate* unmanaged<ushort*> GetTintLookupTable;
        public delegate* unmanaged<uint> GetMaskColor;
        public delegate* unmanaged<void*> GetScanEdgeBuffer;
        public delegate* unmanaged<byte, void*> GetCamera;
        public delegate* unmanaged<byte, void*> GetShader;
        public delegate* unmanaged<ushort, void*> GetModel;
        public delegate* unmanaged<ushort, void*> GetScene3D;
        public delegate* unmanaged<RSDK.Animator, ushort, void> DrawDynamicAniTiles;

        // Audio
        public delegate* unmanaged<ushort, void*> GetSfx;
        public delegate* unmanaged<byte, void*> GetChannel;

        // Objects/Entities
        public delegate* unmanaged<ushort, void**, uint> GetGroupEntities;

        // Collision
        public delegate* unmanaged<RSDK.CollisionSensor*, void> SetPathGripSensors; // expects 5 sensors
        public delegate* unmanaged<RSDK.CollisionSensor*, void> FindFloorPosition;
        public delegate* unmanaged<RSDK.CollisionSensor*, void> FindLWallPosition;
        public delegate* unmanaged<RSDK.CollisionSensor*, void> FindRoofPosition;
        public delegate* unmanaged<RSDK.CollisionSensor*, void> FindRWallPosition;
        public delegate* unmanaged<RSDK.CollisionSensor*, void> FloorCollision;
        public delegate* unmanaged<RSDK.CollisionSensor*, void> LWallCollision;
        public delegate* unmanaged<RSDK.CollisionSensor*, void> RoofCollision;
        public delegate* unmanaged<RSDK.CollisionSensor*, void> RWallCollision;
        public delegate* unmanaged<ushort, ushort, byte, byte, void> CopyCollisionMask;
        public delegate* unmanaged<RSDK.CollisionMask**, RSDK.TileInfo**, void> GetCollisionInfo;
#endif
    }
#endif

#if RETRO_REV02
    public unsafe struct APIFunctionTable
    {
        // API Core
        public delegate* unmanaged<int> GetUserLanguage;
        public delegate* unmanaged<bool32> GetConfirmButtonFlip;
        public delegate* unmanaged<void> ExitGame;
        public delegate* unmanaged<void> LaunchManual;
#if RETRO_REV0U
        public delegate* unmanaged<int> GetDefaultGamepadType;
#endif
        public delegate* unmanaged<uint, bool32> IsOverlayEnabled;
        public delegate* unmanaged<int, bool32> CheckDLC;
#if RETRO_USE_EGS
        public delegate* unmanaged<bool32> SetupExtensionOverlay;
        public delegate* unmanaged<int, bool32> CanShowExtensionOverlay;
#endif
        public delegate* unmanaged<int, bool32> ShowExtensionOverlay;
#if RETRO_USE_EGS
        public delegate* unmanaged<int, bool32> CanShowAltExtensionOverlay;
        public delegate* unmanaged<int, bool32> ShowAltExtensionOverlay;
        public delegate* unmanaged<int> GetConnectingStringID;
        public delegate* unmanaged<int, void> ShowLimitedVideoOptions;
#endif

        // Achievements
        public delegate* unmanaged<RSDK.AchievementID*, void> TryUnlockAchievement;
        public delegate* unmanaged<bool32> GetAchievementsEnabled;
        public delegate* unmanaged<bool32, void> SetAchievementsEnabled;
#if RETRO_USE_EGS
        public delegate* unmanaged<bool32> CheckAchievementsEnabled;
        public delegate* unmanaged<RSDK.String**, int, void> SetAchievementNames;
#endif

        // Leaderboards
#if RETRO_USE_EGS
        public delegate* unmanaged<bool32> CheckLeaderboardsEnabled;
#endif
        public delegate* unmanaged<void> InitLeaderboards;
        public delegate* unmanaged<RSDK.LeaderboardID*, bool32, void> FetchLeaderboard;
        public delegate* unmanaged<RSDK.LeaderboardID*, int, delegate* unmanaged<bool32, int, void>, void> TrackScore;
        public delegate* unmanaged<int> GetLeaderboardsStatus;
        public delegate* unmanaged<RSDK.LeaderboardAvail> LeaderboardEntryViewSize;
        public delegate* unmanaged<RSDK.LeaderboardAvail> LeaderboardEntryLoadSize;
        public delegate* unmanaged<int, uint, int, void> LoadLeaderboardEntries;
        public delegate* unmanaged<void> ResetLeaderboardInfo;
        public delegate* unmanaged<uint, RSDK.LeaderboardEntry*> ReadLeaderboardEntry;

        // Rich Presence
        public delegate* unmanaged<int, RSDK.String*, void> SetRichPresence;

        // Stats
        public delegate* unmanaged<RSDK.StatInfo*, void> TryTrackStat;
        public delegate* unmanaged<bool32> GetStatsEnabled;
        public delegate* unmanaged<bool32, void> SetStatsEnabled;

        // Authorization
        public delegate* unmanaged<void> ClearPrerollErrors;
        public delegate* unmanaged<void> TryAuth;
        public delegate* unmanaged<int> GetUserAuthStatus;
        public delegate* unmanaged<RSDK.String*, bool32> GetUsername;

        // Storage
        public delegate* unmanaged<void> TryInitStorage;
        public delegate* unmanaged<int> GetStorageStatus;
        public delegate* unmanaged<int> GetSaveStatus;
        public delegate* unmanaged<void> ClearSaveStatus;
        public delegate* unmanaged<void> SetSaveStatusContinue;
        public delegate* unmanaged<void> SetSaveStatusOK;
        public delegate* unmanaged<void> SetSaveStatusForbidden;
        public delegate* unmanaged<void> SetSaveStatusError;
        public delegate* unmanaged<bool32, void> SetNoSave;
        public delegate* unmanaged<bool32> GetNoSave;

        // User File Management
        public delegate* unmanaged<string, void*, uint, delegate* unmanaged<int, void>, void> LoadUserFile;         // load user file from game dir
        public delegate* unmanaged<string, void*, uint, delegate* unmanaged<int, void>, bool, void> SaveUserFile;   // save user file to game dir
        public delegate* unmanaged<string, delegate* unmanaged<int, void>, void> DeleteUserFile;                    // delete user file from game dir

        // User DBs
        public delegate* unmanaged<string, string, string, string, string, string, ushort> InitUserDB; // first string is "name", the other ones are used for va_list. ye only get 5
        public delegate* unmanaged<string, delegate* unmanaged<int, void>, ushort> LoadUserDB;
        public delegate* unmanaged<ushort, delegate* unmanaged<int, void>, bool32> SaveUserDB;
        public delegate* unmanaged<ushort, void> ClearUserDB;
        public delegate* unmanaged<void> ClearAllUserDBs;
        public delegate* unmanaged<ushort, ushort> SetupUserDBRowSorting;
        public delegate* unmanaged<ushort, bool32> GetUserDBRowsChanged;
        public delegate* unmanaged<ushort, int, string, void*, int> AddRowSortFilter;
        public delegate* unmanaged<ushort, int, string, bool32, int> SortDBRows;
        public delegate* unmanaged<ushort, int> GetSortedUserDBRowCount;
        public delegate* unmanaged<ushort, ushort, int> GetSortedUserDBRowID;
        public delegate* unmanaged<ushort, int> AddUserDBRow;
        public delegate* unmanaged<ushort, uint, int, string, void*, bool32> SetUserDBValue;
        public delegate* unmanaged<ushort, uint, int, string, void*, bool32> GetUserDBValue;
        public delegate* unmanaged<ushort, ushort, uint> GetUserDBRowUUID;
        public delegate* unmanaged<ushort, uint, ushort> GetUserDBRowByID;
        public delegate* unmanaged<ushort, ushort, StringBuilder, UIntPtr, string, void> GetUserDBRowCreationTime; // is this correct?
        public delegate* unmanaged<ushort, ushort, bool32> RemoveDBRow;
        public delegate* unmanaged<ushort, bool32> RemoveAllDBRows;
    }
#endif

    public unsafe struct RSDKFunctionTable
    {
        // Registration
#if RETRO_REV0U
        public delegate* unmanaged<IntPtr, int, IntPtr, void> RegisterGlobalVariables;
        public delegate* unmanaged<void**, string, uint, int, IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, void> RegisterObject;
#else
        // TODO:
        public delegate* unmanaged<IntPtr, int, void> RegisterGlobalVariables;
        public IntPtr RegisterObject;
#endif
#if RETRO_REV02
        public delegate* unmanaged<void**, string, uint, void> RegisterStaticVariables;
#endif
        // Entities & Objects
        public delegate* unmanaged<ushort, void**, bool32> GetActiveEntities;
        public delegate* unmanaged<ushort, void**, bool32> GetAllEntities;
        public delegate* unmanaged<void> BreakForeachLoop;
        public delegate* unmanaged<byte, string, byte, int, void> SetEditableVar;
        public delegate* unmanaged<ushort, void*> GetEntity;
        public delegate* unmanaged<void*, int> GetEntitySlot;
        public delegate* unmanaged<ushort, bool32, int> GetEntityCount;
        public delegate* unmanaged<byte, ushort, int> GetDrawListRefSlot;
        public delegate* unmanaged<byte, ushort, void*> GetDrawListRef;
        public delegate* unmanaged<void*, ushort, void*, void> ResetEntity;
        public delegate* unmanaged<ushort, ushort, void*, void> ResetEntitySlot;
        public delegate* unmanaged<ushort, void*, int, int, void*> CreateEntity;
        public delegate* unmanaged<void*, void*, bool32, void> CopyEntity;
        public delegate* unmanaged<void*, RSDK.Vector2*, bool32> CheckOnScreen;
        public delegate* unmanaged<RSDK.Vector2*, RSDK.Vector2*, bool32> CheckPosOnScreen;
        public delegate* unmanaged<byte, ushort, void> AddDrawListRef;
        public delegate* unmanaged<byte, ushort, ushort, ushort, void> SwapDrawListEntries;
        public delegate* unmanaged<byte, bool32, delegate* unmanaged<void>, void> SetDrawGroupProperties;

        // Scene Management
        public delegate* unmanaged<string, string, void> SetScene;
        public delegate* unmanaged<byte, void> SetEngineState;
#if RETRO_REV02
        public delegate* unmanaged<bool32, void> ForceHardReset;
#endif
        public delegate* unmanaged<bool32> CheckValidScene;
        public delegate* unmanaged<string, bool32> CheckSceneFolder;
        public delegate* unmanaged<void> LoadScene;
        public delegate* unmanaged<string, int> FindObject;

        // Cameras
        public delegate* unmanaged<void> ClearCameras;
        public delegate* unmanaged<RSDK.Vector2*, int, int, bool32, void> AddCamera;

        // API (Rev01 only)
#if !RETRO_REV02
        public delegate* unmanaged<string, void*> GetAPIFunction;
#endif

        // Window/Video Settings
        public delegate* unmanaged<int, int> GetVideoSetting;
        public delegate* unmanaged<int, int, int> SetVideoSetting;
        public delegate* unmanaged<void> UpdateWindow;

        // Math
        public delegate* unmanaged<int, int> Sin1024;
        public delegate* unmanaged<int, int> Cos1024;
        public delegate* unmanaged<int, int> Tan1024;
        public delegate* unmanaged<int, int> ASin1024;
        public delegate* unmanaged<int, int> ACos1024;
        public delegate* unmanaged<int, int> Sin512;
        public delegate* unmanaged<int, int> Cos512;
        public delegate* unmanaged<int, int> Tan512;
        public delegate* unmanaged<int, int> ASin512;
        public delegate* unmanaged<int, int> ACos512;
        public delegate* unmanaged<int, int> Sin256;
        public delegate* unmanaged<int, int> Cos256;
        public delegate* unmanaged<int, int> Tan256;
        public delegate* unmanaged<int, int> ASin256;
        public delegate* unmanaged<int, int> ACos256;
        public delegate* unmanaged<int, int, int> Rand;
        public delegate* unmanaged<int, int, int*, int> RandSeeded;
        public delegate* unmanaged<int, void> SetRandSeed;
        public delegate* unmanaged<int, int, byte> ATan2;

        // Matrices
        public delegate* unmanaged<RSDK.Matrix*, void> SetIdentityMatrix;
        public delegate* unmanaged<RSDK.Matrix*, RSDK.Matrix*, RSDK.Matrix*, void> MatrixMultiply;
        public delegate* unmanaged<RSDK.Matrix*, int, int, int, bool32, void> MatrixTranslateXYZ;
        public delegate* unmanaged<RSDK.Matrix*, int, int, int, void> MatrixScaleXYZ;
        public delegate* unmanaged<RSDK.Matrix*, short, void> MatrixRotateX;
        public delegate* unmanaged<RSDK.Matrix*, short, void> MatrixRotateY;
        public delegate* unmanaged<RSDK.Matrix*, short, void> MatrixRotateZ;
        public delegate* unmanaged<RSDK.Matrix*, short, short, short, void> MatrixRotateXYZ;
        public delegate* unmanaged<RSDK.Matrix*, RSDK.Matrix*, void> MatrixInverse;
        public delegate* unmanaged<RSDK.Matrix*, RSDK.Matrix*, void> MatrixCopy;

        // Strings
        public delegate* unmanaged<RSDK.String*, string, uint, void> InitString;
        public delegate* unmanaged<RSDK.String*, RSDK.String*, void> CopyString;
        public delegate* unmanaged<RSDK.String*, string, void> SetString;
        public delegate* unmanaged<RSDK.String*, RSDK.String*, void> AppendString;
        public delegate* unmanaged<RSDK.String*, string, void> AppendText;
        public delegate* unmanaged<RSDK.String*, string, uint, void> LoadStringList;
        public delegate* unmanaged<RSDK.String*, RSDK.String*, int, int, bool32> SplitStringList;
        public delegate* unmanaged<StringBuilder, RSDK.String*, void> GetCString;
        public delegate* unmanaged<RSDK.String*, RSDK.String*, bool32, bool32> CompareStrings;

        // Screens & Displays
        public delegate* unmanaged<int*, int*, int*, int*, StringBuilder, void> GetDisplayInfo;
        public delegate* unmanaged<int*, int*, void> GetWindowSize;
        public delegate* unmanaged<byte, ushort, ushort, int> SetScreenSize;
        public delegate* unmanaged<byte, int, int, int, int, void> SetClipBounds;
#if RETRO_REV02
        public delegate* unmanaged<byte, byte, byte, byte, byte, void> SetScreenVertices;
#endif

        // Spritesheets
        public delegate* unmanaged<string, byte, ushort> LoadSpriteSheet;

        // Palettes & Colors
#if RETRO_REV02
        public delegate* unmanaged<ushort*, void> SetTintLookupTable;
#else
        public delegate* unmanaged<ushort*> GetTintLookupTable;
#endif
        public delegate* unmanaged<uint, void> SetPaletteMask;
        public delegate* unmanaged<byte, byte, uint, void> SetPaletteEntry;
        public delegate* unmanaged<byte, byte, uint> GetPaletteEntry;
        public delegate* unmanaged<byte, int, int, void> SetActivePalette;
        public delegate* unmanaged<byte, byte, byte, byte, byte, void> CopyPalette;
#if RETRO_REV02
        public delegate* unmanaged<byte, string, ushort, void> LoadPalette;
#endif
        public delegate* unmanaged<byte, byte, byte, bool32, void> RotatePalette;
        public delegate* unmanaged<byte, byte, byte, short, int, int, void> SetLimitedFade;
#if RETRO_REV02
        public delegate* unmanaged<byte, uint*, uint*, int, int, int, void> BlendColors;
#endif

        // Drawing
        public delegate* unmanaged<int, int, int, int, uint, int, int, bool32, void> DrawRect;
        public delegate* unmanaged<int, int, int, int, uint, int, int, bool32, void> DrawLine;
        public delegate* unmanaged<int, int, int, uint, int, int, bool32, void> DrawCircle;
        public delegate* unmanaged<int, int, int, int, uint, int, int, bool32, void> DrawCircleOutline;
        public delegate* unmanaged<RSDK.Vector2*, int, int, int, int, int, int, void> DrawFace;
        public delegate* unmanaged<RSDK.Vector2*, uint*, int, int, int, void> DrawBlendedFace;
        public delegate* unmanaged<RSDK.Animator*, RSDK.Vector2*, bool32, void> DrawSprite;
        public delegate* unmanaged<ushort, int, bool32, void> DrawDeformedSprite;
        public delegate* unmanaged<RSDK.Animator*, RSDK.Vector2*, RSDK.String*, int, int, int, int, void*, RSDK.Vector2*, bool32, void> DrawText;
        public delegate* unmanaged<ushort*, int, int, RSDK.Vector2*, RSDK.Vector2*, bool32, void> DrawTile;
        public delegate* unmanaged<ushort, ushort, ushort, void> CopyTile;
        public delegate* unmanaged<ushort, ushort, ushort, ushort, ushort, ushort, void> DrawAniTiles;
#if RETRO_REV0U
        public delegate* unmanaged<RSDK.Animator*, ushort, void> DrawDynamicAniTiles;
#endif
        public delegate* unmanaged<uint, int, int, int, void> FillScreen;

        // Meshes & 3D Scenes
        public delegate* unmanaged<string, byte, ushort> LoadMesh;
        public delegate* unmanaged<string, ushort, byte, ushort> Create3DScene;
        public delegate* unmanaged<ushort, void> Prepare3DScene;
        public delegate* unmanaged<ushort, byte, byte, byte, void> SetDiffuseColor;
        public delegate* unmanaged<ushort, byte, byte, byte, void> SetDiffuseIntensity;
        public delegate* unmanaged<ushort, byte, byte, byte, void> SetSpecularIntensity;
        public delegate* unmanaged<ushort, ushort, byte, RSDK.Matrix*, RSDK.Matrix*, uint, void> AddModelTo3DScene;
        public delegate* unmanaged<ushort, RSDK.Animator*, short, byte, bool32, short, void> SetModelAnimation;
        public delegate* unmanaged<ushort, ushort, RSDK.Animator*, byte, RSDK.Matrix*, RSDK.Matrix*, uint, void> AddMeshFrameTo3DScene;
        public delegate* unmanaged<ushort, void> Draw3DScene;

        // Sprite Animations & Frames
        public delegate* unmanaged<string, byte, ushort> LoadSpriteAnimation;
        public delegate* unmanaged<string, uint, uint, byte, ushort> CreateSpriteAnimation;
#if RETRO_MOD_LOADER_VER_2
        public delegate* unmanaged<ushort, ushort, RSDK.Animator*, bool32, int, void> SetSpriteAnimation;
#else
        public delegate* unmanaged<ushort, ushort, RSDK.Animator*, bool32, short, void> SetSpriteAnimation; 
#endif
        public delegate* unmanaged<ushort, ushort, string, int, ushort, short, byte, byte, void> EditSpriteAnimation;
        public delegate* unmanaged<ushort, ushort, RSDK.String*, void> SetSpriteString;
        public delegate* unmanaged<ushort, string, ushort> FindSpriteAnimation;
        public delegate* unmanaged<ushort, ushort, int, RSDK.SpriteFrame*> GetFrame;
        public delegate* unmanaged<RSDK.Animator*, byte, RSDK.Hitbox*> GetHitbox;
        public delegate* unmanaged<RSDK.Animator*, short> GetFrameID;
        public delegate* unmanaged<ushort, ushort, RSDK.String*, int, int, int, int> GetStringWidth;
        public delegate* unmanaged<RSDK.Animator*, void> ProcessAnimation;

        // Tile Layers
        public delegate* unmanaged<string, ushort> GetTileLayerID;
        public delegate* unmanaged<ushort, RSDK.TileLayer*> GetTileLayer;
        public delegate* unmanaged<ushort, RSDK.Vector2*, bool32, void> GetLayerSize;
        public delegate* unmanaged<ushort, int, int, ushort> GetTile;
        public delegate* unmanaged<ushort, int, int, ushort, void> SetTile;
        public delegate* unmanaged<ushort, int, int, ushort, int, int, int, int, void> CopyTileLayer;
        public delegate* unmanaged<RSDK.TileLayer*, void> ProcessParallax;
        public delegate* unmanaged<RSDK.ScanlineInfo*> GetScanlines;

        // Object & Tile Collisions
        public delegate* unmanaged<void*, RSDK.Hitbox*, void*, RSDK.Hitbox*, bool32> CheckObjectCollisionTouchBox;
        public delegate* unmanaged<void*, int, void*, int, bool32> CheckObjectCollisionTouchCircle;
        public delegate* unmanaged<void*, RSDK.Hitbox*, void*, RSDK.Hitbox*, bool32, byte> CheckObjectCollisionBox;
        public delegate* unmanaged<void*, RSDK.Hitbox*, void*, RSDK.Hitbox*, bool32, bool32> CheckObjectCollisionPlatform;
        public delegate* unmanaged<void*, ushort, byte, byte, int, int, bool32, bool32> ObjectTileCollision;
        public delegate* unmanaged<void*, ushort, byte, byte, int, int, int, bool32> ObjectTileGrip;
        public delegate* unmanaged<void*, RSDK.Hitbox*, RSDK.Hitbox*, void> ProcessObjectMovement;
#if RETRO_REV0U
        public delegate* unmanaged<int, byte, byte, byte, byte, byte, void> SetupCollisionConfig;
        public delegate* unmanaged<RSDK.CollisionSensor*, void> SetPathGripSensors; // expects 5 sensors
        public delegate* unmanaged<RSDK.CollisionSensor*, void> FindFloorPosition;
        public delegate* unmanaged<RSDK.CollisionSensor*, void> FindLWallPosition;
        public delegate* unmanaged<RSDK.CollisionSensor*, void> FindRoofPosition;
        public delegate* unmanaged<RSDK.CollisionSensor*, void> FindRWallPosition;
        public delegate* unmanaged<RSDK.CollisionSensor*, void> FloorCollision;
        public delegate* unmanaged<RSDK.CollisionSensor*, void> LWallCollision;
        public delegate* unmanaged<RSDK.CollisionSensor*, void> RoofCollision;
        public delegate* unmanaged<RSDK.CollisionSensor*, void> RWallCollision;
#endif
        public delegate* unmanaged<ushort, byte, byte, int> GetTileAngle;
        public delegate* unmanaged<ushort, byte, byte, byte, void> SetTileAngle;
        public delegate* unmanaged<ushort, byte, byte> GetTileFlags;
        public delegate* unmanaged<ushort, byte, byte, void> SetTileFlags;
#if RETRO_REV0U
        public delegate* unmanaged<ushort, ushort, byte, byte, void> CopyCollisionMask;
        public delegate* unmanaged<RSDK.CollisionMask**, RSDK.TileInfo**, void> GetCollisionInfo;
#endif

        // Audio
        public delegate* unmanaged<string, ushort> GetSfx;
        public delegate* unmanaged<ushort, int, int, int> PlaySfx;
        public delegate* unmanaged<ushort, void> StopSfx;
        public delegate* unmanaged<string, uint, uint, uint, uint, int> PlayStream;
        public delegate* unmanaged<byte, float, float, float, void> SetChannelAttributes;
        public delegate* unmanaged<uint, void> StopChannel;
        public delegate* unmanaged<uint, void> PauseChannel;
        public delegate* unmanaged<uint, void> ResumeChannel;
        public delegate* unmanaged<ushort, bool32> IsSfxPlaying;
        public delegate* unmanaged<uint, bool32> ChannelActive;
        public delegate* unmanaged<uint, uint> GetChannelPos;

        // Videos & "HD Images"
        public delegate* unmanaged<string, double, delegate* unmanaged<bool32>, bool32> LoadVideo;
        public delegate* unmanaged<string, double, double, delegate* unmanaged<bool32>, bool32> LoadImage;

        // Input
#if RETRO_REV02
        public delegate* unmanaged<byte, uint> GetInputDeviceID;
        public delegate* unmanaged<bool32, bool32, uint, uint> GetFilteredInputDeviceID;
        public delegate* unmanaged<uint, int> GetInputDeviceType;
        public delegate* unmanaged<uint, bool32> IsInputDeviceAssigned;
        public delegate* unmanaged<uint, int> GetInputDeviceUnknown;
        public delegate* unmanaged<uint, int, int, int> InputDeviceUnknown1;
        public delegate* unmanaged<uint, int, int, int> InputDeviceUnknown2;
        public delegate* unmanaged<byte, int> GetInputSlotUnknown;
        public delegate* unmanaged<byte, int, int, int> InputSlotUnknown1;
        public delegate* unmanaged<byte, int, int, int> InputSlotUnknown2;
        public delegate* unmanaged<byte, uint, void> AssignInputSlotToDevice;
        public delegate* unmanaged<byte, bool32> IsInputSlotAssigned;
        public delegate* unmanaged<void> ResetInputSlotAssignments;
#endif
#if !RETRO_REV02
        public delegate* unmanaged<int, int, int*, void> GetUnknownInputValue;
#endif

        // User File Management
        public delegate* unmanaged<string, void*, uint, bool32> LoadUserFile; // load user file from exe dir
        public delegate* unmanaged<string, void*, uint, bool32> SaveUserFile; // save user file to exe dir

        // Printing (Rev02)
#if RETRO_REV02
        public delegate* unmanaged<RSDK.Dev.PrintModes, string, void> PrintLog;
        public delegate* unmanaged<RSDK.Dev.PrintModes, string, void> PrintText;
        public delegate* unmanaged<RSDK.Dev.PrintModes, RSDK.String*, void> PrintString;
        public delegate* unmanaged<RSDK.Dev.PrintModes, string, uint, void> PrintUInt32;
        public delegate* unmanaged<RSDK.Dev.PrintModes, string, int, void> PrintInt32;
        public delegate* unmanaged<RSDK.Dev.PrintModes, string, float, void> PrintFloat;
        public delegate* unmanaged<RSDK.Dev.PrintModes, string, RSDK.Vector2, void> PrintVector2;
        public delegate* unmanaged<RSDK.Dev.PrintModes, string, RSDK.Hitbox, void> PrintHitbox;
#endif

        // Editor
        public delegate* unmanaged<int, string, void> SetActiveVariable;
        public delegate* unmanaged<string, void> AddVarEnumValue;

        // Printing (Rev01)
#if !RETRO_REV02
        public delegate* unmanaged<void*, byte, void> PrintMessage;
#endif

        // Debugging
#if RETRO_REV02
        public delegate* unmanaged<void> ClearViewableVariables;
        public delegate* unmanaged<string, void*, RSDK.Dev.ViewableVarTypes, int, int, void> AddViewableVariable;
#endif

#if RETRO_REV0U
        // Origins Extras
        public delegate* unmanaged<int, int, int, int, void> NotifyCallback;
        public delegate* unmanaged<void> SetGameFinished;
        public delegate* unmanaged<void> StopAllSfx;
#endif
    }

    public enum GamePlatforms
    {
        PLATFORM_PC,
        PLATFORM_PS4,
        PLATFORM_XB1,
        PLATFORM_SWITCH,

        PLATFORM_DEV = 0xFF,
    }

    public struct SKUInfo
    {
        public int platform;
        public int language;
        public int region;
    }

    public enum StatusCodes
    {
        STATUS_NONE = 0,
        STATUS_CONTINUE = 100,
        STATUS_OK = 200,
        STATUS_FORBIDDEN = 403,
        STATUS_NOTFOUND = 404,
        STATUS_ERROR = 500,
        STATUS_NOWIFI = 503,
        STATUS_TIMEOUT = 504,
        STATUS_CORRUPT = 505,
        STATUS_NOSPACE = 506,
    }

    // None of these besides the named 2 are even used
    // and even then they're not even set in plus
    public struct UnknownInfo
    {
        public int unknown1;
        public int unknown2;
        public int unknown3;
        public int unknown4;
        public bool32 pausePress;
        public int unknown5;
        public int unknown6;
        public int unknown7;
        public int unknown8;
        public int unknown9;
        public bool32 anyKeyPress;
        public int unknown10;
    }

    public struct GameInfo
    {
        public char[] gameTitle = new char[0x40];
        public char[] gameSubtitle = new char[0x100];
        public char[] version = new char[0x10];

        public GameInfo() { }
    }

    public struct InputState
    {
        public bool32 down;
        public bool32 press;
        public int keyMap;
    }

    public struct ControllerState
    {
        public InputState keyUp;
        public InputState keyDown;
        public InputState keyLeft;
        public InputState keyRight;
        public InputState keyA;
        public InputState keyB;
        public InputState keyC;
        public InputState keyX;
        public InputState keyY;
        public InputState keyZ;
        public InputState keyStart;
        public InputState keySelect;

        // Rev01 hasn't split these into different structs yet
#if RETRO_REV01
        public InputState keyBumperL;
        public InputState keyBumperR;
        public InputState keyTriggerL;
        public InputState keyTriggerR;
        public InputState keyStickL;
        public InputState keyStickR;
#endif
    }

    public struct AnalogState
    {
        public InputState keyUp;
        public InputState keyDown;
        public InputState keyLeft;
        public InputState keyRight;
#if RETRO_REV02
        public InputState keyStick;
        public float deadzone;
        public float hDelta;
        public float vDelta;
#else
        public float deadzone;
        public float triggerDeltaL;
        public float triggerDeltaR;
        public float hDeltaL;
        public float vDeltaL;
        public float hDeltaR;
        public float vDeltaR;
#endif
    }

#if RETRO_REV02
    public struct TriggerState
    {
        public InputState keyBumper;
        public InputState keyTrigger;
        public float bumperDelta;
        public float triggerDelta;
    }
#endif

    public struct TouchInfo
    {
        public float[] x = new float[0x10];
        public float[] y = new float[0x10];
        public bool32[] down = new bool32[0x10];
        public byte count;
#if !RETRO_REV02
        public bool32 pauseHold;
        public bool32 pausePress;
        public bool32 waitRetryState;
        public bool32 anyKeyHold;
        public bool32 anyKeyPress;
        public bool32 unknown2;
#endif

        public TouchInfo() { }
    }

    public struct ScreenInfo
    {
        public ushort[] frameBuffer = new ushort[Const.SCREEN_XMAX * Const.SCREEN_YSIZE];
        public RSDK.Vector2 position;
        public RSDK.Vector2 size;
        public RSDK.Vector2 center;
        public int pitch;
        public int clipBound_X1;
        public int clipBound_Y1;
        public int clipBound_X2;
        public int clipBound_Y2;
        public int waterDrawPos;

        public ScreenInfo() { }
    }

    public unsafe struct EngineInfo
    {
        public IntPtr functionTable;
#if RETRO_REV02
        public IntPtr APITable;
#endif
        public IntPtr gameInfo;
#if RETRO_REV02
        public RSDK.SKUInfo* currentSKU;
#endif
        public RSDK.SceneInfo* sceneInfo;
        public IntPtr controllerInfo;
        public IntPtr stickInfoL;
#if RETRO_REV02
        public IntPtr stickInfoR;
        public IntPtr triggerInfoL;
        public IntPtr triggerInfoR;
#endif
        public IntPtr touchInfo;
#if RETRO_REV02
        public IntPtr unknownInfo;
#endif
        public IntPtr screenInfo;
#if RETRO_REV02 && RETRO_REV0U
        public IntPtr hedgehogLink;
#endif
#if RETRO_USE_MOD_LOADER
        public IntPtr modFunctionTable;
#endif
    }

    public unsafe class EngineAPI
    {
        public static RSDKFunctionTable RSDKTable = new();
        public static APIFunctionTable APITable = new();
#if RETRO_USE_MOD_LOADER
        public static ModFunctionTable modTable = new();
#endif
        public static SceneInfo* sceneInfo = null;

        public static GameInfo gameInfo = new();
        public static SKUInfo *SKU = null;

        public static ControllerState controllerInfo = new();
        public static AnalogState analogStickInfoL = new();
        public static AnalogState analogStickInfoR = new();
        public static TriggerState triggerInfoL = new();
        public static TriggerState triggerInfoR = new();
        public static TouchInfo touchInfo = new();

        public static UnknownInfo unknownInfo = new();

        public static ScreenInfo screenInfo = new();

        public static void InitEngineInfo(EngineInfo* info)
        {
            RSDKTable = Marshal.PtrToStructure<RSDKFunctionTable>(info->functionTable);
#if RETRO_REV02
            APITable = Marshal.PtrToStructure<APIFunctionTable>(info->APITable);
#endif
            gameInfo = Marshal.PtrToStructure<GameInfo>(info->gameInfo);
#if RETRO_REV02
            SKU = info->currentSKU;
#endif
            sceneInfo = info->sceneInfo;
            controllerInfo = Marshal.PtrToStructure<ControllerState>(info->controllerInfo);

            analogStickInfoL = Marshal.PtrToStructure<AnalogState>(info->stickInfoL);
#if RETRO_REV02
            analogStickInfoR = Marshal.PtrToStructure<AnalogState>(info->stickInfoR);
            triggerInfoL = Marshal.PtrToStructure<TriggerState>(info->triggerInfoL);
            triggerInfoR = Marshal.PtrToStructure<TriggerState>(info->triggerInfoR);
#endif
            touchInfo = Marshal.PtrToStructure<TouchInfo>(info->touchInfo);

#if RETRO_REV02
            unknownInfo = Marshal.PtrToStructure<UnknownInfo>(info->unknownInfo);
#endif
            screenInfo = Marshal.PtrToStructure<ScreenInfo>(info->screenInfo);

#if RETRO_USE_MOD_LOADER
            modTable = Marshal.PtrToStructure<ModFunctionTable>(info->modFunctionTable);
#endif
        }
    }
}
