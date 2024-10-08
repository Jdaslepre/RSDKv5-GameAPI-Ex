using System.Text;
using static RSDK.EngineAPI;

namespace RSDK
{
    public unsafe struct String
    {
        public String() { }

        public String(string str) => Init(str);
        public String(RSDK.String other) => Init(other.ToString());
        public String(RSDK.String* other) => Init(other->ToString());

        public String(ref RSDK.String other)
        {
            chars = other.chars;
            length = other.length;
            size = other.size;
        }


        // TODO: Compare operator


        public void Init(string str, uint length = 0)
        {
            fixed (RSDK.String* s = &this) RSDKTable.InitString(s, str, length);
        }

        public void Set(string str)
        {
            fixed (RSDK.String* s = &this) RSDKTable.SetString(s, str);
        }

        public static void Copy(RSDK.String* dst, RSDK.String* src) => RSDKTable.CopyString(dst, src);
        public static void Copy(RSDK.String* dst, string src) => RSDKTable.SetString(dst, src);
        public static uint Compare(RSDK.String* strA, RSDK.String* strB, uint exactMatch) { return RSDKTable.CompareStrings(strA, strB, exactMatch); }

        public void CStr(StringBuilder buffer) { fixed (RSDK.String* s = &this) RSDKTable.GetCString(buffer, s); }

        public bool32 Initialized() { return chars != null; }
        public bool32 Empty() { return length == 0; }

        public void SetSpriteString(SpriteAnimation aniFrames, ushort listID) { fixed (RSDK.String* s = &this) RSDKTable.SetSpriteString(aniFrames.aniFrames, listID, s); }
        public int GetWidth(SpriteAnimation aniFrames, ushort listID, int spacing)
        {
            fixed (RSDK.String* s = &this) return RSDKTable.GetStringWidth(aniFrames.aniFrames, listID, s, 0, length, spacing);
        }
        public int GetWidth(SpriteAnimation aniFrames, ushort listID, int start, int length, int spacing)
        {
            fixed (RSDK.String* s = &this) return RSDKTable.GetStringWidth(aniFrames.aniFrames, listID, s, start, length, spacing);
        }

        public void LoadStrings(string filepath)
        {
            fixed (RSDK.String* s = &this) RSDKTable.LoadStringList(s, filepath, 16);
        }
        public bool Split(RSDK.String* list, int startID, int count) { fixed (RSDK.String* s = &this) return RSDKTable.SplitStringList(list, s, startID, count); }

        public ushort* chars = null;
        public ushort length = 0;
        public ushort size = 0;
    }
}
