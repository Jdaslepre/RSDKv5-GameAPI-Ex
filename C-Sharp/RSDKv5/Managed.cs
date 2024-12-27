using System.Reflection;

namespace RSDK;

public unsafe class Managed
{
    public static IntPtr GetFunctionPtr<T>(Type entityType, string methodName) where T : Delegate
    {
        MethodInfo method = entityType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
        if (method == null) return IntPtr.Zero;

        var @delegate = (T)Delegate.CreateDelegate(typeof(T), method);
        return Marshal.GetFunctionPointerForDelegate<T>(@delegate);
    }

    public static void* GetFieldPtr<T>(Type type, string fieldName)
    {
        FieldInfo field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.Public);
        if (field == null) return null;

        IntPtr val = (IntPtr)field.GetValue(null);
        return val.ToPointer();
    }
}