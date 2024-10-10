namespace RSDK;

public struct SpriteSheet
{
    public uint16 id;

    public void Init() mut => id = (.)(-1);

    public void Load(char8* path, Scopes scopeType) mut { id = RSDKTable.LoadSpriteSheet(path, (.)scopeType); }

    public bool32 Loaded() => id != (.)(-1);

    public bool32 Matches(Self other) => id == other.id;
    public bool32 Matches(Self* other) => other != null ? id == other.id : id == (.)(-1);
}

[System.CRepr] public struct SpriteFrame
{
    public int16 sprX;
    public int16 sprY;
    public int16 width;
    public int16 height;
    public int16 pivotX;
    public int16 pivotY;
    public uint16 duration;
    public uint16 unicodeChar;
    public uint8 sheetID;
}

public struct SpriteAnimation
{
    public uint16 id;

    public void Init() mut => id = (.)(-1);

    public void Load(char8* path, Scopes scopeType) mut => id = RSDKTable.LoadSpriteAnimation(path, (.)scopeType);
    public void Create(char8* filename, uint32 frameCount, uint32 listCount, Scopes scopeType) mut => id = RSDKTable.CreateSpriteAnimation(filename, frameCount, listCount, (.)scopeType);

    public void Edit(uint16 listID, char8* name, int32 frameOffset, uint16 frameCount, int16 speed, uint8 loopIndex, uint8 rotationStyle) => RSDKTable.EditSpriteAnimation(id, listID, name, frameOffset, frameCount, speed, loopIndex, rotationStyle);

    public uint16 FindAnimation(char8* name) => RSDKTable.FindSpriteAnimation(id, name);

    public SpriteFrame* GetFrame(int32 animID, int32 frameID) => RSDKTable.GetFrame(id, (.)animID, frameID);

    public bool32 Loaded() => id != (.)(-1);

    public bool32 Matches(Self other) => id == other.id;
    public bool32 Matches(Self* other) => other != null ? id == other.id : id == (.)(-1);
}