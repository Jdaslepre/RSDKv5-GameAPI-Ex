namespace RSDK;

public unsafe struct Channel
{
    public Channel() => id = 0;
    public Channel(byte id) => this.id = id;

    public void SetAttributes(float volume, float pan, float speed) => RSDKTable.SetChannelAttributes(id, volume, pan, speed);

    public void Stop() => RSDKTable.StopChannel(id);
    public void Pause() => RSDKTable.PauseChannel(id);
    public void Resume() => RSDKTable.ResumeChannel(id);
    public bool32 IsActive() => RSDKTable.ChannelActive(id);

    public uint AudioPos() => RSDKTable.GetChannelPos(id);

    public int PlayStream(string filename, uint startPos, uint loopPoint, bool32 loadASync) => RSDKTable.PlayStream(filename, id, startPos, loopPoint, loadASync);

    public byte id;
}

public unsafe struct SoundFX
{
    public ushort id;

    public void Init() => id = 0xFFFF;

    public void Get(string path) => id = RSDKTable.GetSfx(path);

    public int Play(int loopPoint = 0, int priority = 0xFF) => RSDKTable.PlaySfx(id, loopPoint, priority);

    public void Stop() => RSDKTable.StopSfx(id);

    public bool32 IsPlaying() => RSDKTable.IsSfxPlaying(id);

#if RETRO_REV0U
    public static void StopAll() => RSDKTable.StopAllSfx();
#endif

    public bool32 Loaded() => id != 0xFFFF;

    public bool32 Matches(SoundFX other) => id == other.id;
    public bool32 Matches(SoundFX* other) => other != null ? id == other->id : id == 0xFFFF;
}

public class Audio
{
    public static Channel[] channels = [ new(0), new(1), new(2), new(3),
                                         new(4), new(5), new(6), new(7),
                                         new(8), new(9), new(10), new(11),
                                         new(12), new(13), new(14), new(15) ];
}