using System.Collections;

namespace RSDK.Mod.ForEach;

#if RETRO_USE_MOD_LOADER
public class List
{
    public static LinkedList<String> GetIDs(LinkedList<String> list)
    {
        String string = .();
        while (modTable.ForeachModID(&string) != 0) {
            String copy = .(ref string);
            list.AddLast(copy);
        }

        return list;
    }
}

#if RETRO_MOD_LOADER_VER_2
public class Settings
{
    public LinkedList<String> Get(char8 *id, LinkedList<String> list)
    {
        String string = .();
        while (modTable.ForeachSetting(id, &string) != 0) {
            String copy = .(ref string);
            list.AddLast(copy);
        }

        return list;
    }

    public LinkedList<String> GetCategories(char8 *id, LinkedList<String> list)
    {
        String string = .();
        while (modTable.ForeachSettingCategory(id, &string) != 0) {
            String copy = .(ref string);
            list.AddLast(copy);
        }

        return list;
    }
}
#endif

public class Config
{
    public LinkedList<String> Get(LinkedList<String> list)
    {
        String string = .();
        while (modTable.ForeachConfig(&string) != 0) {
            String copy = .(ref string);
            list.AddLast(copy);
        }

        return list;
    }

    public LinkedList<String> GetCategories(LinkedList<String> list)
    {
        String string = .();
        while (modTable.ForeachConfigCategory(&string) != 0) {
            String copy = .(ref string);
            list.AddLast(copy);
        }

        return list;
    }
}
#endif