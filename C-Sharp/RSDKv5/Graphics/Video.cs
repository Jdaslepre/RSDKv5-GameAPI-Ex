namespace RSDK;

public unsafe class Video
{
    public bool32 LoadVideo(string filename, double startDelay, delegate* unmanaged<bool32> skipCallback) => RSDKTable->LoadVideo(filename, startDelay, skipCallback);
    public bool32 LoadImage(string filename, double displayLength, double speed, delegate* unmanaged<bool32> skipCallback) => RSDKTable->LoadImage(filename, displayLength, speed, skipCallback);
}