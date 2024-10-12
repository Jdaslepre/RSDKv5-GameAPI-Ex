using System;

namespace GameLogic;

[AlwaysInclude]
public class Program
{
    public static this()
    {
    }

    public static ~this()
    {
    }

    public static int Main(System.String[] args)
    {
        return 0;
    }

    enum DLL_REASON
    {
        DLL_PROCESS_DETACH,
        DLL_PROCESS_ATTACH,
        DLL_THREAD_ATTACH,
        DLL_THREAD_DETACH
    }
}
