using System;

namespace RSDK;

static
{
    public static RSDK.RSDKFunctionTable* RSDKTable;
    public static RSDK.APIFunctionTable* APITable;
#if RETRO_USE_MOD_LOADER
    public static RSDK.ModFunctionTable* modTable;
#endif

    public static RSDK.SceneInfo* sceneInfo;

    public static RSDK.GameInfo* gameInfo;
    public static RSDK.SKUInfo* SKU;

    public static RSDK.ControllerState* controllerInfo;
    public static RSDK.AnalogState* analogStickInfoL;
    public static RSDK.AnalogState* analogStickInfoR;
    public static RSDK.TriggerState* triggerInfoL;
    public static RSDK.TriggerState* triggerInfoR;
    public static RSDK.TouchInfo* touchInfo;

    public static RSDK.UnknownInfo* unknownInfo;

    public static RSDK.ScreenInfo* screenInfo;
}

public enum Scopes : uint8
{
    NONE,
    GLOBAL,
    STAGE,
}

public enum GameRegions
{
    US,
    JP,
    EU,
}

public enum GameLanguages
{
    EN,
    FR,
    IT,
    GE,
    SP,
    JP,
    KO,
    SC,
    TC,
}

[CRepr] public struct ModFunctionTable
{
    // Registration & Core
#if RETRO_REV0U
    public function void(char8 *globalsPath, void **globals, uint32 size, function void(void *globals) initCB) RegisterGlobals;
    public function void(void **staticVars, void **modStaticVars, char8 *name, uint32 entityClassSize, uint32 staticClassSize,
                           uint32 modClassSize, function void() update, function void() lateUpdate, function void() staticUpdate, function void() draw,
                           function void(void*) create, function void() stageLoad, function void() editorLoad, function void() editorDraw,
                           function void() serialize, function void(void* staticVars) staticLoad, char8 *inherited) RegisterObject;
#else
    public function void(char8* globalsPath, void** globals, uint32 size) RegisterGlobals;
    public function void(void** staticVars, void** modStaticVars, char8* name, uint32 entityClassSize, uint32 staticClassSize,
        uint32 modClassSize, function void() update, function void() lateUpdate, function void() staticUpdate, function void() draw,
        function void(void*) create, function void() stageLoad, function void() editorLoad, function void() editorDraw,
        function void() serialize, char8* inherited) RegisterObject;
#endif

    public void* RegisterObject_STD;
    public function void(void** staticVars, char8* staticName) RegisterObjectHook;
    public function void*(char8* name) FindObject;
    public function void*() GetGlobals;
    public function void(int32 classID, int32 callback, void* data) Super;

    // Mod Info
    public function bool32(char8* id, RSDK.String* name, RSDK.String* description, RSDK.String* version, bool32* active) LoadModInfo;
    public function void(char8* id, RSDK.String* result) GetModPath;
    public function int32(bool32 active) GetModCount;
    public function char8*(uint32 index) GetModIDByIndex;
    public function bool32(RSDK.String* id) ForeachModID;

    // Mod Callbacks & Public Functions
    public function void(int32 callbackID, function void(void*) callback) AddModCallback;
    public void* AddModCallback_STD;
    public function void(char8* functionName, void* functionPtr) AddPublicFunction;
    public function void*(char8* id, char8* functionName) GetPublicFunction;

    // Mod Settings
    public function bool32(char8* id, char8* key, bool32 fallback) GetSettingsBool;
    public function int32(char8* id, char8* key, int32 fallback) GetSettingsInteger;
    public function float(char8* id, char8* key, float fallback) GetSettingsFloat;
    public function void(char8* id, char8* key, RSDK.String* result, char8* fallback) GetSettingsString;
    public function void(char8* key, bool32 val) SetSettingsBool;
    public function void(char8* key, int32 val) SetSettingsInteger;
    public function void(char8* key, float val) SetSettingsFloat;
    public function void(char8* key, RSDK.String* val) SetSettingsString;
    public function void() SaveSettings;

    // Config
    public function bool32(char8* key, bool32 fallback) GetConfigBool;
    public function int32(char8* key, int32 fallback) GetConfigInteger;
    public function float(char8* key, float fallback) GetConfigFloat;
    public function void(char8* key, RSDK.String* result, char8* fallback) GetConfigString;
    public function bool32(RSDK.String* config) ForeachConfig;
    public function bool32(RSDK.String* category) ForeachConfigCategory;

    // Achievements
    public function void(char8* identifier, char8* name, char8* desc) RegisterAchievement;
    public function void(uint32 id, RSDK.String* name, RSDK.String* description, RSDK.String* identifier, bool32* achieved) GetAchievementInfo;
    public function int32(char8* identifier) GetAchievementIndexByID;
    public function int32() GetAchievementCount;

    // Shaders
    public function void(char8* shaderName, bool32 linear) LoadShader;

    // StateMachine
    public function void(function void() state) StateMachineRun;
    public function void(function void() state, function bool32(bool32 skippedState) hook, bool32 priority) RegisterStateHook;
    // runs all high priority state hooks hooked to the address of 'state', returns if the main state should be skipped or not
    public function bool32(function void() state) HandleRunState_HighPriority;
    // runs all low priority state hooks hooked to the address of 'state'
    public function void(function void() state, bool32 skipState) HandleRunState_LowPriority;

#if RETRO_MOD_LOADER_VER_2
    // Mod Settings (Part 2)
    public function bool32(char8 *id, RSDK.String *setting) ForeachSetting;
    public function bool32(char8 *id, RSDK.String *category) ForeachSettingCategory;

    // Files
    public function bool32(char8 *id, char8 *path) ExcludeFile;
    public function bool32(char8 *id) ExcludeAllFiles;
    public function bool32(char8 *id, char8 *path) ReloadFile;
    public function bool32(char8 *id) ReloadAllFiles;

    // Graphics
    public function void*(uint16 id) GetSpriteAnimation;
    public function void*(uint16 id) GetSpriteSurface;
    public function uint16*(uint8 id) GetPaletteBank;
    public function uint8*() GetActivePaletteBuffer;
    public function void(uint16 **rgb32To16_R, uint16 **rgb32To16_G, uint16 **rgb32To16_B) GetRGB32To16Buffer;
    public function uint16*() GetBlendLookupTable;
    public function uint16*() GetSubtractLookupTable;
    public function uint16*() GetTintLookupTable;
    public function color() GetMaskColor;
    public function void*() GetScanEdgeBuffer;
    public function void*(uint8 id) GetCamera;
    public function void *(uint8 id) GetShader;
    public function void*(uint16 id) GetModel;
    public function void*(uint16 id) GetScene3D;
    public function void(RSDK.Animator *animator, uint16 tileIndex) DrawDynamicAniTiles;

    // Audio
    public function void*(uint16 id) GetSfx;
    public function void*(uint8 id) GetChannel;

    // Objects/Entities
    public function bool32(uint16 group, void **entity) GetGroupEntities;

    // Collision
    public function void (RSDK.CollisionSensor *sensors) SetPathGripSensors; // expects 5 sensors
    public function void (RSDK.CollisionSensor *sensor) FindFloorPosition;
    public function void (RSDK.CollisionSensor *sensor) FindLWallPosition;
    public function void (RSDK.CollisionSensor *sensor) FindRoofPosition;
    public function void (RSDK.CollisionSensor *sensor) FindRWallPosition;
    public function void (RSDK.CollisionSensor *sensor) FloorCollision;
    public function void (RSDK.CollisionSensor *sensor) LWallCollision;
    public function void (RSDK.CollisionSensor *sensor) RoofCollision;
    public function void (RSDK.CollisionSensor *sensor) RWallCollision;
    public function void (uint16 dst, uint16 src, uint8 cPlane, uint8 cMode) CopyCollisionMask;
    public function void (RSDK.CollisionMask **masks, RSDK.TileInfo **tileInfo) GetCollisionInfo;
#endif
}

