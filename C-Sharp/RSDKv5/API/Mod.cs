#if RETRO_USE_MOD_LOADER
namespace RSDK;

public struct ModVersionInfo
{
    public byte engineVer;
    public byte gameVer;
    public byte modLoaderVer;
}

public static class ModCallbackEvents
{
    public const int MODCB_ONGAMESTARTUP = 0;
    public const int MODCB_ONSTATICLOAD = 1;
    public const int MODCB_ONSTAGELOAD = 2;
    public const int MODCB_ONUPDATE = 3;
    public const int MODCB_ONLATEUPDATE = 4;
    public const int MODCB_ONSTATICUPDATE = 5;
    public const int MODCB_ONDRAW = 6;
    public const int MODCB_ONSTAGEUNLOAD = 7;
    public const int MODCB_ONSHADERLOAD = 8;
    public const int MODCB_ONVIDEOSKIPCB = 9;
    public const int MODCB_ONSCANLINECB = 10;
}

public static class ModSuper
{
    public const int SUPER_UPDATE = 0;
    public const int SUPER_LATEUPDATE = 1;
    public const int SUPER_STATICUPDATE = 2;
    public const int SUPER_DRAW = 3;
    public const int SUPER_CREATE = 4;
    public const int SUPER_STAGELOAD = 5;
    public const int SUPER_EDITORLOAD = 6;
    public const int SUPER_EDITORDRAW = 7;
    public const int SUPER_SERIALIZE = 8;
}

public unsafe class Mod
{
    public static string id = ":Unknown Mod ID:";

    // --------------------------------
    // Mod Callbacks & Public Functions
    // --------------------------------

    public static void AddModCallback(int callbackID, delegate* unmanaged<void*, void> callback) => modTable->AddModCallback(callbackID, callback);
    public static void AddPublicFunction(string functionName, void* functionPtr) => modTable->AddPublicFunction(functionName, functionPtr);
    public static void* GetPublicFunction(string id, string functionName) => modTable->GetPublicFunction(id, functionName);

    // -------
    // Shaders
    // -------

    public static void LoadShader(string shaderName, uint linear) => modTable->LoadShader(shaderName, linear);

    // ----
    // Misc
    // ----

    public static void* GetGlobals() => modTable->GetGlobals();

    public class List
    {
        public static bool32 LoadModInfo(string id, String* name, String* description, String* version, bool32* active) => modTable->LoadModInfo(id, name, description, version, active);
        public static void GetModPath(string id, String* result) => modTable->GetModPath(id, result);
        public static int GetModCount(uint active = 0) => modTable->GetModCount(active);
        public static string GetModIDByIndex(uint index) => modTable->GetModIDByIndex(index);
    }

    public class Achievements
    {
        public static void Register(string identifier, string name, string desc) => modTable->RegisterAchievement(identifier, name, desc);
        public static void GetInfo(uint id, String* name, String* description, String* identifier, bool32* achieved) => modTable->GetAchievementInfo(id, name, description, identifier, achieved);
        public static int GetIndexByID(string identifier) => modTable->GetAchievementIndexByID(identifier);
        public static int GetCount() => modTable->GetAchievementCount();
    }

    public class Settings
    {
        public static bool32 GetBool(string id, string key, bool32 fallback) => modTable->GetSettingsBool(id, key, fallback);
        public static int GetInteger(string id, string key, int fallback) => modTable->GetSettingsInteger(id, key, fallback);
        public static float GetFloat(string id, string key, float fallback) => modTable->GetSettingsFloat(id, key, fallback);
        public static void GetString(string id, string key, String* result, string fallback) => modTable->GetSettingsString(id, key, result, fallback);
        public static void SetBool(string key, bool32 val) => modTable->SetSettingsBool(key, val);
        public static void SetInteger(string key, int val) => modTable->SetSettingsInteger(key, val);
        public static void SetFloat(string key, float val) => modTable->SetSettingsFloat(key, val);
        public static void SetString(string key, String* val) => modTable->SetSettingsString(key, val);
        public static void SaveSettings() => modTable->SaveSettings();
    }

    public class Config
    {
        public static bool32 GetBool(string id, bool32 fallback) => modTable->GetConfigBool(id, fallback);
        public static int GetInteger(string id, int fallback) => modTable->GetConfigInteger(id, fallback);
        public static float GetFloat(string id, float fallback) => modTable->GetConfigFloat(id, fallback);
        public static void GetString(string id, String* result, string fallback) => modTable->GetConfigString(id, result, fallback);
    }

#if RETRO_MOD_LOADER_VER_2
    public class Files
    {
        public static bool32 ExcludeFile(string id, string path) => modTable->ExcludeFile(id, path);
        public static bool32 ExcludeAllFiles(string id) => modTable->ExcludeAllFiles(id);
        public static bool32 ReloadFile(string id, string path) => modTable->ReloadFile(id, path);
        public static bool32 ReloadAllFiles(string id) => modTable->ReloadAllFiles(id);
    }

    public class Engine
    {
        public static void* GetSpriteAnimation(ushort id) => modTable->GetSpriteAnimation(id);
        public static void* GetSpriteSurface(ushort id) => modTable->GetSpriteSurface(id);
        public static ushort* GetPaletteBank(byte id) => modTable->GetPaletteBank(id);
        public static byte* GetActivePaletteBuffer() => modTable->GetActivePaletteBuffer();
        public static void GetRGB32To16Buffer(ushort** rgb32To16_R, ushort** rgb32To16_G, ushort** rgb32To16_B) => modTable->GetRGB32To16Buffer(rgb32To16_R, rgb32To16_G, rgb32To16_B);
        public static ushort* GetBlendLookupTable() => modTable->GetBlendLookupTable();
        public static ushort* GetSubtractLookupTable() => modTable->GetSubtractLookupTable();
        public static ushort* GetTintLookupTable() => modTable->GetTintLookupTable();
        public static uint GetMaskColor() => modTable->GetMaskColor();
        public static void* GetScanEdgeBuffer() => modTable->GetScanEdgeBuffer();
        public static void* GetCamera(byte id) => modTable->GetCamera(id);
        public static void* GetShader(byte id) => modTable->GetShader(id);
        public static void* GetModel(ushort id) => modTable->GetModel(id);
        public static void* GetScene3D(ushort id) => modTable->GetScene3D(id);
        public static void* GetSfx(ushort id) => modTable->GetSfx(id);
        public static void* GetChannel(byte id) => modTable->GetChannel(id);
    }
#endif
    public static void RegisterStateHook(delegate* unmanaged<void> state, delegate* unmanaged<uint, uint> hook, uint priority) => modTable->RegisterStateHook(state, hook, priority);
}
#endif