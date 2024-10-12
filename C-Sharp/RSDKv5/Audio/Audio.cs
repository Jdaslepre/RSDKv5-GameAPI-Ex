namespace RSDK;

public unsafe struct Channel
{
    public Channel() => id = 0;
    public Channel(byte id) => this.id = id;

    public void SetAttributes(float volume, float pan, float speed) => RSDKTable->SetChannelAttributes(id, volume, pan, speed);

    public void Stop() => RSDKTable->StopChannel(id);
    public void Pause() => RSDKTable->PauseChannel(id);
    public void Resume() => RSDKTable->ResumeChannel(id);
    public bool32 IsActive() => RSDKTable->ChannelActive(id);

    public uint AudioPos() => RSDKTable->GetChannelPos(id);

    public int PlayStream(string filename, uint startPos, uint loopPoint, bool32 loadASync) => RSDKTable->PlayStream(filename, id, startPos, loopPoint, loadASync);

    public byte id;
}

public unsafe struct SoundFX
{
    public ushort id;

    public void Init() => id = 0xFFFF;

    public void Get(string path) => id = RSDKTable->GetSfx(path);
   
    public int Play(int loopPoint = 0, int priority = 0xFF) => RSDKTable->PlaySfx(id, loopPoint, priority);

    public void Stop() => RSDKTable->StopSfx(id);

    public bool32 IsPlaying() => RSDKTable->IsSfxPlaying(id);

#if RETRO_REV0U
    public static void StopAll() => RSDKTable->StopAllSfx();
#endif

    public bool32 Loaded() => id != 0xFFFF;

    public bool32 Matches(SoundFX other) => id == other.id;
    public bool32 Matches(SoundFX* other) => other != null ? id == other->id : id == 0xFFFF;
}

public class Audio
{
    public static Channel[] channels = [ new Channel(0), new Channel(1), new Channel(2), new Channel(3),
                                         new Channel(4), new Channel(5), new Channel(6), new Channel(7),
                                         new Channel(8), new Channel(9), new Channel(10), new Channel(11),
                                         new Channel(12), new Channel(13), new Channel(14), new Channel(15) ];
}