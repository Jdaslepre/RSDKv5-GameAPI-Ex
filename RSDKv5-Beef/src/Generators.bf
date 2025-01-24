namespace RSDK;

// ---------------------------
// Generates a new RSDK Object
// ---------------------------

public class EntityGenerator1 : System.Compiler.Generator
{
    public override System.String Name => "RSDK Object Class";

    public override void InitUI()
    {
        AddEdit("name", "Class Name", "");
    }

    public override void Generate(System.String outFileName, System.String outText, ref Flags generateFlags)
    {
        var name = mParams["name"];
        if (name.EndsWith(".bf", .OrdinalIgnoreCase))
            name.RemoveFromEnd(3);
        outFileName.Append(name);

        outText.AppendF($"""
				using RSDK;

				namespace GameLogic;
				
				[RegisterObject]
				public struct {name} : GameObject.Entity
				{{
				    // -----------------
				    // Enums / Constants
				    // -----------------

				    // ----------------
				    // Static Variables
				    // ----------------

				    public struct Static : GameObject.Static
				    {{
				    }}

				    // ----------------
				    // Entity Variables
				    // ----------------

				    // -------------
				    // Entity Events
				    // -------------

				    public void Update()
				    {{
				    }}

				    public void LateUpdate()
				    {{
				    }}

				    public static void StaticUpdate()
				    {{
				    }}

				    public void Draw()
				    {{
				    }}

				    public void Create(void* data)
				    {{
				    }}

				    public static void StageLoad()
				    {{
				    }}

				#if GAME_INCLUDE_EDITOR
				    public static void EditorLoad()
				    {{
				    }}

				    public void EditorDraw()
				    {{
				    }}
				#endif

				    public static void Serialize()
				    {{
				    }}

				#if RETRO_REV0U
				    public static void StaticLoad(Static* sVars)
				    {{
				        GameObject.InitStatic(sVars);
				    }}
				#endif
				}}
				"""
            );
    }
}

// ------------------------------
// Generates a modded RSDK Object
// ------------------------------

public class EntityGenerator2 : System.Compiler.Generator
{
    public override System.String Name => "RSDK Object Class (Modded)";

    public override void InitUI()
    {
        AddEdit("name", "Class Name", "");
    }

    public override void Generate(System.String outFileName, System.String outText, ref Flags generateFlags)
    {
        var name = mParams["name"];
        if (name.EndsWith(".bf", .OrdinalIgnoreCase))
            name.RemoveFromEnd(3);
        outFileName.Append(name);

        outText.AppendF($"""
        	using RSDK;

        	namespace GameLogic;
        	
        	[ModRegisterObject]
        	public struct {name} : GameObject.Entity
        	{{
        	    // -----------------
        	    // Enums / Constants
        	    // -----------------

        	    // ----------------
        	    // Static Variables
        	    // ----------------

        	    public struct Static : GameObject.Static
        	    {{
        	    }}

        	    public struct ModStatic : GameObject.Static
        	    {{
        	    }}

        	    // ----------------
        	    // Entity Variables
        	    // ----------------

        	    // -------------
        	    // Entity Events
        	    // -------------

        	    public void Update()
        	    {{
        	    }}

        	    public void LateUpdate()
        	    {{
        	    }}

        	    public static void StaticUpdate()
        	    {{
        	    }}

        	    public void Draw()
        	    {{
        	    }}

        	    public void Create(void* data)
        	    {{
        	    }}

        	    public static void StageLoad()
        	    {{
        	    }}

        	#if GAME_INCLUDE_EDITOR
        	    public static void EditorLoad()
        	    {{
        	    }}

        	    public void EditorDraw()
        	    {{
        	    }}
        	#endif

        	    public static void Serialize()
        	    {{
        	    }}

        	#if RETRO_REV0U
        	    public static void StaticLoad(Static* sVars)
        	    {{
        	        GameObject.InitStatic(sVars);
        	    }}
        	#endif
        	}}
        	"""
            );
    }
}