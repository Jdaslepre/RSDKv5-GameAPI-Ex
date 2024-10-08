using static RSDK.EngineAPI;

#if RETRO_REV02
namespace RSDK
{
    public struct AchievementID
    {
        public byte idPS4;     // achievement ID (PS4)
        public int idUnknown;  // achievement ID (unknown platform)
        public string id;      // achievement ID (as a string, used for most platforms)
    };

    namespace API 
    {
        public unsafe class Achievements
        {
            public static void TryUnlockAchievement(RSDK.AchievementID* id) => APITable.TryUnlockAchievement(id);
            public static bool32 GetEnabled() { return APITable.GetAchievementsEnabled(); }
            public static void SetEnabled(bool32 enabled) => APITable.SetAchievementsEnabled(enabled);
#if RETRO_USE_EGS
            public static bool32 CheckEnabled() { return APITable.CheckAchievementsEnabled(); }
            public static void SetNames(RSDK.String **names, int count) => APITable.SetAchievementNames(names, count);
#endif
        }
    }
}
#endif