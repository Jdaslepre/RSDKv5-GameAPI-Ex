using static RSDK.EngineAPI;

#if RETRO_USE_MOD_LOADER

namespace RSDK
{
    public struct ModVersionInfo
    {
        public byte engineVer;
        public byte gameVer;
        public byte modLoaderVer;
    }

    public enum ModCallbackEvents
    {
        MODCB_ONGAMESTARTUP,
        MODCB_ONSTATICLOAD,
        MODCB_ONSTAGELOAD,
        MODCB_ONUPDATE,
        MODCB_ONLATEUPDATE,
        MODCB_ONSTATICUPDATE,
        MODCB_ONDRAW,
        MODCB_ONSTAGEUNLOAD,
        MODCB_ONSHADERLOAD,
        MODCB_ONVIDEOSKIPCB,
        MODCB_ONSCANLINECB,
    }

    public enum ModSuper
    {
        SUPER_UPDATE,
        SUPER_LATEUPDATE,
        SUPER_STATICUPDATE,
        SUPER_DRAW,
        SUPER_CREATE,
        SUPER_STAGELOAD,
        SUPER_EDITORDRAW,
        SUPER_EDITORLOAD,
        SUPER_SERIALIZE
    }

    public unsafe class Mod
    {
        // Mod Callbacks & Public Functions
        public static void AddModCallback(int callbackID, delegate* unmanaged<void*, void> callback) => modTable.AddModCallback(callbackID, callback);
        public static void AddPublicFunction(string functionName, void* functionPtr) => modTable.AddPublicFunction(functionName, functionPtr);
        public static void* GetPublicFunction(string id, string functionName) { return modTable.GetPublicFunction(id, functionName); }

        // Shaders
        public static void LoadShader(string shaderName, uint linear) => modTable.LoadShader(shaderName, linear);

        // Misc
        public static void* GetGlobals() { return modTable.GetGlobals(); }

        public class List
        {
            public static uint LoadModInfo(string id, RSDK.String* name, RSDK.String* description, RSDK.String* version, uint* active) { return modTable.LoadModInfo(id, name, description, version, active); }
            public static void GetModPath(string id, RSDK.String* result) => modTable.GetModPath(id, result);
            public static int GetModCount(uint active = 0) { return modTable.GetModCount(active); }
            public static string GetModIDByIndex(uint index) { return modTable.GetModIDByIndex(index); }
        }

        public class Achievements
        {
            public static void Register(string identifier, string name, string desc) => modTable.RegisterAchievement(identifier, name, desc);
            public static void GetInfo(uint id, RSDK.String* name, RSDK.String* description, RSDK.String* identifier, uint* achieved) => modTable.GetAchievementInfo(id, name, description, identifier, achieved);
            public static int GetIndexByID(string identifier) { return modTable.GetAchievementIndexByID(identifier); }
            public static int GetCount() { return modTable.GetAchievementCount(); }
        }

        public class Settings
        {
            public static uint GetBool(string id, string key, uint fallback) { return modTable.GetSettingsBool(id, key, fallback); }
            public static int GetInteger(string id, string key, int fallback) { return modTable.GetSettingsInteger(id, key, fallback); }
            public static float GetFloat(string id, string key, float fallback) { return modTable.GetSettingsFloat(id, key, fallback); }
            public static void GetString(string id, string key, RSDK.String* result, string fallback) => modTable.GetSettingsString(id, key, result, fallback);
            public static void SetBool(string key, uint val) => modTable.SetSettingsBool(key, val);
            public static void SetInteger(string key, int val) => modTable.SetSettingsInteger(key, val);
            public static void SetFloat(string key, float val) => modTable.SetSettingsFloat(key, val);
            public static void SetString(string key, RSDK.String* val) => modTable.SetSettingsString(key, val);
            public static void SaveSettings() => modTable.SaveSettings();
        }

        public class Config
        {
            public static uint GetBool(string id, uint fallback) { return modTable.GetConfigBool(id, fallback); }
            public static int GetInteger(string id, int fallback) { return modTable.GetConfigInteger(id, fallback); }
            public static float GetFloat(string id, float fallback) { return modTable.GetConfigFloat(id, fallback); }
            public static void GetString(string id, RSDK.String* result, string fallback) => modTable.GetConfigString(id, result, fallback);
        }

#if RETRO_MOD_LOADER_VER_2
        public class Files
        {
            public static uint ExcludeFile(string id, string path) { return modTable.ExcludeFile(id, path); }
            public static uint ExcludeAllFiles(string id) { return modTable.ExcludeAllFiles(id); }
            public static uint ReloadFile(string id, string path) { return modTable.ReloadFile(id, path); }
            public static uint ReloadAllFiles(string id) { return modTable.ReloadAllFiles(id); }
        }

        public class Engine
        {
            public static void* GetSpriteAnimation(ushort id) { return modTable.GetSpriteAnimation(id); }
            public static void* GetSpriteSurface(ushort id) { return modTable.GetSpriteSurface(id); }
            public static ushort* GetPaletteBank(byte id) { return modTable.GetPaletteBank(id); }
            public static byte* GetActivePaletteBuffer() { return modTable.GetActivePaletteBuffer(); }
            public static void GetRGB32To16Buffer(ushort** rgb32To16_R, ushort** rgb32To16_G, ushort** rgb32To16_B) => modTable.GetRGB32To16Buffer(rgb32To16_R, rgb32To16_G, rgb32To16_B);
            public static ushort* GetBlendLookupTable() { return modTable.GetBlendLookupTable(); }
            public static ushort* GetSubtractLookupTable() { return modTable.GetSubtractLookupTable(); }
            public static ushort* GetTintLookupTable() { return modTable.GetTintLookupTable(); }
            public static uint GetMaskColor() { return modTable.GetMaskColor(); }
            public static void* GetScanEdgeBuffer() { return modTable.GetScanEdgeBuffer(); }
            public static void* GetCamera(byte id) { return modTable.GetCamera(id); }
            public static void* GetShader(byte id) { return modTable.GetShader(id); }
            public static void* GetModel(ushort id) { return modTable.GetModel(id); }
            public static void* GetScene3D(ushort id) { return modTable.GetScene3D(id); }
            public static void* GetSfx(ushort id) { return modTable.GetSfx(id); }
            public static void* GetChannel(byte id) { return modTable.GetChannel(id); }
        }
#endif
        public static void RegisterStateHook(delegate* unmanaged<void> state, delegate* unmanaged<uint, uint> hook, uint priority) => modTable.RegisterStateHook(state, hook, priority);
    }
}

#endif