#if RETRO_REV02
using System;

namespace RSDK;

[CRepr] public struct StatInfo
{
	public uint8 statID;
	public char8* name;
	public void[64] *data;
}

namespace RSDK.API;
	
public static class Stats
{
	public static void TryTrackStat(RSDK.StatInfo* stat) => APITable.TryTrackStat(stat);
	public static bool32 GetEnabled() { return APITable.GetStatsEnabled(); }
	public static void SetEnabled(bool32 enabled) => APITable.SetStatsEnabled(enabled);
}
#endif