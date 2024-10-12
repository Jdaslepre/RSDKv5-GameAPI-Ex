#if RETRO_USE_MOD_LOADER
namespace RSDK.ForEach;

public unsafe class List
{
    public static List<String> GetIDs()
    {
        List<String> list = new();

        String str = new();
        while (modTable->ForeachModID(&str) != false)
        {
            String copy = new();
            copy.chars = str.chars;
            copy.length = str.length;
            copy.size = str.size;

            list.Add(copy);
        }

        return list;
    }
}

#if RETRO_MOD_LOADER_VER_2
public unsafe class Settings
{
    public List<String> Get(string id)
    {
        List<String> list = new();

        String str = new();
        while (modTable->ForeachSetting(id, &str) != false)
        {
            String copy = new();
            copy.chars = str.chars;
            copy.length = str.length;
            copy.size = str.size;

            list.Add(copy);
        }

        return list;
    }

    public List<String> GetCategories(string id)
    {
        List<String> list = new();

        String str = new();
        while (modTable->ForeachSettingCategory(id, &str) != false)
        {
            String copy = new();
            copy.chars = str.chars;
            copy.length = str.length;
            copy.size = str.size;

            list.Add(copy);
        }

        return list;
    }
}
#endif

public unsafe class Config
{
    public List<String> Get()
    {
        List<String> list = new();

        String str = new();
        while (modTable->ForeachConfig(&str) != false)
        {
            String copy = new();
            copy.chars = str.chars;
            copy.length = str.length;
            copy.size = str.size;

            list.Add(copy);
        }

        return list;
    }

    public List<String> GetCategories()
    {
        List<String> list = new();

        String str = new();
        while (modTable->ForeachConfigCategory(&str) != false)
        {
            String copy = new();
            copy.chars = str.chars;
            copy.length = str.length;
            copy.size = str.size;

            list.Add(copy);
        }

        return list;
    }
}
#endif