[CRepr] public struct APIFunctionTable
{
    // API Core
    public function int32() GetUserLanguage;
    public function bool32() GetConfirmButtonFlip;
    public function void() ExitGame;
    public function void() LaunchManual;

#if RETRO_REV0U
    public function int32() GetDefaultGamepadType;
#endif
    public function bool32(uint32 deviceID) IsOverlayEnabled;
    public function bool32(int32 dlc) CheckDLC;
#if RETRO_USE_EGS
    public function bool32() SetupExtensionOverlay;
    public function bool32(int32 overlay) CanShowExtensionOverlay;
#endif
    public function bool32(int32 overlay) ShowExtensionOverlay;
#if RETRO_USE_EGS
    public function bool32(int32 overlay) CanShowAltExtensionOverlay;
    public function bool32(int32 overlay) ShowAltExtensionOverlay;
    public function int32() GetConnectingStringID;
    public function bool32(int32 id) ShowLimitedVideoOptions;
#endif

    // Achievements
    public function void(RSDK.AchievementID* id) TryUnlockAchievement;
    public function bool32() GetAchievementsEnabled;
    public function void(bool32 enabled) SetAchievementsEnabled;
#if RETRO_USE_EGS
    public function bool32() CheckAchievementsEnabled;
    public function void (RSDK.String **names, int32 count) SetAchievementNames;
#endif
    // Leaderboards
#if RETRO_USE_EGS
    public function bool32() CheckLeaderboardsEnabled;
#endif
    public function void() InitLeaderboards;
    public function void(RSDK.LeaderboardID* leaderboard, bool32 isUser) FetchLeaderboard;
    public function void(RSDK.LeaderboardID* leaderboard, int32 score, function void(bool32 success, int32 rank) callback) TrackScore;
    public function int32() GetLeaderboardsStatus;
    public function RSDK.LeaderboardAvail() LeaderboardEntryViewSize;
    public function RSDK.LeaderboardAvail() LeaderboardEntryLoadSize;
    public function void(int32 start, uint32 end, int32 type) LoadLeaderboardEntries;
    public function void() ResetLeaderboardInfo;
    public function RSDK.LeaderboardEntry*(uint32 entryID) ReadLeaderboardEntry;

    // Rich Presence
    public function void(int32 id, RSDK.String* text) SetRichPresence;

    // Stats
    public function void(RSDK.StatInfo* stat) TryTrackStat;
    public function bool32() GetStatsEnabled;
    public function void(bool32 enabled) SetStatsEnabled;

    // Authorization
    public function void() ClearPrerollErrors;
    public function void() TryAuth;
    public function int32() GetUserAuthStatus;
    public function bool32(RSDK.String* userName) GetUsername;

    // Storage
    public function void() TryInitStorage;
    public function int32() GetStorageStatus;
    public function int32() GetSaveStatus;
    public function void() ClearSaveStatus;
    public function void() SetSaveStatusContinue;
    public function void() SetSaveStatusOK;
    public function void() SetSaveStatusForbidden;
    public function void() SetSaveStatusError;
    public function void(bool32 noSave) SetNoSave;
    public function bool32() GetNoSave;

    // User File Management
    public function void(char8* name, void* buffer, uint32 size, function void(int32 status) callback) LoadUserFile; // load user file from game dir
    public function void(char8* name, void* buffer, uint32 size, function void(int32 status) callback,
        bool32 compressed) SaveUserFile; // save user file to game dir
    public function void(char8* name, function void(int32 status) callback) DeleteUserFile; // delete user file from game dir

    // User DBs
    public function uint16(char8* name, ...) InitUserDB;
    public function uint16(char8* filename, function void(int32 status) callback) LoadUserDB;
    public function bool32(uint16 tableID, function void(int32 status) callback) SaveUserDB;
    public function void(uint16 tableID) ClearUserDB;
    public function void() ClearAllUserDBs;
    public function uint16(uint16 tableID) SetupUserDBRowSorting;
    public function bool32(uint16 tableID) GetUserDBRowsChanged;
    public function int32(uint16 tableID, int32 type, char8* name, void* value) AddRowSortFilter;
    public function int32(uint16 tableID, int32 type, char8* name, bool32 sortAscending) SortDBRows;
    public function int32(uint16 tableID) GetSortedUserDBRowCount;
    public function int32(uint16 tableID, uint16 row) GetSortedUserDBRowID;
    public function int32(uint16 tableID) AddUserDBRow;
    public function bool32(uint16 tableID, uint32 row, int32 type, char8* name, void* value) SetUserDBValue;
    public function bool32(uint16 tableID, uint32 row, int32 type, char8* name, void* value) GetUserDBValue;
    public function uint32(uint16 tableID, uint16 row) GetUserDBRowUUID;
    public function uint16(uint16 tableID, uint32 uuid) GetUserDBRowByID;
    public function void(uint16 tableID, uint16 row, char8* buffer, size_t bufferSize, char8* format) GetUserDBRowCreationTime;
    public function bool32(uint16 tableID, uint16 row) RemoveDBRow;
    public function bool32(uint16 tableID) RemoveAllDBRows;
}

