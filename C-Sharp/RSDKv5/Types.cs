global using size_t = uint;

namespace RSDK
{
    public struct bool32
    {
        private uint _value;

        public bool32(bool value) => _value = (uint)(value ? 1 : 0);
        public bool32(uint value) => _value = value;
        public bool32(int value) => _value = (uint)value;

        public static implicit operator bool32(bool value)
        {
            return new bool32(value);
        }
        public static implicit operator bool(bool32 value)
        {
            return value._value != 0;
        }
        public static implicit operator bool32(uint value)
        {
            return new bool32(value);
        }
        public static implicit operator uint(bool32 value)
        {
            return value._value;
        }
        public static implicit operator bool32(int value)
        {
            return new bool32(value);
        }
        public static implicit operator int(bool32 value)
        {
            return (int)value._value;
        }

        public override string ToString()
        {
            return (_value != 0).ToString();
        }
    }
}
