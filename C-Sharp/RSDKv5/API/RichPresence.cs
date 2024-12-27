#if RETRO_REV02
namespace RSDK;

public unsafe class RichPresence
{
    public static void Set(int id, String text) => APITable.SetRichPresence(id, ref text);
}
#endif