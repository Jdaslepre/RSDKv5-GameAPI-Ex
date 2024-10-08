using System;

#if RETRO_REV02
namespace RSDK;
[CRepr] public struct AchievementID {
	public uint8 idPS4;     // achievement ID (PS4)
	public int32 idUnknown; // achievement ID (unknown platform)
	public char8 *id;  		// achievement ID (as a string, used for most platforms)
}

namespace RSDK.API;
public static class Achievements
{
	public static void TryUnlockAchievement(RSDK.AchievementID *id) => APITable.TryUnlockAchievement(id);
	public static bool32 GetEnabled() { return APITable.GetAchievementsEnabled(); }
	public static void SetEnabled(bool32 enabled) => APITable.SetAchievementsEnabled(enabled);
#if RETRO_USE_EGS
	public static inline bool CheckEnabled() { return APITable.CheckAchievementsEnabled(); }
	public static inline void SetNames(RSDK.String **names, int32 count) => APITable.SetAchievementNames(names, count);
#endif
}
#endif