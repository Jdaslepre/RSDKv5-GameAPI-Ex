using static RSDK.EngineAPI;

#if RETRO_REV02
namespace RSDK.API
{
    public unsafe class RichPresence
    {
        public static void Set(int id, RSDK.String* text) => APITable.SetRichPresence(id, text);
    }
}
#endif