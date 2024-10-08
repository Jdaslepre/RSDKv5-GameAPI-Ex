using static RSDK.EngineAPI;

namespace RSDK
{
    public unsafe struct SpriteSheet
    {
        public ushort id;

        public void Init() { id = unchecked((ushort)-1); }

        public void Load(string path, Scopes scope) => id = RSDKTable.LoadSpriteSheet(path, (byte)scope);
        public bool32 Loaded() { return id != unchecked((ushort)-1); }

        public bool32 Matches(RSDK.SpriteSheet other) { return id == other.id; }
        public bool32 Matches(RSDK.SpriteSheet* other)
        {
            if (other != null)
                return id == other->id;
            else
                return id == unchecked((ushort)-1);
        }
    }

    public struct SpriteFrame
    {
        public short sprX;
        public short sprY;
        public short width;
        public short height;
        public short pivotX;
        public short pivotY;
        public ushort duration;
        public ushort unicodeChar;
        public byte sheetID;
    }

    public unsafe struct SpriteAnimation
    {
        public ushort aniFrames;

        public void Init() => aniFrames = unchecked((ushort)-1);

        public void Load(string path, Scopes scope) => aniFrames = RSDKTable.LoadSpriteAnimation(path, (byte)scope);
        public void Create(string filename, uint frameCount, uint listCount, Scopes scope) => aniFrames = RSDKTable.CreateSpriteAnimation(filename, frameCount, listCount, (byte)scope);

        public void Edit(ushort listID, string name, int frameOffset, ushort frameCount, short speed, byte loopIndex, byte rotationStyle) => RSDKTable.EditSpriteAnimation(aniFrames, listID, name, frameOffset, frameCount, speed, loopIndex, rotationStyle);

        public ushort FindAnimation(string name) { return RSDKTable.FindSpriteAnimation(aniFrames, name); }

        public bool32 Loaded() { return aniFrames != unchecked((ushort)-1); }

        public RSDK.SpriteFrame* GetFrame(int animID, int frameID) { return RSDKTable.GetFrame(aniFrames, (ushort)animID, frameID); }

        public bool32 Matches(RSDK.SpriteAnimation other) { return aniFrames == other.aniFrames; }
        public bool32 Matches(RSDK.SpriteAnimation* other)
        {
            if (other != null)
                return aniFrames == other->aniFrames;
            else
                return aniFrames == unchecked((ushort)-1);
        }
    }
}
