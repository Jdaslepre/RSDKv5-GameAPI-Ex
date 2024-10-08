namespace RSDK.API;

public static class RichPresence
{
	public static void Set(int32 id, RSDK.String *text) => APITable.SetRichPresence(id, text);
}