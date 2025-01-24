#if RETRO_REV02
namespace RSDK;

public enum LeaderboardLoadTypes
{
    LEADERBOARD_LOAD_INIT,
    LEADERBOARD_LOAD_PREV,
    LEADERBOARD_LOAD_NEXT,
}

[System.CRepr] public struct LeaderboardID
{
    public int idPS4;     // leaderboard id (PS4)
    public int idUnknown; // leaderboard id (unknown platform)
    public int idSwitch;  // leaderboard id (switch)
    public char8* idXbox; // Xbox One Leaderboard id (making an assumption based on the MS docs)
    public char8* idPC;   // Leaderboard id (as a string, used for PC platforms)
}

[System.CRepr] public struct LeaderboardAvail
{
    public int start;
    public int length;
}

[System.CRepr] public struct LeaderboardEntry
{
    public String username;
    public String userID;
    public int globalRank;
    public int score;
    public bool32 isUser;
    public int status;
}

namespace RSDK.API;

public static class Leaderboards
{
#if RETRO_USE_EGS
    public static bool32 CheckEnabled() => APITable.CheckLeaderboardsEnabled();
#endif
    public static void Init() => APITable.InitLeaderboards();
    public static void Fetch(LeaderboardID* leaderboard, bool32 isUser) => APITable.FetchLeaderboard(leaderboard, isUser);
    public static void TrackScore(LeaderboardID* leaderboard, int32 score, function void(bool32 success, int32 rank) callback) => APITable.TrackScore(leaderboard, score, callback);

    public static int GetStatus() => APITable.GetLeaderboardsStatus();

    public static LeaderboardAvail EntryViewSize() => APITable.LeaderboardEntryViewSize();
    public static LeaderboardAvail EntryLoadSize() => APITable.LeaderboardEntryLoadSize();

    public static void LoadEntries(int32 start, uint32 end, int32 type) => APITable.LoadLeaderboardEntries(start, end, type);
    public static void ResetInfo()                                      => APITable.ResetLeaderboardInfo();
    public static LeaderboardEntry* ReadEntry(uint32 entryID)           => APITable.ReadLeaderboardEntry(entryID);
 }
#endif