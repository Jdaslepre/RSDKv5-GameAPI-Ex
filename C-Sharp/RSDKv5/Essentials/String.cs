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


    // TODO: Compare operator


    public void Init(string str, uint length = 0)
    {
        fixed (String* s = &this) RSDKTable->InitString(s, str, length);
    }

    public void Set(string str)
    {
        fixed (String* s = &this) RSDKTable->SetString(s, str);
    }

    public static void Copy(String* dst, String* src) => RSDKTable->CopyString(dst, src);
    public static void Copy(String* dst, string src) => RSDKTable->SetString(dst, src);
    public static bool32 Compare(String* strA, String* strB, uint exactMatch) { return RSDKTable->CompareStrings(strA, strB, exactMatch); }

    public void CStr(char* buffer) { fixed (String* s = &this) RSDKTable->GetCString(buffer, s); }

    public bool32 Initialized() { return chars != null; }
    public bool32 Empty() { return length == 0; }

    public void SetSpriteString(SpriteAnimation spriteAni, ushort listID) { fixed (String* s = &this) RSDKTable->SetSpriteString(spriteAni.id, listID, s); }
    public int GetWidth(SpriteAnimation spriteAni, ushort listID, int spacing)
    {
        fixed (String* s = &this) return RSDKTable->GetStringWidth(spriteAni.id, listID, s, 0, length, spacing);
    }
    public int GetWidth(SpriteAnimation spriteAni, ushort listID, int start, int length, int spacing)
    {
        fixed (String* s = &this) return RSDKTable->GetStringWidth(spriteAni.id, listID, s, start, length, spacing);
    }

    public void LoadStrings(string filepath)
    {
        fixed (String* s = &this) RSDKTable->LoadStringList(s, filepath, 16);
    }
    public bool32 Split(String* list, int startID, int count) { fixed (String* s = &this) return RSDKTable->SplitStringList(list, s, startID, count); }

    public ushort* chars = null;
    public ushort length = 0;
    public ushort size = 0;
}