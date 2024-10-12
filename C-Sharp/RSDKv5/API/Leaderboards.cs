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
        public String username;
        public String userID;
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
            public static bool32 CheckEnabled() => APITable->CheckLeaderboardsEnabled();
#endif
            public static void Init() => APITable->InitLeaderboards();
            public static void Fetch(LeaderboardID* leaderboard, bool32 isUser) => APITable->FetchLeaderboard(leaderboard, isUser);
            public static void TrackScore(LeaderboardID* leaderboard, int score, delegate* unmanaged<bool32, int, void> callback) => APITable->TrackScore(leaderboard, score, callback);
            public static int GetStatus() => APITable->GetLeaderboardsStatus();
            public static LeaderboardAvail EntryViewSize() => APITable->LeaderboardEntryViewSize();
            public static LeaderboardAvail EntryLoadSize() => APITable->LeaderboardEntryLoadSize();
            public static void LoadEntries(int start, uint end, int type) => APITable->LoadLeaderboardEntries(start, end, type);
            public static void ResetInfo() => APITable->ResetLeaderboardInfo();
            public static LeaderboardEntry* ReadEntry(uint entryID) => APITable->ReadLeaderboardEntry(entryID);
        }
    }
}
#endif