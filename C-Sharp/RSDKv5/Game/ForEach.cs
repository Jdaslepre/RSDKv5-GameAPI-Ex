using static RSDK.EngineAPI;

namespace RSDK.ForEach
{
#if RETRO_USE_MOD_LOADER
    public unsafe class List
    {
        public static List<RSDK.String> GetIDs()
        {
            List<RSDK.String> list = new();
            
            RSDK.String str = new();
            while (modTable.ForeachModID(&str) != 0)
            {
                RSDK.String copy = new();
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
        public List<RSDK.String> Get(string id)
        {
            List<RSDK.String> list = new();

            RSDK.String str = new();
            while (modTable.ForeachSetting(id, &str) != 0)
            {
                RSDK.String copy = new();
                copy.chars = str.chars;
                copy.length = str.length;
                copy.size = str.size;

                list.Add(copy);
            }

            return list;
        }

        public List<RSDK.String> GetCategories(string id)
        {
            List<RSDK.String> list = new();

            RSDK.String str = new();
            while (modTable.ForeachSettingCategory(id, &str) != 0)
            {
                RSDK.String copy = new();
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
        public List<RSDK.String> Get()
        {
            List<RSDK.String> list = new();

            RSDK.String str = new();
            while (modTable.ForeachConfig(&str) != 0)
            {
                RSDK.String copy = new();
                copy.chars = str.chars;
                copy.length = str.length;
                copy.size = str.size;

                list.Add(copy);
            }

            return list;
        }

        public List<RSDK.String> GetCategories()
        {
            List<RSDK.String> list = new();

            RSDK.String str = new();
            while (modTable.ForeachConfigCategory(&str) != 0)
            {
                RSDK.String copy = new();
                copy.chars = str.chars;
                copy.length = str.length;
                copy.size = str.size;

                list.Add(copy);
            }

            return list;
        }
    }
#endif
}