[CRepr] public struct RSDKFunctionTable
{
    // Registration
#if RETRO_REV0U
    public function void(void** globals, int32 size, function void(void* globals) initCB) RegisterGlobalVariables;
    public function void(void** staticVars, char8* name, uint32 entityClassSize, uint32 staticClassSize,
        function void() update, function void() lateUpdate, function void() staticUpdate, function void() draw,
        function void(void* data) create, function void() stageLoad, function void() editorLoad, function void() editorDraw,
        function void() serialize, function void(void* staticVars) staticLoad) RegisterObject;
#else
    public function void(void** globals, int32 size) RegisterGlobalVariables;
    public function void(void** staticVars, char8* name, uint32 entityClassSize, uint32 staticClassSize,
        function void() update, function void() lateUpdate, function void() staticUpdate, function void() draw,
        function void(void* data) create, function void() stageLoad, function void() editorLoad, function void() editorDraw,
        function void() serialize) RegisterObject;
#endif

#if RETRO_REV02
    public function void(void** varClass, char8* name, uint32 classSize) RegisterStaticVariables;
#endif

    // Entities & Objects
    public function bool32(uint16 group, void** entity) GetActiveEntities;
    public function bool32(uint16 classID, void** entity) GetAllEntities;
    public function void() BreakForeachLoop;
    public function void(uint8 type, char8* name, uint8 classID, int32 offset) SetEditableVar;
    public function void*(uint16 slot) GetEntity;
    public function int32(void* entity) GetEntitySlot;
    public function int32(uint16 classID, bool isActive) GetEntityCount;
    public function int32(uint8 drawGroup, uint16 listPos) GetDrawListRefSlot;
    public function void*(uint8 drawGroup, uint16 listPos) GetDrawListRef;
    public function void(void* entity, uint16 classID, void* data) ResetEntity;
    public function void(uint16 slot, uint16 classID, void* data) ResetEntitySlot;
    public function void*(uint16 classID, void* data, int32 x, int32 y) CreateEntity;
    public function void(void* destEntity, void* srcEntity, bool32 clearSrcEntity) CopyEntity;
    public function bool32(void* entity, RSDK.Vector2* range) CheckOnScreen;
    public function bool32(RSDK.Vector2* position, RSDK.Vector2* range) CheckPosOnScreen;
    public function void(uint8 drawGroup, uint16 entitySlot) AddDrawListRef;
    public function void(uint8 drawGroup, uint16 slot1, uint16 slot2, uint16 count) SwapDrawListEntries;
    public function void(uint8 drawGroup, bool32 sorted, function void() hookCB) SetDrawGroupProperties;

    // Scene Management
    public function void(char8* categoryName, char8* sceneName) SetScene;
    public function void(uint8 state) SetEngineState;
#if RETRO_REV02
    public function void(bool32 shouldHardReset) ForceHardReset;
#endif
    public function bool32() CheckValidScene;
    public function bool32(char8* folderName) CheckSceneFolder;
    public function void() LoadScene;
    public function int32(char8* name) FindObject;

    // Cameras
    public function void() ClearCameras;
    public function void(RSDK.Vector2* targetPos, int32 offsetX, int32 offsetY, bool32 worldRelative) AddCamera;

    // API (Rev01 only)
#if !RETRO_REV02
    public function void*(char8* funcName) GetAPIFunction;
#endif

    // Window/Video Settings
    public function int32(int32 id) GetVideoSetting;
    public function void(int32 id, int32 value) SetVideoSetting;
    public function void() UpdateWindow;

    // Math
    public function int32(int32 angle) Sin1024;
    public function int32(int32 angle) Cos1024;
    public function int32(int32 angle) Tan1024;
    public function int32(int32 angle) ASin1024;
    public function int32(int32 angle) ACos1024;
    public function int32(int32 angle) Sin512;
    public function int32(int32 angle) Cos512;
    public function int32(int32 angle) Tan512;
    public function int32(int32 angle) ASin512;
    public function int32(int32 angle) ACos512;
    public function int32(int32 angle) Sin256;
    public function int32(int32 angle) Cos256;
    public function int32(int32 angle) Tan256;
    public function int32(int32 angle) ASin256;
    public function int32(int32 angle) ACos256;
    public function int32(int32 min, int32 max) Rand;
    public function int32(int32 min, int32 max, int32* seed) RandSeeded;
    public function void(int32 seed) SetRandSeed;
    public function uint8(int32 x, int32 y) ATan2;

    // Matrices
    public function void(RSDK.Matrix* matrix) SetIdentityMatrix;
    public function void(RSDK.Matrix* dest, RSDK.Matrix* matrixA, RSDK.Matrix* matrixB) MatrixMultiply;
    public function void(RSDK.Matrix* matrix, int32 x, int32 y, int32 z, bool32 setIdentity) MatrixTranslateXYZ;
    public function void(RSDK.Matrix* matrix, int32 x, int32 y, int32 z) MatrixScaleXYZ;
    public function void(RSDK.Matrix* matrix, int16 angle) MatrixRotateX;
    public function void(RSDK.Matrix* matrix, int16 angle) MatrixRotateY;
    public function void(RSDK.Matrix* matrix, int16 angle) MatrixRotateZ;
    public function void(RSDK.Matrix* matrix, int16 x, int16 y, int16 z) MatrixRotateXYZ;
    public function void(RSDK.Matrix* dest, RSDK.Matrix* matrix) MatrixInverse;
    public function void(RSDK.Matrix* matDest, RSDK.Matrix* matSrc) MatrixCopy;

    // Strings
    public function void(RSDK.String* string, char8* text, uint32 textLength) InitString;
    public function void(RSDK.String* dst, RSDK.String* src) CopyString;
    public function void(RSDK.String* string, char8* text) SetString;
    public function void(RSDK.String* string, RSDK.String* appendString) AppendString;
    public function void(RSDK.String* string, char8* appendText) AppendText;
    public function void(RSDK.String* stringList, char8* filePath, uint32 charSize) LoadStringList;
    public function bool32(RSDK.String* splitStrings, RSDK.String* stringList, int32 startStringID, int32 stringCount) SplitStringList;
    public function void(char8* destChars, RSDK.String* string) GetCString;
    public function bool32(RSDK.String* string1, RSDK.String* string2, bool32 exactMatch) CompareStrings;

    // Screens & Displays
    public function void(int32* displayID, int32* width, int32* height, int32* refreshRate, char8* text) GetDisplayInfo;
    public function void(int32* width, int32* height) GetWindowSize;
    public function int32(uint8 screenID, uint16 width, uint16 height) SetScreenSize;
    public function void(uint8 screenID, int32 x1, int32 y1, int32 x2, int32 y2) SetClipBounds;
#if RETRO_REV02
    public function void(uint8 startVert2P_S1, uint8 startVert2P_S2, uint8 startVert3P_S1, uint8 startVert3P_S2, uint8 startVert3P_S3) SetScreenVertices;
#endif

    // Spritesheets
    public function uint16(char8* filePath, uint8 scopeType) LoadSpriteSheet;

    // Palettes & Colors
#if RETRO_REV02
    public function void(uint16* lookupTable) SetTintLookupTable;
#else
    public function uint16*() GetintLookupTable;
#endif

    public function void(color maskColor) SetPaletteMask;
    public function void(uint8 bankID, uint8 index, uint32 color) SetPaletteEntry;
    public function color(uint8 bankID, uint8 index) GetPaletteEntry;
    public function void(uint8 newActiveBank, int32 startLine, int32 endLine) SetActivePalette;
    public function void(uint8 sourceBank, uint8 srcBankStart, uint8 destinationBank, uint8 destBankStart, uint8 count) CopyPalette;
#if RETRO_REV02
    public function void(uint8 bankID, char8* path, uint16 disabledRows) LoadPalette;
#endif
    public function void(uint8 bankID, uint8 startIndex, uint8 endIndex, bool32 right) RotatePalette;
    public function void(uint8 destBankID, uint8 srcBankA, uint8 srcBankB, int16 blendAmount, int32 startIndex, int32 endIndex) SetLimitedFade;
#if RETRO_REV02
    public function void(uint8 destBankID, color* srcColorsA, color* srcColorsB, int32 blendAmount, int32 startIndex, int32 count) BlendColors;
#endif

    // Drawing
    public function void(int32 x, int32 y, int32 width, int32 height, uint32 color, int32 alpha, int32 inkEffect, bool32 screenRelative) DrawRect;
    public function void(int32 x1, int32 y1, int32 x2, int32 y2, uint32 color, int32 alpha, int32 inkEffect, bool32 screenRelative) DrawLine;
    public function void(int32 x, int32 y, int32 radius, uint32 color, int32 alpha, int32 inkEffect, bool32 screenRelative) DrawCircle;
    public function void(int32 x, int32 y, int32 innerRadius, int32 outerRadius, uint32 color, int32 alpha, int32 inkEffect,
        bool32 screenRelative) DrawCircleOutline;
    public function void(RSDK.Vector2* vertices, int32 vertCount, int32 r, int32 g, int32 b, int32 alpha, int32 inkEffect) DrawFace;
    public function void(RSDK.Vector2* vertices, color* vertColors, int32 vertCount, int32 alpha, int32 inkEffect) DrawBlendedFace;
    public function void(RSDK.Animator* animator, RSDK.Vector2* position, bool32 screenRelative) DrawSprite;
    public function void(uint16 sheetID, int32 inkEffect, bool32 screenRelative) DrawDeformedSprite;
    public function void(RSDK.Animator* animator, RSDK.Vector2* position, RSDK.String* string, int32 endFrame, int32 textLength, int32 align, int32 spacing,
        void* unused, RSDK.Vector2* charOffsets, bool32 screenRelative) DrawText;
    public function void(uint16* tiles, int32 countX, int32 countY, RSDK.Vector2* position, RSDK.Vector2* offset, bool32 screenRelative) DrawTile;
    public function void(uint16 dest, uint16 src, uint16 count) CopyTile;
    public function void(uint16 sheetID, uint16 tileIndex, uint16 srcX, uint16 srcY, uint16 width, uint16 height) DrawAniTiles;
#if RETRO_REV0U
    public function void(RSDK.Animator* animator, uint16 tileIndex) DrawDynamicAniTiles;
#endif
    public function void(uint32 color, int32 alphaR, int32 alphaG, int32 alphaB) FillScreen;

    // Meshes & 3D Scenes
    public function uint16(char8* filename, uint8 scopeType) LoadMesh;
    public function uint16(char8* identifier, uint16 faceCount, uint8 scopeType) Create3DScene;
    public function void(uint16 sceneIndex) Prepare3DScene;
    public function void(uint16 sceneIndex, uint8 x, uint8 y, uint8 z) SetDiffuseColor;
    public function void(uint16 sceneIndex, uint8 x, uint8 y, uint8 z) SetDiffuseIntensity;
    public function void(uint16 sceneIndex, uint8 x, uint8 y, uint8 z) SetSpecularIntensity;
    public function void(uint16 modelFrames, uint16 sceneIndex, uint8 drawMode, RSDK.Matrix* matWorld, RSDK.Matrix* matView, color color) AddModelTo3DScene;
    public function void(uint16 modelFrames, RSDK.Animator* animator, int16 speed, uint8 loopIndex, bool32 forceApply, int16 frameID) SetModelAnimation;
    public function void(uint16 modelFrames, uint16 sceneIndex, RSDK.Animator* animator, uint8 drawMode, RSDK.Matrix* matWorld, RSDK.Matrix* matView,
        color color) AddMeshFrameTo3DScene;
    public function void(uint16 sceneIndex) Draw3DScene;

    // Sprite Animations & Frames
    public function uint16(char8* filePath, uint8 scopeType) LoadSpriteAnimation;
    public function uint16(char8* filePath, uint32 frameCount, uint32 listCount, uint8 scopeType) CreateSpriteAnimation;
#if RETRO_MOD_LOADER_VER_2
    public function void(uint16 aniFrames, uint16 listID, RSDK.Animator* animator, uint32 forceApply, int32 frameID) SetSpriteAnimation;
#else
    public function void(uint16 aniFrames, uint16 listID, RSDK.Animator* animator, uint32 forceApply, int16 frameID) SetSpriteAnimation;
#endif
    public function void(uint16 aniFrames, uint16 listID, char8* name, int32 frameOffset, uint16 frameCount, int16 speed, uint8 loopIndex,
        uint8 rotationStyle) EditSpriteAnimation;
    public function void(uint16 aniFrames, uint16 listID, RSDK.String* string) SetSpriteString;
    public function uint16(uint16 aniFrames, char8* name) FindSpriteAnimation;
    public function RSDK.SpriteFrame*(uint16 aniFrames, uint16 listID, int32 frameID) GetFrame;
    public function RSDK.Hitbox*(RSDK.Animator* animator, uint8 hitboxID) GetHitbox;
    public function int16(RSDK.Animator* animator) GetFrameID;
    public function int32(uint16 aniFrames, uint16 listID, RSDK.String* string, int32 startIndex, int32 length, int32 spacing) GetStringWidth;
    public function void(RSDK.Animator* animator) ProcessAnimation;

    // Tile Layers
    public function uint16(char8* name) GetTileLayerID;
    public function RSDK.TileLayer*(uint16 layerID) GetTileLayer;
    public function void(uint16 layer, RSDK.Vector2* size, bool32 usePixelUnits) GetLayerSize;
    public function uint16(uint16 layer, int32 x, int32 y) GetTile;
    public function void(uint16 layer, int32 x, int32 y, uint16 tile) SetTile;
    public function void(uint16 dstLayerID, int32 dstStartX, int32 dstStartY, uint16 srcLayerID, int32 srcStartX, int32 srcStartY, int32 countX,
        int32 countY) CopyTileLayer;
    public function void(RSDK.TileLayer* tileLayer) ProcessParallax;
    public function RSDK.ScanlineInfo*() GetScanlines;

    // Object & Tile Collisions
    public function bool32(void* thisEntity, RSDK.Hitbox* thisHitbox, void* otherEntity, RSDK.Hitbox* otherHitbox) CheckObjectCollisionTouchBox;
    public function bool32(void* thisEntity, int32 thisRadius, void* otherEntity, int32 otherRadius) CheckObjectCollisionTouchCircle;
    public function uint8(void* thisEntity, RSDK.Hitbox* thisHitbox, void* otherEntity, RSDK.Hitbox* otherHitbox, bool32 setPos) CheckObjectCollisionBox;
    public function bool32(void* thisEntity, RSDK.Hitbox* thisHitbox, void* otherEntity, RSDK.Hitbox* otherHitbox, bool32 setPos) CheckObjectCollisionPlatform;
    public function bool32(void*, uint16 collisionLayers, uint8 collisionMode, uint8 collisionPlane, int32 xOffset, int32 yOffset,
        bool32 setPos) ObjectTileCollision;
    public function bool32(void*, uint16 collisionLayers, uint8 collisionMode, uint8 collisionPlane, int32 xOffset, int32 yOffset,
        int32 tolerance) ObjectTileGrip;
    public function void(void* entity, RSDK.Hitbox* outer, RSDK.Hitbox* inner) ProcessObjectMovement;

#if RETRO_REV0U
    public function void(int32 minDistance, uint8 lowTolerance, uint8 highTolerance, uint8 floorAngleTolerance, uint8 wallAngleTolerance,
        uint8 roofAngleTolerance) SetupCollisionConfig;
    public function void(RSDK.CollisionSensor* sensors) SetPathGripSensors; // expects 5 sensors
    public function void(RSDK.CollisionSensor* sensor) FindFloorPosition;
    public function void(RSDK.CollisionSensor* sensor) FindLWallPosition;
    public function void(RSDK.CollisionSensor* sensor) FindRoofPosition;
    public function void(RSDK.CollisionSensor* sensor) FindRWallPosition;
    public function void(RSDK.CollisionSensor* sensor) FloorCollision;
    public function void(RSDK.CollisionSensor* sensor) LWallCollision;
    public function void(RSDK.CollisionSensor* sensor) RoofCollision;
    public function void(RSDK.CollisionSensor* sensor) RWallCollision;
#endif
    public function int32(uint16 tile, uint8 cPlane, uint8 cMode) GetTileAngle;
    public function void(uint16 tile, uint8 cPlane, uint8 cMode, uint8 angle) SetTileAngle;
    public function uint8(uint16 tile, uint8 cPlane) GetTileFlags;
    public function void(uint16 tile, uint8 cPlane, uint8 flag) SetTileFlags;
#if RETRO_REV0U
    public function void(uint16 dst, uint16 src, uint8 cPlane, uint8 cMode) CopyCollisionMask;
    public function void(RSDK.CollisionMask** masks, RSDK.TileInfo** tileInfo) GetCollisionInfo;
#endif

    // Audio
    public function uint16(char8* path) GetSfx;
    public function int32(uint16 sfx, int32 loopPoint, int32 priority) PlaySfx;
    public function void(uint16 sfx) StopSfx;
    public function int32(char8* filename, uint32 channel, uint32 startPos, uint32 loopPoint, bool32 loadASync) PlayStream;
    public function void(uint8 channel, float volume, float pan, float speed) SetChannelAttributes;
    public function void(uint32 channel) StopChannel;
    public function void(uint32 channel) PauseChannel;
    public function void(uint32 channel) ResumeChannel;
    public function bool32(uint16 sfx) IsSfxPlaying;
    public function bool32(uint32 channel) ChannelActive;
    public function uint32(uint32 channel) GetChannelPos;

    // Videos & "HD Images"
    public function bool32(char8* filename, double startDelay, function bool32() skipCallback) LoadVideo;
    public function bool32(char8* filename, double displayLength, double fadeSpeed, function bool32() skipCallback) LoadImage;

    // Input
#if RETRO_REV02
    public function uint32(uint8 inputSlot) GetInputDeviceID;
    public function uint32(bool32 confirmOnly, bool32 unassignedOnly, uint32 maxInactiveTimer) GetFilteredInputDeviceID;
    public function int32(uint32 deviceID) GetInputDeviceType;
    public function bool32(uint32 deviceID) IsInputDeviceAssigned;
    public function int32(uint32 deviceID) GetInputDeviceUnknown;
    public function int32(uint32 deviceID, int32 unknown1, int32 unknown2) InputDeviceUnknown1;
    public function int32(uint32 deviceID, int32 unknown1, int32 unknown2) InputDeviceUnknown2;
    public function int32(uint8 inputSlot) GetInputSlotUnknown;
    public function int32(uint8 inputSlot, int32 unknown1, int32 unknown2) InputSlotUnknown1;
    public function int32(uint8 inputSlot, int32 unknown1, int32 unknown2) InputSlotUnknown2;
    public function void(uint8 inputSlot, uint32 deviceID) AssignInputSlotToDevice;
    public function bool32(uint8 inputSlot) IsInputSlotAssigned;
    public function void() ResetInputSlotAssignments;
#endif
#if !RETRO_REV02
    public function void(int32 inputSlot, int32 type, int32* value) GetUnknownInputValue;
#endif

    // User File Management
    public function bool32(char8* fileName, void* buffer, uint32 size) LoadUserFile; // load user file from exe dir
    public function bool32(char8* fileName, void* buffer, uint32 size) SaveUserFile; // save user file to exe dir

    // Printing (Rev02)
#if RETRO_REV02
    public function void(int32 mode, char8* message, ...) PrintLog;
    public function void(int32 mode, char8* message) PrintText;
    public function void(int32 mode, RSDK.String* message) PrintString;
    public function void(int32 mode, char8* message, uint32 i) PrintUInt32;
    public function void(int32 mode, char8* message, int32 i) PrintInt32;
    public function void(int32 mode, char8* message, float f) PrintFloat;
    public function void(int32 mode, char8* message, RSDK.Vector2 vec) PrintVector2;
    public function void(int32 mode, char8* message, RSDK.Hitbox hitbox) PrintHitbox;
#endif

    // Editor
    public function void(int32 classID, char8* name) SetActiveVariable;
    public function void(char8* name) AddVarEnumValue;

    // Printing (Rev01)
#if !RETRO_REV02
    public function void(void* message, uint8 type) PrintMessage;
#endif

    // Debugging
#if RETRO_REV02
    public function void() ClearViewableVariables;
    public function void(char8* name, void* value, int32 type, int32 min, int32 max) AddViewableVariable;
#endif

#if RETRO_REV0U
    // Origins Extras
    public function void(int32 callbackID, int32 param1, int32 param2, int32 param3) NotifyCallback;
    public function void() SetGameFinished;
    public function void() StopAllSfx;
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

[CRepr] public struct SKUInfo
{
    public int32 platform;
    public int32 language;
    public int32 region;
}

public enum StatusCodes
{
    STATUS_NONE      = 0,
    STATUS_CONTINUE  = 100,
    STATUS_OK        = 200,
    STATUS_FORBIDDEN = 403,
    STATUS_NOTFOUND  = 404,
    STATUS_ERROR     = 500,
    STATUS_NOWIFI    = 503,
    STATUS_TIMEOUT   = 504,
    STATUS_CORRUPT   = 505,
    STATUS_NOSPACE   = 506,
}

// None of these besides the named 2 are even used
// and even then they're not even set in plus
[CRepr] public struct UnknownInfo
{
    public int32 unknown1;
    public int32 unknown2;
    public int32 unknown3;
    public int32 unknown4;
    public bool32 pausePress;
    public int32 unknown5;
    public int32 unknown6;
    public int32 unknown7;
    public int32 unknown8;
    public int32 unknown9;
    public bool32 anyKeyPress;
    public int32 unknown10;
}

[CRepr] public struct GameInfo
{
    public char8[0x40] gameTitle;
    public char8[0x100] gameSubtitle;
    public char8[0x10] version;
}

[CRepr] public struct InputState
{
    public bool32 down;
    public bool32 press;
    public int32 keyMap;
}

[CRepr] public struct ControllerState
{
    public RSDK.InputState keyUp;
    public RSDK.InputState keyDown;
    public RSDK.InputState keyLeft;
    public RSDK.InputState keyRight;
    public RSDK.InputState keyA;
    public RSDK.InputState keyB;
    public RSDK.InputState keyC;
    public RSDK.InputState keyX;
    public RSDK.InputState keyY;
    public RSDK.InputState keyZ;
    public RSDK.InputState keyStart;
    public RSDK.InputState keySelect;

    // Rev01 hasn't split these into different structs yet
#if RETRO_REV01
    public RSDK.InputState keyBumperL;
    public RSDK.InputState keyBumperR;
    public RSDK.InputState keyTriggerL;
    public RSDK.InputState keyTriggerR;
    public RSDK.InputState keyStickL;
    public RSDK.InputState keyStickR;
#endif
}

[CRepr] public struct AnalogState
{
    public RSDK.InputState keyUp;
    public RSDK.InputState keyDown;
    public RSDK.InputState keyLeft;
    public RSDK.InputState keyRight;
#if RETRO_REV02
    public RSDK.InputState keyStick;
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
[CRepr] public struct TriggerState {
    public RSDK.InputState keyBumper;
    public RSDK.InputState keyTrigger;
    public float bumperDelta;
    public float triggerDelta;
}
#endif

[CRepr] public struct TouchInfo
{
    public float[0x10] x;
    public float[0x10] y;
    public bool32[0x10] down;
    public uint8 count;
#if !RETRO_REV02
    public bool32 pauseHold;
    public bool32 pausePress;
    public bool32 waitRetryState;
    public bool32 anyKeyHold;
    public bool32 anyKeyPress;
    public bool32 unknown2;
#endif
}

[CRepr] public struct ScreenInfo
{
    public uint16[Const.SCREEN_XMAX * Const.SCREEN_YSIZE] frameBuffer;
    public RSDK.Vector2 position;
    public RSDK.Vector2 size;
    public RSDK.Vector2 center;
    public int32 pitch;
    public int32 clipBound_X1;
    public int32 clipBound_Y1;
    public int32 clipBound_X2;
    public int32 clipBound_Y2;
    public int32 waterDrawPos;
}

[CRepr] public struct EngineInfo
{
    public RSDK.RSDKFunctionTable* RSDKTable = null;
#if RETRO_REV02
    public RSDK.APIFunctionTable* APITable = null;
#endif

    public RSDK.GameInfo* gameInfo = null;
#if RETRO_REV02
    public RSDK.SKUInfo* currentSKU = null;
#endif
    public RSDK.SceneInfo* sceneInfo = null;

    public RSDK.ControllerState* controllerInfo = null;
    public RSDK.AnalogState* stickInfoL = null;
#if RETRO_REV02
    public RSDK.AnalogState* stickInfoR = null;
    public RSDK.TriggerState* triggerInfoL = null;
    public RSDK.TriggerState* triggerInfoR = null;
#endif
    public RSDK.TouchInfo* touchInfo = null;

#if RETRO_REV02
    public RSDK.UnknownInfo* unknownInfo = null;
#endif

    public RSDK.ScreenInfo* screenInfo = null;

#if RETRO_REV02 && RETRO_REV0U
    public void* hedgehogLink = null;
#endif

#if RETRO_USE_MOD_LOADER
    public RSDK.ModFunctionTable *modFunctionTable = null;
#endif
}

static
{
    public static void InitEngineInfo(RSDK.EngineInfo* info)
    {
        RSDKTable = info.RSDKTable;
#if RETRO_REV02
        APITable = info.APITable;
#endif

        gameInfo = info.gameInfo;
#if RETRO_REV02
        SKU = info.currentSKU;
#endif
        sceneInfo      = info.sceneInfo;
        controllerInfo = info.controllerInfo;

        analogStickInfoL = info.stickInfoL;
#if RETRO_REV02
        analogStickInfoR = info.stickInfoR;
        triggerInfoL     = info.triggerInfoL;
        triggerInfoR     = info.triggerInfoR;
#endif
        touchInfo = info.touchInfo;

#if RETRO_REV02
        unknownInfo = info.unknownInfo;
#endif
        screenInfo = info.screenInfo;

#if RETRO_USE_MOD_LOADER
        modTable = info.modFunctionTable;
#endif
    }
}
