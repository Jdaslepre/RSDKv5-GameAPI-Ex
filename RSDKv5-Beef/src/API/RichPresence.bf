namespace RSDK.API;

public static class RichPresence
{
    public static void Set(int32 id, String* text) => APITable.SetRichPresence(id, text);
}