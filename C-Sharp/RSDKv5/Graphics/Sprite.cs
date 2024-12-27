namespace RSDK;

public unsafe struct SpriteSheet
{
    public ushort id;

    public void Init() => id = 0xFFFF;

    public void Load(string path, Scopes scope) => id = RSDKTable.LoadSpriteSheet(path, (byte)scope);

    public bool32 Loaded() => id != 0xFFFF;

    public bool32 Matches(RSDK.SpriteSheet other) => id == other.id;
    public bool32 Matches(SpriteSheet* other) => other != null ? id == other->id : id == 0xFFFF;
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
    public ushort id;

    public void Init() => id = 0xFFFF;

    public void Load(string path, Scopes scope) => id = RSDKTable.LoadSpriteAnimation(path, (byte)scope);
    public void Create(string filename, uint frameCount, uint listCount, Scopes scope) => id = RSDKTable.CreateSpriteAnimation(filename, frameCount, listCount, (byte)scope);

    public void Edit(ushort listID, string name, int frameOffset, ushort frameCount, short speed, byte loopIndex, byte rotationStyle) => RSDKTable.EditSpriteAnimation(id, listID, name, frameOffset, frameCount, speed, loopIndex, rotationStyle);

    public ushort FindAnimation(string name) => RSDKTable.FindSpriteAnimation(id, name);

    public SpriteFrame* GetFrame(ushort animID, int frameID) => RSDKTable.GetFrame(id, animID, frameID);

    public bool32 Loaded() => id != 0xFFFF;

    public bool32 Matches(SpriteAnimation other) => id == other.id;
    public bool32 Matches(SpriteAnimation* other) => other != null ? id == other->id : id == 0xFFFF;
}