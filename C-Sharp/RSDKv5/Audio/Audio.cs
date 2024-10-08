using System;
using static RSDK.EngineAPI;

namespace RSDK
{
    public unsafe struct Channel
    {
        public Channel() { id = 0; }
        public Channel(byte id) { this.id = id; }

        public void SetAttributes(float volume, float pan, float speed) => RSDKTable.SetChannelAttributes(id, volume, pan, speed);
        public void Stop() => RSDKTable.StopChannel(id);
        public void Pause() => RSDKTable.PauseChannel(id);
        public void Resume() => RSDKTable.ResumeChannel(id);
        public bool32 IsActive() { return RSDKTable.ChannelActive(id); }
        public uint AudioPos() { return RSDKTable.GetChannelPos(id); }
        public int PlayStream(string filename, uint startPos, uint loopPoint, bool32 loadASync) { return RSDKTable.PlayStream(filename, id, startPos, loopPoint, loadASync); }

        public byte id;
    }

    public unsafe struct SoundFX
    {
        public ushort id;

        public void Init() => id = unchecked((ushort)-1);
        public void Get(string path) => id = RSDKTable.GetSfx(path);
        public bool32 Loaded() { return id != unchecked((ushort)-1); }
        public int Play(int loopPoint = 0, int priority = 0xFF) { return RSDKTable.PlaySfx(id, loopPoint, priority); }
        public void Stop() => RSDKTable.StopSfx(id);
        public bool32 IsPlaying() { return RSDKTable.IsSfxPlaying(id); }
#if RETRO_REV0U
        public static void StopAll() => RSDKTable.StopAllSfx();
#endif
        public bool32 Matches(SoundFX other) { return id == other.id; }
        public bool32 Matches(SoundFX* other)
        {
            if (other != null)
                return id == other->id;
            else
                return id == unchecked((ushort)-1);
        }
    }

    public class Audio
    {
        public static RSDK.Channel[] channels = new RSDK.Channel[Const.CHANNEL_COUNT];
    }
}
