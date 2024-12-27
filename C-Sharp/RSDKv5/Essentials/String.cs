namespace RSDK;

public unsafe struct String
{
    public String() { }
    public String(string str) => Init(str);
    public String(String other) => Init(other.ToString());
    public String(String* other) => Init(other->ToString());

    public String(ref String other)
    {
        chars = other.chars;
        length = other.length;
        size = other.size;
    }

    public void Init(string str, uint length = 0) => RSDKTable.InitString(ref this, str, length);
    public void Set(string str) => RSDKTable.SetString(ref this, str);

    // public void Prepend(String* str) { *this = str + *this; }
    // public void Prepend(string str) { this = new String(str) + this; }

    public void Append(ref String str) => RSDKTable.AppendString(ref this, ref str);
    public void Append(string str) => RSDKTable.AppendText(ref this, str);

    public static void Copy(String* dst, String* src) => RSDKTable.CopyString(dst, src);
    public static void Copy(ref String dst, string src) => RSDKTable.SetString(ref dst, src);
    public static bool32 Compare(String strA, String strB, bool32 exactMatch) { return RSDKTable.CompareStrings(ref strA, ref strB, exactMatch); }

    public void CStr(char* buffer) => RSDKTable.GetCString(buffer, ref this);

    public bool32 Initialized() => chars != null;
    public bool32 Empty() => length == 0;

    public void SetSpriteString(SpriteAnimation spriteAni, ushort listID) => RSDKTable.SetSpriteString(spriteAni.id, listID, ref this);
    public int GetWidth(SpriteAnimation spriteAni, ushort listID, int spacing)
    {
        return RSDKTable.GetStringWidth(spriteAni.id, listID, ref this, 0, length, spacing);
    }
    public int GetWidth(SpriteAnimation spriteAni, ushort listID, int start, int length, int spacing)
    {
        return RSDKTable.GetStringWidth(spriteAni.id, listID, ref this, start, length, spacing);
    }

    public void LoadStrings(string filepath) => RSDKTable.LoadStringList(ref this, filepath, 0x10);
    public bool32 Split(ref String list, int startID, int count) => RSDKTable.SplitStringList(ref list, ref this, startID, count);

    public ushort* chars = null;
    public ushort length = 0;
    public ushort size = 0;
}