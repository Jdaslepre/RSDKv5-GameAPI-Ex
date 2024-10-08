using System.Reflection;
using System.Runtime.InteropServices;

namespace RSDK
{
    public unsafe class Managed
    {
        public static IntPtr GetFieldPtr<T>(Type entityType, string methodName) where T : Delegate
        {
            MethodInfo method = entityType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
            if (method == null) return IntPtr.Zero;

            var @delegate = (T)Delegate.CreateDelegate(typeof(T), method);
            return Marshal.GetFunctionPointerForDelegate<T>(@delegate);
        }
    }
}
