namespace RSDK;

public static class Video
{
    public static bool32 LoadVideo(char8* filename, double startDelay, function bool32() skipCallback) => RSDKTable.LoadVideo(filename, startDelay, skipCallback);
    public static bool32 LoadImage(char8* filename, double displayLength, double speed, function bool32() skipCallback) => RSDKTable.LoadImage(filename, displayLength, speed, skipCallback);
}