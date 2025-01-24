namespace RSDK;

public struct Channel
{
    public this() => id = 0;
    public this(uint8 channelID) => id = channelID;
    public this(int32 channelID) => id = (.)channelID;

    public void SetAttributes(float volume, float pan, float speed) => RSDKTable.SetChannelAttributes(id, volume, pan, speed);

    public void Stop()       => RSDKTable.StopChannel(id);
    public void Pause()      => RSDKTable.PauseChannel(id);
    public void Resume()     => RSDKTable.ResumeChannel(id);
    public bool32 IsActive() => RSDKTable.ChannelActive(id);

    public uint32 AudioPos() => RSDKTable.GetChannelPos(id);

    public int32 PlayStream(char8* filename, uint32 startPos, uint32 loopPoint, bool32 loadASync) => RSDKTable.PlayStream(filename, id, startPos, loopPoint, loadASync);

    public uint8 id;
}

static
{
    public static Channel[Const.CHANNEL_COUNT] channels = .(.(0), .(1), .(2), .(3), .(4), .(5), .(6), .(7), .(8), .(9), .(10), .(11), .(12), .(13), .(14), .(15));;
}

public struct SoundFX
{
    public uint16 id;

    public void Init() mut => id = (.)(-1);

    public void Get(char8* path) mut => id = RSDKTable.GetSfx(path);

    public int32 Play(int32 loopPoint = 0, int32 priority = 0xFF) => RSDKTable.PlaySfx(id, loopPoint, priority);

    public void Stop() => RSDKTable.StopSfx(id);

    public bool32 IsPlaying() => RSDKTable.IsSfxPlaying(id);

#if RETRO_REV0U
    public static void StopAll() => RSDKTable.StopAllSfx();
#endif

    public bool32 Loaded() mut => id != (.)(-1);

    public bool32 Matches(Self other)  => id == other.id;
    public bool32 Matches(Self* other) => other != null ? id == other.id : id == (.)(-1);
}