using System;

namespace RSDK
{
    // TODO: Entity Events
    public class EntityGenerator : Compiler.Generator
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

            outText.AppendF(
$"""
				using RSDK;

				namespace GameLogic;
				
				[RegisterClass, System.CRepr]
				public struct {name} : GameObject.Entity
				{{
				    // -----------------
				    // Enums / Constants
				    // -----------------

				    // ----------------
				    // Static Variables
				    // ----------------

				    [System.AlwaysInclude(AssumeInstantiated = true)]
				    public struct Static : GameObject.Static
				    {{

				    }}

				    // ----------------
				    // Entity Variables
				    // ----------------

				    // -------------
				    // Entity Events
				    // -------------
				}}
				"""                );
        }
    }

    // TODO: Actual modded stuff
    public class ModEntityGenerator : Compiler.Generator
    {
        public override System.String Name => "RSDK Modded Object Class";

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
        }
    }

} // Namespace RSDK