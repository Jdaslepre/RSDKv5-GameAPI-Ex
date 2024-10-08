using System;

#if RETRO_REV02
namespace RSDK
{
	public enum LeaderboardLoadTypes
	{
	    LEADERBOARD_LOAD_INIT,
	    LEADERBOARD_LOAD_PREV,
	    LEADERBOARD_LOAD_NEXT,
	}

	[CRepr] public struct LeaderboardID
	{
	    public int idPS4;     // leaderboard id (PS4)
	    public int idUnknown; // leaderboard id (unknown platform)
	    public int idSwitch;  // leaderboard id (switch)
	    public char8* idXbox; // Xbox One Leaderboard id (making an assumption based on the MS docs)
	    public char8* idPC;   // Leaderboard id (as a string, used for PC platforms)
	}

	[CRepr] public struct LeaderboardAvail
	{
	    public int start;
	    public int length;
	}

	[CRepr] public struct LeaderboardEntry
	{
	    public RSDK.String username;
	    public RSDK.String userID;
	    public int globalRank;
	    public int score;
	    public bool32 isUser;
	    public int status;
	}

namespace API
{
	public static class Leaderboards
	{
#if RETRO_USE_EGS
		public static bool32 CheckEnabled() { return APITable.CheckLeaderboardsEnabled(); }
#endif
		public static void Init() => APITable.InitLeaderboards();
		public static void Fetch(RSDK.LeaderboardID* leaderboard, bool isUser) => APITable.FetchLeaderboard(leaderboard, isUser);
		public static void TrackScore(RSDK.LeaderboardID* leaderboard, int32 score, function void(bool32 success, int32 rank) callback) => APITable.TrackScore(leaderboard, score, callback);
		public static int GetStatus() { return APITable.GetLeaderboardsStatus(); }
		public static RSDK.LeaderboardAvail EntryViewSize() { return APITable.LeaderboardEntryViewSize(); }
		public static RSDK.LeaderboardAvail EntryLoadSize() { return APITable.LeaderboardEntryLoadSize(); }
		public static void LoadEntries(int32 start, uint32 end, int32 type) => APITable.LoadLeaderboardEntries(start, end, type);
		public static void ResetInfo() => APITable.ResetLeaderboardInfo();
		public static RSDK.LeaderboardEntry* ReadEntry(uint32 entryID) { return APITable.ReadLeaderboardEntry(entryID); }
	}
}
}
#endif