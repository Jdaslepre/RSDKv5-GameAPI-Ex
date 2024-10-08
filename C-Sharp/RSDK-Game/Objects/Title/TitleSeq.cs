using RSDK;
using static RSDK.EngineAPI;
using System.Runtime.InteropServices;
using Windows.UI.Input.Inking.Analysis;
using System.Runtime.CompilerServices;

namespace CS.GameLogic
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class TitleSeq : GameObject.Entity
    {
        // ----------------
        // Static Variables
        // ----------------

        public struct Static
        {
            public GameObject.Static vars;
        }

        // -------------
        // Entity Events
        // -------------

        public new void Update() => Dev.Print(Dev.PrintModes.PRINT_NORMAL, "TitleSeq Class -> Update");
        public new void LateUpdate() => Dev.Print(Dev.PrintModes.PRINT_NORMAL, "TitleSeq Class -> LateUpdate");
        public new static void StaticUpdate() => Dev.Print(Dev.PrintModes.PRINT_NORMAL, "TitleSeq Class -> StaticUpdate");

        public new void Create(void* data)
        {
            visible = 1;
            active = ActiveFlags.ACTIVE_ALWAYS;
            Dev.Print(Dev.PrintModes.PRINT_NORMAL, "TitleSeq Class -> Create :D");
        }

        public new static void StageLoad()
        {
            Dev.Print(Dev.PrintModes.PRINT_NORMAL, "TitleSeq Class -> StageLoad");
            RSDKTable.CreateEntity(sVars->vars.classID, null, 12, 12);
        }

        //
        // Declare Attribute
        //

        public static Static* sVars = null;
        public static void _Create(void* data) => ((TitleSeq*)&sceneInfo->entity)->Create(data);
        public static void _Draw() => ((TitleSeq*)&sceneInfo->entity)->Draw();
        public static void _Update() => ((TitleSeq*)&sceneInfo->entity)->Update();
        public static void _LateUpdate() => ((TitleSeq*)&sceneInfo->entity)->LateUpdate();
    }
}
