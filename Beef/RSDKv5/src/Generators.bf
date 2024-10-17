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

				    public new void Update()
				    {{
				    }}

				    public new void LateUpdate()
				    {{
				    }}

				    public new static void StaticUpdate()
				    {{
				    }}

				    public new void Draw()
				    {{
				    }}

				    public new void Create(void* data)
				    {{
				    }}

				    public new static void StageLoad()
				    {{
				    }}

				#if GAME_INCLUDE_EDITOR
				    public new static void EditorLoad()
				    {{
				    }}

				    public new void EditorDraw()
				    {{
				    }}
				#endif

				    public new static void Serialize()
				    {{
				    }}

				#if RETRO_REV0U
				    public new static void StaticLoad(void* data)
				    {{
				        System.Internal.MemSet(data, 0, sizeof(Static));
				        var sVars = (Static*)data;
				    }}
				#endif
				}}
				"""
            );
    }
}

// ---------------------------------------------------------
// Generates a new RSDK Object, without predefined functions
// ---------------------------------------------------------

public class EntityGenerator2 : System.Compiler.Generator
{
    public override System.String Name => "RSDK Object Class (Clean)";

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
                    // ----------------
                    // Static Variables
                    // ----------------

                    public struct Static : GameObject.Static
                    {{
                    }}
                }}
                """
            );
    }
}

// ------------------------------
// Generates a modded RSDK Object
// ------------------------------

public class EntityGenerator3 : System.Compiler.Generator
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

        	    public new void Update()
        	    {{
        	    }}

        	    public new void LateUpdate()
        	    {{
        	    }}

        	    public new static void StaticUpdate()
        	    {{
        	    }}

        	    public new void Draw()
        	    {{
        	    }}

        	    public new void Create(void* data)
        	    {{
        	    }}

        	    public new static void StageLoad()
        	    {{
        	    }}

        	#if GAME_INCLUDE_EDITOR
        	    public new static void EditorLoad()
        	    {{
        	    }}

        	    public new void EditorDraw()
        	    {{
        	    }}
        	#endif

        	    public new static void Serialize()
        	    {{
        	    }}

        	#if RETRO_REV0U
        	    public new static void StaticLoad(void* data)
        	    {{
        	        System.Internal.MemSet(data, 0, sizeof(Static));
        	        var sVars = (Static*)data;
        	    }}
        	#endif
        	}}
        	"""
            );
    }
}

// ------------------------------------------------------------
// Generates a modded RSDK Object, without predefined functions
// ------------------------------------------------------------

public class EntityGenerator4 : System.Compiler.Generator
{
    public override System.String Name => "RSDK Object Class (Modded, Clean)";

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
                    // ----------------
                    // Static Variables
                    // ----------------

                    public struct Static : GameObject.Static
                    {{
                    }}
                }}
                """
            );
    }
}