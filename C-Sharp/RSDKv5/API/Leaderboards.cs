using static RSDK.EngineAPI;

#if RETRO_REV02
namespace RSDK
{
    public enum LeaderboardLoadTypes
    {
        LEADERBOARD_LOAD_INIT,
        LEADERBOARD_LOAD_PREV,
        LEADERBOARD_LOAD_NEXT,
    }

    public struct LeaderboardID
    {
        public int idPS4;     // leaderboard id (PS4)
        public int idUnknown; // leaderboard id (unknown platform)
        public int idSwitch;  // leaderboard id (switch)
        public string idXbox; // Xbox One Leaderboard id (making an assumption based on the MS docs)
        public string idPC;   // Leaderboard id (as a string, used for PC platforms)
    }

    public struct LeaderboardAvail
    {
        public int start;
        public int length;
    }

    public struct LeaderboardEntry
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
        public unsafe class Leaderboards
        {
#if RETRO_USE_EGS
            public static bool32 CheckEnabled() { return APITable.CheckLeaderboardsEnabled(); }
#endif
            public static void Init() => APITable.InitLeaderboards();
            public static void Fetch(RSDK.LeaderboardID* leaderboard, bool32 isUser) => APITable.FetchLeaderboard(leaderboard, isUser);
            public static void TrackScore(RSDK.LeaderboardID* leaderboard, int score, delegate* unmanaged<bool32, int, void> callback) => APITable.TrackScore(leaderboard, score, callback);
            public static int GetStatus() { return APITable.GetLeaderboardsStatus(); }
            public static RSDK.LeaderboardAvail EntryViewSize() { return APITable.LeaderboardEntryViewSize(); }
            public static RSDK.LeaderboardAvail EntryLoadSize() { return APITable.LeaderboardEntryLoadSize(); }
            public static void LoadEntries(int start, uint end, int type) => APITable.LoadLeaderboardEntries(start, end, type);
            public static void ResetInfo() => APITable.ResetLeaderboardInfo();
            public static RSDK.LeaderboardEntry* ReadEntry(uint entryID) { return APITable.ReadLeaderboardEntry(entryID); }
        }
    }
}
#endif