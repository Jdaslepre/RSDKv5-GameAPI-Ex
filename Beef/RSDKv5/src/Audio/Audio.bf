using System;

namespace RSDK;

public struct Channel
{
    public this() => id = 0;
    public this(uint8 channelID) => id = channelID;
    public this(int32 channelID) => id = (.)channelID;

    public void SetAttributes(float volume, float pan, float speed) => RSDKTable.SetChannelAttributes(id, volume, pan, speed);

    public void Stop() => RSDKTable.StopChannel(id);
    public void Pause() => RSDKTable.PauseChannel(id);
    public void Resume() => RSDKTable.ResumeChannel(id);
    public bool32 IsActive() { return RSDKTable.ChannelActive(id); }

    public uint32 AudioPos() { return RSDKTable.GetChannelPos(id); }

    public int32 PlayStream(char8* filename, uint32 startPos, uint32 loopPoint, bool loadASync)
    {
        return RSDKTable.PlayStream(filename, id, startPos, loopPoint, loadASync);
    }

    public uint8 id;
}

static
{
    public static RSDK.Channel[Const.CHANNEL_COUNT] channels = .();
}

public struct SoundFX
{
    public uint16 id;

    public void Init() mut => id = (.)(-1);

    public void Get(char8* path) mut => id = RSDKTable.GetSfx(path);

    public int32 Play(int32 loopPoint = 0, int32 priority = 0xFF) { return RSDKTable.PlaySfx(id, loopPoint, priority); }

    public void Stop() => RSDKTable.StopSfx(id);

    public bool32 IsPlaying() { return RSDKTable.IsSfxPlaying(id); }

#if RETRO_REV0U
    public static void StopAll() => RSDKTable.StopAllSfx();
#endif

    public bool32 Loaded() mut { return id != (.)(-1); }

    public bool32 Matches(RSDK.SoundFX other) { return this.id == other.id; }
    public bool32 Matches(RSDK.SoundFX* other)
    {
        return other != null ? id == other.id : id == (.)(-1);
    }
}