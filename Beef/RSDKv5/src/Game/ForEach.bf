using System;
using System.Collections;

namespace RSDK.Mod.ForEach;

#if RETRO_USE_MOD_LOADER
public class List
{
    public static List<RSDK.String> GetIDs()
    {
        List<RSDK.String> list = null;

        RSDK.String string = .();
        while (modTable.ForeachModID(&string) != 0) {
            RSDK.String copy = .();
            copy.chars  = string.chars;
            copy.length = string.length;
            copy.size   = string.size;

            list.Add(copy);
        }

        return list;
    }
}

#if RETRO_MOD_LOADER_VER_2
public class Settings
{
    public List<RSDK.String> Get(char8 *id)
    {
        List<RSDK.String> list = null;

        RSDK.String string = .();
        while (modTable.ForeachSetting(id, &string) != 0) {
            RSDK.String copy = .();
            copy.chars  = string.chars;
            copy.length = string.length;
            copy.size   = string.size;

            list.Add(copy);
        }

        return list;
    }

    public List<RSDK.String> GetCategories(char8 *id)
    {
        List<RSDK.String> list = null;

        RSDK.String string = .();
        while (modTable.ForeachSettingCategory(id, &string) != 0) {
            RSDK.String copy = .();
            copy.chars  = string.chars;
            copy.length = string.length;
            copy.size   = string.size;

            list.Add(copy);
        }

        return list;
    }
}
#endif

public class Config
{
    public List<RSDK.String> Get()
    {
        List<RSDK.String> list = null;

        RSDK.String string = .();
        while (modTable.ForeachConfig(&string) != 0) {
            RSDK.String copy = .();
            copy.chars  = string.chars;
            copy.length = string.length;
            copy.size   = string.size;

            list.Add(copy);
        }

        return list;
    }

    public List<RSDK.String> GetCategories()
    {
        List<RSDK.String> list = null;

        RSDK.String string = .();
        while (modTable.ForeachConfigCategory(&string) != 0) {
            RSDK.String copy = .();
            copy.chars  = string.chars;
            copy.length = string.length;
            copy.size   = string.size;

            list.Add(copy);
        }

        return list;
    }
}
#endif