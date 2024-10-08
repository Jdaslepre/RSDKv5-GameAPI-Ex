using System;

namespace RSDK;

public struct String
{
    public this() { }

    public this(ref RSDK.String other) => Internal.MemCpy(&this, &other, sizeof(RSDK.String));
    public this(RSDK.String* other) => Internal.MemCpy(&this, other, sizeof(RSDK.String));

    public void Init(char8* str, uint32 length = 0) mut => RSDKTable.InitString(&this, str, length);
    public void Set(char8* str) mut => RSDKTable.SetString(&this, str);

    public static void Copy(RSDK.String* dst, RSDK.String* src) => RSDKTable.CopyString(dst, src);
    public static void Copy(RSDK.String* dst, char8* src) => RSDKTable.SetString(dst, src);
    public static bool32 Compare(RSDK.String* strA, RSDK.String* strB, bool32 exactMatch) { return RSDKTable.CompareStrings(strA, strB, exactMatch); }

    public void CStr(char8* buffer) mut => RSDKTable.GetCString(buffer, &this);

    public static implicit operator uint16*(ref Self str)
    {
        return str.chars;
    }

    public bool32 Initialized() { return chars != null; }
    public bool32 Empty() { return length == 0; }

    public void SetSpriteString(RSDK.SpriteAnimation aniFrames, uint16 listID) mut => RSDKTable.SetSpriteString(aniFrames.aniFrames, listID, &this);
    public int32 GetWidth(RSDK.SpriteAnimation aniFrames, uint16 listID, int32 spacing) mut
    {
        return RSDKTable.GetStringWidth(aniFrames.aniFrames, listID, &this, 0, length, spacing);
    }
    public int32 GetWidth(RSDK.SpriteAnimation aniFrames, uint16 listID, int32 start, int32 length, int32 spacing) mut
    {
        return RSDKTable.GetStringWidth(aniFrames.aniFrames, listID, &this, start, length, spacing);
    }

    public void LoadStrings(char8* filepath) mut => RSDKTable.LoadStringList(&this, filepath, 16);
    public bool32 Split(RSDK.String* list, int32 startID, int32 count) mut { return RSDKTable.SplitStringList(list, &this, startID, count); }

    public uint16* chars = null;
    public uint16 length = 0;
    public uint16 size   = 0;
}