namespace RSDK;

public struct String
{
    public this() { }
    public this(char8* str) => Init(str);
    public this(ref Self other)
    {
        System.String str = scope .();
        other.ToString(str);
        Init(str);
    }
    public this(Self* other)
    {
        System.String str = scope .();
        other.ToString(str);
        Init(str);
    }

    public void operator +=(ref Self other) mut => Append(&other);

    public static Self operator +(ref Self lhs, ref Self rhs) => lhs += rhs;
    public static Self operator -(Self lhs, Self rhs) => lhs - rhs;
    public static Self operator -(Self value) => value;
    public static implicit operator uint16*(ref Self str) => str.chars;

    public void Init(char8* str, uint32 length = 0) mut => RSDKTable.InitString(&this, str, length);
    public void Set(char8* str) mut => RSDKTable.SetString(&this, str);

    // public void Prepend(Self* str) mut => this = str + this;
    // public void Prepend(char8* str) mut => this = String(str) + this;

    public void Append(Self* str) mut => RSDKTable.AppendString(&this, str);
    public void Append(char8* str) mut => RSDKTable.AppendText(&this, str);

    public static void Copy(Self* dst, Self* src) => RSDKTable.CopyString(dst, src);
    public static void Copy(Self* dst, char8* src) => RSDKTable.SetString(dst, src);
    public static bool32 Compare(Self* strA, Self* strB, bool32 exactMatch) => RSDKTable.CompareStrings(strA, strB, exactMatch);

    public void CStr(char8* buffer) mut => RSDKTable.GetCString(buffer, &this);
    public char8* ToString() mut
    {
        char8[0x400] buffer = .();
        CStr(&buffer);
        return &buffer;
    }

    public bool32 Initialized() => chars != null;
    public bool32 Empty()       => length == 0;

    public void SetSpriteString(SpriteAnimation aniFrames, uint16 listID) mut => RSDKTable.SetSpriteString(aniFrames.id, listID, &this);

    public int32 GetWidth(SpriteAnimation aniFrames, uint16 listID, int32 spacing) mut
    {
        return RSDKTable.GetStringWidth(aniFrames.id, listID, &this, 0, length, spacing);
    }
    public int32 GetWidth(SpriteAnimation aniFrames, uint16 listID, int32 start, int32 length, int32 spacing) mut
    {
        return RSDKTable.GetStringWidth(aniFrames.id, listID, &this, start, length, spacing);
    }

    public void LoadStrings(char8* filepath) mut => RSDKTable.LoadStringList(&this, filepath, 16);
    public bool32 Split(Self* list, int32 startID, int32 count) mut => RSDKTable.SplitStringList(list, &this, startID, count);

    public uint16* chars = null;
    public uint16 length = 0;
    public uint16 size   = 0;
}