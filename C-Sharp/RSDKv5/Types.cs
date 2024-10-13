global using size_t = uint;

namespace RSDK;

public struct bool32
{
    public uint val;

    public bool32(uint value) => val = value;
    public bool32(bool value) => val = value ? 1u : 0u;
    
    public bool Equals(bool32 other) => val.Equals(other.val);

    public override bool Equals(object obj)
    {
        if (obj is bool32 other)
            return Equals(other);
        return false;
    }

    public static implicit operator bool(bool32 src) => !src.val.Equals(default(bool32));
    public static implicit operator int(bool32 src) => Convert.ToInt32(src.val);
    public static implicit operator bool32(bool value) => new(value);
    public static implicit operator bool32(uint value) => new(value);

    public static bool operator ==(bool32 lhs, bool32 rhs) => lhs.Equals(rhs);
    public static bool operator !=(bool32 lhs, bool32 rhs) => !lhs.Equals(rhs);

    public override int GetHashCode() => val.GetHashCode();
}

public struct Boolean<T> where T : struct, IConvertible, IComparable<T>, IEquatable<T>
{
    public T val;

    public Boolean(T value) => val = value;
    public Boolean(bool value)
    {
        val = (T)(object)(value ? 1 : 0);
    }

    public bool Equals(Boolean<T> other) => val.Equals(other.val);

    public override bool Equals(object obj)
    {
        if (obj is Boolean<T> other)
            return Equals(other);
        return false;
    }

    public static implicit operator bool(Boolean<T> src) => !src.val.Equals(default(T));
    public static implicit operator int(Boolean<T> src) => Convert.ToInt32(src.val);
    public static implicit operator Boolean<T>(bool value) => new Boolean<T>(value);
    public static implicit operator Boolean<T>(T value) => new Boolean<T>(value);

    public static bool operator ==(Boolean<T> lhs, Boolean<T> rhs) => lhs.Equals(rhs);
    public static bool operator !=(Boolean<T> lhs, Boolean<T> rhs) => !lhs.Equals(rhs);

    public override int GetHashCode() => val.GetHashCode();
}
