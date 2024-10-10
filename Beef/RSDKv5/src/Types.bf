namespace RSDK;

[System.CRepr] public struct bool32 : System.IEquatable<Self>
{
    public uint32 val;

    public this(int32 value) => val = (.)value;
    public this(uint32 value) => val = value;
    public this(bool value) => val = value ? 1 : 0;

    public bool Equals(Self other) => val == other.val;
    public bool Equals(System.Object obj) => obj is Self && Equals((Self)obj);

    public static implicit operator bool(Self src) => src.val != 0;

    public static implicit operator int(Self src) => src.val;
    public static implicit operator int32(Self src) => (.)src.val;

    public static implicit operator uint(Self src) => src.val;
    public static implicit operator uint32(Self src) => src.val;

    public static implicit operator Self(bool value) => value ? 1 : 0;
    public static implicit operator Self(uint32 value) => Self(value);

    public static bool operator ==(Self lhs, Self rhs) => lhs.val == rhs.val;
    public static bool operator !=(Self lhs, Self rhs) => lhs.val != rhs.val;

    public void operator ^=(uint32 v) mut => val ^= v;
}

[System.CRepr] public struct Boolean<T> where T : var, System.IInteger, System.IMinMaxValue<T>
{
    public T val;

    public this(T value) => val = value;
    public this(bool value) => val = value ? 1 : 0;

    public bool Equals(Self other) => val == other.val;
    public bool Equals(System.Object obj) => obj is Self && Equals((Self)obj);

    public static implicit operator bool(Self src) => src.val != 0;

    public static implicit operator int(Self src) => src.val;
    public static implicit operator int32(Self src) => src.val;

    public static implicit operator uint(Self src) => src.val;
    public static implicit operator uint32(Self src) => src.val;

    public static implicit operator Self(bool value) => value ? 1 : 0;
    public static implicit operator Self(T value) => Self(value);

    public static bool operator ==(Self lhs, Self rhs) => lhs.val == rhs.val;
    public static bool operator !=(Self lhs, Self rhs) => lhs.val != rhs.val;

    public void operator ^=(uint32 v) mut => val ^= v;
}

typealias color = uint32;
typealias size_t = uint;
typealias long = int64;