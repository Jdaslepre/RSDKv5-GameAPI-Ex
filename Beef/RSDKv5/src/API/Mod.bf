using System;

namespace RSDK.Mod;

#if RETRO_USE_MOD_LOADER

static
{
	public static char8* id = ":Unknown Mod ID:";
}

[CRepr] public struct ModVersionInfo
{
	public uint8 engineVer;
	public uint8 gameVer;
	public uint8 modLoaderVer;
}

public enum ModCallbackEvents : int32
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

static
{
	// Mod Callbacks & Public Functions
	public static void AddModCallback(int32 callbackID, function void(void*) callback) => modTable.AddModCallback(callbackID, callback);
	public static void AddPublicFunction(char8* functionName, void* functionPtr) => modTable.AddPublicFunction(functionName, functionPtr);
	public static void* GetPublicFunction(char8* id, char8* functionName) { return modTable.GetPublicFunction(id, functionName); }

	// Shaders
	public static void LoadShader(char8* shaderName, bool32 linear) => modTable.LoadShader(shaderName, linear);

	// Misc
	public static void* GetGlobals() { return modTable.GetGlobals(); }

	public static class List
	{
		public static bool32 LoadModInfo(char8* id, RSDK.String* name, RSDK.String* description, RSDK.String* version, bool32* active) { return modTable.LoadModInfo(id, name, description, version, active); }
		public static void GetModPath(char8* id, RSDK.String* result) => modTable.GetModPath(id, result);
		public static int32 GetModCount(bool32 active = 0) { return modTable.GetModCount(active); }
		public static char8* GetModIDByIndex(bool32 index) { return modTable.GetModIDByIndex(index); }
	}

	public static class Achievements
	{
		public static void Register(char8* identifier, char8* name, char8* desc) => modTable.RegisterAchievement(identifier, name, desc);
		public static void GetInfo(uint32 id, RSDK.String* name, RSDK.String* description, RSDK.String* identifier, bool32* achieved) => modTable.GetAchievementInfo(id, name, description, identifier, achieved);
		public static int32 GetIndexByID(char8* identifier) { return modTable.GetAchievementIndexByID(identifier); }
		public static int32 GetCount() { return modTable.GetAchievementCount(); }
	}


	public static class Settings
	{
		public static bool32 GetBool(char8* id, char8* key, bool32 fallback) { return modTable.GetSettingsBool(id, key, fallback); }
		public static int GetInteger(char8* id, char8* key, int32 fallback) { return modTable.GetSettingsInteger(id, key, fallback); }
		public static float GetFloat(char8* id, char8* key, float fallback) { return modTable.GetSettingsFloat(id, key, fallback); }
		public static void GetString(char8* id, char8* key, RSDK.String* result, char8* fallback) => modTable.GetSettingsString(id, key, result, fallback);
		public static void SetBool(char8* key, bool32 val) => modTable.SetSettingsBool(key, val);
		public static void SetInteger(char8* key, int32 val) => modTable.SetSettingsInteger(key, val);
		public static void SetFloat(char8* key, float val) => modTable.SetSettingsFloat(key, val);
		public static void SetString(char8* key, RSDK.String* val) => modTable.SetSettingsString(key, val);
		public static void SaveSettings() => modTable.SaveSettings();
	}

	public static class Config
	{
		public static bool32 GetBool(char8* id, bool32 fallback) { return modTable.GetConfigBool(id, fallback); }
		public static int GetInteger(char8* id, int32 fallback) { return modTable.GetConfigInteger(id, fallback); }
		public static float GetFloat(char8* id, float fallback) { return modTable.GetConfigFloat(id, fallback); }
		public static void GetString(char8* id, RSDK.String* result, char8* fallback) => modTable.GetConfigString(id, result, fallback);
	}

#if RETRO_MOD_LOADER_VER_2
	public static class Files
	{
		public static bool32 ExcludeFile(char8* id, char8* path) { return modTable.ExcludeFile(id, path); }
		public static bool32 ExcludeAllFiles(char8* id) { return modTable.ExcludeAllFiles(id); }
		public static bool32 ReloadFile(char8* id, char8* path) { return modTable.ReloadFile(id, path); }
		public static bool32 ReloadAllFiles(char8* id) { return modTable.ReloadAllFiles(id); }
	}

	public static class Engine
	{
		public static void* GetSpriteAnimation(uint16 id) { return modTable.GetSpriteAnimation(id); }
		public static void* GetSpriteSurface(uint16 id) { return modTable.GetSpriteSurface(id); }
		public static uint16* GetPaletteBank(uint8 id) { return modTable.GetPaletteBank(id); }
		public static uint8* GetActivePaletteBuffer() { return modTable.GetActivePaletteBuffer(); }
		public static void GetRGB32To16Buffer(uint16** rgb32To16_R, uint16** rgb32To16_G, uint16** rgb32To16_B) => modTable.GetRGB32To16Buffer(rgb32To16_R, rgb32To16_G, rgb32To16_B);
		public static uint16* GetBlendLookupTable() { return modTable.GetBlendLookupTable(); }
		public static uint16* GetSubtractLookupTable() { return modTable.GetSubtractLookupTable(); }
		public static uint16* GetTintLookupTable() { return modTable.GetTintLookupTable(); }
		public static uint GetMaskColor() { return modTable.GetMaskColor(); }
		public static void* GetScanEdgeBuffer() { return modTable.GetScanEdgeBuffer(); }
		public static void* GetCamera(uint8 id) { return modTable.GetCamera(id); }
		public static void* GetShader(uint8 id) { return modTable.GetShader(id); }
		public static void* GetModel(uint16 id) { return modTable.GetModel(id); }
		public static void* GetScene3D(uint16 id) { return modTable.GetScene3D(id); }
		public static void* GetSfx(uint16 id) { return modTable.GetSfx(id); }
		public static void* GetChannel(uint8 id) { return modTable.GetChannel(id); }
	}
#endif

	public static void RegisterStateHook(function void() state, function bool32(bool32 skippedState) hook, bool32 priority) => modTable.RegisterStateHook(state, hook, priority);
}

#endif