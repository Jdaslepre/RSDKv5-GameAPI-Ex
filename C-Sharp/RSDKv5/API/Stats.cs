#if RETRO_REV02
namespace RSDK
{
    public unsafe struct StatInfo
    {
        public byte statID;
        public string name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public void*[] data;
    };

    namespace API
    {
        public unsafe class Stats
        {
            public static void TryTrackStat(StatInfo* stat) => APITable->TryTrackStat(stat);
            public static bool32 GetEnabled() => APITable->GetStatsEnabled();
            public static void SetEnabled(bool32 enabled) => APITable->SetStatsEnabled(enabled);
        }
    }
}
#endif