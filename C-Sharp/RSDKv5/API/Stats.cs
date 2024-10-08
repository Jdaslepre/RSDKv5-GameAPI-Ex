using static RSDK.EngineAPI;

#if RETRO_REV02
namespace RSDK
{
    public unsafe struct StatInfo
    {
        public byte statID;
        public string? name;
        public void*[] data = new void*[64];

        public StatInfo() { }
    };

    namespace API
    {
        public unsafe class Stats
        {
            public static void TryTrackStat(RSDK.StatInfo* stat) => APITable.TryTrackStat(stat);
            public static bool32 GetEnabled() { return APITable.GetStatsEnabled(); }
            public static void SetEnabled(bool32 enabled) => APITable.SetStatsEnabled(enabled);
        }
    }
}
#endif